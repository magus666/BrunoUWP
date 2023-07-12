Imports SQLite
Public Class VentaArticuloModel

    <PrimaryKey, AutoIncrement>
    Public Property Id_VentaArticulo As Integer
    Public Property Codigo_VentaArticulo As String
    Public Property Fecha_VentaArticulo As Date
    Public Property Id_TipoTransaccion As Integer
    Public Property Cantidad_VentaArticulo As Integer
    Public Property Id_MetodoPago As Integer
    Public Property Valor_VentaArticulo As Double
    Public Property Id_Articulo As Integer

End Class
