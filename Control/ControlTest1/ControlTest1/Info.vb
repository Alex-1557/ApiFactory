Imports Info
Imports System.Text
Imports System.Net
Imports Microsoft.VisualBasic.Logging
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports ControlStub1

<TestClass()>
Public Class TstInfo

    <TestInitialize>
    Public Sub Start()

    End Sub

    <TestMethod()>
    Public Sub ComputerInfo()
        Dim InfoComObjType = Type.GetTypeFromProgID("Control.Info")
        Dim X As IInfo = Activator.CreateInstance(InfoComObjType)
        Console.WriteLine(X.ComputerInfo().ToPrettyString)
    End Sub

    <TestMethod()>
    Public Sub NetworkInfo()
        Dim NetComObjType = Type.GetTypeFromProgID("Control.Info")
        Dim X As IInfo = Activator.CreateInstance(NetComObjType)
        Console.WriteLine(X.NetworkInfo().ToPrettyString)
    End Sub

    <TestMethod()>
    Public Sub UserInfo()
        Dim UserComObjType = Type.GetTypeFromProgID("Control.Info")
        Dim X As IInfo = Activator.CreateInstance(UserComObjType)
        Console.WriteLine(X.UserInfo().ToPrettyString)
    End Sub

    <TestMethod()>
    Public Sub GetProcesses()
        Dim ProcComObjType = Type.GetTypeFromProgID("Control.Info")
        Dim X As IInfo = Activator.CreateInstance(ProcComObjType)
        Console.WriteLine(X.GetProcesses().ToPrettyString)
    End Sub


End Class
