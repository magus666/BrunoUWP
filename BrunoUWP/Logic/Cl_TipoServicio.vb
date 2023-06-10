Public Class Cl_TipoServicio

    Public Async Function InsertTipoServicio(NombreTipoServicio As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New TipoServicioModel With {
                .Nombre_TipoServicio = NombreTipoServicio
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaTipoServicio() As Task(Of List(Of TipoServicioModel))
        Try
            Await ConfiguraSqlite()
            Dim GetTipoServicio = Await ConexionDB.Table(Of TipoServicioModel)().ToListAsync()
            Dim ListaTipoServicio = (From x In GetTipoServicio
                                     Select x).ToList
            Return ListaTipoServicio
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
