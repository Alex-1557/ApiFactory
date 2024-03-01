
Imports System.ServiceModel

<ServiceContract>
Public Interface ICalculator
    <OperationContract()>
    Function Adding(ByVal x As Integer, ByVal y As Integer) As Integer
    <OperationContract()>
    Function Subtracting(ByVal x As Integer, ByVal y As Integer) As Integer
    <OperationContract()>
    Function Multiplying(ByVal x As Integer, ByVal y As Integer) As Integer
    <OperationContract()>
    Function Dividing(ByVal x As Integer, ByVal y As Integer) As Integer
End Interface
