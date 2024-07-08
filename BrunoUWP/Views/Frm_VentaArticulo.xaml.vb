' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.Security.Credentials.UI

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_VentaArticulo
    Inherits Page
    Dim GetVentaArticulo As New Cl_VentaArticulo

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try

            Dim consentResult = Await UserConsentVerifier.RequestVerificationAsync("Iniciar sesión con PIN")

            If consentResult = UserConsentVerificationResult.Verified Then
                Dim ObtenerVentaArticulo = Await GetVentaArticulo.ConsultaListaVentasTotales
                If ObtenerVentaArticulo.Count = 0 Then
                    NvvVentaArticulo.SelectedItem = NvmCreaVentaArticulo
                    MarcoTrabajo = FrmContenido
                    MarcoTrabajo.Navigate(GetType(Frm_CreaVentaArticulo))
                Else
                    NvvVentaArticulo.SelectedItem = NvmConsultaVentaArticulo
                    MarcoTrabajo = FrmContenido
                    MarcoTrabajo.Navigate(GetType(Frm_ConsultaVentaArticulo))
                End If
            Else
                FrmContenido.Navigate(GetType(Frm_Inicio))
            End If
        Catch ex As Exception

        End Try
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
