' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Cliente
    Inherits Page

    Private Sub NvvCliente_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Creacion de Clientes"
                    sender.Content = New Frm_CrearCliente()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvvCliente.SelectedItem = NvmNuevoCliente
            MarcoTrabajo = FrmContenido
            MarcoTrabajo.Navigate(GetType(Frm_CrearCliente))
        Catch ex As Exception

        End Try
    End Sub
End Class
