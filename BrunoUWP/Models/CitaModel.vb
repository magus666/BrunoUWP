Imports SQLite

Public Class CitaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Cita As Integer
    Public Property Codigo_Cita As String
    Public Property FechaHora_Cita As Date
    Public Property Estado_Cita As Boolean
    Public Property Id_TipoServicio As Integer
    Public Property Id_Mascota As Integer

End Class
