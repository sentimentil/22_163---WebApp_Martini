Public Class Configurazione
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

        Try

            cmbTipologia.Items.Clear()
            cmbTipologia.Items.Add("Pallettizzazione")
            cmbTipologia.Items.Add("Depallettizzazione")

            Dim terminale = Session("Terminale")

            If terminale IsNot Nothing Then

                Dim tipo = terminale.ToString.Substring(0, 1)

                If terminale.ToString.StartsWith("DP") Then
                    cmbTipologia.SelectedIndex = 0
                    txtBaia.Text = CInt(terminale.Substring(terminale.ToString.Count - 2, 2))
                    txtLinea.Text = CInt(terminale.ToString.Substring(2, 2))

                ElseIf terminale.ToString.StartsWith("D") Then
                    cmbTipologia.SelectedIndex = 1
                    txtBaia.Text = CInt(terminale.Substring(terminale.ToString.Count - 4, 2))
                    txtLinea.Text = CInt(terminale.ToString.Substring(1, 1))
                End If


            End If

        Catch ex As Exception
            label1.Text = "ERRORE!  " & ex.Message
            label1.ForeColor = System.Drawing.Color.Red
        End Try



    End Sub

    Protected Sub btnConferma_Click(sender As Object, e As EventArgs) Handles btnConferma.Click

        Try

            If String.IsNullOrWhiteSpace(cmbTipologia.Text) Then Throw New Exception("Selezionare la schermata!")
            If String.IsNullOrWhiteSpace(txtLinea.Text) Then Throw New Exception("Digitare il numero della linea!")
            If String.IsNullOrWhiteSpace(txtBaia.Text) Then Throw New Exception("Digitare il numero della baia!")


            Dim tipo = cmbTipologia.Text.Substring(0, 1)
            Dim baia = CInt(txtBaia.Text).ToString("00")

            Dim terminale = ""
            Select Case tipo
                Case "D"
                    terminale = String.Format("D{0}00{1}A0", txtLinea.Text, baia)

                Case "P"
                    Dim linea = CInt(txtLinea.Text).ToString("00")
                    terminale = String.Format("DP{0}{1}", linea, baia)
            End Select


            Environment.SetEnvironmentVariable("TERMINALE", terminale, EnvironmentVariableTarget.User)

            Dim check = Environment.GetEnvironmentVariable("TERMINALE", EnvironmentVariableTarget.User)
            If check Is Nothing OrElse check <> terminale Then Throw New Exception("Errore nella registrazione del terminale!")


            Session.Add("Linea", txtLinea.Text.Trim)
            Session.Add("Terminale", terminale)
            Session.Add("Baia", txtBaia.Text.Trim)

            CambioPagina()

        Catch ex As Exception
            label1.Text = "ERRORE!  " & ex.Message
            label1.ForeColor = System.Drawing.Color.Red
        End Try



    End Sub


    Private Sub CambioPagina()
        Response.Redirect("Iniziale.aspx")
    End Sub

End Class