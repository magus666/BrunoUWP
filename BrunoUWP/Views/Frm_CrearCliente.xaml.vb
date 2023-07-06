﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CrearCliente
    Inherits Page
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetNotificacionas As New Cl_Notificaciones
    Dim GetCliente As New Cl_Cliente
    Dim GetIntegracionWhatsapp As New Cl_IntegracionWhatsapp
    Dim newViewId As Integer = 0
    Public GetSexo As New Cl_Sexo
    Dim IdSexo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await GetSexo.InsertaActualizaSexo()
            CmbSexo.ItemsSource = Await GetSexo.ConsultaSexo()
            CmbSexo.DisplayMemberPath = "Nombre_Sexo"
            Dim CodigoAleatrorio As String = GetUtilitarios.GenerarCodigoCliente()
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim MensajeWha As String
            Dim CodigoCliente As String = GetUtilitarios.GenerarCodigoCliente()
            If GetValidaciones.ValidaTextBoxVacio(TxtDocumento) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Documento no puede estar Vacio", TxtDocumento)
                Exit Sub
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtNombres) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Nombre no puede estar Vacio", TxtNombres)
                Exit Sub
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtApellidos) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Apellido no puede estar Vacio", TxtApellidos)
                Exit Sub
            End If
            If GetValidaciones.ValidarApellidos(TxtApellidos.Text) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Debe contener dos Apellidos Validos", TxtApellidos)
                Exit Sub
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtDireccion) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La Direccion no puede estar Vacia", TxtDireccion)
                Exit Sub
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtTelefono) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Telefono no puede estar Vacio", TxtTelefono)
                Exit Sub
            End If

            If GetValidaciones.ValidarNumeroTelefonico(TxtTelefono.Text) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Telefono no tiene el Formato correcto", TxtTelefono)
                Exit Sub
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtCorreo) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Correo no puede estar Vacio", TxtCorreo)
                Exit Sub
            End If
            If GetValidaciones.ValidaCorreo(TxtCorreo.Text) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El Correo no Tiene el formato Correcto", TxtCorreo)
                Exit Sub
            End If

            If NbbEdad.Text < 14 Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "No puede Ser Menor de 14 años", NbbEdad)
                Exit Sub
            End If
            If GetValidaciones.ValidaComboBoxVacio(CmbSexo) = False Then
                GetNotificacionas.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un Sexo Correcto", CmbSexo)
                Exit Sub
            End If
            PgrGuardarCliente.IsActive = True
            If Await GetCliente.InsertarCliente(TxtDocumento.Text, TxtNombres.Text, TxtApellidos.Text,
                                                 TxtDireccion.Text, TxtTelefono.Text, TxtCorreo.Text, NbbEdad.Text,
                                                 IdSexo, CodigoCliente, True) = True Then
                Dim NombreCompleto = TxtNombres.Text & " " & TxtApellidos.Text
                MensajeWha = "Bienvenida/o " & NombreCompleto & " " & "y gracias por ser parte del Club Bruno Spa.
                                                                       Te esperan los mejores servicios y las mejores ofertas para el cuidado de tu peludito.
                                                                       Tu codigo de Cliente es: " & CodigoCliente & "."
                Await GetIntegracionWhatsapp.EnviaMensajeWhatsapp(TxtTelefono.Text, MensajeWha)
                GetUtilitarios.LimpiarControles(StpDatosCliente)
                PgrGuardarCliente.IsActive = False
                GetNotificacionas.AlertaExitoInfoBar(InfAlerta, "Exito", "El cliente se ha guardado con Exito")
            Else
                GetNotificacionas.AlertaAdvertenciaInfoBar(InfAlerta, "Atemcion", "El cliente no puedo ser Guardado.")
            End If
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub CmbSexo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim ComboBoxSexo As ComboBox = CType(sender, ComboBox)
            Dim ItemSeleccionado As SexoModel = CType(ComboBoxSexo.SelectedItem, SexoModel)
            If CmbSexo.SelectedIndex = -1 Then
                IdSexo = 0
            Else
                IdSexo = ItemSeleccionado.Id_Sexo
            End If
        Catch ex As Exception
            GetNotificacionas.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
End Class
