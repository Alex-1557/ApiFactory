Imports System.IO
Imports System.Net
Imports System.Net.Sockets

Module Module1

    Dim listener As Sockets.TcpListener
    Dim listen As Boolean = True
    Dim IP As IPAddress = IPAddress.Parse("127.0.0.1")

    Public Sub Main()
        MainAsync.wait
    End Sub
    Private Async Function MainAsync() As Task


        listener = New Sockets.TcpListener(IP, 500)
        listener.Start()

        While listen
            If listener.Pending() Then
                Await HandleClient(Await listener.AcceptTcpClientAsync())
            Else
                Await Task.Delay(100)
            End If
        End While
    End Function

    Private Async Function HandleClient(ByVal clt As TcpClient) As Task
        Using ns As NetworkStream = clt.GetStream()
            Using sr As StreamReader = New StreamReader(ns)
                Dim msg As String = Await sr.ReadToEndAsync()
                Console.WriteLine($"Received new message ({msg.Length} bytes): {msg}")
            End Using
        End Using
    End Function


End Module
