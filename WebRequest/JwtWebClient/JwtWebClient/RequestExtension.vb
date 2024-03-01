Imports Newtonsoft.Json
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Text

Public Module RequestExtension
    <Extension>
    Public Function JwtPostRequest(Of T)(Request As JwtWebClient, Token As String, URL As String, PostPrm As T) As String
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Bearer: " & Token)
        Dim PostData = JsonConvert.SerializeObject(PostPrm)
        Try
            Dim Response = Encoding.UTF8.GetString(Request.UploadData(URL, Encoding.UTF8.GetBytes(PostData)))
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function BasicAuPostRequest(Of T)(Request As JwtWebClient, URL As String, Login As String, Password As String, PostPrm As T, ByRef PostData As String) As String
        Dim BasicAU As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Login & ":" & Password))
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Basic " + BasicAU)
        PostData = JsonConvert.SerializeObject(PostPrm)
        Try
            Dim Response = Encoding.UTF8.GetString(Request.UploadData(URL, Encoding.UTF8.GetBytes(PostData)))
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function BasicAuPostRequest(Request As JwtWebClient, URL As String, Login As String, Password As String) As String
        Dim BasicAU As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Login & ":" & Password))
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Basic " + BasicAU)
        Dim JsonSerializer = New JsonSerializer()
        Try
            Dim Response = Request.UploadString(URL, "")
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function BasicAuDeleteRequest(Request As JwtWebClient, URL As String, Login As String, Password As String) As String
        Dim BasicAU As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Login & ":" & Password))
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Basic " + BasicAU)
        Try
            Dim Response = Request.UploadString(URL, "DELETE", "")
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function BasicAuGetRequest(Request As JwtWebClient, URL As String, Login As String, Password As String) As String
        Dim BasicAU As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Login & ":" & Password))
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Basic " + BasicAU)
        Try
            Dim Response = Request.DownloadString(URL)
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function BasicAuJsonPostRequest(Request As JwtWebClient, URL As String, Login As String, Password As String, PostData As String) As String
        Dim BasicAU As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(Login & ":" & Password))
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Basic " + BasicAU)
        Try
            Dim Response = Encoding.UTF8.GetString(Request.UploadData(URL, Encoding.UTF8.GetBytes(PostData)))
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function GetRequest(Request As JwtWebClient, Token As String, URL As String) As String
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        If String.IsNullOrEmpty(Request.Headers("Authorization")) Then Request.Headers.Add("Authorization", "Bearer: " & Token)
        Try
            Dim Response = Request.DownloadString(URL)
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function

    <Extension>
    Public Function GetAnonRequest(Request As JwtWebClient, URL As String) As String
        If String.IsNullOrEmpty(Request.Headers("Content-Type")) Then Request.Headers.Add("Content-Type", "application/json")
        Try
            Dim Response = Request.DownloadString(URL)
            Return Response
        Catch ex As WebException
            Dim Resp As String = ""
            Dim Stream = ex.Response?.GetResponseStream()
            If Stream IsNot Nothing Then
                Dim Sr = New IO.StreamReader(Stream)
                Resp = Sr.ReadToEnd
            End If
            Return JsonConvert.SerializeObject(New With {.Response = Resp, .[Error] = ex.Message})
        End Try
    End Function
End Module