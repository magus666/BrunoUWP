' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.Storage
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CreaMascota
    Inherits Page
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetImagenMascota As New Cl_ImagenMascota
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetCrudBrunoSpa As New Cl_CrudBrunoSpa
    Dim GetMascota As New Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetRazaMascota As New Cl_RazaMascota
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetManipulacionImagens As New Cl_ManipulacionImagenes
    Dim IdPropietario As Integer
    Dim IdTipoMascota As Integer
    Dim IndexTipoMascota As Integer
    Dim IdRazaMascota As Integer
    Dim IdTipoMascotaDialog As Integer
    Public Sub New()
        InitializeComponent()
        NavigationCacheMode = NavigationCacheMode.Required
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await GetTipoMascota.InsertarActualizarTipoMascota()

            CmbRazaMascota.IsEnabled = False

            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascota.ItemsSource = ListaTipoMascota
            CmbTipoMascota.DisplayMemberPath = "Nombre_TipoMascota"


            Dim ListaClientes = Await GetCliente.ConsultaCliente()
            If ListaClientes.Count = 0 Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Aviso", "Cree un Propietario en el Modulo Clientes Antes de Continuar.",
                                                                 CmbPropietario)
            Else
                CmbPropietario.ItemsSource = ListaClientes.OrderBy(Function(cliente)
                                                                       Return cliente.NombreCompleto_Persona
                                                                   End Function).ToList()
                CmbPropietario.DisplayMemberPath = "NombreCompleto_Persona"
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoMascota As String = GetUtilitarios.GenerarCodigoMascota
            If ValidaDatos() = True Then
                Dim GetMascotaModel As New MascotaModel With
                {
                    .Codigo_Mascota = CodigoMascota,
                    .Id_TipoMascota = IdTipoMascota,
                    .Id_Raza = IdRazaMascota,
                    .Nombre_Mascota = TxtNombreMascota.Text,
                    .Edad_Mascota = NbbEdad.Value,
                    .Id_Persona = IdPropietario,
                    .Observaciones_Mascota = TxtObservaciones.Text,
                    .Estado_Mascota = True,
                    .FechaRegistro_Mascota = Date.Now
                }
                If Await GetCrudBrunoSpa.InsertarModelo(GetMascotaModel) = True Then
                    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Mascota se ha Guardado Con Exito.")

                    Dim ObtenerMascotas = Await GetMascota.ConsultaMascotas()
                    Dim IdUltimaMascota = ObtenerMascotas.LastOrDefault().Id_Mascota

                    Select Case IdTipoMascota
                        Case 1
                            Await GetImagenMascota.InsertImagenMascota("Foto1", "https://sims4updates.net/wp-content/uploads/2018/03/7016-310x310.jpg", IdUltimaMascota)
                        Case 2
                            Await GetImagenMascota.InsertImagenMascota("Foto1", "https://farm2.staticflickr.com/1919/45579541712_f58c1fd0ed_o.jpg", IdUltimaMascota)
                        Case 3
                            Await GetImagenMascota.InsertImagenMascota("Foto1", "https://www.conejos.wiki/Imagenes/conejo-blanco.jpg", IdUltimaMascota)
                    End Select

                    GetUtilitarios.LimpiarControles(StpPrincipal)
                End If
                'Await GetMascota.InsertarMascota(CodigoMascota, IdTipoMascota, IdRazaMascota, TxtNombreMascota.Text,
                '                                 NbbEdad.Text, IdPropietario, TxtObservaciones.Text)
                '    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Mascota se ha Guardado Con Exito.")
                '    GetUtilitarios.LimpiarControles(StpPrincipal)
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaComboBoxVacio(CmbTipoMascota) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un tipo de mascota valido", CmbTipoMascota)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaComboBoxVacio(CmbRazaMascota) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione una Raza valida", CmbRazaMascota)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtNombreMascota) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El nombre de la mascota no puede estar Vacio", TxtNombreMascota)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaComboBoxVacio(CmbPropietario) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un propietario valido", CmbPropietario)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtObservaciones) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "las observaciones no pueden estar Vacias", TxtObservaciones)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await GetManipulacionImagens.ImagenPicker(PrpMascota)
    End Sub

    Private Sub CmbPropietario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As ClienteModel = CType(comboBox.SelectedItem, ClienteModel)
            If CmbPropietario.SelectedIndex = -1 Then
                IdPropietario = 0
            Else
                IdPropietario = selectedItem.Id_Persona
            End If

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try

    End Sub

    Private Async Sub CmbTipoMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Await GetTipoMascota.InsertarActualizarTipoMascota()
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            If CmbTipoMascota.SelectedIndex = -1 Then
                IdTipoMascota = 0
            Else
                IdTipoMascota = selectedItem.Id_TipoMascota
                IndexTipoMascota = CmbTipoMascota.SelectedIndex
            End If
            CmbRazaMascota.IsEnabled = True
            BtnNuevaRaza.IsEnabled = True
            Dim LstaRazaMascota = Await GetRazaMascota.ConsultaRazaMascotaId(IdTipoMascota)
            CmbRazaMascota.ItemsSource = LstaRazaMascota
            CmbRazaMascota.DisplayMemberPath = "Nombre_Raza"

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try

    End Sub

    Private Sub CmbRazaMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As RazaModel = CType(comboBox.SelectedItem, RazaModel)
            If CmbRazaMascota.SelectedIndex = -1 Then
                IdRazaMascota = 0
            Else
                IdRazaMascota = selectedItem.Id_Raza
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnNuevaRaza_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascotaDialog.ItemsSource = ListaTipoMascota
            CmbTipoMascotaDialog.DisplayMemberPath = "Nombre_TipoMascota"
            CmbTipoMascotaDialog.SelectedIndex = IndexTipoMascota
            CmbTipoMascotaDialog.IsEnabled = False
            Await CtdNuevaRaza.ShowAsync()
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CtdNuevaRaza_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            Dim CogigoRaza As String = GetUtilitarios.GenerarCodigoRaza
            Await GetRazaMascota.InsertarRaza(CogigoRaza, TxtNombreRazaDialog.Text, TxtDescripcionRazaDialog.Text, IdTipoMascotaDialog)

            Dim LstaRazaMascota = Await GetRazaMascota.ConsultaRazaMascotaId(IdTipoMascota)
            CmbRazaMascota.ItemsSource = LstaRazaMascota
            CmbRazaMascota.DisplayMemberPath = "Nombre_Raza"
            LimpiaControlesContentDialog()

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbTipoMascotaDialog_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            If CmbTipoMascotaDialog.SelectedIndex = -1 Then
                IdTipoMascotaDialog = 0
            Else
                IdTipoMascotaDialog = selectedItem.Id_TipoMascota
            End If

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function LimpiaControlesContentDialog() As Boolean
        Try
            CmbTipoMascotaDialog.SelectedIndex = -1
            TxtNombreRazaDialog.Text = String.Empty
            TxtDescripcionRazaDialog.Text = String.Empty
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
