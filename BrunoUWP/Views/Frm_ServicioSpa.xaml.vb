' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.Threading.Tasks.Dataflow
Imports Microsoft.Graphics.Canvas
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ServicioSpa
    Inherits Page
    Dim GetCita As New Cl_Cita
    Dim GetMascota As New Cl_Mascota
    Dim GetPersona As New Cl_Cliente

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            CdpFechaServicio.Date = Date.Now
            Await ConsultaCitas()
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function ConsultaCitas() As Task
        Try
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim Cliente = Await GetPersona.ConsultaCliente()
            Dim Citas = Await GetCita.ConsultarCitas()
            Dim GetRetornoCitas = (From Cit In Citas
                                   Join Mas In Mascota On
                                       Cit.Id_Mascota Equals Mas.Id_Mascota
                                   Join Cli In Cliente On
                                       Mas.Id_Persona Equals Cli.Id_Persona
                                   Order By Cit.FechaHora_Cita
                                   Select New With {Cli.NombreCompleto_Persona,
                                                    Mas.Nombre_Mascota,
                                                    Cit.Codigo_Cita,
                                                    .Fecha_Cita = Cit.FechaHora_Cita.Date.ToShortDateString(),
                                                    .Hora_Cita = Cit.FechaHora_Cita.ToString("hh:mm tt"),
                                                    .Estado_Cita = If(Cit.Estado_Cita, "Activo", "Inactivo")}).ToList
            LsvCita.ItemsSource = GetRetornoCitas
        Catch ex As Exception

        End Try
    End Function

    Private Sub LsvCita_ItemClick(sender As Object, e As ItemClickEventArgs)

    End Sub

    Private Async Sub BtnComprobar_Click(sender As Object, e As RoutedEventArgs)

        Dim FechaHoraCita As Date = New Date(2023, 6, 10, 10, 30, 0)
        If Await GetCita.InsertCita("2222", FechaHoraCita, True, 1) = True Then
            Dim Respuesa = "Excelente"
            Await ConsultaCitas()
        End If
    End Sub
End Class
