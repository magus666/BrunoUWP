' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.AppCenter.Analytics
Imports Microsoft.UI.Xaml.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Inicio
    Inherits Page
    Dim GetMascotas As New Cl_Mascota
    Dim GetClientes As New Cl_Cliente
    Dim GetVenta As New Cl_Venta
    Dim GetNotificaciones As New Cl_Notificaciones


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
            Dim ContadorClientes As Integer
            Dim VentasTotales As Double
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Hoy"
                    ContadorClientes = Await GetClientes.CountClienteUltimoDia
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoDia
                    LblCantidadMascotas.Text = ContadorMascotas
                    VentasTotales = Await GetVenta.ConsultaVentaUltimoDia
                    LblGananciasTotales.Text = VentasTotales.ToString("c")
                Case "Ultima Semana"
                    ContadorClientes = Await GetClientes.CountClienteUltimaSemana
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimaSemana
                    LblCantidadMascotas.Text = ContadorMascotas
                Case "Ultimo Mes"
                    ContadorClientes = Await GetClientes.CountClienteUltimoMes
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoMes
                    LblCantidadMascotas.Text = ContadorMascotas
                Case "Siempre"
                    ContadorClientes = Await GetClientes.CountClienteTotal
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotas
                    LblCantidadMascotas.Text = ContadorMascotas
                    VentasTotales = Await GetVenta.ConsultaVentaTotal
                    LblGananciasTotales.Text = VentasTotales.ToString("c")
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Public Sub TutorialTips()
        Try
            GetNotificaciones.TutorialTeachingTip(TctTutorial, "Informacion",
                                                  "Con estas opciones puede filtrar la informacion por diferentes periodos de Tiempo.", RdbTiempo)
            'GetNotificaciones.TutorialTeachingTip(TctTutorial, "Informacion",
            '                                      "Informacion de la cantidad de Mascotas Registradas.", GrdMascotas)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
