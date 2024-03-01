Imports ControlStub1
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Starter
    Implements IStarter
    Public Function Cmd(Args As String) As String Implements IStarter.Cmd
        Dim Prm As ProcessStartInfo = New ProcessStartInfo()
        Prm.FileName = "cmd.exe"
        Prm.Arguments = $"/C {Args}"
        Prm.RedirectStandardOutput = True
        Prm.RedirectStandardError = True
        Prm.UseShellExecute = False
        Prm.CreateNoWindow = True
        Dim Start As Process = New Process()
        Start.StartInfo = Prm
        Start.EnableRaisingEvents = True
        Try
            Start.Start()
            'Start.WaitForExit()
            Return Start.StandardOutput.ReadToEnd()
        Catch e As Exception
            Return e.Message
        End Try
    End Function
End Class