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

            Dim cmd As New SqlCommand("SELECT * FROM [dbo].[VistaWebDepal] ORDER BY Scarico DESC, Sequenza, id", Connessione)
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


        'scarico=1 visualizzo, scarico=0 conteggio qta e forse nuovo udp in arrivo se diverso UPD da quello in scarico
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

                Dim lista = tmp.Where(Function(a) a.Item("UDP") = udp)  'righe da analizzare per conteggio e visualizzazione articoli


                Dim totQtaTotale As Integer = 0
                Dim totQtaScaricata As Integer = 0


                'Dim ArticoliInseriti As Integer = 0  'faccio vedere solo i primi due articoli della lista
                Dim concat As String = ""
                Dim lastQta As Integer = 0
                Dim last As String = ""

                For Each articolo In lista   'For Each articolo In row
                    Dim art = articolo.Item("Vincoli_CODICE_ARTICOLO")
                    Dim qta = articolo.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")

                    totQtaTotale += qta     'articolo.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
                    totQtaScaricata += articolo.Item("CasseScaricate")

                    If articolo.Item("Scarico") = "0" Then Continue For  'articolo già caricato, conteggio solo qta


                    Dim stringa = String.Format("{0} - {1}{2}", art, qta, "<br />")

                    If last.Split("-")(0).Trim = art Then
                        'aggiorna quantità ed esco

                        lastQta += CInt(qta)
                        Dim s = String.Format("{0} - {1}{2}", art, lastQta.ToString, "<br />")

                        concat = concat.Replace(last, s)
                        last = s

                        Continue For
                    End If


                    concat += stringa  'stringa & "<br />"
                    last = stringa
                    lastQta = qta
                Next

                LabelArticolo.Text = concat

                LabelQtaRimanente.Text = totQtaTotale - totQtaScaricata
                LabelQtaScaricata.Text = totQtaScaricata
                LabelQtaTotale.Text = totQtaTotale


            Else

                LabelGiro.Text = ""
                LabelBatch.Text = ""
                LabelUDP.Text = ""
                LabelArticolo.Text = ""
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
            LabelArticolo.Text = ""
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



    'Protected Sub btnConfigurazione_Click(sender As Object, e As EventArgs) Handles btnConfigurazione.Click
    '    Timer1.Enabled = False
    '    Response.Redirect("Configurazione.aspx")
    'End Sub


End Class