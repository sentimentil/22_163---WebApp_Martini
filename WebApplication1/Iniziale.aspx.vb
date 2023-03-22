Imports System.Data.SqlClient

Public Class Iniziale
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

        Accedi()
    End Sub


    Private Sub Accedi()

        Dim nomePC As String = ""

        Try
            LabelMessaggio.Text = ""
            Label1.Text = ""
            Dim terminale = ""

            nomePC = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName

            If Session("Terminale") Is Nothing Then    'primo giro

                Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam1").ConnectionString  'ConnectionSam1
                Dim Connection = New SqlConnection(str)
                Connection.Open()

                Dim cmd As New SqlCommand(String.Format("SELECT * FROM [dbo].[ParametriTerminale] WHERE NomeTerminale = '{0}'", nomePC), Connection)
                Dim reader = cmd.ExecuteReader

                Dim table As New DataTable
                table.Load(reader)

                reader.Close()
                cmd.Dispose()
                Connection.Close()

                Dim row = table.AsEnumerable.FirstOrDefault

                If row IsNot Nothing Then terminale = row.Item("Terminale")

            Else
                terminale = Session("Terminale")
            End If

            If String.IsNullOrWhiteSpace(terminale) Then LabelMessaggio.Text = "CONFIGURARE TERMINALE DAL PROGRAMMA SAM - NomePC=" & nomePC : btnAccedi.Visible = True : Exit Sub 'Response.Redirect("Configurazione.aspx") : Exit Sub

            Dim linea As String = ""
            Dim baia As String = ""

            If terminale.StartsWith("DP") Then  'Pallettizzazione

                linea = terminale.Substring(2, 1)
                baia = terminale.Substring(terminale.Count - 2, 2)

            ElseIf terminale.StartsWith("D") Then  'Depallettizzazione

                linea = terminale.Substring(1, 1)
                Dim tmp As String = terminale.Split("A")(0)
                baia = tmp.Substring(tmp.Count - 2, 2)

            End If


            Session.Add("Linea", linea)
            Session.Add("Terminale", terminale)
            Session.Add("Baia", CInt(baia))


            If terminale.StartsWith("DP") Then Response.Redirect("Pallettizzazione.aspx") ': btnConfigurazione.Visible = False
            If terminale.StartsWith("D") Then Response.Redirect("Depallettizzazione.aspx") ': btnConfigurazione.Visible = False

        Catch ex As Exception
            LabelMessaggio.Text = "ERRORE! Nome Terminale=" & nomePC
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