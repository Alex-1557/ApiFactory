Imports System.Security.Cryptography
Imports System.Text
Imports Cryptor
<TestClass()>
Public Class TstSym

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub Crypt()
        Dim Source As String = "@#cZPLeKj2t+74k8TsxZM(Sy72azrqTpNkqT*^dcB@eR4bG7!Ty(C(WdSIn28g6t@#cZPLeKj2t+74k8TsxZM(Sy72azrqTpNkqT*^dcB@eR4bG7!Ty(C(WdSIn28g6t"
        Console.WriteLine($"Source {Source}")
        Dim X As New Symmetric
        Dim Key(100) As Byte
        Dim IV(100) As Byte
        Dim Y = X.EnCryptString(True, Source, Key, IV)
        Console.WriteLine($"Key    {X.ByteKeyToString(Key)}")
        Console.WriteLine($"IV     {X.ByteKeyToString(IV)}")
        Console.WriteLine($"Result {Y}")
        Console.WriteLine($"Key    {X.ByteKeyToString(X.StringToByteKey(X.ByteKeyToString(Key)))}")
        Dim Z = X.DeCryptString(Key, IV, Y)
        Console.WriteLine($"Source {Z}")
    End Sub


End Class
