Imports WMIGatherer.Enums
Imports WMIGatherer.Gathering
Imports Microsoft.VisualBasic
'https://github.com/chrizator/WMIGatherer

Public Class Hardware

    Public ReadOnly Property GraphicsCards As ICollection(Of WMIGatherer.Objects.GraphicsCard)
    Public ReadOnly Property Processors As ICollection(Of WMIGatherer.Objects.Processor)
    Public ReadOnly Property RamSticks As ICollection(Of WMIGatherer.Objects.RamStick)
    Public ReadOnly Property HardDrives As ICollection(Of WMIGatherer.Objects.HardDrive)
    Public ReadOnly Property BaseBoard As WMIGatherer.Objects.BaseBoard
    Public ReadOnly Property TotalRam As ULong?
    Public ReadOnly Property Caption As String
    Public ReadOnly Property BootDevice As String
    Public ReadOnly Property MacAddress As String

    'HwidStrength.Light uses CPU ID
    'HwidStrength.Medium uses CPU ID + HDD Signature
    'HwidStrength..Strong uses CPU ID + HDD Signature + BIOS Serial Number + MAC Address
    Public ReadOnly Property Hwid As String = HardwareGatherer.GetHwid(HwidStrength.Strong)

    Public Sub New()
        Try
            GraphicsCards = HardwareGatherer.GetGraphicsCards()
        Catch ex As Exception

        End Try
        Try
            Processors = HardwareGatherer.GetProcessors()
        Catch ex As Exception

        End Try
        Try
            RamSticks = HardwareGatherer.GetRamSticks()
        Catch ex As Exception

        End Try
        Try
            BaseBoard = HardwareGatherer.GetBaseBoard
        Catch ex As Exception

        End Try
        Try
            HardDrives = HardwareGatherer.GetHardDrives
        Catch ex As Exception

        End Try
        Try
            TotalRam = HardwareGatherer.GetTotalMemoryCapacity()
        Catch ex As Exception

        End Try
        Try
            Hwid = HardwareGatherer.GetHwid(HwidStrength.Strong)
        Catch ex As Exception

        End Try
        Try
            Caption = OsGatherer.GetCaption()
        Catch ex As Exception

        End Try
        Try
            BootDevice = OsGatherer.GetBootDevice()
        Catch ex As Exception

        End Try
        Try
            MacAddress = NetGatherer.GetActiveMacAddress()
        Catch ex As Exception

        End Try
    End Sub

End Class







