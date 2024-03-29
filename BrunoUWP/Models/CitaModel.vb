﻿Imports SQLite

Public Class CitaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Cita As Integer
    Public Property Codigo_Cita As String
    Public Property FechaHoraInicio_Cita As Date
    Public Property FechaHoraFin_Cita As Date
    Public Property Estado_Cita As Boolean
    Public Property Id_TipoServicio As Integer
    Public Property Id_Mascota As Integer
    Public Property Id_DimensionMascota As Integer
    Public Property Id_TipoTransaccion As Integer
    Public Property EstadoVenta_Cita As Boolean
    Public Property EsVisible As Boolean

End Class
