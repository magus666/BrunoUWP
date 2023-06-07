' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.ServiceModel.Security
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaCliente
    Inherits Page
    Dim GetCliente As New Cl_Cliente
    Dim GetSexo As New Cl_Sexo

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim Clientes = Await GetCliente.ConsultaCliente()
            Dim Sexo = Await GetSexo.ConsultaSexo()
            Dim ListadoCliente = (From Cli In Clientes
                                  Join Sex In Sexo On
                                       Cli.Id_Sexo Equals Sex.Id_Sexo
                                  Select New With {Cli.Codigo_Cliente,
                                                   Cli.Documento_Persona,
                                                   Cli.NombreCompleto_Persona,
                                                   Cli.Direccion_Persona,
                                                   Cli.Telefono_Persona,
                                                   Cli.Correo_Persona,
                                                   Cli.Edad_Persona,
                                                   Sex.Nombre_Sexo,
                                                   .Estado_Cliente = If(Cli.Estado_Cliente, "Activo", "Inactivo")})
            LsvCliente.ItemsSource = ListadoCliente.OrderBy(Function(cliente)
                                                             Return cliente.NombreCompleto_Persona
                                                            End Function).ToList()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub LsvCliente_DoubleTapped(sender As Object, e As DoubleTappedRoutedEventArgs)
        Try
            Dim Mensaje = "Perrita"
            Dim Escucha = Mensaje
        Catch ex As Exception

        End Try
    End Sub
End Class
