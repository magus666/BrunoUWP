' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Mascota
    Inherits Page
    Dim GetMascota As New Cl_Mascota

    Private Sub NvvMascota_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Consulta de Mascotas"
                    FrmContenido.Navigate(GetType(Frm_ConsultaMascota))
                Case "Creacion de Mascotas"
                    FrmContenido.Navigate(GetType(Frm_CreaMascota))
                Case "Parametrizacion de Razas"
                    FrmContenido.Navigate(GetType(Frm_CrearRaza))
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ObtenerMascota = Await GetMascota.ConsultaMascotas
            If ObtenerMascota.Count = 0 Then
                NvvMascota.SelectedItem = NvvParametrizacionRaza
                MarcoTrabajo = FrmContenido
                MarcoTrabajo.Navigate(GetType(Frm_CrearRaza))
            Else
                NvvMascota.SelectedItem = NvvConsultaMascota
                MarcoTrabajo = FrmContenido
                MarcoTrabajo.Navigate(GetType(Frm_ConsultaMascota))
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
