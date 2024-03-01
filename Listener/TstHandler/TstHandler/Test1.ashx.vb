Imports System.Web
Imports System.Web.Services
Imports CryptorStub
Public Class Test1
    Implements System.Web.IHttpHandler

    Const OtpApiName As String = "dcKkwqvCosKmwqHCpGDCgcKmwqI="
    Const OtpApiSh As Integer = 50
    Const RegApiName As String = "cMKfwqbCncKhwpzCn1t/wpLClA=="
    Const RegApiSh As Integer = 45
    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim OTP As CryptorStub.IOtp = ApiFactory.GetReference(Of IOtp)(OtpApiName, OtpApiSh)
        Dim Key As String = OTP.GetRandomKey(20)

        Dim Reg As CryptorStub.IRegistry = ApiFactory.GetReference(Of IRegistry)(RegApiName, RegApiSh, "zzzzzzzzzzz")
        Reg.SetValue("Key", Key)
        context.Response.ContentType = "text/plain"
        context.Response.Write(Reg.GetValue(Of String)("Key"))
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class