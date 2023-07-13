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

    Public Async Function ConsultaVenta() As Task(Of List(Of VentaArticuloModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVentaArticulo = Await ConexionDB.Table(Of VentaArticuloModel)().ToListAsync()
            Dim ListaVentaArticulo = (From x In GetVentaArticulo
                                      Select x).ToList()
            Return ListaVentaArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
