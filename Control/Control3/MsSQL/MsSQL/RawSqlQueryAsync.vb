Imports System.Data
Imports System.Data.Common
Imports System.Threading
Imports BackendAPI.Model
Imports Microsoft.EntityFrameworkCore
Imports MySqlConnector

Namespace Helper
    Public Module RawSqlQueryAsync
        <Runtime.CompilerServices.Extension>
        Public Async Function RawSqlQueryAsync(Of T)(Context As ApplicationDbContext, ByVal SqlQuery As String, ByVal RowMapperFunc As Func(Of DbDataReader, T)) As Task(Of Tuple(Of List(Of T), Exception))
            Try
                Dim EF_CN As DbConnection = Context.Database.GetDbConnection()
                Using CN = New MySqlConnection(EF_CN.ConnectionString)
                    Await CN.OpenAsync

                    Using Command = CN.CreateCommand()
                        Command.CommandText = SqlQuery
                        Command.CommandType = CommandType.Text

                        Using RDR = Await Command.ExecuteReaderAsync
                            Dim ResultList = New List(Of T)()

                            While RDR.Read()
                                ResultList.Add(RowMapperFunc(RDR))
                            End While

                            RDR.Close()
                            Return New Tuple(Of List(Of T), Exception)(ResultList, Nothing)
                        End Using
                    End Using
                End Using
            Catch ex As Exception
                'Debug only, because this function show password in AES_DECRYPT()
                Debug.WriteLine(ex.Message & " : " & SqlQuery)
                Return New Tuple(Of List(Of T), Exception)(Nothing, ex)
            End Try
        End Function
    End Module

End Namespace
