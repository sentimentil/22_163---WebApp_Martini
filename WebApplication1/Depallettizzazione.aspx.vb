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
        Dim Connection = New SqlConnection(str)
        Connection.Open()

        Dim cmd As New SqlCommand("SELECT * FROM [dbo].[VistaWebDepal]", Connection)
        Dim reader = cmd.ExecuteReader

        Dim table As New DataTable
        table.Load(reader)

        reader.Close()
        cmd.Dispose()
        Connection.Close()

        Dim tmp = table.AsEnumerable.Where(Function(a) a.Item("LocazioneDepallettizzazione").ToString.Trim = Terminale)
        Dim noResults As Boolean = False
        Dim nextUDP As Boolean = False

        If tmp.Count > 0 Then

            Dim maxPriorita = tmp.Max(Function(a) a.Item("Priorita"))
            Dim row = tmp.Where(Function(a) a.Item("Priorita") = maxPriorita)

            If row Is Nothing Then Throw New Exception("ERRORE Max Priorità")

            If tmp.Count > row.Count Then nextUDP = True

            Dim udp = row.FirstOrDefault.Item("UDP")
            LabelGiro.Text = row.FirstOrDefault.Item("Giro")
            LabelBatch.Text = row.FirstOrDefault.Item("BatchDiAttivazione")
            LabelUDP.Text = udp

            Dim strArticoli As String = ""
            Dim totQtaTotale = 0
            Dim totQtaScaricata = 0

            If row.Count > 1 Then
                Dim tmpArticoli = row.Where(Function(a) a.Item("UDP") = udp)

                For Each art In tmpArticoli
                    strArticoli += art.Item("Vincoli_CODICE_ARTICOLO") & ","
                    totQtaTotale += art.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
                    totQtaScaricata += art.Item("CasseScaricate")
                Next

            Else
                strArticoli = row.FirstOrDefault.Item("Vincoli_CODICE_ARTICOLO")
            End If


            LabelArticolo.Text = strArticoli.Remove(strArticoli.Count - 1).Trim

            'Dim QtaTotale = row.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
            'Dim QtaScaricata = row.Item("CasseScaricate")

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

            noResults = True

        End If

        LabelNextUDP.Visible = nextUDP  'CBool(tmp.Count > 1)



        Dim result As String = "2"


        If Not noResults Then

            Connection.Open()

            'Dim cmd1 As New SqlCommand(String.Format("EXEC [dbo].[WebDepal] {0},0", Baia), Connection)
            Dim cmd1 As New SqlCommand("EXEC [dbo].[WebDepal]", Connection)

            Dim reader1 = cmd1.ExecuteReader

            Dim table1 As New DataTable
            table1.Load(reader1)

            reader1.Close()
            cmd1.Dispose()
            Connection.Close()

            If table1.Rows.Count > 0 Then result = table1.Rows(0).Item(0).ToString

            'Else


            '    SemaforoImage.ImageUrl = Nothing

        End If




        '0=verde; 1=giallo, 2=rosso
        Select Case result

            Case "0"
                SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoVerde.png")

            Case "1"
                SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoGiallo.png")


            Case "2"
                SemaforoImage.ImageUrl = SemaforoImage.ResolveUrl("~/Immagini/SemaforoRosso.png")

            Case "-1"  'ERRORE

                Throw New Exception("ERRORE SP WebDepal (-1)")

        End Select


        LabelDateTime.Text = Date.Now.ToString
        LabelBaia.Text = "BAIA " & Baia.ToString
        LabelBaia.ForeColor = System.Drawing.Color.Black



        'Select Case result

        '    Case "0"
        '        SemaforoImage.ImageUrl = "~/Immagini/SemaforoVerde.jpeg"

        '    Case "1"
        '        SemaforoImage.ImageUrl = "~/Immagini/SemaforoGiallo.jpeg"


        '    Case "2"
        '        SemaforoImage.ImageUrl = "~/Immagini/SemaforoRosso.jpeg"

        '    Case "-1"  'ERRORE

        '        Throw New Exception("ERRORE SP WebDepal (-1)")

        'End Select




        'For i = 0 To tmp.Count - 1

        '    Dim totCasse = tmp(i).Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")   'table.Rows(i).Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
        '    Dim scaricate = table.Rows(i).Item("CasseScaricate")

        '    Dim item As New Tabella With {
        '                                    .Giro = table.Rows(i).Item("Giro"),
        '                                    .Batch = table.Rows(i).Item("BatchDiAttivazione").ToString.Split("_")(3),  'se stringa è sempre composta così
        '                                    .UDP = table.Rows(i).Item("UDP"),
        '                                    .Articolo = table.Rows(i).Item("Vincoli_CODICE_ARTICOLO"),
        '                                    .Priorita = table.Rows(i).Item("Priorita"),
        '                                    .QtaRIMANENTE = totCasse - scaricate,
        '                                    .QtaSCARICATA = scaricate,
        '                                    .QtaTOTALE = totCasse}

        '    Lista.Add(item)

        'Next


        'GridView1.DataSource = Lista
        'GridView1.DataBind()

        'Dim indexGiro As Integer = 0
        'Dim indexBatch As Integer = 0
        'Dim indexQtaRimanente = 0
        'Dim indexQTAScaricata As Integer = 0
        'Dim indexTotQTA As Integer = 0

        'For i = 0 To GridView1.HeaderRow.Cells.Count - 1

        '    Select Case GridView1.HeaderRow.Cells(i).Text
        '        Case "Giro"
        '            indexGiro = i

        '        Case "Batch"
        '            indexBatch = i

        '        Case "QtaRIMANENTE"
        '            indexQtaRimanente = i

        '        Case "QtaSCARICATA"
        '            indexQTAScaricata = i

        '        Case "QtaTOTALE"
        '            indexTotQTA = i

        '    End Select

        'Next

        'For i = 0 To GridView1.Rows.Count - 1

        '    With GridView1.Rows(i).Cells.Item(indexGiro).Font
        '        .Bold = True
        '        '.Underline = True
        '    End With

        '    With GridView1.Rows(i).Cells.Item(indexBatch).Font
        '        .Bold = True
        '        '.Underline = True
        '    End With

        '    GridView1.Rows(i).Cells.Item(indexQtaRimanente).BackColor = System.Drawing.Color.LawnGreen
        '    GridView1.Rows(i).Cells.Item(indexQTAScaricata).BackColor = System.Drawing.Color.Aqua
        '    GridView1.Rows(i).Cells.Item(indexTotQTA).BackColor = System.Drawing.Color.Aqua



        'Next
    End Sub

    'Protected Sub btnConfigurazione_Click(sender As Object, e As EventArgs) Handles btnConfigurazione.Click
    '    Timer1.Enabled = False
    '    Response.Redirect("Configurazione.aspx")
    'End Sub


End Class


'Public Class Tabella
'    Property Giro As String
'    Property Batch As String
'    Property UDP As String
'    Property Articolo As String
'    Property Priorita As String
'    Property QtaRIMANENTE As String
'    Property QtaSCARICATA As String
'    Property QtaTOTALE As String
'End Class