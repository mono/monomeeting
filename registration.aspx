<!-- #include virtual="header.inc" -->
<form runat="server">
<script runat="server">
void Page_Load ()
{
	if (!IsPostBack){
	   return;
	}
	Page.Validate ();

	string token = InsertData (name.Text, email.Text, country.Text, comments.Text);	
	mail (email.Text, token);
	Response.Redirect ("thanks.aspx");
}

static void mail (string recipient, string token)
{
     MailMessage m = new MailMessage ();
     m.From = "monomeeting@gmail.com";
     m.To = recipient;
     m.Subject = "Mono Meeting 2006 Registration Information";
     m.Body = String.Format ("\n\nHello,\n\n" +
"To complete your registration for the Mono Meeting 2006, please go to the\n" +
"following address\n\n\thttp://www.go-mono.com/meeting/confirm.aspx?token={0}\n\n" +
"To find more about the Mono Meeting, visit:\n\n\thttp://www.go-mono.com/meeting\n\n" + 
"The Mono Team (mono@novell.com)", token);

SmtpMail.SmtpServer = "localhost";
SmtpMail.Send (m);
}

</script>

<div id="column-content">

	<h2>Mono Meeting Registration</h2>

	<p>Please enter the following information to register for the
	Mono Meeting, the information obtained here will not be used for marketing purposes.


	<p>Name:<br>
	<asp:TextBox id="name" columns="50" runat="server" maxLength="100" />
	<asp:RequiredFieldValidator runat="server" class="errorst" ControlToValidate="name" ErrorMessage="Please enter your name."/>

	<p>Email:<br>
	<asp:TextBox id="email" columns="50" runat="server" maxLength="100"/>
	<asp:RegularExpressionValidator runat="server" ControlToValidate="email"
	     ValidationExpression="[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]+)*(\.[a-zA-Z]{2,3}){1,2}"
	          ErrorMessage="Please enter your email address."/>

	<p>Country:<br>
	<asp:TextBox id="country" columns="50" runat="server" maxLength="100"/>

	<p>Comments:<br>
	<asp:TextBox id="comments" columns="70" rows="8" TextMode="multiline" runat="server" maxLength="1024"/>

	<p>
	<asp:button type="submit" runat="server" Text="Register" />
</form>
</div>