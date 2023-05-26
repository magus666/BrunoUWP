Imports System.Net.Mime.MediaTypeNames
Imports Windows.Storage.Pickers
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_GestionMascota
    Inherits Page
    Dim GetNotifications As New Cl_Notificaciones()
    Dim GetMascota As New Cl_Mascota
    Dim GetManipulacionImagens As New Cl_ManipulacionImagenes

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Await GetMascota.InsertarMascota(1, 1, "Francisco Franco", 4, 3, "Gonorrea perro")
            'GetNotifications.NotifiacionToast()
            'MarcoTrabajo = MainPage.Current.ContenFrameMenu
            'MarcoTrabajo.Navigate(GetType(Frm_ConsultaMascota))
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await GetManipulacionImagens.ImagenPicker(PrpMascota)
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim QueVemos = Await GetMascota.ConsultaMascotas()
        Dim Metal = QueVemos.ToList
        Dim Fnal = Metal
    End Sub
End Class
