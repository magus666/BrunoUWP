' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Configuracion
    Inherits Page
    Private Sub NvvConfiguracion_ItemInvoked(sender As Controls.NavigationView, args As Controls.NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Configuracion de la Aplicacion"
                    sender.Content = New Frm_ConfiguracionAplicacion()
                Case "Parametrizaciones"
                    sender.Content = New Frm_Parametrizaciones()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvvConfiguracion.SelectedItem = NviConfig
            MarcoTrabajo = FrmContenido
            MarcoTrabajo.Navigate(GetType(Frm_ConfiguracionAplicacion))
        Catch ex As Exception

        End Try
    End Sub
End Class
