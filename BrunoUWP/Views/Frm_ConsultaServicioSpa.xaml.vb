' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaServicioSpa
    Inherits Page
    Dim GetVenta As New Cl_Venta

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try


            Dim VentaTotal As Double = Await GetVenta.ConsultaSumatoriaVentaPorTipoTransaccion(1)

            Dim FiltroVenta = Await GetVenta.ConsultaVentaPorTipoTransaccion(1)
            Dim RetornoFiltroVenta = (From x In FiltroVenta
                                      Select x.Id_Venta,
                                             x.Codigo_Venta,
                                             x.Fecha_Venta.ToShortDateString,
                                             x.Id_TipoTransaccion,
                                             x.Id_MetodoPago,
                                             x.Valor_Venta)
            DtgVentaServicioSpa.ItemsSource = RetornoFiltroVenta
            LblVentaTotal.Text = "Ventas Totales: " & VentaTotal.ToString("c")
        Catch ex As Exception

        End Try
    End Sub
End Class
