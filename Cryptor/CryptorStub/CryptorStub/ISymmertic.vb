<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface ISymmertic
    Function EnCryptString(ByVal GenerateNewKeyAndIV As Boolean, ByVal InputStr As String, ByRef Key As Byte(), ByRef IV As Byte()) As String
    Function EnCryptArray(ByVal GenerateNewKeyAndIV As Boolean, ByVal InputArr As Byte(), ByRef Key As Byte(), ByRef IV As Byte()) As Byte()
    Function DeCryptString(ByVal Key As Byte(), ByVal IV As Byte(), ByVal InputStr As String) As String
    Function DeCryptArray(ByVal Key As Byte(), ByVal IV As Byte(), ByVal InputArr() As Byte) As Byte()
    Function ByteKeyToString(ByVal Key As Byte()) As String
    Function StringToByteKey(ByVal Key As String) As Byte()
    Function PaddingArray16(ByVal InputArr As Byte()) As Byte()
End Interface
