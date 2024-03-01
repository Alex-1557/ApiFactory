Imports ControlStub1
Imports Microsoft.VisualBasic.Devices
Imports Microsoft.VisualBasic

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Info
    Implements IInfo
    Public Function ComputerInfo() As ComputerInfo Implements IInfo.ComputerInfo
        Return My.Computer.Info
    End Function
    Public Function UserInfo() As (String, Boolean, Security.Principal.IPrincipal) Implements IInfo.UserInfo
        Return (My.User.Name, My.User.IsAuthenticated, My.User.CurrentPrincipal)
    End Function
    Public Function NetworkInfo() As Network Implements IInfo.NetworkInfo
        Return My.Computer.Network
    End Function
    Public Function GetProcesses() As Process() Implements IInfo.GetProcesses
        Return Process.GetProcesses()
    End Function
    Public Sub KillProcess(ProcessName As String) Implements IInfo.KillProcess
        For Each One In Process.GetProcessesByName(ProcessName)
            One.Kill()
        Next
    End Sub
End Class
