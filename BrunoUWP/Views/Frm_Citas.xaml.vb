' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Citas
    Inherits Page
    Dim GetNotificacionas As New Cl_Notificaciones
    Dim GetCita As New Cl_Cita
    Dim GetMascota As New Cl_Mascota
    Dim GetPersona As New Cl_Cliente
    Dim GetFechaHora As New Cl_DateTime
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetDimensionMascota As New Cl_DimensionMascota
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetDateTime As New Cl_DateTime
    Dim GetVenta As New Cl_Venta
    Dim GetUtilitarios As New Cl_Utilitarios

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            ClvCitas.SelectedDates.Add(Date.Now)
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub ClvCitas_SelectedDatesChanged(sender As CalendarView, args As CalendarViewSelectedDatesChangedEventArgs)
        Try
            Dim metal = ClvCitas.SelectedDates
            Dim Retorno = (From x In metal
                           Select x.Date)
            Dim FechaActual = Retorno(0).ToShortDateString
            Dim ContadorCitas = Await GetCita.ContadorCitasPorFecha(FechaActual)
            LblContadorCitas.Text = ContadorCitas
            Dim TipoServicio = Await GetTipoServicio.ConsultaTipoServicio()
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim Propietario = Await GetPersona.ConsultaCliente()
            Dim Citas = Await GetCita.ConsultarCitas()
            Dim ListaCitas = (From Cit In Citas
                              Join Tps In TipoServicio On
                                  Cit.Id_TipoServicio Equals Tps.Id_TipoSerivicio
                              Join Msc In Mascota On
                                  Cit.Id_Mascota Equals Msc.Id_Mascota
                              Join Cli In Propietario On
                                  Msc.Id_Persona Equals Cli.Id_Persona
                              Where Cit.FechaHora_Cita.ToShortDateString = FechaActual
                              Order By Cit.FechaHora_Cita
                              Select Codigo = Cit.Codigo_Cita,
                                     Visibilidad = Cit.EsVisible,
                                     TipoServ = Tps.Nombre_TipoServicio,
                                     NombreMascota = Msc.Nombre_Mascota,
                                     Cliente = Cli.NombreCompleto_Persona,
                                     Hora = Cit.FechaHora_Cita.ToShortTimeString,
                                     Estado = If(Cit.Estado_Cita, "Terminado", "Pendiente")).ToList
            lsvCitas.ItemsSource = ListaCitas
        Catch ex As Exception

        End Try

    End Sub
End Class
