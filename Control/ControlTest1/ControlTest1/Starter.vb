Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ControlStub1

<TestClass()>
Public Class Starter

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub Test()
        Dim CmdComObjType = Type.GetTypeFromProgID("Control.Cmd")
        Dim X As IStarter = Activator.CreateInstance(CmdComObjType)
        Console.WriteLine(X.Cmd("dir c:\"))
    End Sub


End Class
