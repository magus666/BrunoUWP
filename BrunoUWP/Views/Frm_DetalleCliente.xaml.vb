' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleCliente
    Inherits Page

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)
        Dim DatosCliente As NewPersonaModel = TryCast(e.Parameter, NewPersonaModel)
        If DatosCliente IsNot Nothing Then
            TxtCodigo.Text = DatosCliente.Codigo_Cliente
            TxtDocumento.Text = DatosCliente.Documento_Persona
            TxtNombreCompleto.Text = DatosCliente.NombreCompleto_Persona
            TxtDireccion.Text = DatosCliente.Direccion_Persona
            TxtTelefono.Text = DatosCliente.Telefono_Persona
            TxtCorreo.Text = DatosCliente.Correo_Persona
            TxtEdad.Text = DatosCliente.Edad_Persona
            TxtSexo.Text = DatosCliente.Nombre_Sexo
        End If
    End Sub

    Private Sub BtnEditarTelefono_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtTelefono.IsEnabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnEditarDireccion_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtDireccion.IsEnabled = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BtnEditarCorreo_Click(sender As Object, e As RoutedEventArgs)
        Try
            TxtCorreo.IsEnabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class
