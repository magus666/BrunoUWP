Public Class Cl_TipoTransaccion

    Public Async Function InsertarActualizarTipoTransaccion() As Task(Of Boolean)
        Try
            Dim ModeloTipoTransaccion As New TipoTransaccionModel
            Dim GetTipoTransaccion = Await ConexionDB.Table(Of TipoTransaccionModel)().ToListAsync()
            Dim ServicioSpa = (From x In GetTipoTransaccion
                               Where x.Nombre_TipoTransaccion = "Servicio de Spa"
                               Select x).Count
            Dim VentaArticulos = (From x In GetTipoTransaccion
                                  Where x.Nombre_TipoTransaccion = "Venta de Articulos"
                                  Select x).Count
            If ServicioSpa = 0 Then
                ModeloTipoTransaccion = New TipoTransaccionModel With {
                    .Nombre_TipoTransaccion = "Servicio de Spa"
                }
                Dim Id = Await ConexionDB.InsertAsync(ModeloTipoTransaccion)
            End If
            If VentaArticulos = 0 Then
                ModeloTipoTransaccion = New TipoTransaccionModel With {
                    .Nombre_TipoTransaccion = "Venta de Articulos"
                }
                Dim Id = Await ConexionDB.InsertAsync(ModeloTipoTransaccion)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaTipoTransaccion() As Task(Of List(Of TipoTransaccionModel))
        Try
            Await ConfiguraSqlite()
            Dim GetTipoTransaccion = Await ConexionDB.Table(Of TipoTransaccionModel)().ToListAsync()
            Dim ListaTipoTransaccion = (From x In GetTipoTransaccion
                                        Select x).ToList
            Return ListaTipoTransaccion
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
