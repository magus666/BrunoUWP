' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ServicioSpa
    Inherits Page
    Dim GetCita As New Cl_Cita
    Dim GetMascota As New Cl_Mascota
    Dim GetPersona As New Cl_Cliente
    Dim GetFechaHora As New Cl_DateTime
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetDimensionMascota As New Cl_DimensionMascota
    Dim FechaCalendarPicker As String
    Dim IdTipoServicio As Integer
    Dim IdMascota As Integer
    Dim IdDimensionMascota As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await GetDimensionMascota.InsertarActualizarDimensionMascota()
            TmpHoraServicio.SelectedTime = Date.Now.TimeOfDay
            CdpFechaServicio.Date = Date.Now

            CmbTIpoServicio.ItemsSource = Await GetTipoServicio.ConsultaTipoServicio
            CmbTIpoServicio.DisplayMemberPath = "Nombre_TipoServicio"

            CmbMascota.ItemsSource = Await GetMascota.ConsultaMascotas
            CmbMascota.DisplayMemberPath = "Nombre_Mascota"

            CmbDimensionMascota.ItemsSource = Await GetDimensionMascota.ConsultaDimensionMascota
            CmbDimensionMascota.DisplayMemberPath = "Nombre_DimensionMascota"

            LsvCita.ItemsSource = Await ConsultaCitas()
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function ConsultaCitas() As Task(Of Object)
        Try
            FechaCalendarPicker = CdpFechaServicio.Date.Value.Date.ToShortDateString()
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim Cliente = Await GetPersona.ConsultaCliente()
            Dim Citas = Await GetCita.ConsultarCitas()
            Dim TipoServicio = Await GetTipoServicio.ConsultaTipoServicio()
            Dim fechaHoraCita As New List(Of Date)
            Dim GetRetornoCitas = (From Cit In Citas
                                   Join Mas In Mascota On
                                       Cit.Id_Mascota Equals Mas.Id_Mascota
                                   Join Cli In Cliente On
                                       Mas.Id_Persona Equals Cli.Id_Persona
                                   Join Tps In TipoServicio On
                                       Cit.Id_TipoServicio Equals Tps.Id_TipoSerivicio
                                   Where Cit.FechaHora_Cita.Date.ToShortDateString() = FechaCalendarPicker
                                   Order By Cit.FechaHora_Cita
                                   Select New With {Cli.NombreCompleto_Persona,
                                                    Mas.Nombre_Mascota,
                                                    Cit.Codigo_Cita,
                                                    Cit.FechaHora_Cita,
                                                    Tps.Nombre_TipoServicio,
                                                    .Fecha_Cita = Cit.FechaHora_Cita.Date.ToShortDateString(),
                                                    .Hora_Cita = Cit.FechaHora_Cita.ToString("hh:mm tt"),
                                                    .Estado_Cita = If(Cit.Estado_Cita, "Activo", "Inactivo")}).ToList

            Return GetRetornoCitas
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub BtnComprobar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoCita As String = GetUtilitarios.GenearCodigoCita
            Dim HoraServicio As String = TmpHoraServicio.SelectedTime.ToString
            Dim FechaHoraConcatenada As String = FechaCalendarPicker & " " & HoraServicio
            If Await GetCita.InsertCita(CodigoCita, FechaHoraConcatenada, True, IdMascota, IdDimensionMascota, IdTipoServicio) = True Then
                Dim Respuesa = "Excelente"
                LsvCita.ItemsSource = Await ConsultaCitas()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub CdpFechaServicio_DateChanged(sender As CalendarDatePicker, args As CalendarDatePickerDateChangedEventArgs)
        Try
            Dim FechaCdp = CdpFechaServicio.Date
            LblTituloCitas.Text = GetFechaHora.MostrarFechaLarga(FechaCdp.ToString)
            LsvCita.ItemsSource = Await ConsultaCitas()
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub BtnNuevoTipoServicio_Click(sender As Object, e As RoutedEventArgs)
        Try
            Await CtdTipoServicio.ShowAsync()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Async Sub CtdTipoServicio_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            If Await GetTipoServicio.InsertTipoServicio(TxtNombreTipoSerivicio.Text) Then
                Dim Respuesta = "Buena la Ñolita"

                CmbTIpoServicio.ItemsSource = Await GetTipoServicio.ConsultaTipoServicio
                CmbTIpoServicio.DisplayMemberPath = "Nombre_TipoServicio"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbTIpoServicio_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoServicioModel = CType(comboBox.SelectedItem, TipoServicioModel)
            IdTipoServicio = selectedItem.Id_TipoSerivicio
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As MascotaModel = CType(comboBox.SelectedItem, MascotaModel)
            IdMascota = selectedItem.Id_Mascota
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbDimensionMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As DimensionMascotaModel = CType(comboBox.SelectedItem, DimensionMascotaModel)
            IdDimensionMascota = selectedItem.Id_DimensionMascota
        Catch ex As Exception

        End Try
    End Sub
End Class
