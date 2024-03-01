Module Module1

    Public Sub Main()
        Dim Wmi As New Hardware
        Console.WriteLine("HardDrives:")
        For Each One In Wmi.HardDrives
            Console.WriteLine($"{One.Signature} {One.Capacity} {One.Model} {One.Caption} ")
        Next
        Console.WriteLine("GraphicsCards:")
        For Each One In Wmi.GraphicsCards
            Console.WriteLine($"{One.Name} {One.MemoryCapacity} {One.Description} {One.Caption} ")
        Next
        Console.WriteLine("RamSticks:")
        For Each One In Wmi.RamSticks
            Console.WriteLine($"{One.SerialNumber} {One.Manufacturer} {One.Capacity} {One.PositionInRow} {One.ClockSpeed}")
        Next
        Console.WriteLine("Processors:")
        For Each One In Wmi.Processors
            Console.WriteLine($"{One.Id} {One.Manufacturer} {One.NumberOfCores} {One.Name} {One.Voltage}")
        Next
        Console.WriteLine("ActiveMacAddress:")
        Console.WriteLine($"{Wmi.MacAddress}")
        Console.WriteLine("BaseBoard:")
        Console.WriteLine($"{Wmi.BaseBoard.Name} {Wmi.BaseBoard.Product} {Wmi.BaseBoard.Manufacturer} {Wmi.BaseBoard.Model} {Wmi.BaseBoard.SerialNumber} ")
        Console.WriteLine("TotalRam:")
        Console.WriteLine($"{Wmi.TotalRam}")
        Console.WriteLine("Caption:")
        Console.WriteLine($"{Wmi.Caption}")
        Console.WriteLine("BootDevice:")
        Console.WriteLine($"{Wmi.BootDevice}")
        Console.WriteLine("Hwid:")
        Console.WriteLine($"{Wmi.Hwid}")
        Console.ReadLine()
    End Sub
End Module
