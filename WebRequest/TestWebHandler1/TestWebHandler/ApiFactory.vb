Module ApiFactory
    Public Function GetReference(Of T)(ApiName As String, Shift As Integer, Optional Parameters As Object = Nothing) As T
        Dim RegComObjType = Type.GetTypeFromProgID(Encoding.UTF8.GetString(Convert.FromBase64String(ApiName)).Caesar(-Shift))
        If Parameters Is Nothing Then
            Return Activator.CreateInstance(RegComObjType)
        Else
            Return Activator.CreateInstance(RegComObjType, Parameters)
        End If
    End Function
End Module
