<%@ Page Title="Uteco Contec" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DepallettizzazioneALL.aspx.vb" Inherits="WebApp_Martini.DepallettizzazioneALL" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


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
                <asp:Label ID="LabelBaia" runat="server" Text="SITUAZIONE GENERALE BAIE DEPALLETTIZZAZIONE" Font-Size="44" Font-Bold="true" Height="28px"></asp:Label>
            </div>

            <div id="Div1" class="Depall1" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label7" runat="server" Text="BAIA 1" Font-Size="40" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label13" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label19" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label1Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>

            <div id="Div2" class="Depall2" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label8" runat="server" Text="BAIA 2" Font-Size="40" Font-Bold="true"></asp:Label>                
                <br />
                <asp:Label ID="Label14" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label20" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label2Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>

            <div id="Div3" class="Depall3" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label9" runat="server" Text="BAIA 3" Font-Size="40" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label15" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label21" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label3Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>

            <div id="Div4" class="Depall4" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image4" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label10" runat="server" Text="BAIA 4" Font-Size="40" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label16" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label22" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label4Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>

            <div id="Div5" class="Depall5" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label11" runat="server" Text="BAIA 5" Font-Size="40" Font-Bold="true"></asp:Label>                
                <br />
                <asp:Label ID="Label17" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label23" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label5Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>

            <div id="Div6" class="Depall6" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Image ID="Image6" runat="server" ImageUrl="~/Immagini/SemaforoRosso.png" Height="80%" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                <asp:Label ID="Label12" runat="server" Text="BAIA 6" Font-Size="40" Font-Bold="true"></asp:Label>                
                <br />
                <asp:Label ID="Label18" runat="server" Text="BATCH: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="Label24" runat="server" Text="Qtà Rimanente: " Font-Size="30" Font-Bold="true"></asp:Label>
                <asp:Label ID="Label6Qta" runat="server" Text="" Font-Size="30" Font-Bold="true"></asp:Label>
            </div>



            <div id="navbar1" class="navbar-inverse navbar-fixed-bottom">
                <asp:Image runat="server" ImageUrl="~/Immagini/logo.jpeg" Height="30px"></asp:Image>
                &nbsp;&nbsp;&nbsp;&nbsp;
                
            </div>


            <div id="navbar" class="navbar-inverse navbar-fixed-bottom">

                <asp:Label ID="LabelDateTime" runat="server" Font-Size="26pt" ForeColor="Black" Font-Italic="true" Text="00/00/0000 00:00:00" Style="text-align: right"></asp:Label>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>





</asp:Content>