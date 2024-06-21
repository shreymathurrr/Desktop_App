Public Class Submission
    ' Properties to hold submission details
    Public Property Name As String
    Public Property Email As String
    Public Property PhoneNum As String
    Public Property GithubLink As String
    Public Property StopwatchTime As TimeSpan

    ' Constructor to initialize a new submission
    Public Sub New(name As String, email As String, phoneNum As String, githubLink As String, stopwatchTime As TimeSpan)
        Me.Name = name
        Me.Email = email
        Me.PhoneNum = phoneNum
        Me.GithubLink = githubLink
        Me.StopwatchTime = stopwatchTime
    End Sub

    ' Default constructor (optional, useful if you need to create a blank submission)
    Public Sub New()
        Me.Name = ""
        Me.Email = ""
        Me.PhoneNum = ""
        Me.GithubLink = ""
        Me.StopwatchTime = TimeSpan.Zero
    End Sub
End Class
