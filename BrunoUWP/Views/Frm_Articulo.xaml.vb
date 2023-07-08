' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Articulo
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvvArticulo.SelectedItem = NvmConsultaArticulo
            MarcoTrabajo = FrmContenido
            MarcoTrabajo.Navigate(GetType(Frm_ConsultaArticulo))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NvvArticulo_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Consulta de Articulos"
                    FrmContenido.Navigate(GetType(Frm_ConsultaArticulo))
                Case "Creacion de Articulos"
                    FrmContenido.Navigate(GetType(Frm_CreaArticulo))
            End Select
        Catch ex As Exception

        End Try
    End Sub


End Class
