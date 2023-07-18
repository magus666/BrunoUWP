' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CrearCliente
    Inherits Page
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetCliente As New Cl_Cliente
    Dim GetIntegracionWhatsapp As New Cl_IntegracionWhatsapp
    Dim MensajeError As String
    Dim newViewId As Integer = 0
    Public GetSexo As New Cl_Sexo
    Dim IdSexo As Integer

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            ValoresIniciales()
            Dim CodigoAleatrorio As String = GetUtilitarios.GenerarCodigoCliente()
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlertaError, "Error", MensajeError)
        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim MensajeWha As String
            Dim CodigoCliente As String = GetUtilitarios.GenerarCodigoCliente()
            If ValidaDatos() = True Then
                PgrGuardarCliente.IsActive = True
                If Await GetCliente.InsertarCliente(TxtDocumento.Text, TxtNombres.Text, TxtApellidos.Text,
                                                     TxtDireccion.Text, TxtTelefono.Text, TxtCorreo.Text, NbbEdad.Text,
                                                     1, CodigoCliente, True) = True Then
                    Dim NombreCompleto = TxtNombres.Text & " " & TxtApellidos.Text
                    MensajeWha = "Bienvenida/o " & NombreCompleto & " " & "y gracias por ser parte del Club Bruno Spa.
                                                                       Te esperan los mejores servicios y las mejores ofertas para el cuidado de tu peludito.
                                                                       Tu codigo de Cliente es: " & CodigoCliente & "."
                    Await GetIntegracionWhatsapp.EnviaMensajeWhatsapp(TxtTelefono.Text, MensajeWha)
                    GetUtilitarios.LimpiarControles(StpDatosCliente)
                    PgrGuardarCliente.IsActive = False
                    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "El cliente se ha guardado con Exito")
                Else
                    GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Atemcion", "El cliente no puedo ser Guardado.")
                End If
            End If
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlertaError, "Error", MensajeError)
        End Try
    End Sub

    Public Sub ValoresIniciales()
        Dim DocumentoCliente As String = GetUtilitarios.GenerarNumeroDocumento()
        TxtDocumento.Text = DocumentoCliente
        TxtDireccion.Text = "N/A"
        TxtCorreo.Text = "N/A"
        NbbEdad.Value = 18
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaTextBoxVacio(TxtDocumento) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Documento no puede estar Vacio", TxtDocumento)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtNombres) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Nombre no puede estar Vacio", TxtNombres)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtApellidos) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Apellido no puede estar Vacio", TxtApellidos)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtDireccion) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La Direccion no puede estar Vacia", TxtDireccion)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtTelefono) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Telefono no puede estar Vacio", TxtTelefono)
                Return False
                Exit Function
            End If

            If GetValidaciones.ValidarNumeroTelefonico(TxtTelefono.Text) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Telefono no tiene el Formato correcto", TxtTelefono)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtCorreo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Correo no puede estar Vacio", TxtCorreo)
                Return False
                Exit Function
            End If
            If NbbEdad.Text < 14 Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "No puede Ser Menor de 14 años", NbbEdad)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub BtnInforme_Click(sender As Object, e As RoutedEventArgs)
        Await GetUtilitarios.CrearLogErrores(MensajeError & vbCrLf)
    End Sub

    Private Sub BtnEditarDocumento_Click(sender As Object, e As RoutedEventArgs)
        TxtDocumento.IsEnabled = True
    End Sub

    Private Sub BtnEditarDireccion_Click(sender As Object, e As RoutedEventArgs)
        TxtDireccion.IsEnabled = True
    End Sub

    Private Sub BtnEditarCorreo_Click(sender As Object, e As RoutedEventArgs)
        TxtCorreo.IsEnabled = True
    End Sub

    Private Sub BtnEditarEdad_Click(sender As Object, e As RoutedEventArgs)
        NbbEdad.IsEnabled = True
    End Sub
End Class
