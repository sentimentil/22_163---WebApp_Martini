Imports System.Data.SqlClient

Public Class Iniziale
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

        Accedi()
    End Sub


    Private Sub Accedi()

        Dim indirizzoPC As String = "?"

        Try
            LabelMessaggio.Text = ""
            Label1.Text = ""
            Dim terminale = ""

            'nomePC = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName  'user hostname (è necessario avere un server DNS)
            indirizzoPC = Request.UserHostAddress  'indirizzo IP (sul server è ::1)
            If indirizzoPC = "::1" Then indirizzoPC = "127.0.0.1"

            'Session.Add("Terminale", "D29001A0")   'test in locale debug

            Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam1").ConnectionString  'ConnectionSam1

            If Session("Terminale") Is Nothing Then    'primo giro

                'Dim Connection = New SqlConnection(str)

                Dim table As New DataTable


                Using Connessione As New SqlConnection(str)
                    Connessione.Open()
                    Dim cmd As New SqlCommand(String.Format("SELECT * FROM [dbo].[ParametriTerminale] WHERE NomeTerminale = '{0}'", indirizzoPC), Connessione)

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
                If row IsNot Nothing Then terminale = row.Item("Terminale")

            Else 'è passato dalla configurazione
                terminale = Session("Terminale")
            End If



            If String.IsNullOrWhiteSpace(terminale) Then LabelMessaggio.Text = "CONFIGURARE TERMINALE DAL PROGRAMMA SAM - IndirizzoPC=" & indirizzoPC : btnAccedi.Visible = True : Exit Sub 'Response.Redirect("Configurazione.aspx") : Exit Sub

            Dim linea As String = ""
            Dim baia As String = ""

            If terminale.StartsWith("DP") Then  'Pallettizzazione

                linea = terminale.Substring(2, 1)
                baia = terminale.Substring(terminale.Count - 2, 2)

                Dim table1 As New DataTable

                Using Connessione As New SqlConnection(str)
                    Connessione.Open()

                    Dim cmd = New SqlCommand("SELECT * FROM [dbo].[ParametriGenerali] WHERE CodiceParametro = 'MinutiPallettizzazioneWebMessage'", Connessione)
                    Dim reader = cmd.ExecuteReader

                    Try
                        table1.Load(reader)
                    Catch ex As Exception
                        Throw New Exception(ex.Message, ex)
                    Finally
                        reader.Close()
                    End Try
                End Using

                Dim row1 = table1.AsEnumerable.FirstOrDefault

                If row1 IsNot Nothing Then Session.Add("minutiSenzaCasse", row1.Item("DescrizioneParametro").ToString)


            ElseIf terminale.StartsWith("D") Then  'Depallettizzazione= D(1/2)900(1..6)A0

                linea = terminale.Substring(1, 1)
                Dim tmp As String = terminale.Split("A")(0)
                baia = tmp.Substring(tmp.Count - 1, 1)

            End If


            Session.Add("Linea", linea)
            Session.Add("Terminale", terminale)
            Session.Add("Baia", CInt(baia))


            If terminale.StartsWith("DP") Then Response.Redirect("Pallettizzazione.aspx") ': btnConfigurazione.Visible = False
            If terminale.StartsWith("D") Then Response.Redirect("Depallettizzazione.aspx") ': btnConfigurazione.Visible = False

        Catch ex As Exception
            LabelMessaggio.Text = "ERRORE! Indirizzo Terminale=" & indirizzoPC
            Label1.Text = ex.Message
            Label1.ForeColor = System.Drawing.Color.Red
            btnAccedi.Visible = True
            'btnConfigurazione.Visible = True
        End Try

    End Sub

    Protected Sub btnAccedi_Click(sender As Object, e As EventArgs)
        Accedi()
    End Sub


    'Protected Sub btnConfigurazione_Click(sender As Object, e As EventArgs) Handles btnConfigurazione.Click
    '    Response.Redirect("Configurazione.aspx")
    'End Sub


End Class