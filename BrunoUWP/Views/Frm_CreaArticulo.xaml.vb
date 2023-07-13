' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CreaArticulo
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo
    Dim GetArticulo As New Cl_Articulo
    Dim IdMaestroArticulo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CmbMaestroArticulo.ItemsSource = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            CmbMaestroArticulo.DisplayMemberPath = "Nombre_MaestroArticulo"
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try

    End Sub

    Private Sub TxtValorArticulo_BeforeTextChanging(sender As TextBox, args As TextBoxBeforeTextChangingEventArgs)
        args.Cancel = args.NewText.Any(Function(c) Not Char.IsDigit(c))
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoArticulo As String = GetUtilitarios.GeneraCodigoArticulo()
            If ValidaDatos() = True Then
                Dim CantidadExistencias = NmbCantidadExistenciasArticulo.Value
                Dim CantidadExistenciasVendidas = 0
                If Await GetArticulo.InsertarArticulo(CodigoArticulo, TxtNombreArticulo.Text, TxtMarcaArticulo.Text,
                                                      TxtDescripcionArticulo.Text, TxtValorArticulo.Text,
                                                      CantidadExistencias, CantidadExistenciasVendidas, Date.Now, IdMaestroArticulo) = True Then
                    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "El Articulo se ha creado con Exito.")
                    GetUtilitarios.LimpiarControles(StpDatosArticulos)
                End If
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaTextBoxVacio(TxtNombreArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El nombre del Articulo no puede estar Vacio", TxtNombreArticulo)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtMarcaArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La marca del Articulo no puede estar Vacia", TxtMarcaArticulo)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtDescripcionArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La descripcion del Articulo no puede estar Vacia", TxtDescripcionArticulo)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtValorArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El valor del Articulo no puede estar Vacio", TxtValorArticulo)
                Return False
                Exit Function
            End If
            If NmbCantidadExistenciasArticulo.Value <= 0 Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La cantidad de existencias del Articulo no puede ser 0", NmbCantidadExistenciasArticulo)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub CmbMaestroArticulo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As CategoriaArticuloModel = CType(comboBox.SelectedItem, CategoriaArticuloModel)
            If CmbMaestroArticulo.SelectedIndex = -1 Then
                IdMaestroArticulo = 0
            Else
                IdMaestroArticulo = selectedItem.Id_MaestroArticulo
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
