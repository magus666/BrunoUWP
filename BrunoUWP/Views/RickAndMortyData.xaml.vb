' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class RickAndMortyData
    Inherits Page
    Dim GetRicandMorty As New RickyRicon

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim Datos = Await GetRicandMorty.GetRickAndMorty()
        LsvRick.ItemsSource = Datos
        'Dim Retorno = (From x In Hola
        '               Select x.name, x.image).FirstOrDefault()
        'txtn.Text = Retorno.name
        'Dim bitmapImage As New BitmapImage(New Uri(Retorno.image))
        'ImgImagen.Source = bitmapImage
    End Sub
End Class
