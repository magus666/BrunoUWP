' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports SQLitePCL
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ServicioSpa
    Inherits Page
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        NvvServicioSpa.SelectedItem = NvmVentaServicioSpa
        MarcoTrabajo = FrmContenido
        MarcoTrabajo.Navigate(GetType(Frm_VentaServicioSpa))
    End Sub

    Private Sub NvvServicioSpa_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Servicio de Spa"
                    FrmContenido.Navigate(GetType(Frm_VentaServicioSpa))
                Case "Consulta de Ventas"
                    FrmContenido.Navigate(GetType(Frm_ConsultaServicioSpa))
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
