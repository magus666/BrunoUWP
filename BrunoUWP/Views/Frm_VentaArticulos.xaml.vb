' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_VentaArticulos
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetMaestroArticulos As New Cl_MaestroArticulo
    Dim GetArticulos As New Cl_Articulo
    Dim Existencias As Integer
    Dim IdMaestroArticulo As Integer
    Dim IdArticulo As Integer
    Dim ValorArticulo As Double
    Dim NombreArticulo As String

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CmbTipoArticulo.ItemsSource = Await GetMaestroArticulos.ConsultaMaestroArticulos()
            CmbTipoArticulo.DisplayMemberPath = "Nombre_MaestroArticulo"
            GrdListaArticulos.Visibility = Visibility.Collapsed
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub CmbTipoArticulo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim ComboBoxSexo As ComboBox = CType(sender, ComboBox)
            Dim ItemSeleccionado As MaestroArticuloModel = CType(ComboBoxSexo.SelectedItem, MaestroArticuloModel)
            If CmbTipoArticulo.SelectedIndex = -1 Then
                IdMaestroArticulo = 0
            Else
                GrdListaArticulos.Visibility = Visibility.Visible
                IdMaestroArticulo = ItemSeleccionado.Id_MaestroArticulo
                Dim ObtenerArticulos = Await GetArticulos.ConsultaArticulos()
                Dim GetListaArticulos = (From Art In ObtenerArticulos
                                         Where Art.Id_MaestroArticulo = IdMaestroArticulo).ToList
                GrvListadoArticulos.ItemsSource = GetListaArticulos
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrvListadoArticulos_ItemClick(sender As Object, e As ItemClickEventArgs)
        Dim GetArticulo As New ArticuloModel
        GetArticulo = e.ClickedItem
        Frame.Navigate(GetType(Frm_DetalleVentaArticulo), GetArticulo)
    End Sub
End Class
