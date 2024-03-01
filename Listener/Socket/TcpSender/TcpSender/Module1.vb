Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Microsoft.SqlServer

Module Module1

    Dim IP As IPAddress = IPAddress.Parse("127.0.0.1")
    Public Sub Main()
        Dim TCP As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Dim RemoteEP As IPEndPoint = New IPEndPoint(IP, 500)

        TCP.Connect(RemoteEP)
        TCP.Send(UTF8Encoding.UTF8.GetBytes("Hello"))
        TCP.Disconnect(False)

    End Sub


End Module
