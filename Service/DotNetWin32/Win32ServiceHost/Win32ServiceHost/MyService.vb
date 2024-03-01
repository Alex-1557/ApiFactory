
Imports DasMulli.Win32.ServiceUtils

Public Class MyService
    Implements IWin32Service

    Public ReadOnly Property ServiceName As String Implements IWin32Service.ServiceName
        Get
            Return "Test Service"
        End Get
    End Property

    Public Sub Start(ByVal startupArguments As String(), ByVal serviceStoppedCallback As ServiceStoppedCallback) Implements IWin32Service.Start
    End Sub

    Public Sub [Stop]() Implements IWin32Service.Stop
    End Sub

End Class
