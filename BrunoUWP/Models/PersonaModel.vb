
Imports SQLite

Public Class PersonaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Persona As Integer
    Public Property Documento_Persona As String
    Public Property Nombre_Persona As String
    Public Property Apellido_Persona As String
    Public Property NombreCompleto_Persona As String
    Public Property Direccion_Persona As String
    Public Property Telefono_Persona As String
    Public Property Correo_Persona As String
    Public Property Edad_Persona As Integer
    Public Property Id_Sexo As Integer
    Public Property FechaCreacion_Persona As Date

End Class
