Imports CryptorStub
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Random
    Implements IRandom
    Public Function Caesar(ByVal source As String, ByVal shift As Int16) As String Implements IRandom.Caesar
        Dim maxChar = Convert.ToInt32(Char.MaxValue)
        Dim minChar = Convert.ToInt32(Char.MinValue)
        Dim buffer = source.ToCharArray()

        For i = 0 To buffer.Length - 1
            Dim shifted = Convert.ToInt32(buffer(i)) + shift

            If shifted > maxChar Then
                shifted -= maxChar
            ElseIf shifted < minChar Then
                shifted += maxChar
            End If

            buffer(i) = Convert.ToChar(shifted)
        Next

        Return New String(buffer)
    End Function
    Public Function CreateRandomPassword(Len As Integer, Optional FromChagCode As UInt32 = &H21, Optional ToCharCode As UInt32 = &H7E, Optional ExcludeChars As String = "<>'""") As String Implements IRandom.CreateRandomPassword
        Dim Ret1 As New System.Text.StringBuilder
        While Ret1.Length < Len
            Dim RandomNum As Integer = FromChagCode + GetRandomInteger(ToCharCode - FromChagCode)
            Dim OneChar As Char = ChrW(RandomNum)
            If Not ExcludeChars.Contains(OneChar) Then
                Ret1.Append(OneChar)
            End If
        End While
        Return Ret1.ToString
    End Function

    Public Function GetRandomInteger(MaxValue As Integer) As Integer Implements IRandom.GetRandomInteger
        Dim byte_count As Byte() = New Byte(3) {}
        Dim random_number As New System.Security.Cryptography.RNGCryptoServiceProvider()
        random_number.GetBytes(byte_count)
        Return (Math.Abs(BitConverter.ToInt32(byte_count, 0)) / Int32.MaxValue) * MaxValue
    End Function

End Class
