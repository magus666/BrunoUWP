Imports SQLite

Public Class PersonaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Persona As Integer
    Public Property Documento_Persona As String
    Public Property Nombre_Persona As String
    Public Property NombreCompleto_Persona As String
    Public Property Apellido_Persona As String
    Public Property Edad_Persona As Integer
    Public Property Sexo_Persona As String
    Public Property FechaCreacion_Persona As Date

End Class
