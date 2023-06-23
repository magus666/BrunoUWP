Imports Windows.Storage
Imports Windows.Storage.Pickers

Public Class Cl_Pickers

    Public Function CrearFileSavePicker(NombreLibroExcel As String) As FileSavePicker
        Dim savePicker = New FileSavePicker()
        savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        savePicker.FileTypeChoices.Add("Libro de Excel", New List(Of String) From {".xlsx"})
        savePicker.SuggestedFileName = NombreLibroExcel & "_" & Date.Now.ToShortDateString()
        Return savePicker
    End Function

    Public Async Function CrearCarpetaBackUpBd(NombreCarpeta As String) As Task(Of IStorageFolder)
        Try
            Dim folder As StorageFolder = KnownFolders.PicturesLibrary
            Dim existingFolder = Await folder.TryGetItemAsync(NombreCarpeta)
            If existingFolder IsNot Nothing Then
                Await existingFolder.DeleteAsync()
            End If
            Dim newFolder As StorageFolder = Await folder.CreateFolderAsync(NombreCarpeta)
            Return newFolder
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
