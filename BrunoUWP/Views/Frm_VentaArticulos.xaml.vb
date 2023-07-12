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

    'Private Async Sub CmbArticulo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
    '    Try
    '        NbmCantidadVentaArticulo.IsEnabled = True
    '        Dim ComboBoxSexo As ComboBox = CType(sender, ComboBox)
    '        Dim ItemSeleccionado As ArticuloModel = CType(ComboBoxSexo.SelectedItem, ArticuloModel)
    '        If CmbArticulo.SelectedIndex = -1 Then
    '            IdArticulo = 0
    '        Else
    '            IdArticulo = ItemSeleccionado.Id_Articulo
    '            Dim GetListaArticulos = Await GetArticulos.ConsultaArticulos()
    '            Dim GetUnicoArticulo = (From Art In GetListaArticulos
    '                                    Where Art.Id_Articulo = IdArticulo
    '                                    Select Art.Nombre_Articulo,
    '                                        Art.Cantidad_Articulo,
    '                                        Art.Valor_Articulo).FirstOrDefault
    '            Existencias = GetUnicoArticulo.Cantidad_Articulo
    '            NombreArticulo = GetUnicoArticulo.Nombre_Articulo
    '            ValorArticulo = GetUnicoArticulo.Valor_Articulo
    '            LblExistenciasArticulo.Visibility = Visibility.Visible
    '            LlenarExistencias()
    '            BtnVerificar.IsEnabled = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub BtnVerificar_Click(sender As Object, e As RoutedEventArgs)
    '    Try
    '        Dim CantidadVenta As Integer = NbmCantidadVentaArticulo.Value
    '        GrdResumenVenta.Visibility = Visibility.Visible

    '        Dim ExistenciasFinales = Existencias - CantidadVenta
    '        Existencias = ExistenciasFinales
    '        LlenarExistencias()

    '        LblNombreArticulo.Text = NombreArticulo
    '        LblCantidad.Text = CantidadVenta
    '        LblValorUnitario.Text = ValorArticulo.ToString("C")

    '        Dim ValorTotal As Double = CantidadVenta * ValorArticulo
    '        LblValorTotal.Text = ValorTotal.ToString("C")

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Public Function LlenarExistencias() As Boolean
    '    Try
    '        NbmCantidadVentaArticulo.Maximum = Existencias
    '        LblCountExistenciasArticulo.Text = Existencias

    '        Select Case Existencias
    '            Case >= 10
    '                LblCountExistenciasArticulo.Foreground = New SolidColorBrush(Colors.Green)
    '            Case >= 5
    '                LblCountExistenciasArticulo.Foreground = New SolidColorBrush(Colors.Yellow)
    '            Case >= 1
    '                LblCountExistenciasArticulo.Foreground = New SolidColorBrush(Colors.Red)
    '        End Select
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    'Public Function ValidaDatos() As Boolean
    '    Try
    '        If GetValidaciones.ValidaComboBoxVacio(CmbArticulo) = False Then
    '            GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un articulo Valido", CmbArticulo)
    '            Return False
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    'Private Async Sub BtnPagar_Click(sender As Object, e As RoutedEventArgs)
    '    Try
    '        Dim ValorPagar As Double
    '        ValorPagar = 4200
    '        Await GetUtilitarios.CrearContentDialogMetodoPago(ValorPagar)
    '        GetUtilitarios.LimpiarControles(StpDatosArticulos)
    '    Catch ex As Exception

    '    End Try

    'End Sub
End Class
