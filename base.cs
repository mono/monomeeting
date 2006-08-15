using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace Meeting {
	public class MyBase : Page {
		
		//
		// Notice that am including this because Gonzalo forced me to!
		//
		
		string create_table = "create table Meeting ( id int not null auto_increment primary key, name varchar(100) unique not null, email varchar(100) not null, country varchar (50) not null, comment blob not null, validated int default 0 );";

		static string constr;
		
		static IDbConnection GetConnection ()
		{
			if (constr == null){
				using (Stream str = File.OpenRead ("/etc/connection-string")){
					Console.WriteLine ("reading");
					constr = new StreamReader (str).ReadLine ();
				}
			}
				
			IDbConnection cnc = new MySqlConnection("Server=localhost; database=meeting;user id=miguel; Password=miguel;");
			cnc.Open ();
			return cnc;
		}
		
		static IDbDataParameter CreateParameter (IDbCommand cmd, string name, string val)
		{
			IDbDataParameter par = cmd.CreateParameter ();
			par.ParameterName = name;
			par.Value = val;
			par.DbType = DbType.String;
			return par;
		}
		
		
		static int InsertNew (string name, string email, string country, string comment)
		{
			if (name == null || email == null || country == null || comment == null)
				throw new ArgumentNullException ();
		
			using (IDbConnection cnc = GetConnection ()){
				IDbCommand cmd = cnc.CreateCommand ();
				//cmd.CommandText = "INSERT INTO Meeting set name = @name, email = @email, country = @country, comment = @comment ;";
				cmd.CommandText = "INSERT INTO Meeting (name, email, country, comment) VALUES (?name, ?email, ?country, ?comment)";
				cmd.Parameters.Add (CreateParameter (cmd, "?name", name));
				cmd.Parameters.Add (CreateParameter (cmd, "?email", email));
				cmd.Parameters.Add (CreateParameter (cmd, "?country", country));
				cmd.Parameters.Add (CreateParameter (cmd, "?comment", comment));
				try {
					cmd.ExecuteNonQuery ();
				} catch (MySqlException m){
					// Duplicate entry 
					if (m.Number != 1062)
						throw;
				}
				cmd.Dispose ();
				cmd = cnc.CreateCommand ();
				cmd.CommandText = "SELECT id FROM Meeting WHERE name = ?name";
				cmd.Parameters.Add (CreateParameter (cmd, "?name", name));
				IDataReader reader = cmd.ExecuteReader ();
				if (reader.Read () == false)
					throw new Exception ("Reader.Read is false");
				return (int) reader.GetValue (0);
			}		
		}

		static string MakeHash (int number)
		{
			using (Stream s = File.OpenRead ("/etc/clave")){
				byte [] buffer = new byte [100];
				
				int n = s.Read (buffer, 4, 96);
				buffer [0] = (byte) (number & 0xff);
				buffer [1] = (byte) ((number >> 8) & 0xff);
				buffer [2] = (byte) ((number >> 16) & 0xff);
				buffer [3] = (byte) ((number >> 24) & 0xff);
	
				byte [] hash = SHA1.Create ().ComputeHash (buffer, 0, n+4);
				return Convert.ToBase64String (hash);
			}
		}
			
		static string GenerateToken (int idx)
		{
			return String.Format ("{0}:{1}", idx, HttpUtility.UrlEncode (MakeHash (idx)));
		}
		
		public static void ValidateToken (string token)
		{
			int p = token.IndexOf (':');
			if (p == -1)
				throw new Exception ("Invalid token format");
			int uid = (int) UInt32.Parse (token.Substring (0, p));
			string hash = token.Substring (p+1);

			if (MakeHash (uid) != hash)
				throw new Exception ("Tampered token");

		    	using (IDbConnection cnc = GetConnection ()){
				IDbCommand cmd;
				cmd = cnc.CreateCommand ();
				cmd.CommandText = "UPDATE Meeting SET validated = 1 WHERE id = " + uid.ToString (CultureInfo.InvariantCulture);
				cmd.ExecuteNonQuery ();
			}
		}
		
		public static string InsertData (string name, string email, string country, string comment)
		{
			int idx = InsertNew (name, email, country, comment);
			return GenerateToken (idx);
		}
	}
}