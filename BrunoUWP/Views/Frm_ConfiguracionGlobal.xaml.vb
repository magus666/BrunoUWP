' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConfiguracionGlobal
    Inherits Page

    Private Async Sub BtnRespaldar_Click(sender As Object, e As RoutedEventArgs)
        Await BackUpDatabase()
    End Sub
End Class
