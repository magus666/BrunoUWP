' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Web.WebView2.Core
Imports Windows.Globalization
Imports Windows.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Citas
    Inherits Page

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            ClvCitas.SelectedDates.Add(Date.Now)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClvCitas_SelectedDatesChanged(sender As CalendarView, args As CalendarViewSelectedDatesChangedEventArgs)
        Dim metal = ClvCitas.SelectedDates
        Dim Retorno = (From x In metal
                       Select x.Date)
        TxtFecha.Text = Retorno(0)
    End Sub
End Class
