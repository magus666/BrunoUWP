Imports System.Drawing
Imports System.Net.Mime.MediaTypeNames
Imports Windows.Storage
Imports Windows.Storage.Pickers
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_GestionMascota
    Inherits Page
    Dim GetNotifications As New Cl_Notificaciones()
    Dim GetMascota As New Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetManipulacionImagens As New Cl_ManipulacionImagenes


    Public Sub New()
        InitializeComponent()
        NavigationCacheMode = NavigationCacheMode.Required
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim localSettings As ApplicationDataContainer
            localSettings = ApplicationData.Current.LocalSettings
            localSettings.Values("NombreMascota") = TxtNombreMascota.Text
            Await GetMascota.InsertarMascota(1, 1, "Francisco Franco", 4, 3, "Gonorrea perro")
            'GetNotifications.NotifiacionToast()
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await GetManipulacionImagens.ImagenPicker(PrpMascota)
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
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
            Dim selectedId As Integer = selectedItem.Id_Persona
            Dim Resultado = selectedId
        Catch ex As Exception

        End Try

    End Sub
End Class
