' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Cliente
    Inherits Page

    Private Sub NvvCliente_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Consulta de Clientes"
                    FrmContenido.Navigate(GetType(Frm_ConsultaCliente))
                Case "Creacion de Clientes"
                    NvvCliente.IsBackEnabled = True
                    NvvCliente.SelectedItem = NvmNuevoCliente
                    FrmContenido.Navigate(GetType(Frm_CrearCliente))
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvvCliente.SelectedItem = NvmConsultaCliente
            MarcoTrabajo = FrmContenido
            MarcoTrabajo.Navigate(GetType(Frm_ConsultaCliente))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NvvCliente_BackRequested(sender As NavigationView, args As NavigationViewBackRequestedEventArgs)
        Try
            If FrmContenido.CanGoBack Then
                FrmContenido.GoBack()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
