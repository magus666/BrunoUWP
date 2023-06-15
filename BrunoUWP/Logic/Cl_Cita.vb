Public Class Cl_Cita

    Public Async Function InsertCita(CodigoCita As String,
                                     FechaHoraCita As Date,
                                     EstadoCita As Boolean,
                                     IdMascota As Integer,
                                     IdDimensionMascota As Integer,
                                     IdTipoServicio As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New CitaModel With {
                .Codigo_Cita = CodigoCita,
                .FechaHora_Cita = FechaHoraCita,
                .Estado_Cita = EstadoCita,
                .Id_Mascota = IdMascota,
                .Id_DimensionMascota = IdDimensionMascota,
                .Id_TipoServicio = IdTipoServicio
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultarCitas() As Task(Of List(Of CitaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetCita = Await ConexionDB.Table(Of CitaModel)().ToListAsync()
            Dim ListaCita = (From x In GetCita
                             Select x).ToList
            Return ListaCita
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
