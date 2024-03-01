<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IRandom
    Function Caesar(ByVal source As String, ByVal shift As Int16) As String
    Function GetRandomInteger(MaxValue As Integer) As Integer
    Function CreateRandomPassword(Len As Integer, Optional FromChagCode As UInt32 = &H21, Optional ToCharCode As UInt32 = &H7E, Optional ExcludeChars As String = "<>'""") As String
End Interface
