' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Inicio
    Inherits Page
    Dim GetMascotas As New Cl_Mascota

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ContadorMascotas As Integer = Await GetMascotas.CountMascotas
            LblCantidadMascotas.Text = ContadorMascotas
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub RdbTiempo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim ContadorMascotas As Integer
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Hoy"
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoDia
                    LblCantidadMascotas.Text = ContadorMascotas
                Case "Ultima Semana"
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimaSemana
                    LblCantidadMascotas.Text = ContadorMascotas
                Case "Ultimo Mes"
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoMes
                    LblCantidadMascotas.Text = ContadorMascotas
                Case "Siempre"
                    ContadorMascotas = Await GetMascotas.CountMascotas
                    LblCantidadMascotas.Text = ContadorMascotas
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
