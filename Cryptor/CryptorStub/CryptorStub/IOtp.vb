<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IOtp
    Function GetRandomKey(length As Integer)
    Function GetTimeBasedCode(SecretBase32String As String, Optional TimerStep As Integer = 30) As String
    Function CheckPassword(SecretBase32String As String, Password As String, Optional TimerStep As Integer = 30) As Boolean
End Interface
