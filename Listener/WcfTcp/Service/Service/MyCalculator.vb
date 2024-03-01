Public Class MyCalculator
    Implements ICalculator

    Public Function Adding(ByVal x As Integer, ByVal y As Integer) As Integer Implements ICalculator.Adding
        Return x + y
    End Function

    Public Function Subtracting(ByVal x As Integer, ByVal y As Integer) As Integer Implements ICalculator.Subtracting
        Return x - y
    End Function

    Public Function Multiplying(ByVal x As Integer, ByVal y As Integer) As Integer Implements ICalculator.Multiplying
        Dim result As Integer = 0
        Dim isNegative As Boolean = False

        If y < 0 Then
            y = -y
            isNegative = True
        End If

        For i As Integer = 0 To y - 1
            result += x
        Next

        Return If(isNegative, -result, result)
    End Function

    Public Function Dividing(ByVal x As Integer, ByVal y As Integer) As Integer Implements ICalculator.Dividing
        If y = 0 Then
            Throw New ArgumentException("Cannot divide by zero")
        End If

        Dim quotient As Integer = 0
        Dim isNegative As Boolean = False

        If x < 0 Then
            x = -x
            isNegative = Not isNegative
        End If

        If y < 0 Then
            y = -y
            isNegative = Not isNegative
        End If

        While x >= y
            x -= y
            quotient += 1
        End While

        Return If(isNegative, -quotient, quotient)
    End Function
End Class
