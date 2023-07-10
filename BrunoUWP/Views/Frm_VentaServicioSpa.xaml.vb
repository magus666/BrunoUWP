' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.ApplicationModel.Appointments
Imports Windows.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_VentaServicioSpa
    Inherits Page
    Dim getValidaciones As New Cl_Validaciones
    Dim GetNotificacionas As New Cl_Notificaciones
    Dim GetCita As New Cl_Cita
    Dim GetMascota As New Cl_Mascota
    Dim GetPersona As New Cl_Cliente
    Dim GetFechaHora As New Cl_DateTime
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetDimensionMascota As New Cl_DimensionMascota
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetDateTime As New Cl_DateTime
    Dim GetVenta As New Cl_VentaSpa
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim FechaCalendarPicker As String
    Dim IdCita As Integer
    Dim CodigoCita As String
    Dim IdTipoServicio As Integer
    Dim IdMascota As Integer
    Dim IdMetodoPago As Integer
    Dim IdDimensionMascota As Integer
    Dim RetornoValor As Double
    Dim FechaHoraFinServicio As String

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            FechaCalendarPicker = Date.Now.Date.ToShortDateString()
            CdpFechaServicio.Date = Date.Now
            CdpFechaServicio.MinDate = Date.Now

            CmbTIpoServicio.ItemsSource = Await GetTipoServicio.ConsultaTipoServicio
            CmbTIpoServicio.DisplayMemberPath = "Nombre_TipoServicio"

            Dim TraerMascotas = Await GetMascota.ConsultaMascotas
            Dim MascotaOrdenada = (From Mas In TraerMascotas
                                   Order By Mas.Nombre_Mascota
                                   Select Mas)
            CmbMascota.ItemsSource = MascotaOrdenada
            CmbMascota.DisplayMemberPath = "Nombre_Mascota"

            CmbDimensionMascota.ItemsSource = Await GetDimensionMascota.ConsultaDimensionMascota
            CmbDimensionMascota.DisplayMemberPath = "Nombre_DimensionMascota"

            LsvCita.ItemsSource = Await ConsultaCitas()
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Async Function ConsultaCitas() As Task(Of Object)
        Try
            FechaCalendarPicker = CdpFechaServicio.Date.Value.Date.ToShortDateString()
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim Cliente = Await GetPersona.ConsultaCliente()
            Dim Citas = Await GetCita.ConsultarCitas()
            Dim TipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            Dim DimensionMascota = Await GetDimensionMascota.ConsultaDimensionMascota()
            Dim TipoServicio = Await GetTipoServicio.ConsultaTipoServicio()
            Dim GetRetornoCitas = (From Cit In Citas
                                   Join Mas In Mascota On
                                       Cit.Id_Mascota Equals Mas.Id_Mascota
                                   Join Tms In TipoMascota On
                                        Mas.Id_TipoMascota Equals Tms.Id_TipoMascota
                                   Join Cli In Cliente On
                                       Mas.Id_Persona Equals Cli.Id_Persona
                                   Join Tps In TipoServicio On
                                       Cit.Id_TipoServicio Equals Tps.Id_TipoSerivicio
                                   Join Dsm In DimensionMascota On
                                       Cit.Id_DimensionMascota Equals Dsm.Id_DimensionMascota
                                   Where Cit.FechaHoraInicio_Cita.Date.ToShortDateString() = FechaCalendarPicker
                                   Order By Cit.FechaHoraInicio_Cita
                                   Select New NewCitaModel With {.Id_Cita = Cit.Id_Cita,
                                                                 .Nombre_Cliente = Cli.NombreCompleto_Persona,
                                                                 .Nombre_Mascota = Mas.Nombre_Mascota,
                                                                 .Nombre_TipoMascota = Tms.Nombre_TipoMascota,
                                                                 .Codigo_Cita = Cit.Codigo_Cita,
                                                                 .Id_TipoServicio = Tps.Id_TipoSerivicio,
                                                                 .Nombre_TipoServicio = Tps.Nombre_TipoServicio,
                                                                 .Id_DimensionMascota = Dsm.Id_DimensionMascota,
                                                                 .Nombre_DimensionMascota = Dsm.Nombre_DimensionMascota,
                                                                 .FechaInicio_Cita = Cit.FechaHoraInicio_Cita.Date.ToShortDateString(),
                                                                 .HoraInicio_Cita = Cit.FechaHoraInicio_Cita.ToString("hh:mm tt"),
                                                                 .FechaFin_Cita = Cit.FechaHoraFin_Cita.Date.ToShortDateString(),
                                                                 .HoraFin_Cita = Cit.FechaHoraFin_Cita.ToString("hh:mm tt"),
                                                                 .Estado_Cita = If(Cit.Estado_Cita, "Activo", "Inactivo"),
                                                                 .EstadoVenta_Cita = If(Cit.EstadoVenta_Cita, "Pagada", "Por Pagar")}).ToList

            Return GetRetornoCitas
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub BtnComprobar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoCita As String = GetUtilitarios.GenearCodigoCita
            Dim HoraServicio As String = TmpHoraServicio.SelectedTime.ToString
            Dim HoraTimeSpan = TmpHoraServicio.SelectedTime
            Dim FechaHoraIncioCita As String = FechaCalendarPicker & " " & HoraServicio
            If ValidaDatos() = True Then
                Dim FechaHoraFinCita As String = ObtenerHoraFinalServicio()
                If Await GetCita.InsertCita(CodigoCita, FechaHoraIncioCita, FechaHoraFinCita, False, IdMascota, IdDimensionMascota, IdTipoServicio, 1, False) = True Then
                    GetNotificacionas.AlertaExitoInfoBar(InfAlerta, "Exito", "La cita Se Agrego Correctamente")
                    Await ValidaCitas()
                    LsvCita.ItemsSource = Await ConsultaCitas()
                End If
            End If
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ValidaDatos() As Boolean
        If getValidaciones.ValidaComboBoxVacio(CmbTIpoServicio) = False Then
            GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un Tipo de Servicio valido", CmbTIpoServicio)
            Return False
            Exit Function
        End If
        If getValidaciones.ValidaComboBoxVacio(CmbMascota) = False Then
            GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione una mascota valida", CmbMascota)
            Return False
            Exit Function
        End If
        If getValidaciones.ValidaComboBoxVacio(CmbDimensionMascota) = False Then
            GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione una dimension valida", CmbDimensionMascota)
            Return False
            Exit Function
        End If
        If CdpFechaServicio.Date = Nothing Then
            GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione una Hora Correcta", CdpFechaServicio)
            Return False
        End If
        If TmpHoraServicio.SelectedTime Is Nothing Then
            GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione una Hora Correcta", TmpHoraServicio)
            Return False
            Exit Function
        End If
        Return True
    End Function

    Private Async Sub CdpFechaServicio_DateChanged(sender As CalendarDatePicker, args As CalendarDatePickerDateChangedEventArgs)
        Try
            Dim FechaCdp = CdpFechaServicio.Date
            LblTituloCitas.Text = GetFechaHora.MostrarFechaLarga(FechaCdp.ToString)

            Await ValidaCitas()

            LsvCita.ItemsSource = Await ConsultaCitas()
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Async Function ValidaCitas() As Task
        Try
            Dim resultado = Await ConsultaCitas()
            Dim listaDeResultados = CType(resultado, List(Of NewCitaModel))
            Dim cantidadDeRegistros = listaDeResultados.Count
            If cantidadDeRegistros = 0 Then
                StpContenidoCitas.Visibility = Visibility.Visible
                ScvCitas.Visibility = Visibility.Collapsed
                GrdDetalleServicio.Visibility = Visibility.Collapsed
            Else
                StpContenidoCitas.Visibility = Visibility.Collapsed
                ScvCitas.Visibility = Visibility.Visible
                LblCantidadCitas.Text = "Cantidad de Citas: " & cantidadDeRegistros
                GrdDetalleServicio.Visibility = Visibility.Visible
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Private Sub CmbTIpoServicio_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoServicioModel = CType(comboBox.SelectedItem, TipoServicioModel)
            IdTipoServicio = selectedItem.Id_TipoSerivicio
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As MascotaModel = CType(comboBox.SelectedItem, MascotaModel)
            IdMascota = selectedItem.Id_Mascota
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbDimensionMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As DimensionMascotaModel = CType(comboBox.SelectedItem, DimensionMascotaModel)
            IdDimensionMascota = selectedItem.Id_DimensionMascota
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub LsvCita_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            StpMensajeServicio.Visibility = Visibility.Collapsed
            StpFinalizarServicio.Visibility = Visibility.Visible

            Dim GetCitasModel As New NewCitaModel
            GetCitasModel = e.ClickedItem

            IdCita = GetCitasModel.Id_Cita
            Dim IdDimenensionMascota = GetCitasModel.Id_DimensionMascota
            Dim IdTipoServicioCIta = GetCitasModel.Id_TipoServicio

            CodigoCita = GetCitasModel.Codigo_Cita

            LblTipoMascota.Text = "Tipo de Mascota: " & GetCitasModel.Nombre_TipoMascota
            LblNombreMascota.Text = "Nombre de la Mascota: " & GetCitasModel.Nombre_Mascota
            LblNombreCliente.Text = "Dueño: " & GetCitasModel.Nombre_Cliente
            LblDimension.Text = "Tamaño: " & GetCitasModel.Nombre_DimensionMascota
            LblFechaInicio.Text = "Fecha: " & GetCitasModel.FechaInicio_Cita
            LblHoraInicio.Text = "Hora de Inicio: " & GetCitasModel.HoraInicio_Cita
            LblHoraFin.Text = "Hora estimada de Finalizacion: " & GetCitasModel.HoraFin_Cita
            LblServicio.Text = "Servicio: " & GetCitasModel.Nombre_TipoServicio
            If GetCitasModel.EstadoVenta_Cita = "Pagada" Then
                LblEstadoVenta.Foreground = New SolidColorBrush(Colors.Green)
            Else
                LblEstadoVenta.Foreground = New SolidColorBrush(Colors.Yellow)
            End If
            LblEstadoVenta.Text = GetCitasModel.EstadoVenta_Cita
            If GetCitasModel.EstadoVenta_Cita = "Por Pagar" Then
                BtnFinalizarServicio.Visibility = Visibility.Visible
            Else
                BtnFinalizarServicio.Visibility = Visibility.Collapsed
            End If

            LblValorTotal.Text = CalculaValorServicio(IdTipoServicioCIta, IdDimenensionMascota).ToString("C0")


        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function CalculaValorServicio(TipoServicio As Integer, Optional DimensionoPerro As Integer = Nothing) As Integer
        Try
            Select Case TipoServicio
                Case 1
                    RetornoValor = 4000
                Case 2
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = 15000
                        Case 2
                            RetornoValor = 22000
                        Case 3
                            RetornoValor = 30000
                    End Select
                Case 3
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = 20000
                        Case 2
                            RetornoValor = 35000
                        Case 3
                            RetornoValor = 50000
                    End Select
                Case 4
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = 15000
                        Case 2
                            RetornoValor = 22000
                        Case 3
                            RetornoValor = 30000
                    End Select
            End Select
            Return RetornoValor
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub BtnFinalizarServicio_Click(sender As Object, e As RoutedEventArgs)
        Try
            CmbMetodoPago.ItemsSource = Await GetMetodoPago.ConsultaMetodoPago
            CmbMetodoPago.DisplayMemberPath = "Nombre_MetodoPago"
            LblValorTotalPago.Text = RetornoValor.ToString("c")
            CtdFinalizaPago.IsPrimaryButtonEnabled = False
            Await CtdFinalizaPago.ShowAsync()
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CtdFinalizaPago_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            Dim CodigoVenta As String = GetUtilitarios.GenearCodigoVenta()
            Dim GetIdCita = IdCita
            If Await GetVenta.InsertVentaSpa(CodigoVenta, GetDateTime.ObtenerFechaActual, IdTipoServicio, 1,
                                          IdMascota, IdMetodoPago, RetornoValor) = True Then

                Await GetCita.ActualizarCita(GetIdCita, True)

                LsvCita.ItemsSource = Await ConsultaCitas()
                LblEstadoVenta.Foreground = New SolidColorBrush(Colors.Green)
                LblEstadoVenta.Text = "Pagada"
                BtnFinalizarServicio.Visibility = Visibility.Collapsed
                GetNotificacionas.AlertaExitoInfoBar(InfAlerta, "Exito", "El Pago Se realizó Correctamente")
            End If
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbMetodoPago_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            If CmbMetodoPago.SelectedItem IsNot Nothing Then
                CtdFinalizaPago.IsPrimaryButtonEnabled = True
            End If
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As MetodoPagoModel = CType(comboBox.SelectedItem, MetodoPagoModel)
            IdMetodoPago = selectedItem.Id_MetodoPago

        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ObtenerHoraFinalServicio() As String
        Try
            Dim HoraInicio As TimeSpan = TmpHoraServicio.SelectedTime

            Select Case IdTipoServicio
                Case 1
                    Dim HoraFin = HoraInicio.Add(New TimeSpan(1, 0, 0))
                    FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                Case 2
                    Select Case IdDimensionMascota
                        Case 1
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(1, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 2
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(2, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 3
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(3, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                    End Select
                Case 3
                    Select Case IdDimensionMascota
                        Case 1
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(2, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 2
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(3, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 3
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(4, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                    End Select
                Case 4
                    Select Case IdDimensionMascota
                        Case 1
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(1, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 2
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(2, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                        Case 3
                            Dim HoraFin = HoraInicio.Add(New TimeSpan(3, 0, 0))
                            FechaHoraFinServicio = FechaCalendarPicker & " " & HoraFin.ToString
                    End Select
            End Select
            Return FechaHoraFinServicio
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
