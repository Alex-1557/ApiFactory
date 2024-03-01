Imports System.Net
Imports Newtonsoft.Json
Imports System.Runtime.CompilerServices
Imports System.Text

Public Class JwtWebClient
    Inherits WebClient
    Protected Overloads Function GetWebRequest(URL As Uri) As WebRequest
        Dim WebRequest = MyBase.GetWebRequest(URL)
        Debug.WriteLine(WebRequest.RequestUri)
        WebRequest.ContentType = "application/json"
        WebRequest.Timeout = Integer.MaxValue
        Return WebRequest
    End Function

End Class


