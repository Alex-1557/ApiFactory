Imports System.Text
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cryptor
<TestClass()>
Public Class GetApiName

    <TestInitialize>
    Public Sub Start()

    End Sub

    <DataRow("AAAAAAA.Otp", 50)>
    <DataRow("AAAAAAA.Reg", 45)>
    <DataRow("AAAAAAA.Sym", 48)>
    <DataTestMethod>
    Public Sub CaesarTest(S As String, Shift As Integer)
        Dim X As New Random
        Dim Caesared = X.Caesar(S, Shift)
        Dim Caesared64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(Caesared))
        Console.WriteLine($"{S} ({Shift}) {Caesared64}")
        Dim Restored = X.Caesar(Encoding.UTF8.GetString(Convert.FromBase64String(Caesared64)), -Shift)
        Console.WriteLine(IIf(Restored = S, "OK", "NO"))
    End Sub


End Class
