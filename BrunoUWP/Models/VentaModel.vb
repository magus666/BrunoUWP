Imports SQLite

Public Class VentaModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Venta As Integer
    Public Property Codigo_Venta As String
    Public Property Fecha_Venta As Date
    Public Property Id_TipoTransaccion As Integer
    Public Property Id_TipoServicio As Integer
    Public Property Id_Mascota As Integer
    Public Property Id_MetodoPago As Integer
    Public Property Valor_Venta As Double
End Class
