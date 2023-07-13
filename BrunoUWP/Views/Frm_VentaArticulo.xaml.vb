' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_VentaArticulo
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub NvvVentaArticulo_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Consulta de Ventas"
                    FrmContenido.Navigate(GetType(Frm_ConsultaVentaArticulo))
                Case "Creacion de Ventas"
                    FrmContenido.Navigate(GetType(Frm_CreaVentaArticulo))
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
