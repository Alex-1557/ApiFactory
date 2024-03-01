Imports Microsoft.VisualBasic.Devices
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IInfo
    Function ComputerInfo() As ComputerInfo
    Function UserInfo() As (String, Boolean, Security.Principal.IPrincipal)
    Function NetworkInfo() As Network
    Function GetProcesses() As Process()
    Sub KillProcess(ProcessName As String)

End Interface
