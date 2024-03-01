Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ControlStub1

<TestClass()>
Public Class File

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub Snap()
        Dim CmdComObjType = Type.GetTypeFromProgID("Control.Snap")
        Dim X As ISnapshot = Activator.CreateInstance(CmdComObjType)
        Dim Y = X.ScanDir("E:\Projects\License\Control\Control1\Control1")
        Console.WriteLine(X.FileInfo.ToPrettyString)
        Console.WriteLine(X.DirInfo.ToPrettyString)
    End Sub

    <TestMethod()>
    Public Sub Drive()
        Dim DrvComObjType = Type.GetTypeFromProgID("Control.Drive")
        Dim X As IDrive = Activator.CreateInstance(DrvComObjType)
        X.Drives.ToList.ForEach(Sub(Y As System.IO.DriveInfo) Console.WriteLine(Y))
    End Sub

    <TestMethod()>
    Public Sub GetDirTime()
        Dim DirComObjType = Type.GetTypeFromProgID("Control.Dir")
        Dim X As IDir = Activator.CreateInstance(DirComObjType)
        Console.WriteLine(X.GetDirTime("E:\Projects\License\Control\Control1\Control1\bin\Debug"))
    End Sub

    <TestMethod()>
    Public Sub FileOperations()
        Dim FileComObjType = Type.GetTypeFromProgID("Control.File")
        Dim X As IFile = Activator.CreateInstance(FileComObjType)
        Console.WriteLine(X.GetFileTime("E:\Projects\License\Control\Control1\Control1\bin\Debug\Control1.xml"))
        Console.WriteLine(X.GetFileAttributes("E:\Projects\License\Control\Control1\Control1\bin\Debug\Control1.xml"))
        Console.WriteLine(X.ReadTxtFile("E:\Projects\License\Control\Control1\Control1\bin\Debug\Control1.xml"))
    End Sub

End Class