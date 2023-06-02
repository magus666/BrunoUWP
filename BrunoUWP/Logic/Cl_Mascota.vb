﻿Imports Windows.Perception

Public Class Cl_Mascota

    Public Async Function InsertarMascota(TipoMascota As Integer,
                                          RazaMascota As Integer,
                                          NombreMascota As String,
                                          EdadMascota As Integer,
                                          PropietarioMascota As Integer,
                                          ObservacionesMenscota As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cliente = New MascotaModel With {
                .Tipo_Mascota = TipoMascota,
                .Raza_Mascota = RazaMascota,
                .Nombre_Mascota = NombreMascota,
                .Edad_Mascota = EdadMascota,
                .Propietario_Mascota = PropietarioMascota,
                .Observaciones_Mascota = ObservacionesMenscota,
                .FechaRegistro_Mascota = Date.Now
            }
            Dim Id = Await ConexionDB.InsertAsync(Cliente)
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
