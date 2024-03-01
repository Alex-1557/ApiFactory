Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Microsoft.Win32.SafeHandles
Imports Cryptor
Imports Newtonsoft.Json

<TestClass()>
Public Class CryptoStreamWithOtp

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod>
    <DynamicData(NameOf(InputTestPrms), DynamicDataSourceType.Method)>
    Public Sub EncryptObjectToStream(OtpSecret As String, KeySecret As String, IvSecret As String, Request As String)
        Console.WriteLine($"Source stream={Request}")
        'create Totp
        Dim OtpComObjType = Type.GetTypeFromProgID("Cryptor.Otp")
        Dim Otp As Cryptor.Otp = Activator.CreateInstance(OtpComObjType)
        Console.WriteLine($"Secret OTP key={OtpSecret}")
        Dim Totp As String = Otp.GetTimeBasedCode(OtpSecret)
        Console.WriteLine($"TOTP={Totp}")
        'encript request
        Dim Zip As Cryptor.Gzip = New Cryptor.Gzip
        Dim Sym As New Cryptor.Symmetric
        Dim KeyBuf As Byte() = Sym.StringToByteKey(KeySecret)
        Dim IvBuf As Byte() = Sym.StringToByteKey(IvSecret)
        Dim CryptoStream As String = Sym.EnCryptString(False, Request, KeyBuf, IvBuf)
        'add Totp and zip
        Console.WriteLine($"Crypted Request With Totp={Totp}{CryptoStream}")
        Dim ZippedStream As String = Zip.Compress($"{Totp}{CryptoStream}")
        Console.WriteLine($"Crypted and Zipped Request={ZippedStream}")
        'than unzip
        Dim UnzippedStream As String = Zip.Decompress(ZippedStream)
        Console.WriteLine($"UnzippedStream without Totp={UnzippedStream.Substring(6)}")
        'extract and check Totp
        Dim ExtractTotp As String = UnzippedStream.Substring(0, 6)
        Dim PasswordMatch As Boolean = Otp.CheckPassword(OtpSecret, ExtractTotp)
        If PasswordMatch Then
            Dim DeCryptedStream As String = Sym.DeCryptString(KeyBuf, IvBuf, UnzippedStream.Substring(6))
            Console.WriteLine($"Restored stream={DeCryptedStream}")
        End If
    End Sub

    Public Shared Iterator Function InputTestPrms() As IEnumerable(Of Object())
        Yield New Object() {
            "A5PAJJFGAA2CLSIZCZPXVIPCWMWSACIU",
            "9AA127424742CC0006401DF8CB1179894DEE6FC4E444731E08612653F6B61ECB",
            "55507930F2CD9460FB5D9B6F8BE7E271",
            "qwrty=1,asdf=2"}
        Dim StringedObj As String = JsonConvert.SerializeObject(New With {
            .qwrty = 1,
            .asdf = 2}
             )
        Yield New Object() {
            "A5PAJJFGAA2CLSIZCZPXVIPCWMWSACIU",
            "9AA127424742CC0006401DF8CB1179894DEE6FC4E444731E08612653F6B61ECB",
            "55507930F2CD9460FB5D9B6F8BE7E271",
            StringedObj
            }
    End Function



End Class
