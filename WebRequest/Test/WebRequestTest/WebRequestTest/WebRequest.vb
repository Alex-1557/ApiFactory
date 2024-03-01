Imports System.Net
Imports System.Text
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NLog
Imports JwtWebClient

<TestClass()>
Public Class TstWebRequest
    Friend ReadOnly BaseUrl As String = "http://localhost:7000/"
    Friend Request As JwtWebClient
    Friend Shared Log As NLog.Logger = LogManager.GetCurrentClassLogger()


    <TestInitialize>
    Public Sub Start()
        Request = New JwtWebClient
        Request.BaseAddress = BaseUrl
        Request.Headers.Add("Content-Type", "application/json")
    End Sub


    <TestMethod()>
    Public Sub TestWebRequest(FileName As String)
        Try
            Request.GetRequest(Log, GetAdminToken(Request), "/Notification/ListAllDockerEventsConnectionToHub")
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Log.Info(Resp & vbCrLf & ex.Message)
        End Try
    End Sub

End Class