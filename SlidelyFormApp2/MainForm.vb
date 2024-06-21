Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Public Class MainForm

    Public Shared Submissions As New List(Of Submission)
    Private currentIndex As Integer = -1
    Private ReadOnly client As New HttpClient()


    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize any necessary data or setup
    End Sub

    Private Async Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim firstSubmission As Submission = Await RetrieveDataAsync(0)
        If firstSubmission IsNot Nothing Then
            Submissions.Add(firstSubmission)
        End If

        Dim viewForm As New ViewSubmission()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Dim createForm As New CreateSubmission()
        createForm.Show() ' Use Show() instead of ShowDialog() for non-modal behavior
    End Sub

    Public Sub AddSubmission(submission As Submission)
        Submissions.Add(submission)
    End Sub

    Private Async Function SubmitDataAsync(submission As Submission) As Task
        Dim url As String = "http://localhost:3000/submit"
        Dim json As String = JsonConvert.SerializeObject(submission)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Dim response As HttpResponseMessage = Await client.PostAsync(url, content)

        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission added successfully!")
        Else
            MessageBox.Show("Error submitting data: " & response.ReasonPhrase)
        End If
    End Function

    Private Async Function RetrieveDataAsync(index As Integer) As Task(Of Submission)
        Dim url As String = $"http://localhost:3000/read?index={index}"

        Dim response As HttpResponseMessage = Await client.GetAsync(url)

        If response.IsSuccessStatusCode Then
            Dim responseData As String = Await response.Content.ReadAsStringAsync()
            Return JsonConvert.DeserializeObject(Of Submission)(responseData)
        Else
            MessageBox.Show("Error retrieving data: " & response.ReasonPhrase)
            Return Nothing
        End If
    End Function

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission.PerformClick()
        End If
    End Sub

End Class
