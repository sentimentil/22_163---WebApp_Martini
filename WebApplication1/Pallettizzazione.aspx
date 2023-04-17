<%@ Page Title="Uteco Contec" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Pallettizzazione.aspx.vb" Inherits="WebApp_Martini.Pallettizzazione" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" runat="server" Interval="500" />

    <%--<link href="/Content/Visualizzazione.css" rel="stylesheet" />--%>
    <%--<link href="http://UTC-PRG01:85/Content/Visualizzazione.css" rel="stylesheet" />--%>
    <link href="http://192.168.0.97:85/Content/Visualizzazione.css" rel="stylesheet" />



    <div class="navbar-inverse navbar-fixed-top" style="background-color: gainsboro; text-align: center">
        <asp:Label ID="LabelBaia" runat="server" Text="BAIA" Font-Size="46" Font-Bold="true" Height="30px"></asp:Label>

        <br />

        <asp:Label ID="LabelOperatore" runat="server" Text="RISPETTARE L'ORDINE DI ARRIVO DELLE CASSETTE" Font-Size="44" BackColor="Orange" Font-Bold="true"></asp:Label>
    </div>



    <asp:UpdatePanel ID="PanelMultiUDS" runat="server" UpdateMode="Conditional">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
        <ContentTemplate>


            <div id="Alto" class="Alto" runat="server">


                <div id="Alto1" class="Alto1" runat="server">

                    <asp:Label ID="Label9" runat="server" Text="Codice UDS: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCodiceUDS1" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label7" runat="server" Text="Cassette: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCassettePerUDS1" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label10" runat="server" Text="Volume: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelVolumeUDS1" runat="server" Text="" Font-Size="46"></asp:Label>

                </div>


                <div id="Alto2" class="Alto2" runat="server">

                    <asp:Label ID="Label11" runat="server" Text="Codice UDS: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCodiceUDS2" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label13" runat="server" Text="Cassette: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCassettePerUDS2" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label15" runat="server" Text="Volume: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelVolumeUDS2" runat="server" Text="" Font-Size="46"></asp:Label>

                </div>


                <div id="Alto3" class="Alto3" runat="server">

                    <asp:Label ID="Label12" runat="server" Text="Codice UDS: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCodiceUDS3" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label16" runat="server" Text="Cassette: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCassettePerUDS3" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label18" runat="server" Text="Volume: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelVolumeUDS3" runat="server" Text="" Font-Size="46"></asp:Label>

                </div>


                <div id="Alto4" class="Alto4" runat="server">

                    <asp:Label ID="Label14" runat="server" Text="Codice UDS: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCodiceUDS4" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label19" runat="server" Text="Cassette: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCassettePerUDS4" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label21" runat="server" Text="Volume: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelVolumeUDS4" runat="server" Text="" Font-Size="46"></asp:Label>

                </div>


            </div>



        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="PanelMonoUDS" runat="server" UpdateMode="Conditional" Visible="false">
        <Triggers>
           <%--<asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />--%>
        </Triggers>
        <ContentTemplate>




            <div id="Div1" class="Alto" runat="server">


                <div id="Alto0" class="Alto0" runat="server">

                    <asp:Label ID="Label6" runat="server" Text="Codice UDS: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCodiceUDS0" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label20" runat="server" Text="Cassette: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelCassettePerUDS0" runat="server" Text="" Font-Size="46"></asp:Label>

                    <br />

                    <asp:Label ID="Label23" runat="server" Text="Volume: " Font-Size="40" Width="320"></asp:Label>
                    <asp:Label ID="LabelVolumeUDS0" runat="server" Text="" Font-Size="46"></asp:Label>

                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



    <div id="Div3" class="Alto" runat="server">

        <%--<img alt="" src="Immagini/Pallet.jpeg" id="PalletImage1" width="315" runat="server" />--%>


        <asp:Image ID="PalletImage" runat="server" ImageUrl="~/Immagini/Pallet.jpeg" Height="100%" Width="107%" />

        <%--<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />--%>



        <br />

        <asp:Label ID="LabelAvviso" runat="server" Text="" Font-Size="46" Visible="false" Font-Bold="true"></asp:Label>
        <asp:Button ID="btnUDS" runat="server" Text="UDS CHIUSO" Visible="false" BackColor="LawnGreen" OnClick="btnUDS_Click" Font-Size="30" Width="380" Height="140px"/>
        <asp:Button ID="btnPallet" runat="server" Text="CAMBIO PALLET" Visible="false" BackColor="LawnGreen" OnClick="btnPallet_Click" Font-Size="26" Width="400" Height="140px"/>


    </div>



    <div id="Basso" class="Basso" runat="server">

        <div id="Sinistra1" class="Sinistra1" runat="server">

            <asp:Label ID="Label2" runat="server" Text="Giro: " Font-Size="40" Width="160"></asp:Label>
            <asp:Label ID="LabelGiro" runat="server" Text="" Font-Size="46" Font-Bold="true"></asp:Label>

            <br />

            <asp:Label ID="Label4" runat="server" Text="Batch: " Font-Size="40" Width="160"></asp:Label>
            <asp:Label ID="LabelBatch" runat="server" Text="" Font-Size="46" Font-Bold="true"></asp:Label>
        </div>



        <div id="Destra1" class="Destra1" runat="server">

            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
            <asp:Label ID="Label3" runat="server" Text="Cassa: " Font-Size="40"></asp:Label>

            <asp:Label ID="LabelUltimoBarcode" runat="server" Text="" Font-Size="46" BackColor="Coral"></asp:Label>

            <br />

            <asp:Label ID="Label8" runat="server" Text="Note: " Font-Size="40"></asp:Label>
            <asp:Label ID="LabelMessaggio" runat="server" Text="" Font-Size="46"></asp:Label>

        </div>



    </div>


    <div id="Div2" class="Centro1" runat="server">

        <asp:Label ID="LabelCodicePallet" runat="server" Text="" Font-Size="46"></asp:Label>

        <br />
        <br />
        <br />
        <br />
        <br />

        <%--&nbsp;&nbsp;&nbsp;&nbsp;--%>
        <asp:Label ID="LabelnUDS" runat="server" Text="N° UDS: " Font-Size="46" Font-Underline="true" BackColor="Aqua"></asp:Label>

    </div>



    <div id="navbar1" class="navbar-inverse navbar-fixed-bottom">
        <asp:Image runat="server" ImageUrl="~/Immagini/logo.jpeg" Height="30px"></asp:Image>
    </div>

    <div id="navbar" class="navbar-inverse navbar-fixed-bottom">
        <%--<asp:Button ID="btnConfigurazione" runat="server" Text="Configurazione" Font-Size="16" Height="34px" />--%>
        <%--&nbsp;&nbsp;&nbsp;--%>
        <asp:Label ID="LabelDateTime" runat="server" Font-Size="26pt" ForeColor="Black" Font-Italic="true" Text="00/00/0000 00:00:00" Style="text-align: right"></asp:Label>
    </div>



    <asp:Label ID="LabelTERMINALE" runat="server" Text="" Font-Size="40" Visible="false"></asp:Label>
</asp:Content>
