Imports System.Data.Common
Imports BackendAPI.Model
Imports Microsoft.EntityFrameworkCore
Imports MySqlConnector
Imports MySQL.Data.EntityFrameworkCore.Extensions
Imports Microsoft.Extensions.Options
Imports Pomelo.EntityFrameworkCore
Imports System.Threading

Namespace Helper
    Public Module Sql

        Dim ConnectionCount As Integer

        <Runtime.CompilerServices.Extension>
        Public Function ReOpen(_DB As ApplicationDbContext) As ApplicationDbContext
            If _DB.Database.GetDbConnection.State = _DB.Database.GetDbConnection.State.Closed Then
                Try
                    _DB.Database.GetDbConnection.Open()
                    Return _DB
                Catch ex As System.ObjectDisposedException
                    _DB.Database.GetDbConnection.Dispose()
                    Interlocked.Increment(ConnectionCount)
                    Debug.WriteLine($"Creating new EF Core connection {ConnectionCount}")
                    _DB = New ApplicationDbContext(_DB._Options)
                    If Not ReOpenMySQL(_DB.Database.GetDbConnection) Then
                        Throw New Exception($"MySqlConnection can not open {_DB.Database.GetDbConnection.ConnectionString.Substring(0, _DB.Database.GetDbConnection.ConnectionString.IndexOf("Password=") + 9) & "***********"}")
                    End If
                End Try
            ElseIf _DB.Database.GetDbConnection.State = _DB.Database.GetDbConnection.State.Open Then
                _DB.Database.GetDbConnection.Close()
                '_DB.Database.GetDbConnection.Dispose()
                _DB.Database.GetDbConnection.Open()
            End If
            Return _DB
        End Function

        <Runtime.CompilerServices.Extension>
        Public Function ExecNonQuery(_DB As ApplicationDbContext, SQL As String, Optional Transaction As Data.Common.DbTransaction = Nothing) As Integer
            Dim CMD1 = _DB.Database.GetDbConnection().CreateCommand()
            CMD1.CommandText = SQL
            If Transaction IsNot Nothing Then
                CMD1.Transaction = Transaction
            End If
            Return CMD1.ExecuteNonQuery()
        End Function

        Public Function ExecRDR(Of T)(_DB As ApplicationDbContext, SQL As String, RowMapperFunc As Func(Of DbDataReader, T), Optional Transaction As Data.Common.DbTransaction = Nothing) As List(Of T)
            Dim Ret1 As New List(Of T)
            Dim CMD1 = _DB.Database.GetDbConnection().CreateCommand()
            CMD1.CommandText = SQL
            If Transaction IsNot Nothing Then
                CMD1.Transaction = Transaction
            End If
            Dim RDR1 = CMD1.ExecuteReader
            While RDR1.Read
                Dim X = RowMapperFunc(RDR1)
                Ret1.Add(X)
            End While
            RDR1.Close()
            Return Ret1
        End Function

        Public Async Function ExecNonQueryAsync(_DB As ApplicationDbContext, SQL As String, Optional Transaction As Data.Common.DbTransaction = Nothing) As Task(Of Integer)
            Try
                Dim EF_CN As DbConnection = _DB.Database.GetDbConnection()
                Using CN = New MySqlConnection(EF_CN.ConnectionString)
                    Await CN.OpenAsync
                    Using CMD = CN.CreateCommand
                        CMD.CommandText = SQL
                        If Transaction IsNot Nothing Then
                            CMD.Transaction = Transaction
                        End If
                        Dim Ret = CMD.ExecuteNonQueryAsync
                        Await Ret
                        Return Ret.Result
                    End Using
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.Message & vbCrLf & SQL)
            End Try
        End Function

        Public Async Function ExecNonQueryAsync(CN As MySqlConnection, SQL As String) As Task(Of Integer)
            Try
                Using CMD = CN.CreateCommand
                    CMD.CommandText = SQL
                    Dim Ret = CMD.ExecuteNonQueryAsync
                    Await Ret
                    Return Ret.Result
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.Message & vbCrLf & SQL)
            End Try
        End Function

        Public Function ExecRDR(Of T)(CN As MySqlConnection, SQL As String, RowMapperFunc As Func(Of DbDataReader, T), Optional Transaction As Data.Common.DbTransaction = Nothing) As List(Of T)
            Dim Ret1 As New List(Of T)
            ReOpenMySQL(CN)
            Dim CMD1 = CN.CreateCommand()
            CMD1.CommandText = SQL
            If Transaction IsNot Nothing Then
                CMD1.Transaction = Transaction
            End If
            Dim RDR1 = CMD1.ExecuteReader
            While RDR1.Read
                Dim X = RowMapperFunc(RDR1)
                Ret1.Add(X)
            End While
            RDR1.Close()
            Return Ret1
        End Function

        Public Function ReOpenMySQL(ByRef CN As MySqlConnection) As Boolean
            If CN Is Nothing Then
                CN = New MySqlConnection(CN.ConnectionString)
Open:
                Try
                    CN.Open()
                    If CN.State = Data.ConnectionState.Open Then
                        Dim CMD1 = CN.CreateCommand()
                        CMD1.CommandText = "SELECT count(*) as Count  FROM cryptochestmax.GetProcessList;"
                        Dim RDR1 = CMD1.ExecuteReader
                        RDR1.Read()
                        Dim Count As Integer = RDR1("Count")
                        RDR1.Close()
                        Debug.Print($"Found {Count} MySQL connection.")
                        Return True
                    Else
                        Return False
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message & vbCrLf & CN.ConnectionString)
                    Return False
                End Try
            Else
                If CN.State = Data.ConnectionState.Open Then
                    Return True
                Else
                    GoTo Open
                End If
            End If
        End Function

    End Module
End Namespace