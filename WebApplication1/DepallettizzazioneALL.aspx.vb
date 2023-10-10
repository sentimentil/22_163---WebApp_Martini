Imports System.Data.SqlClient

Public Class DepallettizzazioneALL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

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


    Private Sub AggiornaDati()

        Dim Linea = Session("Linea")
        If Linea Is Nothing Then Response.Redirect("Configurazione.aspx") ': Throw New Exception("Linea non configurata!")  'perdita di connessione

        Dim str = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ConnectionSam" & Linea).ConnectionString  ' & Linea
        Dim table As New DataTable
        Dim tableSemafori As New DataTable

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


            Dim cmd1 As New SqlCommand("SELECT * FROM [dbo].[Depallettizzatori]", Connessione)
            Dim reader1 = cmd1.ExecuteReader

            Try
                tableSemafori.Load(reader1)
            Catch ex As Exception
                Throw New Exception(ex.Message, ex)
            Finally
                reader1.Close()
            End Try

        End Using


        For i = 1 To 6

            Dim index = i
            Dim record = table.AsEnumerable.Where(Function(a) a.Item("Scarico") = "1" AndAlso a.Item("LocazioneAttuale").ToString.Trim = index)
            Dim s = tableSemafori.AsEnumerable.Where(Function(a) a.Item("Id").ToString.Trim = index).FirstOrDefault

            Dim batch = "" 'If(record IsNot Nothing, record.Item("BatchDiAttivazione"), "")
            Dim semaforo = If(s IsNot Nothing, s.Item("Semaforo"), 0)

            Dim qtaRimanente = 0

            If record.Any Then

                batch = record.FirstOrDefault.Item("BatchDiAttivazione")
                Dim udp = record.FirstOrDefault.Item("UDP")

                Dim righe = record.Where(Function(a) a.Item("UDP") = udp)

                For Each r In righe
                    Dim qtaTot = r.Item("Vincoli_NUMERO_CASSE_SET_ASSEGNAZIONE")
                    Dim qtaScaricate = r.Item("CasseScaricate")
                    qtaRimanente += (qtaTot - qtaScaricate)
                Next


            End If

            Select Case i

                Case 1

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image1.ImageUrl = Image1.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image1.ImageUrl = Image1.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image1.ImageUrl = Image1.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image1.ImageUrl = Image1.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label1.Text = batch
                    Label1Qta.Text = qtaRimanente


                Case 2

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image2.ImageUrl = Image2.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image2.ImageUrl = Image2.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image2.ImageUrl = Image2.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image2.ImageUrl = Image2.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label2.Text = batch
                    Label2Qta.Text = qtaRimanente


                Case 3

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image3.ImageUrl = Image3.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image3.ImageUrl = Image3.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image3.ImageUrl = Image3.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image3.ImageUrl = Image3.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label3.Text = batch
                    Label3Qta.Text = qtaRimanente


                Case 4

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image4.ImageUrl = Image4.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image4.ImageUrl = Image4.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image4.ImageUrl = Image4.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image4.ImageUrl = Image4.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label4.Text = batch
                    Label4Qta.Text = qtaRimanente


                Case 5

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image5.ImageUrl = Image5.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image5.ImageUrl = Image5.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image5.ImageUrl = Image5.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image5.ImageUrl = Image5.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label5.Text = batch
                    Label5Qta.Text = qtaRimanente


                Case 6

                    If semaforo >= 1 AndAlso semaforo <= 10 Then
                        Image6.ImageUrl = Image6.ResolveUrl("~/Immagini/SemaforoVerde.png")


                    ElseIf semaforo >= 11 AndAlso semaforo <= 20 Then
                        Image6.ImageUrl = Image6.ResolveUrl("~/Immagini/SemaforoGiallo.png")


                    ElseIf semaforo >= 21 AndAlso semaforo <= 30 Then
                        Image6.ImageUrl = Image6.ResolveUrl("~/Immagini/SemaforoRosso.png")

                    Else

                        Image6.ImageUrl = Image6.ResolveUrl("~/Immagini/SemaforoGrigio.png")
                    End If

                    Label6.Text = batch
                    Label6Qta.Text = qtaRimanente

            End Select

        Next

        LabelDateTime.Text = Date.Now.ToString
        LabelBaia.ForeColor = System.Drawing.Color.Black
    End Sub


End Class