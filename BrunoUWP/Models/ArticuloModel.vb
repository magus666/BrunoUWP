Imports SQLite

Public Class ArticuloModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Articulo As Integer
    Public Property Codigo_Articulo As String
    Public Property Nombre_Articulo As String
    Public Property Marca_Articulo As String
    Public Property Descripcion_Articulo As String
    Public Property Valor_Articulo As Double
    Public Property Cantidad_Articulo As Integer
    Public Property CantidadTotalVenta_Articulo As Integer
    Public Property FechaCreacion_Articulo As Date
    Public Property Id_MaestroArticulo As Integer

End Class
