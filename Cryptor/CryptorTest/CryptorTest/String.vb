Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cryptor
Imports System.Security.Cryptography

<TestClass()>
Public Class TestString

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub SecureString()
        Dim Pass = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx=="
        Console.WriteLine(Pass.ConvertToSecureString.Dump)
    End Sub

End Class
