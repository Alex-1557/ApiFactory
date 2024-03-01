Imports Microsoft.Web.Administration
<Runtime.InteropServices.ComVisible(True)>
<Runtime.InteropServices.Guid("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx")>
<Runtime.InteropServices.InterfaceType(Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)>
Public Interface IIIS
    Function GetSiteCollection() As SiteCollection
    Function GetSiteConfiguration(SiteName As String) As Configuration
    Function GetWorkerProces() As WorkerProcessCollection
    Function GetApplicationPools() As ApplicationPoolCollection

End Interface
