<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IGzip
    Function Compress(ByVal input As String) As String
    Function Compress(ByVal input As Byte()) As Byte()
    Function Decompress(ByVal input As String) As String
    Function Decompress(ByVal input As Byte()) As Byte()

End Interface
