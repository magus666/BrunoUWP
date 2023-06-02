Imports SQLite

Public Class MascotaModel

    <PrimaryKey, AutoIncrement>
    Public Property Id_Mascota As Integer
    Public Property Tipo_Mascota As Integer
    Public Property Raza_Mascota As Integer
    Public Property Nombre_Mascota As String
    Public Property Edad_Mascota As Integer
    Public Property Propietario_Mascota As Integer
    Public Property Observaciones_Mascota As String
    Public Property FechaRegistro_Mascota As Date

End Class
