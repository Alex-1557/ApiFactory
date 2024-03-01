Imports Microsoft.Win32
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IRegistry
    ReadOnly Property SoftwareName As String
    Event Err(Msg As String)
    Event FirstStart()
    Sub SetValue(Name As String, Value As Boolean)
    Sub SetValue(Name As String, Value() As Byte)
    Sub SetValue(Name As String, Value As Date)
    Sub SetValue(Name As String, Value As Decimal)
    Sub SetValue(Name As String, Value As Object, RegistryValueKind As RegistryValueKind)
    Sub SetValue(Name As String, Value As String)
    Sub SetValue(Name As String, Value As UInteger)
    Sub SetValue(Name As String, Value As ULong)
    Function GetValue(Of T)(Name As String) As Object
End Interface
