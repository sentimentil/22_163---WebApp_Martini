<%@ Page Title="Uteco Contec" Language="vb" AutoEventWireup="false" CodeBehind="Configurazione.aspx.vb" Inherits="WebApp_Martini.Configurazione" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Uteco Contec</title>
</head>

<body>
    
    <form id="form1" runat="server">


        <div>



            <div class="navbar-inverse navbar-fixed-top" style="background-color:gainsboro; text-align:center">
                <asp:Label ID="label1" runat="server" Text="CONFIGURAZIONE TERMINALE" Font-Size="34" Font-Bold="true"></asp:Label>
            </div>

            <br />

            <asp:Label ID="Label6" runat="server" Text="Selezionare che schermata visualizzare: " Font-Size="28"></asp:Label>
            <asp:DropDownList ID="cmbTipologia" runat="server" Font-Size="34" style="text-align:center"></asp:DropDownList>

            <br />
            <br />

            <asp:Label ID="Label3" runat="server" Text="Digitare il numero della Linea: " Font-Size="28" ></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLinea" runat="server" Font-Size="34" Width="50" style="text-align:center"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="(1 - 2) " Font-Size="26"></asp:Label>
            
            <br />
            <br />

            <asp:Label ID="Label5" runat="server" Text="Digitare il numero della Baia: " Font-Size="28"></asp:Label>
            &nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtBaia" runat="server" Font-Size="34" Width="50" style="text-align:center"></asp:TextBox>
            &nbsp;
            <asp:Label ID="Label2" runat="server" Text="(1 ... 10)" Font-Size="26"></asp:Label>

            <br />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnConferma" runat="server" Text="Conferma" Font-Size="32" />


            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />


            <asp:Image runat="server" ImageUrl="~/Immagini/logo.jpeg" Height="30px"></asp:Image>            


        </div>
    </form>
</body>
</html>
