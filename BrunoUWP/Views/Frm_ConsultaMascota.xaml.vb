' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaMascota
    Inherits Page
    Dim GetMascota As New Cl_Mascota

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim Mascotas = Await GetMascota.ConsultaMascotas()
            DtgMovimientoEquipos.ItemsSource = Mascotas
        Catch ex As Exception

        End Try
    End Sub
End Class
