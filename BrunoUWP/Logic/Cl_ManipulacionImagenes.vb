Imports Microsoft.UI.Xaml.Controls
Imports Windows.Storage.Pickers

Public Class Cl_ManipulacionImagenes

    Public Async Function ImagenPicker(PrpImagen As PersonPicture) As Task(Of PersonPicture)
        Try
            Dim openPicker = New FileOpenPicker()
            openPicker.ViewMode = PickerViewMode.Thumbnail
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary
            openPicker.FileTypeFilter.Add(".jpg")
            openPicker.FileTypeFilter.Add(".jpeg")
            openPicker.FileTypeFilter.Add(".png")
            Dim file = Await openPicker.PickSingleFileAsync()
            If file Is Nothing Then
                Dim Retorno = "Cancelado"
                Return Nothing
            Else
                Dim stream = Await file.OpenAsync(Windows.Storage.FileAccessMode.Read)
                Dim bitmapImage As New BitmapImage()
                Await bitmapImage.SetSourceAsync(stream)
                PrpImagen.ProfilePicture = bitmapImage
                Return PrpImagen
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
