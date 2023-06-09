' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Transacciones
    Inherits Page

    Private Sub NvvTransacciones_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Servicios Spa"
                    FrmContenido.Navigate(GetType(Frm_ServicioSpa))
                Case "Venta de Articulos"
                    FrmContenido.Navigate(GetType(Frm_CrearCliente))
                Case "Consultas"
                    FrmContenido.Navigate(GetType(Frm_CrearCliente))
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvvTransacciones.SelectedItem = NvmServicios
            MarcoTrabajo = FrmContenido
            MarcoTrabajo.Navigate(GetType(Frm_ServicioSpa))
        Catch ex As Exception

        End Try
    End Sub
End Class
