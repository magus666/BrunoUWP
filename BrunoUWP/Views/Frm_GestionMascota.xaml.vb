' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.Net.Mime.MediaTypeNames
Imports Microsoft
Imports Windows.Storage.Pickers
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_GestionMascota
    Inherits Page
    Dim GetNotifications As New Cl_Notificaciones()

    Private Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            GetNotifications.NotifiacionToast()
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function ClickImagen() As Task
        Try

            Dim openPicker = New FileOpenPicker()
            openPicker.ViewMode = PickerViewMode.Thumbnail
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
            openPicker.FileTypeFilter.Add(".jpg")
            openPicker.FileTypeFilter.Add(".jpeg")
            openPicker.FileTypeFilter.Add(".png")
            Dim file = Await openPicker.PickSingleFileAsync()
            Dim Imagen As String = file.Path.ToString
            If file IsNot Nothing Then
                PrpMascota.ProfilePicture = New BitmapImage(New Uri(Imagen))
            Else

            End If
        Catch ex As Exception

        End Try
    End Function

    Private Async Sub PrpMascota_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Await ClickImagen()
    End Sub
End Class
