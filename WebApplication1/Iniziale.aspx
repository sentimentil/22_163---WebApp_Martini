<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Iniziale.aspx.vb" Inherits="WebApp_Martini.Iniziale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="navbar-inverse navbar-fixed-top" style="background-color: gainsboro; text-align: center">
            <asp:Label ID="LabelMessaggio" runat="server" Text="" Font-Size="32" Font-Bold="true" Height="30px" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAccedi" runat="server" Text="RIPROVA" Font-Size="26" Visible ="false" OnClick="btnAccedi_Click" />
        </div>
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Text="" Font-Size="25"></asp:Label>

            <br />
            <br />
            <%--<asp:Button ID="btnConfigurazione" runat="server" Text="Configurazione" Font-Size="20" Visible ="false" />--%>
        </div>
    </form>
</body>
</html>
