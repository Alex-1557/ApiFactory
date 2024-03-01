Imports ControlStub2
Imports Microsoft.Web.Administration
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.AutoDispatch)>
<Runtime.InteropServices.ProgId("XXXXXXX.YYYYYY")>
Public Class IIS
    Implements IIIS
    Public Function GetApplicationPools() As ApplicationPoolCollection Implements IIIS.GetApplicationPools
        Dim SM = New ServerManager()
        Return SM.ApplicationPools()
    End Function
    Public Function GetSiteCollection() As SiteCollection Implements IIIS.GetSiteCollection
        Dim SM = New ServerManager()
        Return SM.Sites()
    End Function
    Public Function GetWorkerProces() As WorkerProcessCollection Implements IIIS.GetWorkerProces
        Dim SM = New ServerManager()
        Return SM.WorkerProcesses
    End Function
    Public Function GetSiteConfiguration(SiteName As String) As Configuration Implements IIIS.GetSiteConfiguration
        Dim SM = New ServerManager()
        Return SM.GetWebConfiguration(SiteName)
    End Function

End Class
