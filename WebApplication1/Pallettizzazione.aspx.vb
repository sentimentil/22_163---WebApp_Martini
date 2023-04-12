Imports System.Data.SqlClient


Public Class Pallettizzazione
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

        Dim Baia = Session("Baia")
        If Baia Is Nothing Then
            LabelBaia.Text = "BAIA NON TROVATA!"
            LabelBaia.ForeColor = System.Drawing.Color.Red
            Response.Redirect("Iniziale.aspx")
        Else
            LabelBaia.Text = "BAIA " & Baia.ToString
            LabelBaia.ForeColor = System.Drawing.Color.Black
            LabelDateTime.Text = Date.Now.ToString
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try


            Timer1.Enabled = False

            'DatiTest()
            AggiornaDati()


        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red

        Finally
            Timer1.Enabled = True
        End Try

    End Sub


    Private Sub AggiornaDati()

        Dim Baia = Session("Baia")
        Dim Linea = Session("Linea")
        Dim Terminale = Session("Terminale")

        If Baia Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Baia non configurata!")  'perdita di connessione
        If Linea Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Linea non configurata!")  'perdita di connessione
        If Terminale Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Terminale non configurato!")  'perdita di connessione


        Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  ' & Linea
        Dim Connection = New SqlConnection(str)
        Connection.Open()

        Dim cmd As New SqlCommand(String.Format("SELECT * FROM [dbo].[VistaPallettizzatori] WHERE CodiceLinea = '{0}'", Baia.ToString.PadLeft(2, "0")), Connection)
        Dim reader = cmd.ExecuteReader

        Dim table As New DataTable
        table.Load(reader)

        reader.Close()
        cmd.Dispose()
        Connection.Close()

        Dim row = table.AsEnumerable.FirstOrDefault
        If row Is Nothing Then SvuotaDati() : Exit Sub


        Dim descrizione = row.Item("Descrizione")
        LabelBaia.Text = String.Format("BAIA {0} - {1}", Baia, descrizione)


        Dim VisualizzaImmaine As Boolean = False
        Dim NumeroUDS = row.Item("nUDS")
        Dim ChiusuraUDS As Boolean = False
        Dim numeroChiusuraUDS As String = ""

        'rendo visible=false questi oggetti e poi se serve gli cambio stato
        PanelMultiUDS.Visible = False
        PanelMonoUDS.Visible = False


        If row.Item("ChiusuraPallet") = "1" Then

            LabelAvviso.Text = "CAMBIA PALLET!"
            LabelAvviso.Visible = True
            btnUDS.Visible = False
            btnPallet.Visible = True


        Else


            Dim Giro = row.Item("Giro")
            Dim Batch = row.Item("BatchDiAttivazione")
            Dim CodicePallet = row.Item("CodicePallet")
            Dim UltimoBarcode = row.Item("UltimoBarcodeLetto")
            Dim Messaggio = row.Item("Messaggio")



            If NumeroUDS = 1 Then

                If row.Item("StatoUDS0") = 1 Then  'uds in chiusura

                    ChiusuraUDS = True
                    numeroChiusuraUDS = 0
                    Session.Add("UDS", 0)

                Else

                    Dim CodiceUDS = row.Item("UDS0")
                    Dim Volume = row.Item("VolUDS0")
                    Dim CassePallettizzate = row.Item("CassePalletizzate0")
                    Dim TotCasse = row.Item("CasseAttese0")

                    VisualizzaImmaine = True


                    LabelCodiceUDS0.Text = CodiceUDS
                    LabelVolumeUDS0.Text = Volume
                    LabelCassettePerUDS0.Text = String.Format("{0} di {1}", CassePallettizzate, TotCasse)

                End If




            ElseIf NumeroUDS = 4 Then


                If row.Item("StatoUDS1") = 1 Then
                    'uds in chiusura
                    ChiusuraUDS = True
                    numeroChiusuraUDS = 1
                    Session.Add("UDS", 1)


                ElseIf row.Item("StatoUDS2") = 1 Then
                    'uds in chiusura
                    ChiusuraUDS = True
                    numeroChiusuraUDS = 2
                    Session.Add("UDS", 2)


                ElseIf row.Item("StatoUDS3") = 1 Then
                    'uds in chiusura
                    ChiusuraUDS = True
                    numeroChiusuraUDS = 3
                    Session.Add("UDS", 3)


                ElseIf row.Item("StatoUDS4") = 1 Then
                    'uds in chiusura
                    ChiusuraUDS = True
                    numeroChiusuraUDS = 4
                    Session.Add("UDS", 4)


                Else


                    Dim CodiceUDS1 = row.Item("UDS1")
                    Dim Volume1 = row.Item("VolUDS1")
                    Dim CassePallettizzate1 = row.Item("CassePalletizzate1")
                    Dim TotCasse1 = row.Item("CasseAttese1")

                    Dim CodiceUDS2 = row.Item("UDS2")
                    Dim Volume2 = row.Item("VolUDS2")
                    Dim CassePallettizzate2 = row.Item("CassePalletizzate2")
                    Dim TotCasse2 = row.Item("CasseAttese2")

                    Dim CodiceUDS3 = row.Item("UDS3")
                    Dim Volume3 = row.Item("VolUDS3")
                    Dim CassePallettizzate3 = row.Item("CassePalletizzate3")
                    Dim TotCasse3 = row.Item("CasseAttese3")

                    Dim CodiceUDS4 = row.Item("UDS4")
                    Dim Volume4 = row.Item("VolUDS4")
                    Dim CassePallettizzate4 = row.Item("CassePalletizzate4")
                    Dim TotCasse4 = row.Item("CasseAttese4")

                    VisualizzaImmaine = True


                    LabelCodiceUDS1.Text = CodiceUDS1
                    LabelVolumeUDS1.Text = Volume1
                    LabelCassettePerUDS1.Text = String.Format("{0} di {1}", CassePallettizzate1, TotCasse1)

                    LabelCodiceUDS2.Text = CodiceUDS2
                    LabelVolumeUDS2.Text = Volume2
                    LabelCassettePerUDS2.Text = String.Format("{0} di {1}", CassePallettizzate2, TotCasse2)

                    LabelCodiceUDS3.Text = CodiceUDS3
                    LabelVolumeUDS3.Text = Volume3
                    LabelCassettePerUDS3.Text = String.Format("{0} di {1}", CassePallettizzate3, TotCasse3)

                    LabelCodiceUDS4.Text = CodiceUDS4
                    LabelVolumeUDS4.Text = Volume4
                    LabelCassettePerUDS4.Text = String.Format("{0} di {1}", CassePallettizzate4, TotCasse4)



                End If



            End If


            If ChiusuraUDS Then

                LabelAvviso.Text = String.Format("UDS {0} IN CHIUSURA!", numeroChiusuraUDS)
                LabelAvviso.Visible = True
                btnPallet.Visible = False
                btnUDS.Visible = True


            Else

                If NumeroUDS = "1" Then
                    PanelMultiUDS.Visible = False
                    PanelMonoUDS.Visible = True

                Else
                    PanelMonoUDS.Visible = False
                    PanelMultiUDS.Visible = True
                End If

            End If


            LabelGiro.Text = Giro
            LabelBatch.Text = Batch

            Select Case NumeroUDS
                Case "1"
                    LabelnUDS.Text = "MONO-UDS"

                Case "2", "3", "4"
                    LabelnUDS.Text = "MULTI-UDS"

                Case Else
                    LabelnUDS.Text = ""
            End Select

            LabelCodicePallet.Text = CodicePallet
            LabelUltimoBarcode.Text = UltimoBarcode
            LabelMessaggio.Text = Messaggio

        End If






        PalletImage.Visible = VisualizzaImmaine

        If VisualizzaImmaine Then

            Dim result = row.Item("DestinazioneUltimaCassa")

            '0=tutto; 1,2,3,4 = gli angoli
            Select Case result

                Case "0"
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletALL.jpeg")


                Case "1"
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletAS.jpeg")

                Case "2"
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletAD.jpeg")

                Case "3"
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletBS.jpeg")

                Case "4"
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletBD.jpeg")

                Case Else
                    PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/Pallet.jpeg")

            End Select

        End If


        LabelDateTime.Text = Date.Now.ToString




        'PalletImage.Visible = False
        'PalletImage1.Src = "\Immagini/PalletALL.jpeg"
        'PalletImage.Visible = False
        'Dim path = "/Immagini/PalletALL.jpeg"
        'Dim cm = "SetImmagine(" & PalletImage.ID & ")"
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallMyFunction", "SetImmagine()", True)
        'PalletImage.ImageUrl = "~/Immagini/PalletALL.jpeg"
        'PalletImage.DataBind()
        'PalletImage.Visible = False
        'PalletImage.Visible = True

    End Sub

    'Protected Sub btnConfigurazione_Click(sender As Object, e As EventArgs) Handles btnConfigurazione.Click
    '    Timer1.Enabled = False
    '    Response.Redirect("Configurazione.aspx")
    'End Sub

    Protected Sub btnUDS_Click(sender As Object, e As EventArgs) Handles btnUDS.Click

        Try
            Dim Linea = Session("Linea")
            Dim Baia = Session("Baia")

            If Baia Is Nothing Then Throw New Exception("Baia non configurata!")
            If Linea Is Nothing Then Throw New Exception("Linea non configurata!")

            Dim UDS = Session("UDS")
            If UDS Is Nothing Then Throw New Exception("UDS non registrato!")

            Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  '
            Dim Connection = New SqlConnection(str)
            Connection.Open()

            Dim cmd As New SqlCommand(String.Format("UPDATE [dbo].[VistaPallettizzatori] SET StatoUDS{1} = '2' WHERE CodiceLinea = '{0}'", Baia.ToString.PadLeft(2, "0"), UDS), Connection)
            Dim reader = cmd.ExecuteReader

            reader.Close()
            cmd.Dispose()
            Connection.Close()

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red
        End Try

    End Sub

    Protected Sub btnPallet_Click(sender As Object, e As EventArgs) Handles btnPallet.Click

        Try
            Dim Linea = Session("Linea")
            Dim Baia = Session("Baia")

            If Baia Is Nothing Then Throw New Exception("Baia non configurata!")
            If Linea Is Nothing Then Throw New Exception("Linea non configurata!")

            Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  '
            Dim Connection = New SqlConnection(str)
            Connection.Open()

            Dim cmd As New SqlCommand(String.Format("EXEC [dbo].[VistaPallettizzatori_PalletCompleto] '{0}'", Baia.ToString.PadLeft(2, "0")), Connection)
            Dim reader = cmd.ExecuteReader

            Dim table As New DataTable
            table.Load(reader)

            reader.Close()
            cmd.Dispose()
            Connection.Close()

            Dim result = ""
            If table.Rows.Count > 0 Then result = table.Rows(0).Item(0).ToString

            If Not String.IsNullOrWhiteSpace(result) Then LabelBaia.Text = result

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red
        End Try

    End Sub


    Private Sub SvuotaDati()

        'PanelMonoUDS.Visible = False
        'PanelMultiUDS.Visible = True

        LabelCodiceUDS0.Text = ""
        LabelVolumeUDS0.Text = ""
        LabelCassettePerUDS0.Text = ""

        LabelCodiceUDS1.Text = ""
        LabelVolumeUDS1.Text = ""
        LabelCassettePerUDS1.Text = ""

        LabelCodiceUDS2.Text = ""
        LabelVolumeUDS2.Text = ""
        LabelCassettePerUDS2.Text = ""

        LabelCodiceUDS3.Text = ""
        LabelVolumeUDS3.Text = ""
        LabelCassettePerUDS3.Text = ""

        LabelCodiceUDS4.Text = ""
        LabelVolumeUDS4.Text = ""
        LabelCassettePerUDS4.Text = ""

        LabelGiro.Text = ""
        LabelBatch.Text = ""
        LabelnUDS.Text = ""
        LabelCodicePallet.Text = ""
        LabelUltimoBarcode.Text = ""
        LabelMessaggio.Text = ""

        LabelAvviso.Text = ""
        btnUDS.Visible = False
        btnPallet.Visible = False

        PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/Pallet.jpeg")

    End Sub



    Private Sub DatiTest()

        PanelMonoUDS.Visible = False
        PanelMultiUDS.Visible = True

        LabelCodiceUDS0.Text = "TEST"
        LabelVolumeUDS0.Text = "TEST"
        LabelCassettePerUDS0.Text = String.Format("{0} di {1}", 0, 20)

        LabelCodiceUDS1.Text = "TEST"
        LabelVolumeUDS1.Text = "TEST"
        LabelCassettePerUDS1.Text = String.Format("{0} di {1}", 0, 10)

        LabelCodiceUDS2.Text = "TEST"
        LabelVolumeUDS2.Text = "TEST"
        LabelCassettePerUDS2.Text = String.Format("{0} di {1}", 4, 15)

        LabelCodiceUDS3.Text = "TEST"
        LabelVolumeUDS3.Text = "TEST"
        LabelCassettePerUDS3.Text = String.Format("{0} di {1}", 2, 5)

        LabelCodiceUDS4.Text = "TEST"
        LabelVolumeUDS4.Text = "TEST"
        LabelCassettePerUDS4.Text = String.Format("{0} di {1}", 12, 30)

        LabelGiro.Text = "154_20230109"
        LabelBatch.Text = "29_154_20230109"
        LabelnUDS.Text = "MULTI-UDS"
        LabelCodicePallet.Text = "TEST"
        LabelUltimoBarcode.Text = "123456789123456"
        LabelMessaggio.Text = "TEST"

        'PalletImage.Visible = False
        'LabelAvviso.Text = "CAMBIA PALLET!"
        'LabelAvviso.Visible = True
        'btnPallet.Visible = False

        'LabelAvviso.Text = String.Format("UDS {0} IN CHIUSURA!", 0)
        'LabelAvviso.Visible = True
        'btnUDS.Visible = True


        Dim Result = New Random().Next(0, 6)

        Select Case Result

            Case "0"
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletALL.jpeg")


            Case "1"
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletAS.jpeg")

            Case "2"
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletAD.jpeg")

            Case "3"
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletBS.jpeg")

            Case "4"
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/PalletBD.jpeg")

            Case Else
                PalletImage.ImageUrl = PalletImage.ResolveUrl("~/Immagini/Pallet.jpeg")
        End Select

    End Sub

End Class