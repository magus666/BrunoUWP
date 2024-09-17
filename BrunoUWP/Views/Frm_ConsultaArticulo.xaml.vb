' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaArticulo
    Inherits Page
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo
    Dim GetArticulos As New Cl_Articulo
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim IdMaestroArticulo As Integer
    Dim IdArticulo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim CargaArticulos = Await GetArticulos.IArticulo_ConsultaArticulos()
            If CargaArticulos.Count = 0 Then
                LblTituloCreacionArticulo.Visibility = Visibility.Visible
                DtgArticulos.Visibility = Visibility.Collapsed
            Else
                DtgArticulos.ItemsSource = CargaArticulos
                LblTituloCreacionArticulo.Visibility = Visibility.Collapsed
                DtgArticulos.Visibility = Visibility.Visible
                CmbCategoriaArticulo.ItemsSource = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
                CmbCategoriaArticulo.DisplayMemberPath = "Nombre_MaestroArticulo"
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CmbCategoriaArticulo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As CategoriaArticuloModel = CType(comboBox.SelectedItem, CategoriaArticuloModel)
            If CmbCategoriaArticulo.SelectedIndex = -1 Then
                IdMaestroArticulo = 0
            Else
                IdMaestroArticulo = selectedItem.Id_MaestroArticulo
                Dim Articulo = Await GetArticulos.IArticulo_ConsultaArticulos
                Dim ListaRetornoArticulo = (From Mar In Articulo
                                            Where Mar.Id_MaestroArticulo = IdMaestroArticulo
                                            Select Mar).ToList
                DtgArticulos.ItemsSource = ListaRetornoArticulo
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            If Await GetArticulos.IArticulo_CreaExcelArticulo = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub DtgArticulos_DoubleTapped(sender As Object, e As DoubleTappedRoutedEventArgs)
        Try
            Dim ObtenerArticulo As New ArticuloModel
            ObtenerArticulo = DtgArticulos.SelectedItem
            IdArticulo = ObtenerArticulo.Id_Articulo
            txtNombreArticuloDialog.Text = ObtenerArticulo.Nombre_Articulo
            TxtValorArticuloDialog.Text = ObtenerArticulo.Valor_Articulo
            TxtCantidadArticuloDialog.Text = ObtenerArticulo.Cantidad_Articulo
            Await CtdModificaArticulo.ShowAsync()
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CtdModificaArticulo_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            Dim CargaArticulos = Await GetArticulos.IArticulo_ConsultaArticulos()
            Dim RetornoArticulos = (From Art In CargaArticulos
                                    Where Art.Id_Articulo = IdArticulo
                                    Select Art).FirstOrDefault

            Dim CantidadNuevaArticulos = CInt(TxtCantidadArticuloDialog.Text)
            Dim CantidadTotalArticulos As Integer = RetornoArticulos.CantidadTotalVenta_Articulo
            If Await GetArticulos.IArticulo_ActualizarArticulo(IdArticulo, TxtValorArticuloDialog.Text,
                                                     CantidadNuevaArticulos, CantidadTotalArticulos) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "El articulo se ha Actualizado con exito")
                DtgArticulos.ItemsSource = Await GetArticulos.IArticulo_ConsultaArticulos()
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub DtgArticulos_Sorting(sender As Object, e As DataGridColumnEventArgs)
        Try
            Dim ObtenerDatos = Await GetArticulos.IArticulo_ConsultaArticulos

            Dim Columna As String = e.Column.Tag.ToString

            Select Case Columna
                Case "NombreArticulo"
                    If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                        DtgArticulos.ItemsSource = (From item In ObtenerDatos
                                                    Order By item.Nombre_Articulo Ascending)
                        e.Column.SortDirection = DataGridSortDirection.Ascending
                    Else
                        DtgArticulos.ItemsSource = (From item In ObtenerDatos
                                                    Order By item.Nombre_Articulo Descending)
                        e.Column.SortDirection = DataGridSortDirection.Descending
                    End If
                Case "MarcaArticulo"
                    If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                        DtgArticulos.ItemsSource = (From item In ObtenerDatos
                                                    Order By item.Marca_Articulo Ascending)
                        e.Column.SortDirection = DataGridSortDirection.Ascending
                    Else
                        DtgArticulos.ItemsSource = (From item In ObtenerDatos
                                                    Order By item.Marca_Articulo Descending)
                        e.Column.SortDirection = DataGridSortDirection.Descending
                    End If
                Case Else
            End Select
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
End Class
