' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ServicioSpa
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CdpFechaServicio.Date = Date.Now
        Catch ex As Exception

        End Try
    End Sub
End Class
