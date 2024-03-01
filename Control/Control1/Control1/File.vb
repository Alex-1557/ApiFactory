Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports ControlStub1

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Snapshot
    Implements ISnapshot

    Public Property FileInfo As List(Of IOneFileInfo) Implements ISnapshot.FileInfo
    Public Property DirInfo As List(Of IOneDirInfo) Implements ISnapshot.DirInfo
    Public Sub New()
        FileInfo = New List(Of IOneFileInfo)
        DirInfo = New List(Of IOneDirInfo)
    End Sub

    Public Function ScanDir(ByVal StartDir As String) As Boolean Implements ISnapshot.ScanDir
        Dim Start As New System.IO.DirectoryInfo(StartDir)
        'В этой директории будут разобраны ВСЕ файлы и ВСЕ поддиректории
        Try
            For Each F As System.IO.FileInfo In Start.GetFiles
                Dim X As New OneFileInfo(F)
                FileInfo.Add(X)
                X = Nothing
            Next
            For Each D As System.IO.DirectoryInfo In Start.GetDirectories
                Dim X As New OneDirInfo(D)
                DirInfo.Add(X)
                X = Nothing
                'собственно рекурсия 
                If Not ScanDir(D.FullName) Then Return False
            Next
        Catch ex As Exception
            'например System Volume Information не читается - не делаем ничего
        End Try
        Return True
    End Function
End Class

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class OneDirInfo
    Implements IOneDirInfo
    Public Sub New(ByVal D As System.IO.DirectoryInfo)
        Dir = D.FullName
        CreationTime = D.CreationTime
        LastWriteTime = D.LastWriteTime
    End Sub
    Public Property Dir As String Implements IOneDirInfo.Dir
    Public Property CreationTime As DateTime Implements IOneDirInfo.CreationTime
    Public Property LastWriteTime As DateTime Implements IOneDirInfo.LastWriteTime

End Class

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class OneFileInfo
    Implements IOneFileInfo
    Public Sub New(ByVal F As System.IO.FileInfo)
        Dir = F.FullName
        FileName = F.Name
        CreationTime = F.CreationTime
        LastWriteTime = F.LastWriteTime
        Length = F.Length
        Hash = GetHash(F.FullName.ToString)
    End Sub

    Public Property Dir As String Implements IOneFileInfo.Dir
    Public Property FileName As String Implements IOneFileInfo.FileName
    Public Property CreationTime As DateTime Implements IOneFileInfo.CreationTime
    Public Property LastWriteTime As DateTime Implements IOneFileInfo.LastWriteTime
    Public Property Length As Long Implements IOneFileInfo.Length
    Public Property Hash As Byte() Implements IOneFileInfo.Hash

    ' Расчет MD5-хеша файла - 16 байт (или нули, если файл не открывается)
    Function GetHash(ByVal FileName As String) As Byte()    'SHA1 - 20 байт, но считается дольше
        Try
            Dim MD5 As System.Security.Cryptography.MD5 = Security.Cryptography.MD5.Create
            Dim FS As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            Return MD5.ComputeHash(FS)
            FS.Close()
        Catch ex As Exception
            'занято, открыть низзя
            Dim X(15) As Byte
            Return X
        End Try
    End Function
End Class

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Drive
    Implements IDrive
    Public Function Drives() As ReadOnlyCollection(Of DriveInfo) Implements IDrive.Drives
        Return My.Computer.FileSystem.Drives()
    End Function
    Public Function Drive(DriveName As String) As DriveInfo Implements IDrive.Drive
        Return My.Computer.FileSystem.GetDriveInfo(DriveName)
    End Function
End Class

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Dir
    Implements IDir
    Public Function DirExists(DirName As String) As Boolean Implements IDir.DirExists
        Return My.Computer.FileSystem.DirectoryExists(DirName)
    End Function
    Public Sub DeleteDirectory(DirName As String) Implements IDir.DeleteDirectory
        My.Computer.FileSystem.DeleteDirectory(DirName, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub
    Public Sub CreateDirectory(DirName As String) Implements IDir.CreateDirectory
        My.Computer.FileSystem.CreateDirectory(DirName)
    End Sub
    Public Function GetCurrentDirectory(DirName As String) As String Implements IDir.GetCurrentDirectory
        Return My.Computer.FileSystem.CurrentDirectory()
    End Function
    Public Function GetDirectories(DirName As String) As ReadOnlyCollection(Of String) Implements IDir.GetDirectories
        Return My.Computer.FileSystem.GetDirectories(DirName)
    End Function
    Public Function GetFiles(DirName As String) As ReadOnlyCollection(Of String) Implements IDir.GetFiles
        Return My.Computer.FileSystem.GetFiles(DirName)
    End Function
    Public Function GetDirectoryInfo(DirName As String) As DirectoryInfo Implements IDir.GetDirectoryInfo
        Return My.Computer.FileSystem.GetDirectoryInfo(DirName)
    End Function
    Public Sub CopyDirectory(SourceDirName As String, TargetDirName As String, Overwrite As Boolean) Implements IDir.CopyDirectory
        My.Computer.FileSystem.CopyDirectory(SourceDirName, TargetDirName, Overwrite)
    End Sub
    Public Sub MoveDirectory(SourceDirName As String, TargetDirName As String, Overwrite As Boolean) Implements IDir.MoveDirectory
        My.Computer.FileSystem.MoveDirectory(SourceDirName, TargetDirName, Overwrite)
    End Sub
    Public Sub SetDirTime(DirName As String, NewDatetime As DateTime) Implements IDir.SetDirTime
        IO.Directory.SetCreationTime(DirName, NewDatetime)
        IO.Directory.SetLastWriteTime(DirName, NewDatetime)
        IO.Directory.SetLastAccessTime(DirName, NewDatetime)
    End Sub
    Public Function GetDirTime(FileName As String) As (DateTime, DateTime, DateTime) Implements IDir.GetDirTime
        Return (IO.Directory.GetCreationTime(FileName), IO.Directory.GetLastWriteTime(FileName), IO.Directory.GetLastAccessTime(FileName))
    End Function
End Class

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class File
    Implements IFile

    Public Function FileExists(FileName As String) As Boolean Implements IFile.FileExists
        Return My.Computer.FileSystem.FileExists(FileName)
    End Function
    Public Function GetFileInfo(FileName As String) As FileInfo Implements IFile.GetFileInfo
        Return My.Computer.FileSystem.GetFileInfo(FileName)
    End Function
    Public Function ReadTxtFile(FileName As String) As String Implements IFile.ReadTxtFile
        Return My.Computer.FileSystem.ReadAllText(FileName)
    End Function
    Public Function ReadBinFile(FileName As String) As Byte() Implements IFile.ReadBinFile
        Return My.Computer.FileSystem.ReadAllBytes(FileName)
    End Function
    Public Function GetTempFileName(DirName As String) As String Implements IFile.GetTempFileName
        Return My.Computer.FileSystem.GetTempFileName
    End Function
    Public Sub DeleteFile(FileName As String) Implements IFile.DeleteFile
        My.Computer.FileSystem.DeleteFile(FileName)
    End Sub
    Public Sub CopyFile(SourceFileName As String, TargetFileName As String, Overwrite As Boolean) Implements IFile.CopyFile
        My.Computer.FileSystem.CopyFile(SourceFileName, TargetFileName, Overwrite)
    End Sub
    Public Sub MoveFile(SourceFileName As String, TargetFileName As String, Overwrite As Boolean) Implements IFile.MoveFile
        My.Computer.FileSystem.MoveFile(SourceFileName, TargetFileName, Overwrite)
    End Sub
    Public Sub WriteText(FileName As String, Text As String, Append As Boolean) Implements IFile.WriteText
        My.Computer.FileSystem.WriteAllText(FileName, Text, Append)
    End Sub
    Public Sub WriteBuf(FileName As String, Buf As Byte(), Append As Boolean) Implements IFile.WriteBuf
        My.Computer.FileSystem.WriteAllBytes(FileName, Buf, Append)
    End Sub
    Public Sub SetFileTime(FileName As String, NewDatetime As DateTime) Implements IFile.SetFileTime
        IO.File.SetCreationTime(FileName, NewDatetime)
        IO.File.SetLastWriteTime(FileName, NewDatetime)
        IO.File.SetLastAccessTime(FileName, NewDatetime)
    End Sub
    Public Function GetFileTime(FileName As String) As (DateTime, DateTime, DateTime) Implements IFile.GetFileTime
        Return (IO.File.GetCreationTime(FileName), IO.File.GetLastWriteTime(FileName), IO.File.GetLastAccessTime(FileName))
    End Function
    Public Function GetFileAttributes(FileName As String) As FileAttributes Implements IFile.GetFileAttributes
        Return IO.File.GetAttributes(FileName)
    End Function
    Public Function DeleteFileByKernel(FileName As Object) As Boolean Implements IFile.DeleteFileByKernel
        Return kernel32.DeleteFile(FileName)
    End Function

    Public Function DeleteFileByKernelAfterReboot(FileName As Object) As Boolean Implements IFile.DeleteFileByKernelAfterReboot
        Return kernel32.MoveFileEx(FileName, Nothing, MoveFileFlags.DelayUntilReboot)
    End Function
End Class