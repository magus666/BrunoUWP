' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleCliente
    Inherits Page
    Dim GetCliente As New Cl_Cliente
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim MensajeError As String
    Dim IdCliente As Integer

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)
        Dim DatosCliente As NewPersonaModel = TryCast(e.Parameter, NewPersonaModel)
        If DatosCliente IsNot Nothing Then
            IdCliente = DatosCliente.Id_Cliente
            TxtCodigo.Text = DatosCliente.Codigo_Cliente
            TxtDocumento.Text = DatosCliente.Documento_Persona
            TxtNombres.Text = DatosCliente.Nombre_Persona
            TxtApellidos.Text = DatosCliente.Apellido_Persona
            TxtDireccion.Text = DatosCliente.Direccion_Persona
            TxtTelefono.Text = DatosCliente.Telefono_Persona
            TxtCorreo.Text = DatosCliente.Correo_Persona
            NbbEdad.Value = DatosCliente.Edad_Persona
            TxtSexo.Text = DatosCliente.Nombre_Sexo
            Dim EstadoCliente = DatosCliente.NombreEstado_Cliente
            Select Case EstadoCliente
                Case "Activo"
                    TgsEstadoCliente.IsOn = True
                Case "Inactivo"
                    TgsEstadoCliente.IsOn = False
            End Select
        End If
    End Sub

    Private Sub BtnEditarTelefono_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtTelefono.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Sub BtnEditarDireccion_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtDireccion.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub
    Private Sub BtnEditarCorreo_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtCorreo.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Sub BtnEditarDocumento_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtDocumento.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Sub BtnEditarNombres_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtNombres.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Sub BtnEditarApellidos_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtApellidos.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Sub BtnEditarEdad_Click(sender As Object, e As RoutedEventArgs)
        Try
            NbbEdad.IsEnabled = True
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub

    Private Async Sub BtnActualizarCliente_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim GetDataToggle As Boolean
            If TgsEstadoCliente.IsOn = True Then
                GetDataToggle = True
            Else
                GetDataToggle = False
            End If
            If Await GetCliente.ActualizarCliente(IdCliente, TxtDocumento.Text, TxtNombres.Text,
                                                  TxtApellidos.Text, TxtDireccion.Text, TxtTelefono.Text,
                                                  TxtCorreo.Text, NbbEdad.Value, GetDataToggle) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "Cliente actualizado con Exito.")
            End If
        Catch ex As Exception
            MensajeError = "- [" & Date.Now & "]" & vbCrLf & "Metodo" & vbCrLf & Me.GetType.FullName() & vbCrLf & "Mensaje de Error" & vbCrLf & ex.Message
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", MensajeError)
        End Try
    End Sub


End Class
