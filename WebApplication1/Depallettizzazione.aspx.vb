Imports System.Data.SqlClient
Imports System.Runtime.InteropServices


Public Class Depallettizzazione
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

            AggiornaDati()

        Catch ex As Exception
            LabelBaia.Text = "ERRORE! " & ex.Message
            LabelBaia.ForeColor = System.Drawing.Color.Red

        Finally
            Timer1.Interval = 3000
            Timer1.Enabled = True
        End Try

    End Sub

    Private Sub SemaforoImage_PreRender(sender As Object, e As EventArgs) Handles SemaforoImage.PreRender
        'SemaforoImage.Visible = False
        'SemaforoImage.Visible = True
    End Sub


    Private Sub AggiornaDati()

        'Dim Lista As New List(Of Tabella)

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

            Dim cmd As New SqlCommand("SELECT * FROM [dbo].[VistaWebDepal] ORDER BY Scarico DESC, DataMessaInScarico DESC, Sequenza, id", Connessione)
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
        'reader.Close()
        'cmd.Dispose()
        'Connection.Close()


        'scarico=1 visualizzo a prescindere dall'udp, scarico=0 nuovo udp in arrivo
        Dim tmp = table.AsEnumerable.Where(Function(a) a.Item("LocazioneAttuale").ToString.Trim = Baia.ToString)
        'Dim noResults As Boolean = False
        Dim nextUDP As Boolean = False

        If tmp.Any Then

            Dim row = tmp.Where(Function(a) a.Item("Scarico") = "1")
            Dim udp = ""

            If row.Any Then
                udp = row.FirstOrDefault.Item("UDP")
                LabelGiro.Text = row.FirstOrDefault.Item("Giro")
                LabelBatch.Text = row.FirstOrDefault.Item("BatchDiAttivazione")
                LabelUDP.Text = udp

                'Dim lista = tmp.Where(Function(a) a.Item("UDP") = udp)  'righe da analizzare per conteggio e visualizzazione articoli


                Dim totQtaTotale As Integer = 0
                Dim totQtaScaricata As Integer = 0


                'Dim ArticoliInseriti As Integer = 0  'faccio vedere solo i primi due articoli della lista
                Dim concat As String = ""
                Dim lastQta As Integer = 0
                Dim lastRimanente As Integer = 0
                Dim last As String = ""

                PulisciLabelArticoli()
                Dim indiceLabel As Integer = 0

                For Each articolo In row   'For Each articolo In row
                    Dim art = articolo.Item("Vincoli_CODICE_ARTICOLO")
                    Dim qta = articolo.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
                    Dim view_udp = articolo.Item("UDP")
                    Dim scaricate = articolo.Item("CasseScaricate")
                    Dim rimanente = qta - scaricate

                    totQtaTotale += qta     'articolo.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
                    totQtaScaricata += scaricate

                    'If articolo.Item("Scarico") = "0" Then Continue For  'articolo già caricato, conteggio solo qta


                    Dim stringa = String.Format("{0}-{1} - {2}-{3}", view_udp, art, qta, rimanente)  'String.Format("{0} - {1}{2}", art, qta, "<br />")

                    If last.Trim <> "" Then
                        Dim spl = last.Split("-")
                        Dim old_check = String.Format("{0}-{1}", spl(0).Trim, spl(1).Trim)
                        Dim new_check = String.Format("{0}-{1}", view_udp, art)

                        If old_check = new_check Then
                            'aggiorna quantità ed esco

                            lastQta += CInt(qta)
                            lastRimanente += CInt(rimanente)
                            Dim s = String.Format("{0}-{1} - {2}-{3}", view_udp, art, lastQta.ToString, lastRimanente.ToString)  'String.Format("{0} - {1}{2}", art, lastQta.ToString, "<br />")

                            'If view_udp <> udp Then s = "* " & s

                            Select Case indiceLabel
                                Case 1
                                    LabelArticolo1.Text = s
                                    If view_udp <> udp Then LabelArticolo1.ForeColor = Drawing.Color.Red

                                Case 2
                                    LabelArticolo2.Text = s
                                    If view_udp <> udp Then LabelArticolo2.ForeColor = Drawing.Color.Red

                                Case 3
                                    LabelArticolo3.Text = s
                                    If view_udp <> udp Then LabelArticolo3.ForeColor = Drawing.Color.Red

                                Case 4
                                    LabelArticolo4.Text = s
                                    If view_udp <> udp Then LabelArticolo4.ForeColor = Drawing.Color.Red

                                Case 5
                                    LabelArticolo5.Text = s
                                    If view_udp <> udp Then LabelArticolo5.ForeColor = Drawing.Color.Red

                                Case 6
                                    LabelArticolo6.Text = s
                                    If view_udp <> udp Then LabelArticolo6.ForeColor = Drawing.Color.Red

                                Case 7
                                    LabelArticolo7.Text = s
                                    If view_udp <> udp Then LabelArticolo7.ForeColor = Drawing.Color.Red

                                Case 8
                                    LabelArticolo8.Text = s
                                    If view_udp <> udp Then LabelArticolo8.ForeColor = Drawing.Color.Red

                                Case 9
                                    LabelArticolo9.Text = s
                                    If view_udp <> udp Then LabelArticolo9.ForeColor = Drawing.Color.Red

                                Case Else
                            End Select

                            'concat = concat.Replace(last, s)
                            last = s

                            Continue For
                        End If
                    End If

                    'If last.Trim <> "" AndAlso last.Split("-")(0).Trim = art Then
                    '    'aggiorna quantità ed esco

                    '    lastQta += CInt(qta)
                    '    Dim s = String.Format("{0}-{1} - {2}{3}", view_udp, art, lastQta.ToString, "<br />")  'String.Format("{0} - {1}{2}", art, lastQta.ToString, "<br />")

                    '    concat = concat.Replace(last, s)
                    '    last = s

                    '    Continue For
                    'End If

                    'If view_udp <> udp Then stringa = "* " & stringa

                    Select Case indiceLabel
                        Case 0
                            LabelArticolo1.Text = stringa
                            If view_udp <> udp Then LabelArticolo1.ForeColor = Drawing.Color.Red

                        Case 1
                            LabelArticolo2.Text = stringa
                            If view_udp <> udp Then LabelArticolo2.ForeColor = Drawing.Color.Red

                        Case 2
                            LabelArticolo3.Text = stringa
                            If view_udp <> udp Then LabelArticolo3.ForeColor = Drawing.Color.Red

                        Case 3
                            LabelArticolo4.Text = stringa
                            If view_udp <> udp Then LabelArticolo4.ForeColor = Drawing.Color.Red

                        Case 4
                            LabelArticolo5.Text = stringa
                            If view_udp <> udp Then LabelArticolo5.ForeColor = Drawing.Color.Red

                        Case 5
                            LabelArticolo6.Text = stringa
                            If view_udp <> udp Then LabelArticolo6.ForeColor = Drawing.Color.Red

                        Case 6
                            LabelArticolo7.Text = stringa
                            If view_udp <> udp Then LabelArticolo7.ForeColor = Drawing.Color.Red

                        Case 7
                            LabelArticolo8.Text = stringa
                            If view_udp <> udp Then LabelArticolo8.ForeColor = Drawing.Color.Red

                        Case 8
                            LabelArticolo9.Text = stringa
                            If view_udp <> udp Then LabelArticolo9.ForeColor = Drawing.Color.Red

                        Case Else
                            'non compilo nulla, continuo ciclo per quantità
                    End Select

                    indiceLabel += 1
                    'concat += stringa
                    last = stringa
                    lastQta = qta
                    lastRimanente = rimanente
                Next

                'LabelArticolo.Text = concat

                LabelQtaRimanente.Text = totQtaTotale - totQtaScaricata
                LabelQtaScaricata.Text = totQtaScaricata
                LabelQtaTotale.Text = totQtaTotale


            Else

                LabelGiro.Text = ""
                LabelBatch.Text = ""
                LabelUDP.Text = ""
                PulisciLabelArticoli()
                LabelQtaRimanente.Text = ""
                LabelQtaScaricata.Text = ""
                LabelQtaTotale.Text = ""
            End If

            Dim row_nextUDP = tmp.Where(Function(a) a.Item("UDP") <> udp AndAlso a.Item("Scarico") = "0")
            If row_nextUDP.Any Then nextUDP = True


        Else

            LabelGiro.Text = ""
            LabelBatch.Text = ""
            LabelUDP.Text = ""
            PulisciLabelArticoli()
            LabelQtaRimanente.Text = ""
            LabelQtaScaricata.Text = ""
            LabelQtaTotale.Text = ""

            'noResults = True

        End If

        LabelNextUDP.Visible = nextUDP  'CBool(tmp.Count > 1)



        Dim result As Integer = 0

        Dim table1 As New DataTable

        Using Connessione As New SqlConnection(str)
            Connessione.Open()
            Dim cmd1 As New SqlCommand("SELECT * FROM [dbo].[Depallettizzatori]", Connessione)
            Dim reader1 = cmd1.ExecuteReader

            'il reader va sempre chiuso (certe documentazioni di Microsoft dicono si e altre non lo mostrano negli esempi, nel dubbio chiudo)
            Try
                table1.Load(reader1)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            Finally
                reader1.Close()
            End Try

        End Using

        'reader1.Close()
        'cmd1.Dispose()
        'Connection.Close()


        If table1.Rows.Count > 0 Then

            Dim tmp1 = table1.AsEnumerable.Where(Function(a) a.Item("Id").ToString.Trim = Baia.ToString).FirstOrDefault
            If tmp1 IsNot Nothing Then result = tmp1.Item("Semaforo")
        End If

        'Gestione Semafori 1-10 verde 11-20 giallo 21-30 Rosso
        If result >= 1 AndAlso result <= 10 Then
            SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoVerde.png")


        ElseIf result >= 11 AndAlso result <= 20 Then
            SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoGiallo.png")


        ElseIf result >= 21 AndAlso result <= 30 Then
            SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoRosso.png")

        Else

            SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoGrigio.png")
        End If



        LabelDateTime.Text = Date.Now.ToString
        LabelBaia.ForeColor = System.Drawing.Color.Black

    End Sub


    Private Sub PulisciLabelArticoli()

        LabelArticolo1.Text = ""
        LabelArticolo2.Text = ""
        LabelArticolo3.Text = ""
        LabelArticolo4.Text = ""
        LabelArticolo5.Text = ""
        LabelArticolo6.Text = ""
        LabelArticolo7.Text = ""
        LabelArticolo8.Text = ""
        LabelArticolo9.Text = ""

        LabelArticolo1.ForeColor = Drawing.Color.Black
        LabelArticolo2.ForeColor = Drawing.Color.Black
        LabelArticolo3.ForeColor = Drawing.Color.Black
        LabelArticolo4.ForeColor = Drawing.Color.Black
        LabelArticolo5.ForeColor = Drawing.Color.Black
        LabelArticolo6.ForeColor = Drawing.Color.Black
        LabelArticolo7.ForeColor = Drawing.Color.Black
        LabelArticolo8.ForeColor = Drawing.Color.Black
        LabelArticolo9.ForeColor = Drawing.Color.Black

    End Sub


    'Protected Sub btnConfigurazione_Click(sender As Object, e As EventArgs) Handles btnConfigurazione.Click
    '    Timer1.Enabled = False
    '    Response.Redirect("Configurazione.aspx")
    'End Sub


End Class