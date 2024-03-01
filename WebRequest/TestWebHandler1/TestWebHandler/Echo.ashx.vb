Imports System.IO
Imports System.Web
Imports System.Web.Services

Public Class Echo
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim RDR As New StreamReader(context.Request.InputStream)
        Dim Data As String = RDR.ReadToEnd
        context.Response.ContentType = "text/plain"
        context.Response.Write(Data)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class