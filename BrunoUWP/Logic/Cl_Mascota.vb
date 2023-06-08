Imports Windows.Perception

Public Class Cl_Mascota

    Public Async Function InsertarMascota(IdTipoMascota As Integer,
                                          IdRazaMascota As Integer,
                                          NombreMascota As String,
                                          EdadMascota As Integer,
                                          IdPersona As Integer,
                                          ObservacionesMenscota As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Mascota = New MascotaModel With {
                .Id_TipoMascota = IdTipoMascota,
                .Id_Raza = IdRazaMascota,
                .Nombre_Mascota = NombreMascota,
                .Edad_Mascota = EdadMascota,
                .Id_Persona = IdPersona,
                .Observaciones_Mascota = ObservacionesMenscota,
                .FechaRegistro_Mascota = Date.Now
            }
            Dim Id = Await ConexionDB.InsertAsync(Mascota)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaMascotas() As Task(Of List(Of MascotaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim ListaMascota = (From x In GetMascota
                                Select x).ToList
            Return ListaMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotas() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().CountAsync()
            Return GetMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimoDia() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddDays(-1)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimaSemana() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddDays(-7)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimoMes() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddMonths(-1)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
