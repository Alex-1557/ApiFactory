Imports CryptorStub
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Symmetric
    Implements ISymmertic
    'выдает на выход сгененрированный ключ, IV и выходным параметром - криптованную строку
    Public Function EnCryptString(ByVal GenerateNewKeyAndIV As Boolean, ByVal InputStr As String, ByRef Key As Byte(), ByRef IV As Byte()) As String Implements ISymmertic.EnCryptString
        Try
            Dim RMCrypto As New Security.Cryptography.RijndaelManaged()
            If GenerateNewKeyAndIV Then
                RMCrypto.GenerateIV()
                IV = RMCrypto.IV
                RMCrypto.GenerateKey()
                Key = RMCrypto.Key
            Else
                RMCrypto.IV = IV
                RMCrypto.Key = Key
            End If
            Dim InputArr() As Byte = (New System.Text.UnicodeEncoding).GetBytes(InputStr)
            Dim MemStream As New IO.MemoryStream(InputArr)
            Dim CryptStream1 As New Security.Cryptography.CryptoStream(MemStream, RMCrypto.CreateEncryptor(Key, IV), Security.Cryptography.CryptoStreamMode.Read)
            Dim CryptoArrLength As Integer = (InputStr.Length + 32) * 2 'размер буфера (примерный)
            Dim OutArr(CryptoArrLength) As Byte, I As Integer, OneByte As Byte
            While True
                Try
                    OneByte = CryptStream1.ReadByte
                Catch ex As System.OverflowException
                    Exit While
                End Try
                OutArr(I) = OneByte
                I += 1
            End While
            MemStream.Close()
            CryptStream1.Close()
            ReDim Preserve OutArr(I - 1)
            Dim OutBuilder As New System.Text.StringBuilder
            For j As Integer = 0 To OutArr.Length - 1
                OutBuilder.AppendFormat("{0:X2}", OutArr(j))
            Next
            Return OutBuilder.ToString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'выдает на выход сгененрированный ключ, IV и выходным параметром - криптованный массив
    Public Function EnCryptArray(ByVal GenerateNewKeyAndIV As Boolean, ByVal InputArr As Byte(), ByRef Key As Byte(), ByRef IV As Byte()) As Byte() Implements ISymmertic.EnCryptArray
        Try
            Dim RMCrypto As New Security.Cryptography.RijndaelManaged()
            If GenerateNewKeyAndIV Then
                RMCrypto.GenerateIV()
                IV = RMCrypto.IV
                RMCrypto.GenerateKey()
                Key = RMCrypto.Key
            Else
                RMCrypto.IV = IV
                RMCrypto.Key = Key
            End If
            Dim MemStream As New IO.MemoryStream(InputArr)
            Dim CryptStream1 As New Security.Cryptography.CryptoStream(MemStream, RMCrypto.CreateEncryptor(Key, IV), Security.Cryptography.CryptoStreamMode.Read)
            Dim OutArr(InputArr.Length * 16) As Byte, I As Integer, OneByte As Byte
            While True
                Try
                    OneByte = CryptStream1.ReadByte
                Catch ex As System.OverflowException
                    Exit While
                End Try
                OutArr(I) = OneByte
                I += 1
            End While
            MemStream.Close()
            CryptStream1.Close()
            ReDim Preserve OutArr(I - 1)
            Return OutArr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'кушает на входе ключ, IV, криптованную строку и выдает расшифрованную строку
    Public Function DeCryptString(ByVal Key As Byte(), ByVal IV As Byte(), ByVal InputStr As String) As String Implements ISymmertic.DeCryptString
        Try
            Dim RMCrypto As New Security.Cryptography.RijndaelManaged()
            Dim CryptoArrLength As Integer = (InputStr.Length + 32) * 2 'размер буфера (примерный)
            Dim InputArr(CryptoArrLength) As Byte, I As Integer, OneByte As Byte
            For I = 0 To InputStr.Length / 2 - 1
                OneByte = Byte.Parse(InputStr.Substring(I * 2, 2), Globalization.NumberStyles.AllowHexSpecifier)
                InputArr(I) = OneByte
            Next
            ReDim Preserve InputArr(I - 1)
            Dim MemStream As New IO.MemoryStream(InputArr)
            Dim CryptStream1 As New Security.Cryptography.CryptoStream(MemStream, RMCrypto.CreateDecryptor(Key, IV), Security.Cryptography.CryptoStreamMode.Read)
            Dim OutArr(CryptoArrLength) As Byte, j As Integer
            While True
                Try
                    OneByte = CryptStream1.ReadByte
                Catch ex As System.OverflowException
                    Exit While
                End Try
                OutArr(j) = OneByte
                j += 1
            End While
            MemStream.Close()
            CryptStream1.Close()
            ReDim Preserve OutArr(j - 1)
            Return (New System.Text.UnicodeEncoding).GetString(OutArr)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    'кушает на входе ключ, IV, криптованную строку и выдает расшифрованный массив
    Public Function DeCryptArray(ByVal Key As Byte(), ByVal IV As Byte(), ByVal InputArr() As Byte) As Byte() Implements ISymmertic.DeCryptArray
        Try
            Dim RMCrypto As New Security.Cryptography.RijndaelManaged()
            Dim MemStream As New IO.MemoryStream(InputArr)
            Dim CryptStream1 As New Security.Cryptography.CryptoStream(MemStream, RMCrypto.CreateDecryptor(Key, IV), Security.Cryptography.CryptoStreamMode.Read)
            Dim OutArr(InputArr.Length * 16) As Byte, j As Integer, OneByte As Byte
            While True
                Try
                    OneByte = CryptStream1.ReadByte
                Catch ex As System.OverflowException
                    Exit While
                End Try
                OutArr(j) = OneByte
                j += 1
            End While
            MemStream.Close()
            CryptStream1.Close()
            ReDim Preserve OutArr(j - 1)
            Return OutArr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function ByteKeyToString(ByVal Key As Byte()) As String Implements ISymmertic.ByteKeyToString
        Dim KeyBuilder = New System.Text.StringBuilder
        For j As Integer = 0 To Key.Length - 1
            KeyBuilder.AppendFormat("{0:X2}", Key(j))
        Next
        Return KeyBuilder.ToString
    End Function

    Public Function StringToByteKey(ByVal Key As String) As Byte() Implements ISymmertic.StringToByteKey
        Dim RestoreKey As New List(Of Byte)
        For i As Integer = 0 To Key.Length / 2 - 1
            RestoreKey.Add(Convert.ToByte(Key.Substring(i * 2, 2), 16))
        Next
        Return RestoreKey.ToArray
    End Function


    'это просто функция дополнения массива до 16
    Public Function PaddingArray16(ByVal InputArr As Byte()) As Byte() Implements ISymmertic.PaddingArray16
        Dim i As Integer
        Dim extraBytes As Integer = 0
        Dim len As Integer = InputArr.Length
        If len Mod 16 <> 0 Then : extraBytes = 16 - (len Mod 16) : End If
        If extraBytes <> 0 Then
            ReDim Preserve InputArr(len + extraBytes - 1)
            For i = len To (len + extraBytes - 1)
                InputArr(i) = 32 ' ascii value for space
            Next i
        End If
        Return InputArr
    End Function

End Class