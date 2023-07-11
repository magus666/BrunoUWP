' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Articulo
    Inherits Page
    Dim GetMaestroArticulo As New Cl_MaestroArticulo

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try

            Dim ContadorMaestroArticulo = Await GetMaestroArticulo.ConsultaMaestroArticulos()
            If ContadorMaestroArticulo.Count = 0 Then
                MarcoTrabajo = FrmContenido
                NvvArticulo.SelectedItem = NvmMaestroArticulo
                MarcoTrabajo.Navigate(GetType(Frm_CreaMaestroArticulo))
            Else
                MarcoTrabajo = FrmContenido
                NvvArticulo.SelectedItem = NvmConsultaArticulo
                MarcoTrabajo.Navigate(GetType(Frm_ConsultaArticulo))
            End If

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
                Case "Parametrizacion Maestro Articulos"
                    FrmContenido.Navigate(GetType(Frm_CreaMaestroArticulo))
            End Select
        Catch ex As Exception

        End Try
    End Sub


End Class
