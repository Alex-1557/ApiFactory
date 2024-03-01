Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports CryptorStub
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Gzip
    Implements IGzip
    Public Function Compress(ByVal input As String) As String Implements IGzip.Compress
        Return Convert.ToBase64String(Compress(Encoding.UTF8.GetBytes(input)))
    End Function

    Public Function Compress(ByVal input As Byte()) As Byte() Implements IGzip.Compress
        Dim array As Byte()
        Using result As MemoryStream = New MemoryStream()
            Dim lengthBytes As Byte() = BitConverter.GetBytes(CInt(input.Length))
            result.Write(lengthBytes, 0, 4)
            Using compressionStream As GZipStream = New GZipStream(result, CompressionMode.Compress)
                compressionStream.Write(input, 0, CInt(input.Length))
                compressionStream.Flush()
            End Using
            array = result.ToArray()
        End Using
        Return array
    End Function

    Public Function Decompress(ByVal input As String) As String Implements IGzip.Decompress
        Dim decompressed As Byte() = Decompress(Convert.FromBase64String(input))
        Return Encoding.UTF8.GetString(decompressed)
    End Function

    Public Function Decompress(ByVal input As Byte()) As Byte() Implements IGzip.Decompress
        Dim numArray As Byte()
        Using source As MemoryStream = New MemoryStream(input)
            Dim lengthBytes(3) As Byte
            source.Read(lengthBytes, 0, 4)
            Dim length As Integer = BitConverter.ToInt32(lengthBytes, 0)
            Using decompressionStream As GZipStream = New GZipStream(source, CompressionMode.Decompress)
                Dim result(length - 1) As Byte
                Dim totalRead As Integer = 0
                While True
                    Dim num As Integer = decompressionStream.Read(result, totalRead, length - totalRead)
                    Dim bytesRead As Integer = num
                    If (num <= 0) Then
                        Exit While
                    End If
                    totalRead += bytesRead
                End While
                numArray = result
            End Using
        End Using
        Return numArray
    End Function

End Class