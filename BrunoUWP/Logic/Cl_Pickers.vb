Imports Windows.Storage.Pickers

Public Class Cl_Pickers

    Public Function CrearFileSavePicker(NombreLibroExcel As String) As FileSavePicker
        Dim savePicker = New FileSavePicker()
        savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        savePicker.FileTypeChoices.Add("Libro de Excel", New List(Of String) From {".xlsx"})
        savePicker.SuggestedFileName = NombreLibroExcel & "_" & Date.Now.ToShortDateString()
        Return savePicker
    End Function

End Class
