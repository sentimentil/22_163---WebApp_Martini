<%@ Page Title="Uteco Contec" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Depallettizzazione.aspx.vb" Inherits="WebApp_Martini.Depallettizzazione" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script>
        (function($) {
  var cache = [];
  // Arguments are image paths relative to the current page.
  $.preLoadImages = function() {
    var args_len = arguments.length;
    for (var i = args_len; i--;) {
      var cacheImage = document.createElement('img');
      cacheImage.src = arguments[i];
      cache.push(cacheImage);
    }
  }
})(jQuery)
    </script>--%>



    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" runat="server" Interval="500" />

    <%--<link href="~/Content/Visualizzazione.css" rel="stylesheet" />--%>
    <%--<link href="http://UTC-PRG01:85/Content/Visualizzazione.css" rel="stylesheet" />--%>
    <link href="http://192.168.13.10:85/Content/Visualizzazione.css" rel="stylesheet"/>



    <asp:UpdatePanel ID="Panel" runat="server" UpdateMode="Conditional">
        <Triggers>
            <%--            <asp:AsyncPostBackTrigger ControlID="Timer1" />--%>
        </Triggers>
        <ContentTemplate>


            <div class="navbar-inverse navbar-fixed-top" style="background-color: gainsboro; text-align: center">
                <asp:Label ID="LabelBaia" runat="server" Text="BAIA" Font-Size="44" Font-Bold="true" Height="30px"></asp:Label>
            </div>



            <div id="sinistra" class="Sinistra" runat="server">
                <br />
                <asp:Image ID="SemaforoImage" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Width="104%" />
            </div>


            <div id="destra" class="Destra" runat="server">
                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                <asp:Label ID="LabelNextUDP" runat="server" Text="IN ARRIVO NUOVO UDP!" Font-Size="46" BackColor="Coral" Visible="false"></asp:Label>

                <br />
                <br />
                <br />

                <%--<asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-BackColor="#EEEEEE" HeaderStyle-BackColor ="Yellow" RowStyle-HorizontalAlign="Center" ></asp:GridView>--%>

                <asp:Label ID="Label2" runat="server" Text="Giro: " Font-Size="40" Width="160" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelGiro" runat="server" Text="" Font-Size="46" Font-Bold="true"></asp:Label>

                <br />

                <asp:Label ID="Label5" runat="server" Text="Batch: " Font-Size="40" Width="160" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelBatch" runat="server" Text="" Font-Size="46" Font-Bold="true"></asp:Label>

                <br />

                <asp:Label ID="Label4" runat="server" Text="UDP: " Font-Size="40" Width="160" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelUDP" runat="server" Text="" Font-Size="46"></asp:Label>

                <br />
                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                <asp:Label ID="Label7" runat="server" Text="Casse Rimanenti: " Font-Size="40" Width="430" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelQtaRimanente" runat="server" Text="" Font-Size="46" BackColor="LawnGreen"></asp:Label>

                <br />
                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                <asp:Label ID="Label8" runat="server" Text="Casse Scaricate: " Font-Size="40" Width="430" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelQtaScaricata" runat="server" Text="" Font-Size="46" BackColor="Aqua"></asp:Label>

                <br />                


            </div>

            <div id="Destra2" class="Destra2" runat="server">
                <%--<br />
                <br />
                <br />
                <br />--%>
                <asp:Label ID="Label6" runat="server" Text="UDP-ARTICOLO - QTÀ" Font-Size="40" Font-Underline="true" Style="text-align: left"></asp:Label>
                <br />
                <asp:Label ID="LabelArticolo" runat="server" Text="" Font-Size="46"></asp:Label>

                <asp:Label ID="Label9" runat="server" Text="Tot Qtà: " Font-Size="40" Style="text-align: left"></asp:Label>
                <asp:Label ID="LabelQtaTotale" runat="server" Text="" Font-Size="46"></asp:Label>
            </div>




            <div id="navbar1" class="navbar-inverse navbar-fixed-bottom">
                <asp:Image runat="server" ImageUrl="~/Immagini/logo.jpeg" Height="30px"></asp:Image>
                &nbsp;&nbsp;&nbsp;&nbsp;
                
            </div>


            <div id="navbar" class="navbar-inverse navbar-fixed-bottom">

                <%--<asp:Button ID="btnConfigurazione" runat="server" Text="Configurazione" Font-Size="16" Height="34px" />--%>
                <%--&nbsp;&nbsp;&nbsp;--%>

                <asp:Label ID="LabelDateTime" runat="server" Font-Size="26pt" ForeColor="Black" Font-Italic="true" Text="00/00/0000 00:00:00" Style="text-align: right"></asp:Label>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>





</asp:Content>
