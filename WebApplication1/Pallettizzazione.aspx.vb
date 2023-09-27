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
        Dim restart As Boolean = True
        Try


            Timer1.Enabled = False

            'DatiTest()
            restart = AggiornaDati()

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red

            restart = True

        Finally
            Timer1.Interval = 500
            Timer1.Enabled = restart
        End Try

    End Sub


    Private Function AggiornaDati() As Boolean

        Dim returnValue As Boolean = True

        Dim Baia = Session("Baia")
        Dim Linea = Session("Linea")
        Dim Terminale = Session("Terminale")

        If Baia Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Baia non configurata!")  'perdita di connessione
        If Linea Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Linea non configurata!")  'perdita di connessione
        If Terminale Is Nothing Then Response.Redirect("Iniziale.aspx") ': Throw New Exception("Terminale non configurato!")  'perdita di connessione


        Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  ' & Linea
        Dim table As New DataTable

        Using Connessione As New SqlConnection(str)
            Connessione.Open()

            Dim cmd As New SqlCommand(String.Format("SELECT * FROM [dbo].[VistaPallettizzatori] WHERE CodiceLinea = '{0}'", Baia.ToString.PadLeft(2, "0")), Connessione)
            Dim reader = cmd.ExecuteReader

            'il reader va sempre chiuso (certe documentazioni di Microsoft dicono si e altre non lo mostrano negli esempi, nel dubbio chiudo)
            Try
                table.Load(reader)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            Finally
                reader.Close()
            End Try

        End Using

        'reader.Close()
        'cmd.Dispose()
        'Connection.Close()

        Dim row = table.AsEnumerable.FirstOrDefault
        If row Is Nothing Then SvuotaDati() : Return returnValue


        Dim descrizione = row.Item("Descrizione")
        LabelBaia.Text = String.Format("BAIA {0} - {1}", Baia, descrizione)

        Dim lampeggio As Boolean = False
        Dim oraLettura As Date = row.Item("DataOraLettura")
        Dim presenza = row.Item("CassaPresente")
        If IsDBNull(presenza) OrElse presenza = "" Then presenza = "0"
        If (oraLettura <> Nothing) AndAlso CBool(presenza) AndAlso ((Now - oraLettura).TotalMilliseconds <= 4000) Then lampeggio = True


        Dim VisualizzaImmaine As Boolean = False
        Dim NumeroUDS = row.Item("nUDS")
        Dim ChiusuraUDS As Boolean = False
        Dim numeroChiusuraUDS As String = ""
        Dim UDSChiusura As String = ""

        'rendo visible=false questi oggetti e poi se serve gli cambio stato
        PanelMultiUDS.Visible = False
        PanelMonoUDS.Visible = False

        LabelAvviso.Text = ""
        LabelAvviso.Visible = False
        btnPallet.Visible = False
        btnUDS.Visible = False



        Dim Giro = row.Item("Giro")
        Dim Batch = row.Item("BatchDiAttivazione")
        Dim CodicePallet = row.Item("CodicePallet")
        Dim UltimoBarcode = row.Item("UltimoBarcodeLetto")
        Dim Messaggio = row.Item("Messaggio")

        Dim precedente = row.Item("DestinazionePrecedente")

        If NumeroUDS = 1 Then

            Dim CodiceUDS = row.Item("UDS0")
            Dim Volume = row.Item("VolUDS0")
            Dim CassePallettizzate = row.Item("CassePalletizzate0")
            Dim CassePP0 = row.Item("CassePP0")
            Dim TotCasse = row.Item("CasseAttese0")

            VisualizzaImmaine = True


            LabelCodiceUDS0.Text = CodiceUDS
            LabelVolumeUDS0.Text = Volume
            LabelCassettePerUDS0.Text = String.Format("{0} di {1}", CassePallettizzate, TotCasse)
            LabelCassePP0.Text = If(IsDBNull(CassePP0), "", CassePP0)


            Alto0.Style.Item("background-color") = "transparent"

            Select Case precedente
                Case "0"
                    Alto0.Style.Item("background-color") = "LightGray"

                Case Else

            End Select


            Dim concat1 As String = ""
            Dim concat2 As String = ""
            Dim count As Integer = 0
            'Dim totQtaTotale As Integer = 0
            'Dim lastQta As Integer = 0
            'Dim last As String = ""

            Dim Articoli = row.Item("Articoli0").ToString

            If Articoli.Contains("|") Then

                For Each articolo In Articoli.Split("|")

                    If String.IsNullOrWhiteSpace(articolo) OrElse Not articolo.Contains("_") Then Continue For

                    Dim split = articolo.Split("_")

                    Dim art = split(0)
                    Dim qta = split(1)

                    Dim stringa = String.Format("{0} - {1}{2}", art, qta, "<br />")

                    'If last.Split("-")(0).Trim = art Then
                    '    'aggiorna quantità ed esco

                    '    lastQta += CInt(qta)
                    '    Dim s = String.Format("{0} - {1}{2}", art, lastQta.ToString, vbCrLf)

                    '    concat = concat.Replace(last, s)
                    '    last = s

                    '    Continue For
                    'End If


                    If count >= 5 Then
                        concat2 += stringa
                    Else
                        concat1 += stringa
                    End If

                    count += 1

                    'totQtaTotale += qta
                    'last = stringa
                    'lastQta = qta
                Next


            Else
                concat1 = Articoli
            End If


            LabelArticolo1.Text = concat1
            LabelArticolo2.Text = concat2
            'LabelQtaTotale.Text = totQtaTotale

            If row.Item("StatoUDS0") = 1 Then  'uds in chiusura

                ChiusuraUDS = True
                numeroChiusuraUDS = 0
                UDSChiusura = CodiceUDS
                'Session.Add("UDS", 0)


            End If




        ElseIf NumeroUDS = 4 Then


            Dim CodiceUDS1 = row.Item("UDS1")
            Dim Volume1 = row.Item("VolUDS1")
            Dim CassePallettizzate1 = row.Item("CassePalletizzate1")
            Dim CassePP1 = row.Item("CassePP1")
            Dim TotCasse1 = row.Item("CasseAttese1")

            Dim CodiceUDS2 = row.Item("UDS2")
            Dim Volume2 = row.Item("VolUDS2")
            Dim CassePallettizzate2 = row.Item("CassePalletizzate2")
            Dim CassePP2 = row.Item("CassePP2")
            Dim TotCasse2 = row.Item("CasseAttese2")

            Dim CodiceUDS3 = row.Item("UDS3")
            Dim Volume3 = row.Item("VolUDS3")
            Dim CassePallettizzate3 = row.Item("CassePalletizzate3")
            Dim CassePP3 = row.Item("CassePP3")
            Dim TotCasse3 = row.Item("CasseAttese3")

            Dim CodiceUDS4 = row.Item("UDS4")
            Dim Volume4 = row.Item("VolUDS4")
            Dim CassePallettizzate4 = row.Item("CassePalletizzate4")
            Dim CassePP4 = row.Item("CassePP4")
            Dim TotCasse4 = row.Item("CasseAttese4")

            VisualizzaImmaine = True


            LabelCodiceUDS1.Text = CodiceUDS1
            LabelVolumeUDS1.Text = Volume1
            LabelCassettePerUDS1.Text = String.Format("{0} di {1}", CassePallettizzate1, TotCasse1)
            LabelCassePP1.Text = If(IsDBNull(CassePP1), "", CassePP1)

            LabelCodiceUDS2.Text = CodiceUDS2
            LabelVolumeUDS2.Text = Volume2
            LabelCassettePerUDS2.Text = String.Format("{0} di {1}", CassePallettizzate2, TotCasse2)
            LabelCassePP2.Text = If(IsDBNull(CassePP2), "", CassePP2)

            LabelCodiceUDS3.Text = CodiceUDS3
            LabelVolumeUDS3.Text = Volume3
            LabelCassettePerUDS3.Text = String.Format("{0} di {1}", CassePallettizzate3, TotCasse3)
            LabelCassePP3.Text = If(IsDBNull(CassePP3), "", CassePP3)

            LabelCodiceUDS4.Text = CodiceUDS4
            LabelVolumeUDS4.Text = Volume4
            LabelCassettePerUDS4.Text = String.Format("{0} di {1}", CassePallettizzate4, TotCasse4)
            LabelCassePP4.Text = If(IsDBNull(CassePP4), "", CassePP4)




            Alto1.Style.Item("background-color") = "transparent"
            Alto2.Style.Item("background-color") = "transparent"
            Alto3.Style.Item("background-color") = "transparent"
            Alto4.Style.Item("background-color") = "transparent"

            Select Case precedente
                Case "0"

                Case "1"
                    Alto1.Style.Item("background-color") = "LightGray"

                Case "2"
                    Alto2.Style.Item("background-color") = "LightGray"

                Case "3"
                    Alto3.Style.Item("background-color") = "LightGray"

                Case "4"
                    Alto4.Style.Item("background-color") = "LightGray"

                Case Else

            End Select



            If row.Item("StatoUDS1") = 1 Then
                'uds in chiusura
                ChiusuraUDS = True
                numeroChiusuraUDS = 1
                UDSChiusura = CodiceUDS1
                'Session.Add("UDS", 1)


            ElseIf row.Item("StatoUDS2") = 1 Then
                'uds in chiusura
                ChiusuraUDS = True
                numeroChiusuraUDS = 2
                UDSChiusura = CodiceUDS2
                'Session.Add("UDS", 2)


            ElseIf row.Item("StatoUDS3") = 1 Then
                'uds in chiusura
                ChiusuraUDS = True
                numeroChiusuraUDS = 3
                UDSChiusura = CodiceUDS3
                'Session.Add("UDS", 3)


            ElseIf row.Item("StatoUDS4") = 1 Then
                'uds in chiusura
                ChiusuraUDS = True
                numeroChiusuraUDS = 4
                UDSChiusura = CodiceUDS4
                'Session.Add("UDS", 4)

            End If



        ElseIf NumeroUDS = 0 Then

            SvuotaDati()
        End If



        If ChiusuraUDS Then

            LabelAvviso.Text = String.Format("CHIUSURA UDS {0}", UDSChiusura)
            LabelAvviso.Visible = True
            btnPallet.Visible = False
            btnUDS.Visible = True

            VisualizzaImmaine = False
            returnValue = False

            Select Case NumeroUDS
                Case "1"
                    PanelMultiUDS.Visible = False
                    PanelMonoUDS.Visible = True

                Case "2", "3", "4"
                    PanelMonoUDS.Visible = False
                    PanelMultiUDS.Visible = True

                Case Else

            End Select

            Session.Add("UDS", numeroChiusuraUDS)
            Session.Add("Pallet", CodicePallet)


        ElseIf row.Item("ChiusuraPallet") = "1" Then

            LabelAvviso.Text = "CAMBIA PALLET"
            LabelAvviso.Visible = True
            btnUDS.Visible = False
            btnPallet.Visible = True

            VisualizzaImmaine = False
            returnValue = False

            Session.Add("Pallet", CodicePallet)

        Else

            Select Case NumeroUDS
                Case "1"
                    PanelMultiUDS.Visible = False
                    PanelMonoUDS.Visible = True

                Case "2", "3", "4"
                    PanelMonoUDS.Visible = False
                    PanelMultiUDS.Visible = True

                Case Else

            End Select

            VisualizzaImmaine = True
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



        colore.Style.Add("background-color", "transparent")
        top.Style.Add("background-color", "gainsboro")
        'Alto0.Style.Add("background-color", "transparent")
        'Alto1.Style.Add("background-color", "transparent")
        'Alto2.Style.Add("background-color", "transparent")
        'Alto3.Style.Add("background-color", "transparent")
        'Alto4.Style.Add("background-color", "transparent")
        Sinistra1.Style.Add("background-color", "transparent")
        Destra1.Style.Add("background-color", "transparent")
        Div3.Style.Add("background-color", "transparent")


        If Not String.IsNullOrWhiteSpace(Messaggio) Then

            colore.Style.Item("background-color") = "Red"
            top.Style.Item("background-color") = "Red"

            'Alto0.Style.Item("background-color") = "Red"
            'Alto1.Style.Item("background-color") = "Red"
            'Alto2.Style.Item("background-color") = "Red"
            'Alto3.Style.Item("background-color") = "Red"
            'Alto4.Style.Item("background-color") = "Red"

            Sinistra1.Style.Item("background-color") = "Red"
            Destra1.Style.Item("background-color") = "Red"
            Div3.Style.Item("background-color") = "Red"

        End If


        PalletImage.Visible = VisualizzaImmaine

        If VisualizzaImmaine Then

            Dim result = row.Item("DestinazioneUltimaCassa")

            Dim oldStatus = Session("StatoImmagine")
            If oldStatus IsNot Nothing AndAlso lampeggio Then
                result = If(oldStatus <> "99", "99", result)

                If result = "99" Then
                    LabelUltimoBarcode.BackColor = System.Drawing.Color.White
                    LabelMessaggio.BackColor = System.Drawing.Color.Coral
                Else
                    LabelUltimoBarcode.BackColor = System.Drawing.Color.LightGray
                    LabelMessaggio.BackColor = System.Drawing.Color.White
                End If
            End If
            Session.Add("StatoImmagine", result)

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
        LabelBaia.ForeColor = System.Drawing.Color.Black

        Return returnValue
    End Function

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
            Dim Pallet = Session("Pallet")
            If UDS Is Nothing Then Throw New Exception("UDS non registrato!")
            If Pallet Is Nothing Then Throw New Exception("Pallet non registrato!")

            Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  '
            Dim table As New DataTable

            Using Connessione As New SqlConnection(str)
                Connessione.Open()

                Dim cmd As New SqlCommand(String.Format("EXEC [dbo].[VistaPallettizzatori_UDSCompleto] '{0}','{1}',{2},''", Baia.ToString.PadLeft(2, "0"), Pallet, UDS), Connessione)
                Dim reader = cmd.ExecuteReader

                'il reader va sempre chiuso (certe documentazioni di Microsoft dicono si e altre non lo mostrano negli esempi, nel dubbio chiudo)
                Try
                    table.Load(reader)
                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    reader.Close()
                End Try

            End Using

            'Dim Connection = New SqlConnection(str)
            'Connection.Open()

            'Dim cmd As New SqlCommand(String.Format("EXEC [dbo].[VistaPallettizzatori_UDSCompleto] '{0}','{1}',{2},'0'", Baia.ToString.PadLeft(2, "0"), Pallet, UDS), Connection)
            'Dim reader = cmd.ExecuteReader

            'reader.Close()
            'cmd.Dispose()
            'Connection.Close()

            Dim result = ""
            If table.Rows.Count > 0 Then result = table.Rows(0).Item(0).ToString

            If Not String.IsNullOrWhiteSpace(result) Then LabelBaia.Text = result

            Session.Add("UDS", "")
            Session.Add("Pallet", "")

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red
        End Try
        Timer1.Interval = 100
        Timer1.Enabled = True
    End Sub

    Protected Sub btnPallet_Click(sender As Object, e As EventArgs) Handles btnPallet.Click

        Try
            Dim Linea = Session("Linea")
            Dim Baia = Session("Baia")

            If Baia Is Nothing Then Throw New Exception("Baia non configurata!")
            If Linea Is Nothing Then Throw New Exception("Linea non configurata!")

            Dim Pallet = Session("Pallet")
            If Pallet Is Nothing Then Throw New Exception("Pallet non registrato!")

            Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  '
            Dim table As New DataTable

            Using Connessione As New SqlConnection(str)
                Connessione.Open()

                Dim cmd As New SqlCommand(String.Format("EXEC [dbo].[VistaPallettizzatori_PalletCompleto] '{0}','{1}',''", Baia.ToString.PadLeft(2, "0"), Pallet), Connessione)
                Dim reader = cmd.ExecuteReader

                'il reader va sempre chiuso (certe documentazioni di Microsoft dicono si e altre non lo mostrano negli esempi, nel dubbio chiudo)
                Try
                    table.Load(reader)
                Catch ex As Exception
                    Throw New Exception(ex.Message, ex)
                Finally
                    reader.Close()
                End Try

            End Using

            'Dim Connection = New SqlConnection(str)
            'Connection.Open()

            'Dim cmd As New SqlCommand(String.Format("EXEC [dbo].[VistaPallettizzatori_PalletCompleto] '{0}','{1}','0'", Baia.ToString.PadLeft(2, "0"), Pallet), Connection)
            'Dim reader = cmd.ExecuteReader

            'table.Load(reader)

            'reader.Close()
            'cmd.Dispose()
            'Connection.Close()


            Dim result = ""
            If table.Rows.Count > 0 Then result = table.Rows(0).Item(0).ToString

            If Not String.IsNullOrWhiteSpace(result) Then LabelBaia.Text = result

            Session.Add("Pallet", "")

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red
        End Try
        Timer1.Interval = 100
        Timer1.Enabled = True
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