Imports DasMulli.Win32.ServiceUtils
Module Module1
    Public Sub Main(ByVal args As String())
        Dim Srv = New MyService()
        Dim ServiceHost = New Win32ServiceHost(Srv)
        ServiceHost.Run()
    End Sub
End Module
