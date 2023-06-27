Public Class Cl_Venta

    Public Async Function InsertVenta(CodigoVenta As String,
                                      FechaVenta As Date,
                                      IdTipoServicio As Integer,
                                      IdTipoTransaccion As Integer,
                                      IdMetodoPago As Integer,
                                      ValorVenta As Double) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New VentaModel With {
                .Codigo_Venta = CodigoVenta,
                .Fecha_Venta = FechaVenta,
                .Id_TipoServicio = IdTipoServicio,
                .Id_TipoTransaccion = IdTipoTransaccion,
                .Id_MetodoPago = IdMetodoPago,
                .Valor_Venta = ValorVenta
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVenta() As Task(Of List(Of VentaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Select x).ToList()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaTotal() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Select x.Valor_Venta).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaUltimoDia() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Fecha_Venta.Date = Date.Today
                              Select x.Valor_Venta).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaPorTipoTransaccion(IdTipoTransaccion As Integer) As Task(Of List(Of VentaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Id_TipoTransaccion = IdTipoTransaccion
                              Select x).ToList()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaSumatoriaVentaPorTipoTransaccion(IdTipoTransaccion As Integer) As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Id_TipoTransaccion = IdTipoTransaccion
                              Select x.Valor_Venta).Sum()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
