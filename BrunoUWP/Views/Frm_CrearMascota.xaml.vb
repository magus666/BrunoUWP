' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.Storage
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CrearMascota
    Inherits Page
    Dim GetNotifications As New Cl_Notificaciones()
    Dim GetMascota As New Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetRazaMascota As New Cl_RazaMascota
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetManipulacionImagens As New Cl_ManipulacionImagenes
    Dim IdPropietario As Integer
    Dim IdTipoMascota As Integer
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

        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim localSettings As ApplicationDataContainer
            localSettings = ApplicationData.Current.LocalSettings
            localSettings.Values("NombreMascota") = TxtNombreMascota.Text
            Await GetMascota.InsertarMascota(IdTipoMascota, IdRazaMascota, TxtNombreMascota.Text,
                                             NbbEdad.Text, IdPropietario, TxtObservaciones.Text)
            GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Mascota se ha Guardado Con Exito.")
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await GetManipulacionImagens.ImagenPicker(PrpMascota)
    End Sub

    Private Sub CmbPropietario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As ClienteModel = CType(comboBox.SelectedItem, ClienteModel)
            IdPropietario = selectedItem.Id_Persona
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub CmbTipoMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            IdTipoMascota = selectedItem.Id_TipoMascota

            CmbRazaMascota.IsEnabled = True
            BtnNuevaRaza.IsEnabled = True
            Dim LstaRazaMascota = Await GetRazaMascota.ConsultaRazaMascotaId(IdTipoMascota)
            CmbRazaMascota.ItemsSource = LstaRazaMascota
            CmbRazaMascota.DisplayMemberPath = "Nombre_Raza"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmbRazaMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As RazaModel = CType(comboBox.SelectedItem, RazaModel)
            IdRazaMascota = selectedItem.Id_Raza
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub BtnNuevaRaza_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascotaDialog.ItemsSource = ListaTipoMascota
            CmbTipoMascotaDialog.DisplayMemberPath = "Nombre_TipoMascota"

            Await CtdNuevaRaza.ShowAsync()


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Async Sub CtdNuevaRaza_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            Await GetRazaMascota.InsertarRaza(TxtNombreRazaDialog.Text, TxtDescripcionRazaDialog.Text, IdTipoMascotaDialog)

            Dim LstaRazaMascota = Await GetRazaMascota.ConsultaRazaMascotaId(IdTipoMascota)
            CmbRazaMascota.ItemsSource = LstaRazaMascota
            CmbRazaMascota.DisplayMemberPath = "Nombre_Raza"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbTipoMascotaDialog_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            IdTipoMascotaDialog = selectedItem.Id_TipoMascota
        Catch ex As Exception

        End Try
    End Sub
End Class
