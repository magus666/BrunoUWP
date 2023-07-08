Public Class Cl_Articulo

    Public Async Function InsertarArticulo(CodigoArticulo As String,
                                           NombreArticulo As String,
                                           MarcaArticulo As String,
                                           DescripcionArticulo As String,
                                           ValorArticulo As Double,
                                           CantidadArticulo As Integer,
                                           FechaCreacionArticulo As Date) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Articulo = New ArticuloModel With {
                .Codigo_Articulo = CodigoArticulo,
                .Nombre_Articulo = NombreArticulo,
                .Marca_Articulo = MarcaArticulo,
                .Descripcion_Articulo = DescripcionArticulo,
                .Valor_Articulo = ValorArticulo,
                .Cantidad_Articulo = CantidadArticulo,
                .FechaCreacion_Articulo = FechaCreacionArticulo
            }
            Dim Id = Await ConexionDB.InsertAsync(Articulo)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
