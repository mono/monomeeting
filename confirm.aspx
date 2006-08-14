<!-- #include virtual="header.inc" -->
<form runat="server">
<script runat="server">
void Page_Load ()
{
	try {
		if (DbValidate (Request.QueryString ["token"])){
		   ok.Visible = true;
		} else 
	   	   error.Visible = true;
        } catch {
	  error.Visible = true;
	}
}

bool DbValidate (string s)
{
	return false;
}
</script>

<div id="column-content">

	<div id="ok" visible="false" runat="server">
	Your registration to the Mono Meeting is complete.

	<p><a href="index.aspx">Go back to the main site</a>
	</div>

	<div id="error" visible="false" runat="server">

	You have entered an invalid registration.  If you have
	problems with your registration contact <a
	href="mailto:monomeeting@gmail.com">monomeeting@gmail.com</a>

	</div>

</form>
</div>
</body>