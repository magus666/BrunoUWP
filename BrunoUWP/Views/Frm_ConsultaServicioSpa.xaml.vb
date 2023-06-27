' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaServicioSpa
    Inherits Page
    Dim GetVenta As New Cl_Venta
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetMetodoPago As New Cl_MetodoPago


    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try


            Dim VentaTotal As Double = Await GetVenta.ConsultaSumatoriaVentaPorTipoTransaccion(1)
            Dim Venta = Await GetVenta.ConsultaVentaPorTipoTransaccion(1)
            Dim TipoServicio = Await GetTipoServicio.ConsultaTipoServicio()
            Dim TipoTransaccion = Await GetTipoTransaccion.ConsultaTipoTransaccion()
            Dim MetodoPagao = Await GetMetodoPago.ConsultaMetodoPago()
            Dim RetornoFiltroVenta = (From Vtn In Venta
                                      Join Tps In TipoServicio On
                                              Vtn.Id_TipoServicio Equals Tps.Id_TipoSerivicio
                                      Join Ttc In TipoTransaccion On
                                          Vtn.Id_TipoTransaccion Equals Ttc.Id_TipoTransaccion
                                      Join Mdp In MetodoPagao On
                                          Vtn.Id_MetodoPago Equals Mdp.Id_MetodoPago
                                      Select New With {.Codigo = Vtn.Codigo_Venta,
                                                       .Fecha = Vtn.Fecha_Venta.ToShortDateString,
                                                       .TipoServicio = Tps.Nombre_TipoServicio,
                                                       .MetodoPago = Mdp.Nombre_MetodoPago,
                                                       .VarlorTotal = Vtn.Valor_Venta.ToString("c")})
            DtgVentaServicioSpa.ItemsSource = RetornoFiltroVenta
            LblVentaTotal.Text = "Ventas Totales: " & VentaTotal.ToString("c")
        Catch ex As Exception

        End Try
    End Sub
End Class
