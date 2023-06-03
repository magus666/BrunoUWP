Imports System.Drawing
Imports System.Net.Mime.MediaTypeNames
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.UI.Text
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_GestionMascota
    Inherits Page
    Dim GetNotifications As New Cl_Notificaciones()
    Dim GetMascota As New Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetManipulacionImagens As New Cl_ManipulacionImagenes
    Dim IdPropietario As Integer
    Dim IdTipoMascota As Integer


    Public Sub New()
        InitializeComponent()
        NavigationCacheMode = NavigationCacheMode.Required
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim localSettings As ApplicationDataContainer
            localSettings = ApplicationData.Current.LocalSettings
            localSettings.Values("NombreMascota") = TxtNombreMascota.Text
            Await GetMascota.InsertarMascota(IdTipoMascota, 1, TxtNombreMascota.Text,
                                             NbbEdad.Text, IdPropietario, TxtObservaciones.Text)
            'GetNotifications.NotifiacionToast()
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await GetManipulacionImagens.ImagenPicker(PrpMascota)
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await GetTipoMascota.InsertarActualizarMascota()
            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascota.ItemsSource = ListaTipoMascota
            CmbTipoMascota.DisplayMemberPath = "Nombre_TipoMascota"

            Dim ListaClientes = Await GetCliente.ConsultaCliente()
            CmbPropietario.ItemsSource = ListaClientes
            CmbPropietario.DisplayMemberPath = "NombreCompleto_Persona"
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmbPropietario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As ClienteModel = CType(comboBox.SelectedItem, ClienteModel)
            IdPropietario = selectedItem.Id_Persona
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
End Class
