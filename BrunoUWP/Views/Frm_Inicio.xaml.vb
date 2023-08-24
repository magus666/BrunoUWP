' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.Globalization
Imports Microsoft.AppCenter.Analytics
Imports Microsoft.UI.Xaml.Controls
Imports Windows.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Inicio
    Inherits Page
    Dim GetMascotas As New Cl_Mascota
    Dim GetClientes As New Cl_Cliente
    Dim GetArticulos As New Cl_Articulo
    Dim GetVentaSpa As New Cl_VentaSpa
    Dim GetVentaArticulo As New Cl_VentaArticulo
    Dim GetCita As New Cl_Cita
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetCambioMoneda As New Cl_CambioMoneda


    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ObtenerValor = GetCambioMoneda.ConvertirPesosAEuros(34500)
            Dim ContadorMascotas As Integer = Await GetMascotas.CountMascotas
            LblCantidadMascotas.Text = ContadorMascotas
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub RdbTiempo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim ContadorMascotas As Integer
            Dim ContadorCitas As Integer
            Dim ContadorSpasRealizados As Integer
            Dim ContadorClientes As Integer
            Dim ContadorArticulosVendidos As Integer
            Dim VentasTotalesSpa As Double
            Dim VentasTotalesArticulos As Double
            Dim VentasTotales As Double
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Hoy"
                    ContadorCitas = Await GetCita.ContadorCitasPendientes
                    LblCitasPendientes.Text = ContadorCitas
                    ContadorSpasRealizados = Await GetCita.ContadorCitasFinalizadaUltimoDia
                    LblCantidadSpas.Text = ContadorSpasRealizados
                    ContadorClientes = Await GetClientes.CountClienteUltimoDia
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoDia
                    LblCantidadMascotas.Text = ContadorMascotas
                    ContadorArticulosVendidos = Await GetVentaArticulo.CountCantidadVentasUltimoDia
                    LblArticulosVendidos.Text = ContadorArticulosVendidos
                    VentasTotalesSpa = Await GetVentaSpa.ConsultaVentaUltimoDia
                    VentasTotalesArticulos = Await GetVentaArticulo.SumatoriaValorVentaTotalArticuloUtimoDia
                    VentasTotales = VentasTotalesSpa + VentasTotalesArticulos
                    If VentasTotalesSpa = 0 Then
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    Else
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    End If

                    If VentasTotalesArticulos = 0 Then
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    Else
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    End If

                    If VentasTotales = 0 Then
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    Else
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.LimeGreen)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    End If

                Case "Ultima Semana"
                    ContadorCitas = Await GetCita.ContadorCitasPendientes
                    LblCitasPendientes.Text = ContadorCitas
                    ContadorSpasRealizados = Await GetCita.ContadorCitasFinalizadasUltimaSemana
                    LblCantidadSpas.Text = ContadorSpasRealizados
                    ContadorClientes = Await GetClientes.CountClienteUltimaSemana
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimaSemana
                    LblCantidadMascotas.Text = ContadorMascotas
                    ContadorArticulosVendidos = Await GetVentaArticulo.CountCantidadVentasUltimaSemana
                    LblArticulosVendidos.Text = ContadorArticulosVendidos
                    VentasTotalesSpa = Await GetVentaSpa.ConsultaVentaUltimaSemana
                    VentasTotalesArticulos = Await GetVentaArticulo.SumatoriaValorVentaTotalArticuloUtimaSemana
                    VentasTotales = VentasTotalesSpa + VentasTotalesArticulos

                    If VentasTotalesSpa = 0 Then
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    Else
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    End If

                    If VentasTotalesArticulos = 0 Then
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    Else
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    End If

                    If VentasTotales = 0 Then
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    Else
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.LimeGreen)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    End If
                Case "Ultimo Mes"
                    ContadorCitas = Await GetCita.ContadorCitasPendientes
                    LblCitasPendientes.Text = ContadorCitas
                    ContadorSpasRealizados = Await GetCita.ContadorCitasFinalizadasUltimoMes
                    LblCantidadSpas.Text = ContadorSpasRealizados
                    ContadorClientes = Await GetClientes.CountClienteUltimoMes
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotaUltimoMes
                    LblCantidadMascotas.Text = ContadorMascotas
                    ContadorArticulosVendidos = Await GetVentaArticulo.CountCantidadVentasUltimoMes
                    LblArticulosVendidos.Text = ContadorArticulosVendidos
                    VentasTotalesSpa = Await GetVentaSpa.ConsultaVentaUltimoMes
                    VentasTotalesArticulos = Await GetVentaArticulo.SumatoriaValorVentaTotalArticuloUtimaSemana
                    VentasTotales = VentasTotalesSpa + VentasTotalesArticulos
                    If VentasTotalesSpa = 0 Then
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    Else
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    End If
                    If VentasTotalesArticulos = 0 Then
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    Else
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    End If

                    If VentasTotales = 0 Then
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    Else
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.LimeGreen)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    End If
                Case "Siempre"
                    ContadorCitas = Await GetCita.ContadorCitasPendientes
                    LblCitasPendientes.Text = ContadorCitas
                    ContadorSpasRealizados = Await GetCita.ContadorCitasFinalizadas
                    LblCantidadSpas.Text = ContadorSpasRealizados
                    ContadorClientes = Await GetClientes.CountClienteTotal
                    LblClientes.Text = ContadorClientes
                    ContadorMascotas = Await GetMascotas.CountMascotas
                    LblCantidadMascotas.Text = ContadorMascotas
                    ContadorArticulosVendidos = Await GetVentaArticulo.CountCantidadVentas
                    LblArticulosVendidos.Text = ContadorArticulosVendidos
                    VentasTotalesSpa = Await GetVentaSpa.ConsultaVentaTotalSpa
                    VentasTotalesArticulos = Await GetVentaArticulo.SumatoriaValorVentaTotalArticulo
                    VentasTotales = VentasTotalesSpa + VentasTotalesArticulos
                    If VentasTotalesSpa = 0 Then
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    Else
                        LblGananciasTotalesSpas.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesSpas.Text = VentasTotalesSpa.ToString("c")
                    End If

                    If VentasTotalesArticulos = 0 Then
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    Else
                        LblGananciasTotalesArtculos.Foreground = New SolidColorBrush(Colors.CadetBlue)
                        LblGananciasTotalesArtculos.Text = VentasTotalesArticulos.ToString("c")
                    End If

                    If VentasTotales = 0 Then
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.Yellow)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    Else
                        LblGananciasTotales.Foreground = New SolidColorBrush(Colors.LimeGreen)
                        LblGananciasTotales.Text = VentasTotales.ToString("c")
                    End If
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
