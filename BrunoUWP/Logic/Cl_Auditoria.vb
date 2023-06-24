Public Class Cl_Auditoria

    Public Async Function InsertDataBaseStamp(FechaHora As Date) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New AuditoriaModel With {
                .FechaHora_Auditoria = FechaHora
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
