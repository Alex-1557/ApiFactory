Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Cryptor
Imports System.Security.Cryptography

<TestClass()>
Public Class Reg_Otp_Sym_Zip_Test

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub InitializeCryptor()
        Dim RootRegistryKey = "zzzzzzzzzzz"
        'GetRandomKey
        Dim OtpComObjType = Type.GetTypeFromProgID("AAAAAAAAA.Otp")
        Dim Otp As Cryptor.Otp = Activator.CreateInstance(OtpComObjType)
        Dim OtpKey As String = Otp.GetRandomKey(20)
        Console.WriteLine(OtpKey)
        'Write and than read Random Key to registry
        Dim RegComObjType = Type.GetTypeFromProgID("AAAAAAAAA.Reg")
        Dim Reg As Cryptor.Registry = Activator.CreateInstance(RegComObjType, RootRegistryKey)
        Reg.SetValue("Otp", OtpKey)
        Console.WriteLine(Reg.GetValue(Of String)("Otp"))
        'Initialize Rijndael cryptor
        Dim SymComObjType = Type.GetTypeFromProgID("AAAAAAAAA.Sym")
        Dim Sym As Cryptor.Symmetric = Activator.CreateInstance(SymComObjType)
        Dim Key(100) As Byte
        Dim IV(100) As Byte
        Dim Y = Sym.EnCryptString(True, "", Key, IV)
        'Save Rijndael Key/IV to Registry
        Reg.SetValue("Key", Key)
        Reg.SetValue("IV", IV)
        Console.WriteLine(Sym.ByteKeyToString(Key))
        Console.WriteLine(Sym.ByteKeyToString(IV))
        'Merge all and Zip all ini keys
        Dim ZipComObjType = Type.GetTypeFromProgID("AAAAAAAAAA.Zip")
        Dim Zip As Cryptor.Gzip = Activator.CreateInstance(ZipComObjType)
        Dim MergedAllCryptorInitializeKey = Zip.Compress($"{OtpKey}{Sym.ByteKeyToString(Key)}{Sym.ByteKeyToString(IV)}")
        Console.WriteLine($"All merged and zipped:{vbCrLf}{MergedAllCryptorInitializeKey}")
        'check
        Console.WriteLine(Zip.Decompress(MergedAllCryptorInitializeKey))
    End Sub

End Class
