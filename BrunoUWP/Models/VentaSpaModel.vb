Imports SQLite

Public Class VentaSpaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_VentaSpa As Integer
    Public Property Codigo_VentaSpa As String
    Public Property Fecha_VentaSpa As Date
    Public Property Id_TipoTransaccionSpa As Integer
    Public Property Id_TipoServicioSpa As Integer
    Public Property Id_MascotaSpa As Integer
    Public Property Id_MetodoPagoSpa As Integer
    Public Property Valor_VentaSpa As Double
End Class
