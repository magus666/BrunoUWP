Public Class RickAndMortyModel
    Public Property Nombre_Personaje As String
    Public Property name As String
    Public Property status As String
    Public Property Imagen_Personaje As String
    Public Property origin As original

    Public Class original
        Public Property name As String
        Public Property image As String
    End Class

    Public Class RickAndMortyResponse
        Public Property results As ObservableCollection(Of RickAndMortyModel)
    End Class
End Class
