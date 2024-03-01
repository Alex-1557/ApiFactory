Imports System.Collections.ObjectModel
Imports System.IO

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface ISnapshot
    Property FileInfo As List(Of IOneFileInfo)
    Property DirInfo As List(Of IOneDirInfo)
    Function ScanDir(ByVal StartDir As String) As Boolean

End Interface

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IOneFileInfo
    Property Dir As String
    Property FileName As String
    Property CreationTime As DateTime
    Property LastWriteTime As DateTime
    Property Length As Long
    Property Hash As Byte()
End Interface

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IOneDirInfo
    Property Dir As String
    Property CreationTime As DateTime
    Property LastWriteTime As DateTime
End Interface

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IDrive
    Function Drives() As ReadOnlyCollection(Of DriveInfo)
    Function Drive(DriveName As String) As DriveInfo
End Interface

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IDir
    Function DirExists(DirName As String) As Boolean
    Sub DeleteDirectory(DirName As String)
    Sub CreateDirectory(DirName As String)
    Function GetCurrentDirectory(DirName As String) As String
    Function GetDirectories(DirName As String) As ReadOnlyCollection(Of String)
    Function GetFiles(DirName As String) As ReadOnlyCollection(Of String)
    Function GetDirectoryInfo(DirName As String) As DirectoryInfo
    Sub CopyDirectory(SourceDirName As String, TargetDirName As String, Overwrite As Boolean)
    Sub MoveDirectory(SourceDirName As String, TargetDirName As String, Overwrite As Boolean)
    Sub SetDirTime(DirName As String, NewDatetime As DateTime)
    Function GetDirTime(FileName As String) As (DateTime, DateTime, DateTime)

End Interface

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IFile
    Function FileExists(FileName As String) As Boolean
    Function GetFileInfo(FileName As String) As FileInfo
    Function ReadTxtFile(FileName As String) As String
    Function ReadBinFile(FileName As String) As Byte()
    Function GetTempFileName(DirName As String) As String
    Sub DeleteFile(FileName As String)
    Sub CopyFile(SourceFileName As String, TargetFileName As String, Overwrite As Boolean)
    Sub MoveFile(SourceFileName As String, TargetFileName As String, Overwrite As Boolean)
    Sub WriteText(FileName As String, Text As String, Append As Boolean)
    Sub WriteBuf(FileName As String, Buf As Byte(), Append As Boolean)
    Sub SetFileTime(FileName As String, NewDatetime As DateTime)
    Function GetFileTime(FileName As String) As (DateTime, DateTime, DateTime)
    Function GetFileAttributes(FileName As String) As FileAttributes
    Function DeleteFileByKernel(FileName) As Boolean
    Function DeleteFileByKernelAfterReboot(FileName) As Boolean
End Interface
