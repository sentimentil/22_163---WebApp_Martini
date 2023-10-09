Public Class Configurazione
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack Then Exit Sub

        Try

            cmbTipologia.Items.Clear()
            cmbTipologia.Items.Add("Pallettizzazione")
            cmbTipologia.Items.Add("Depallettizzazione")
            cmbLinea.SelectedIndex = 0

            cmbLinea.Items.Clear()
            cmbLinea.Items.Add("1")
            cmbLinea.Items.Add("2")
            cmbLinea.SelectedIndex = 0

            AggiungiBaie()


            Dim terminale = Session("Terminale")

            If terminale IsNot Nothing Then

                If terminale.ToString.StartsWith("DP") Then
                    cmbTipologia.SelectedIndex = 0
                    cmbBaia.Text = CInt(terminale.Substring(terminale.ToString.Count - 2, 2))
                    cmbLinea.SelectedIndex = (CInt(terminale.ToString.Substring(2, 2))) - 1

                ElseIf terminale.ToString.StartsWith("D") Then
                    cmbTipologia.SelectedIndex = 1
                    AggiungiBaie()
                    cmbBaia.Text = CInt(terminale.Substring(terminale.ToString.Count - 4, 2))
                    cmbLinea.SelectedIndex = (CInt(terminale.ToString.Substring(1, 1))) - 1
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
            If String.IsNullOrWhiteSpace(cmbLinea.Text) Then Throw New Exception("Selezionare la dorsale!")
            If String.IsNullOrWhiteSpace(cmbBaia.Text) Then Throw New Exception("Selezionare la baia!")


            Dim tipo = cmbTipologia.Text.Substring(0, 1)

            If tipo = "D" AndAlso cmbBaia.Text = "TUTTE" Then
                Session.Add("Linea", cmbLinea.Text)
                Response.Redirect("DepallettizzazioneALL.aspx")
                Exit Sub
            End If


            Dim baia = CInt(cmbBaia.Text).ToString("00")

            Dim terminale = ""
            Select Case tipo
                Case "D"  'D(1/2)900(1..6)A0
                    terminale = String.Format("D{0}900{1}A0", cmbLinea.Text, cmbBaia.Text)

                Case "P"
                    terminale = String.Format("DP{0}{1}", cmbLinea.Text, baia)
            End Select


            Session.Add("Linea", cmbLinea.Text)
            Session.Add("Terminale", terminale)
            Session.Add("Baia", baia)

            CambioPagina()

        Catch ex As Exception
            label1.Text = "ERRORE!  " & ex.Message
            label1.ForeColor = System.Drawing.Color.Red
        End Try



    End Sub


    Private Sub CambioPagina()
        Response.Redirect("Iniziale.aspx")
    End Sub


    Private Sub cmbTipologia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipologia.SelectedIndexChanged

        AggiungiBaie()

    End Sub

    Private Sub AggiungiBaie()

        cmbBaia.Items.Clear()

        Select Case cmbTipologia.SelectedIndex
            Case 0  'pallettizzazione
                cmbBaia.Items.Add("1")
                cmbBaia.Items.Add("2")
                cmbBaia.Items.Add("3")
                cmbBaia.Items.Add("4")
                cmbBaia.Items.Add("5")
                cmbBaia.Items.Add("6")
                cmbBaia.Items.Add("7")
                cmbBaia.Items.Add("8")
                cmbBaia.Items.Add("9")
                cmbBaia.Items.Add("10")

            Case 1  'depallettizzazione
                cmbBaia.Items.Add("1")
                cmbBaia.Items.Add("2")
                cmbBaia.Items.Add("3")
                cmbBaia.Items.Add("4")
                cmbBaia.Items.Add("5")
                cmbBaia.Items.Add("6")
                cmbBaia.Items.Add("TUTTE")
        End Select

    End Sub


End Class