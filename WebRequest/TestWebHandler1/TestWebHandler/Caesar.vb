Imports System.Runtime.CompilerServices

Module Caesar
    <Extension()>
    Public Function Caesar(ByVal source As String, ByVal shift As Int16) As String
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
End Module
