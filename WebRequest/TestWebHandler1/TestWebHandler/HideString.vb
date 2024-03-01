Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Security

Module HideString
    <Extension>
    Public Function ConvertToSecureString(ByVal password As String) As SecureString
        Return New NetworkCredential("", password).SecurePassword
    End Function
    <Extension>
    Public Function ConvertFromSecureString(theSecureString As SecureString) As String
        Return New NetworkCredential("", theSecureString).Password
    End Function
End Module
