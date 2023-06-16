Public Class Cl_Venta

    Public Async Function InsertVenta(CodigoVenta As String,
                                     FechaVenta As Date,
                                     IdTipoTransaccion As Integer,
                                     IdMetodoPago As Integer,
                                     ValorVenta As Double) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New VentaModel With {
                .Codigo_Venta = CodigoVenta,
                .Fecha_Venta = FechaVenta,
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

End Class
