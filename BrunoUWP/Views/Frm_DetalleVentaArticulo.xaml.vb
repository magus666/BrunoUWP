' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleVentaArticulo
    Inherits Page
    Dim IdArticulo As Integer
    Dim ExistenciasDisponibles As Integer
    Dim ValorUnitario As Integer
    Dim ValorTotal As Integer
    Dim ExistenciasNuevo As Integer
    Dim NombreArticulo As String
    Dim DescripcionArticulo As String
    Dim CantidadComprada As Integer

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

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            EstableceImagen()
            LblTituloNombreArticulo.Text = NombreArticulo
            LblDescripcionArticulo.Text = DescripcionArticulo
            LblExistencias.Text = "Existencias: " & ExistenciasDisponibles
            LblValorUnitaro.Text = "Valor Unitario: " & ValorUnitario
            LblValorTotal.Text = "Valor Total: " & ValorTotal
        Catch ex As Exception

        End Try

    End Sub

    Public Function EstableceImagen() As Boolean
        Try
            Dim bitmapImage As New BitmapImage()
            bitmapImage.UriSource = New Uri("ms-appx:///Assets/BoxerJuguete.jpg")
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
            LblValorTotal.Text = "Valor Total a Pagar: " & ValorTotal.ToString("c")
            CantidadComprada = NbbCantidadArticulos.Value

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnConfirmar_Click(sender As Object, e As RoutedEventArgs)
        Try
            LblMensajeVenta.Visibility = Visibility.Collapsed
            StpDetallesVenta.Visibility = Visibility.Visible

            LblTituloNombreArticuloDetalle.Text = NombreArticulo
            LblDescripcionArticuloDetalle.Text = DescripcionArticulo
            LblExistenciasDetalle.Text = ExistenciasNuevo
            LblCantidadDetalle.Text = CantidadComprada
            LblValorUnitaroDetalle.Text = ValorUnitario.ToString("c")
            LblValorTotalDetalle.Text = ValorTotal.ToString("c")
        Catch ex As Exception

        End Try
    End Sub
End Class
