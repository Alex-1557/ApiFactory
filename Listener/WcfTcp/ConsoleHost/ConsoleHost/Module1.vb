Imports System.ServiceModel
Imports TcpService
'https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-the-net-tcp-port-sharing-service
Module Module1

    Public Sub Main()
        Dim binding As NetTcpBinding = New NetTcpBinding()
        binding.PortSharingEnabled = True
        Dim host As ServiceHost = New ServiceHost(GetType(MyCalculator))
        Dim address As String = $"net.tcp://localhost:9000/calculator/500"
        host.AddServiceEndpoint(GetType(ICalculator), binding, address)
        'Error: The service endpoint failed to listen on the URI 'net.tcp://localhost:9000/calculator/500' because access was denied.
        'Verify that the current user is granted access in the appropriate allowAccounts section of SMSvcHost.exe.config.'
        'https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-the-net-tcp-port-sharing-service
        AddHandler host.Closed, AddressOf ClosedHandler
        AddHandler host.Opened, AddressOf OpenedHandler
        host.Open()
        Console.WriteLine($"{host.State} {host.Description.ToPrettyString}")
        Console.ReadLine()
        'workin as admin
    End Sub

    Public Sub OpenedHandler(sender As Object, e As EventArgs)
        Dim Host = CType(sender, System.ServiceModel.ServiceHost)
        Console.WriteLine("Opened")
    End Sub
    Public Sub ClosedHandler(sender As Object, e As EventArgs)
        Console.WriteLine("Closed")
    End Sub
End Module
