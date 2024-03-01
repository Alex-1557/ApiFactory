Imports CryptorStub
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Registry
    Implements IRegistry

    Public Event FirstStart() Implements IRegistry.FirstStart
    Public Event Err(Msg As String) Implements IRegistry.Err
    Public Property SoftwareName As String Implements IRegistry.SoftwareName
    Public Sub New()

    End Sub
    Public Sub New(SoftwareName1 As String)
        SoftwareName = SoftwareName1
        Try
            Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
                If CurrentKey Is Nothing Then
                    Using SOFTWARE As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE", True)
                        Using MyKey As Microsoft.Win32.RegistryKey = SOFTWARE.CreateSubKey(SoftwareName)
                            RaiseEvent FirstStart()
                        End Using
                    End Using
                End If
            End Using
        Catch ex As Exception
            RaiseEvent Err(ex.Message)
        End Try
    End Sub
    Public Sub SetValue(Name As String, Value As Object, RegistryValueKind As Microsoft.Win32.RegistryValueKind) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, RegistryValueKind)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As String) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As Byte()) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.Binary)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As UInteger) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.DWord)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As ULong) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.QWord)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As Date) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As Boolean) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Sub SetValue(Name As String, Value As Decimal) Implements IRegistry.SetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub
    Public Function GetValue(Of T)(Name As String) As Object Implements IRegistry.GetValue
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                Dim ValueType As Microsoft.Win32.RegistryValueKind
                Dim Ret As Object = CurrentKey.GetValue(Name, Nothing, ValueType)
                If Ret IsNot Nothing Then
                    If GetType(T) = GetType(String) Then
                        Return Ret.ToString
                    ElseIf GetType(T) = GetType(Decimal) Then
                        Return Decimal.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Boolean) Then
                        Return Boolean.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Date) Then
                        Return Date.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Byte()) Then
                        Return TryCast(Ret, Byte())
                    ElseIf GetType(T) = GetType(UInteger) Then
                        Return DirectCast(Ret, UInteger)
                    ElseIf GetType(T) = GetType(ULong) Then
                        Return Convert.ToUInt64(Ret)
                    Else
                        Return Ret
                    End If
                End If
            End If
        End Using
    End Function
End Class
