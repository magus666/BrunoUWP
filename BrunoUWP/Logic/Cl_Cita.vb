Public Class Cl_Cita

    Public Async Function InsertCita(CodigoCita As String,
                                     FechaHoraInicioCita As Date,
                                     FechaHoraFinCita As Date,
                                     EstadoCita As Boolean,
                                     IdMascota As Integer,
                                     IdDimensionMascota As Integer,
                                     IdTipoServicio As Integer,
                                     IdTipoTransaccion As Integer,
                                     EstadoVentaCita As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New CitaModel With {
                .Codigo_Cita = CodigoCita,
                .FechaHoraInicio_Cita = FechaHoraInicioCita,
                .FechaHoraFin_Cita = FechaHoraFinCita,
                .Estado_Cita = EstadoCita,
                .Id_Mascota = IdMascota,
                .Id_DimensionMascota = IdDimensionMascota,
                .Id_TipoServicio = IdTipoServicio,
                .Id_TipoTransaccion = IdTipoTransaccion,
                .EstadoVenta_Cita = EstadoVentaCita
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

    Public Async Function ContadorCitasPorFecha(Fecha As Date) As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCita = Await ConexionDB.Table(Of CitaModel)().ToListAsync()
            Dim ListaCita = (From x In GetCita
                             Where x.FechaHoraInicio_Cita.ToShortDateString = Fecha.ToShortDateString
                             Select x).Count
            Return ListaCita
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Async Function ContadorCitasPendientes() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCita = Await ConexionDB.Table(Of CitaModel)().ToListAsync()
            Dim ListaCita = (From x In GetCita
                             Where x.Estado_Cita = False
                             Select x).Count
            Return ListaCita
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ActualizarCita(IdCita As Integer,
                                         EstadoVenta As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetCita = Await ConexionDB.Table(Of CitaModel)().ToListAsync()
            Dim Cita = (From x In GetCita
                        Where x.Id_Cita = IdCita
                        Select x).FirstOrDefault
            If Cita IsNot Nothing Then
                Cita.EstadoVenta_Cita = EstadoVenta
                Await ConexionDB.UpdateAsync(Cita)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ActualizarEstadoCita(CodigoCita As String,
                                               EstadoCita As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetCita = Await ConexionDB.Table(Of CitaModel)().ToListAsync()
            Dim Cita = (From x In GetCita
                        Where x.Codigo_Cita = CodigoCita
                        Select x).FirstOrDefault
            If Cita IsNot Nothing Then
                Cita.Estado_Cita = EstadoCita
                Await ConexionDB.UpdateAsync(Cita)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
