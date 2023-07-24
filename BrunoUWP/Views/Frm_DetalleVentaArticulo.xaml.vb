' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleVentaArticulo
    Inherits Page
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetArticulo As New Cl_Articulo
    Dim GetVentaArticulo As New Cl_VentaArticulo
    Dim IdArticulo As Integer
    Dim ExistenciasDisponibles As Integer
    Dim CantidadVentaArticulo As Integer
    Dim ValorUnitario As Integer
    Dim ValorTotal As Integer
    Dim ExistenciasNuevo As Integer
    Dim NombreArticulo As String
    Dim DescripcionArticulo As String
    Dim CantidadComprada As Integer
    Dim IdMetodoPago As Integer
    Dim NombreMetodoPago As String

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        Try
            Dim DatosArticulo As ArticuloModel = TryCast(e.Parameter, ArticuloModel)
            If DatosArticulo IsNot Nothing Then
                IdArticulo = DatosArticulo.Id_Articulo
                ExistenciasDisponibles = DatosArticulo.Cantidad_Articulo
                ValorUnitario = DatosArticulo.Valor_Articulo
                NombreArticulo = DatosArticulo.Nombre_Articulo
                DescripcionArticulo = DatosArticulo.Descripcion_Articulo
                NbbCantidadArticulos.Maximum = ExistenciasDisponibles
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try

            If ExistenciasDisponibles = 0 Then
                NbbCantidadArticulos.IsEnabled = False
                CmbMetodoPago.IsEnabled = False
                BtnConfirmar.IsEnabled = False
                LblMensaBienvenidaVenta.Text = "No hay mas existencias del Articulo " & NombreArticulo & ". Por favor agreguelas desde el Modulo Inventario."
            Else
                NbbCantidadArticulos.IsEnabled = True
                CmbMetodoPago.IsEnabled = True
                BtnConfirmar.IsEnabled = True
                LblMensaBienvenidaVenta.Text = "Por Favor Confirme la Venta en el Panel Izquierdo para ver Los detalles y proceder al Pago..."
            End If
            EstableceImagen()
            LblTituloNombreArticulo.Text = NombreArticulo
            LblDescripcionArticulo.Text = DescripcionArticulo
            LblExistencias.Text = "Existencias: " & ExistenciasDisponibles
            LblValorUnitaro.Text = "Valor Unitario: " & ValorUnitario
            LblValorTotal.Text = "Valor Total: " & ValorTotal
            CmbMetodoPago.ItemsSource = Await GetMetodoPago.ConsultaMetodoPago
            CmbMetodoPago.DisplayMemberPath = "Nombre_MetodoPago"
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function EstableceImagen() As Boolean
        Try
            Dim bitmapImage As New BitmapImage()
            bitmapImage.UriSource = New Uri("ms-appx:///Assets/ImagenJuguete.jpeg")
            ImgArticulo.Source = bitmapImage
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub NbbCantidadArticulos_ValueChanged(sender As Microsoft.UI.Xaml.Controls.NumberBox, args As Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs)
        Try
            Dim ValorNumber As Integer = NbbCantidadArticulos.Value
            ExistenciasNuevo = ExistenciasDisponibles - ValorNumber
            ValorTotal = ValorUnitario * ValorNumber
            LblExistencias.Text = "Existencias: " & ExistenciasNuevo
            LblValorTotal.Text = "Valor Total: " & ValorTotal.ToString("c")
            CantidadComprada = NbbCantidadArticulos.Value

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub BtnConfirmar_Click(sender As Object, e As RoutedEventArgs)
        Try
            If GetValidaciones.ValidaComboBoxVacio(CmbMetodoPago) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Aviso", "Seleccione un Metodo de Pago", CmbMetodoPago)
                Exit Sub
            End If
            If NbbCantidadArticulos.Value <= 0 Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Aviso", "La cantidad no puede ser 0", NbbCantidadArticulos)
                Exit Sub
            Else
                LblMensaBienvenidaVenta.Visibility = Visibility.Collapsed
                StpDetallesVenta.Visibility = Visibility.Visible
                LblTituloNombreArticuloDetalle.Text = NombreArticulo
                LblDescripcionArticuloDetalle.Text = DescripcionArticulo
                LblExistenciasDetalle.Text = ExistenciasNuevo
                LblCantidadDetalle.Text = CantidadComprada
                LblMetodoPagoDetalle.Text = NombreMetodoPago
                LblValorUnitaroDetalle.Text = ValorUnitario.ToString("c")
                LblValorTotalDetalle.Text = ValorTotal.ToString("c")
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbMetodoPago_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As MetodoPagoModel = CType(comboBox.SelectedItem, MetodoPagoModel)
            If CmbMetodoPago.SelectedIndex = -1 Then
                IdMetodoPago = 0
            Else
                IdMetodoPago = selectedItem.Id_MetodoPago
                NombreMetodoPago = selectedItem.Nombre_MetodoPago
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnGuardarVenta_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim ObtenerArticulo = Await GetArticulo.ConsultaArticulos
            Dim RetornoArticulo = (From Art In ObtenerArticulo
                                   Where Art.Id_Articulo = IdArticulo
                                   Select Art).FirstOrDefault
            Dim GetExsitenciaArticulo = RetornoArticulo.CantidadTotalVenta_Articulo
            Dim NumeroRealVentaArticulo = GetExsitenciaArticulo + CantidadComprada
            Await GetArticulo.ActualizarArticulo(IdArticulo, ValorUnitario, ExistenciasNuevo, NumeroRealVentaArticulo)

            Dim CodigoVenta As String = GetUtilitarios.GenearCodigoVenta
            If Await GetVentaArticulo.InsertVenta(CodigoVenta, Date.Now, 2, CantidadComprada, IdMetodoPago, ValorTotal, IdArticulo) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La venta se realizó con Exito")
                VolverAtras()
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Sub VolverAtras()
        If Frame.CanGoBack Then
            Frame.GoBack()
        End If
    End Sub
End Class
