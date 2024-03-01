Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Microsoft.Win32.SafeHandles
Imports Cryptor
Imports Newtonsoft.Json

<TestClass()>
Public Class CryptoApiFactory

    <TestInitialize>
    Public Sub Start()

    End Sub


    <DataRow("xxxxxxxxxxxxxxxxxxxxxxxxxxxx==", 45)>
    <TestMethod>
    Public Sub Reg(ApiName As String, Shift As Integer)
        Dim RootRegistryKey = "zzzzzzzzzzz"
        Dim Reg = ApiFactory.GetReference(Of Registry)(ApiName, Shift, "zzzzzzzzzzz")
        Console.WriteLine(Reg.GetValue(Of String)("Otp"))
    End Sub

    <DataRow("xxxxxxxxxxxxxxxxxxxxxxxxxxxx==", 45, "xxxxxxxxxxxxxxxxxxxxxxxxxxxx==", 50)>
    <TestMethod>
    Public Sub Otp(RegApiName As String, RegShift As Integer, OtpApiName As String, OtpShift As Integer)
        Dim RootRegistryKey = "zzzzzzzzzzz"
        Dim Reg = ApiFactory.GetReference(Of Registry)(RegApiName, RegShift, "zzzzzzzzzzz")
        Dim OtpSecureString = Reg.GetValue(Of String)("Otp")

        Dim Otp = ApiFactory.GetReference(Of Otp)(OtpApiName, OtpShift)
        Console.WriteLine(Otp.GetTimeBasedCode(OtpSecureString))
    End Sub

End Class
