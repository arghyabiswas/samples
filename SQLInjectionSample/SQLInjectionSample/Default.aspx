<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SQLInjectionSample.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%if (User.Identity.IsAuthenticated)
                { %>
            Welcome : <%:User.Identity.Name %>
            <br />
            <a href="Login.aspx" >Logout</a>
            <%} %>
        </div>
    </form>
</body>
</html>
