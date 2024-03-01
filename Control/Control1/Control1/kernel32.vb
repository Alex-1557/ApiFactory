Imports System.Runtime.InteropServices


<Flags>
Public Enum MoveFileFlags
    None = 0
    ReplaceExisting = 1
    CopyAllowed = 2
    DelayUntilReboot = 4
    WriteThrough = 8
    CreateHardlink = 16
    FailIfNotTrackable = 32
End Enum

Public Module kernel32
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Function MoveFileEx(ByVal lpExistingFileName As String, ByVal lpNewFileName As String, ByVal dwFlags As MoveFileFlags) As Boolean
    End Function


    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Function DeleteFile(ByVal lpFileName As String) As Boolean

    End Function
End Module
