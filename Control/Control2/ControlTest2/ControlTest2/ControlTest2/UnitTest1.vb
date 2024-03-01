Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports IIS

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()
        Dim X As New IIS.IIS
        X.GetSiteCollection()
    End Sub

End Class