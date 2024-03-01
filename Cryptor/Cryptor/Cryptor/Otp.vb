'https://github.com/kspearrin/Otp.NET
Imports OtpNet
Imports CryptorStub

<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class Otp
    Implements IOtp
    Friend Function TestTotp(TimerStep As Integer) As (String, Totp)
        Dim Key As Byte() = KeyGeneration.GenerateRandomKey(20)
        Dim base32String As String = Base32Encoding.ToString(Key)
        Dim base32Bytes As Byte() = Base32Encoding.ToBytes(base32String)
        Return (base32String, New Totp(base32Bytes, TimerStep))
    End Function
    Friend Function GetTotpObj(SecretBase32String As String, Optional TimerStep As Integer = 30) As Totp
        Dim base32Bytes As Byte() = Base32Encoding.ToBytes(SecretBase32String)
        Return New Totp(base32Bytes, TimerStep)
    End Function
    Public Function GetRandomKey(length As Integer) Implements IOtp.GetRandomKey
        Dim Key As Byte() = KeyGeneration.GenerateRandomKey(length)
        Return Base32Encoding.ToString(Key)
    End Function
    Public Function GetTimeBasedCode(SecretBase32String As String, Optional TimerStep As Integer = 30) As String Implements IOtp.GetTimeBasedCode
        Dim Totp = GetTotpObj(SecretBase32String, TimerStep)
        Return Totp.ComputeTotp(DateTime.UtcNow)
    End Function
    Public Function CheckPassword(SecretBase32String As String, Password As String, Optional TimerStep As Integer = 30) As Boolean Implements IOtp.CheckPassword
        Dim Totp = GetTotpObj(SecretBase32String, TimerStep)
        Return Password.Equals(Totp.ComputeTotp(DateTime.UtcNow))
    End Function

End Class
