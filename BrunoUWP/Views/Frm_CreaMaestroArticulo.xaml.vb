' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CreaMaestroArticulo
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetMaestroArticulo As New Cl_MaestroArticulo
    Dim IdMaestroArticulo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            DtgMaestroArticulo.ItemsSource = Await GetMaestroArticulo.ConsultaMaestroArticulos
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoMastroArticulo As String = GetUtilitarios.GeneraCodigoMaestroArticulo
            If Await GetMaestroArticulo.InsertarMaestroArticulo(CodigoMastroArticulo, TxtNombreMaestroArticulo.Text,
                                                                TxtDescripcionMaestroArticulo.Text, Date.Now) = True Then
                Dim Retorno = "Buena la Ñola"
                DtgMaestroArticulo.ItemsSource = Await GetMaestroArticulo.ConsultaMaestroArticulos
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub DtgMaestroArticulo_DoubleTapped(sender As Object, e As DoubleTappedRoutedEventArgs)
        Try
            Dim ObtenerMaestroArticulo As New MaestroArticuloModel
            ObtenerMaestroArticulo = DtgMaestroArticulo.SelectedItem
            IdMaestroArticulo = ObtenerMaestroArticulo.Id_MaestroArticulo
            TxtNombreMaestroArticuloDialog.Text = ObtenerMaestroArticulo.Nombre_MaestroArticulo
            TxtDescripcionMaestroArticuloDialog.Text = ObtenerMaestroArticulo.Descripcion_MaestroArticulo
            Await CtdModificaArticuloMaestro.ShowAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub CtdModificaArticuloMaestro_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            If Await GetMaestroArticulo.ActualizarMaestroArticulo(IdMaestroArticulo, TxtNombreMaestroArticuloDialog.Text,
                                                            TxtDescripcionMaestroArticuloDialog.Text) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "El Articulo maestro fue Actualizado con Exito.")
                DtgMaestroArticulo.ItemsSource = Await GetMaestroArticulo.ConsultaMaestroArticulos
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
