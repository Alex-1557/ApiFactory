Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json

Module Dumper
    <Extension()>
    Function ToPrettyString(ByVal value As Object) As String
        Dim Setting As New JsonSerializerSettings With {.ReferenceLoopHandling = ReferenceLoopHandling.Ignore}
        Return JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented, Setting)
    End Function

    <Extension()>
    Function Dump(Of T)(ByVal value As T) As T
        Console.WriteLine(value.ToPrettyString())
        Return value
    End Function
End Module
