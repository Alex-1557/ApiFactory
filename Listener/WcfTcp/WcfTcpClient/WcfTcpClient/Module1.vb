'https://learn.microsoft.com/en-us/dotnet/framework/wcf/samples/net-tcp-port-sharing-sample

Imports System.ServiceModel
Imports TcpService

Module Module1

    Public Sub Main(ByVal args As String())
        Dim salt As UShort = 500
        Dim address As String = $"net.tcp://localhost:9000/calculator/{salt}"
        Dim factory As ChannelFactory(Of ICalculator) = New ChannelFactory(Of ICalculator)(New NetTcpBinding())
        AddHandler factory.Closed, AddressOf ClosedHandler
        AddHandler factory.Opened, AddressOf OpenedHandler
        Dim proxy As ICalculator = factory.CreateChannel(New EndpointAddress(address))

        Do While (True)
            ' Call the Add service operation
            Dim value1 As Double = 100.0R
            Dim value2 As Double = 15.99R
            Dim result As Double = proxy.Adding(value1, value2)
            Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result)
            'Call the Subtract service operation.
            value1 = 145.0R
            value2 = 76.54R
            result = proxy.Subtracting(value1, value2)
            Console.WriteLine("Subtract({0},{1}) = {2}", value1, value2, result)
            'Call the Multiply service operation.
            value1 = 9.0R
            value2 = 81.25R
            result = proxy.Multiplying(value1, value2)
            Console.WriteLine("Multiply({0},{1}) = {2}", value1, value2, result)
            'Call the Divide service operation.
            value1 = 22.0R
            value2 = 7.0R
            result = proxy.Dividing(value1, value2)
            Console.WriteLine("Divide({0},{1}) = {2}", value1, value2, result)
            Console.WriteLine("Press Enter to require server again")
            Console.ReadLine()
        Loop
        factory.Close()
    End Sub

    Public Sub OpenedHandler(sender As Object, e As EventArgs)
        Dim Factory = CType(sender, ChannelFactory(Of ICalculator))
        Console.WriteLine("Opened")
    End Sub
    Public Sub ClosedHandler(sender As Object, e As EventArgs)
        Console.WriteLine("Closed")
    End Sub

End Module
