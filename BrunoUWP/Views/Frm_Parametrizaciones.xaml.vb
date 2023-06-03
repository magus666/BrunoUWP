' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Parametrizaciones
    Inherits Page
    Dim IdTipoMascota As Integer
    Dim GetRaza As New Cl_RazaMascota
    Dim GetTipoMascota As New CL_TipoMascota

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Await GetRaza.InsertarRaza(TxtNombreRaza.Text, TxtDescripcionRaza.Text, IdTipoMascota)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbTipoMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            IdTipoMascota = selectedItem.Id_TipoMascota
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascota.ItemsSource = ListaTipoMascota
            CmbTipoMascota.DisplayMemberPath = "Nombre_TipoMascota"
        Catch ex As Exception

        End Try
    End Sub
End Class
