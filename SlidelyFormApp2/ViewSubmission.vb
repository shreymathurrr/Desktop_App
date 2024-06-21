Public Class ViewSubmission
    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission)

    Private Sub ViewSubmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Get the submissions from the MainForm
        Dim mainForm As MainForm = DirectCast(Application.OpenForms("MainForm"), MainForm)
        If mainForm IsNot Nothing Then
            submissions = MainForm.Submissions
        End If

        ' Display the first submission if available
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            DisplaySubmission(0)
        End If
    End Sub

    Private Sub DisplaySubmission(index As Integer)
        If submissions IsNot Nothing AndAlso index >= 0 AndAlso index < submissions.Count Then
            TextBox1.Text = submissions(index).Name
            TextBox2.Text = submissions(index).Email
            TextBox3.Text = submissions(index).PhoneNum
            TextBox4.Text = submissions(index).GithubLink
            TextBox5.Text = submissions(index).StopwatchTime.ToString("hh\:mm\:ss")

        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If submissions IsNot Nothing AndAlso currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub ViewSubmission_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.P
                    btnPrevious.PerformClick()
                Case Keys.N
                    btnNext.PerformClick()
            End Select
        End If
    End Sub
End Class
