Imports System.Diagnostics
Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text

Public Class CreateSubmission
    Private stopwatchRunning As Boolean = False
    Private stopwatch As New Stopwatch()

    Private Sub CreateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ClearSubmissionFields()
    End Sub

    Private Sub CreateForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        End If
    End Sub

    Private Sub ClearSubmissionFields()
        txtName.Text = ""
        txtEmail.Text = ""
        txtPhoneNum.Text = ""
        txtGithubLink.Text = ""
        txtStopwatchTime.Text = "00:00:00"
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim stopwatchTime As TimeSpan
        If Not TimeSpan.TryParse(txtStopwatchTime.Text, stopwatchTime) Then
            MessageBox.Show("Invalid stopwatch time format. Please use hh:mm:ss format.")
            Return
        End If

        Dim newSubmission As New Submission(
            txtName.Text,
            txtEmail.Text,
            txtPhoneNum.Text,
            txtGithubLink.Text,
            stopwatchTime
        )
        Dim json As String = JsonConvert.SerializeObject(newSubmission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        ' Make the API call to save the submission
        Using client As New HttpClient()

            Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission added successfully!")
                Me.Close()
            End If

        End Using

        Dim mainForm As MainForm = DirectCast(Application.OpenForms("MainForm"), MainForm)
        If mainForm IsNot Nothing Then
            mainForm.AddSubmission(newSubmission)
        End If

        MessageBox.Show("Submission added successfully!")
        Me.Close()
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatchRunning Then
            stopwatch.Stop()
            btnToggleStopwatch.Text = "Resume Stopwatch"
        Else
            stopwatch.Start()
            btnToggleStopwatch.Text = "Pause Stopwatch"
        End If
        stopwatchRunning = Not stopwatchRunning
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub
End Class

