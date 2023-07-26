Imports SQLite

Public Class MascotaModel

    <PrimaryKey, AutoIncrement>
    Public Property Id_Mascota As Integer
    Public Property Codigo_Mascota As String
    Public Property Nombre_Mascota As String
    Public Property Edad_Mascota As Integer
    Public Property Observaciones_Mascota As String
    Public Property FechaRegistro_Mascota As Date
    Public Property Estado_Mascota As Boolean
    Public Property Id_Persona As Integer
    Public Property Id_TipoMascota As Integer
    Public Property Id_Raza As Integer

End Class
