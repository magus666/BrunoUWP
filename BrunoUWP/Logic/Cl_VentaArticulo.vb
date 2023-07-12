Public Class Cl_VentaArticulo

    Public Async Function InsertVenta(CodigoVentaArticulo As String,
                                      FechaVentaArticulo As Date,
                                      IdTIpoTransaccion As Integer,
                                      CantidadVentaArticulo As Integer,
                                      IdMetodoPago As Integer,
                                      ValorVentaArticulo As Double,
                                      IdArticulo As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New VentaArticuloModel With {
                .Codigo_VentaArticulo = CodigoVentaArticulo,
                .Fecha_VentaArticulo = FechaVentaArticulo,
                .Id_TipoTransaccion = IdTIpoTransaccion,
                .Cantidad_VentaArticulo = CantidadVentaArticulo,
                .Id_MetodoPago = IdMetodoPago,
                .Valor_VentaArticulo = ValorVentaArticulo,
                .Id_Articulo = IdArticulo
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
