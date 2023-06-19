' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaCliente
    Inherits Page
    Dim GetCliente As New Cl_Cliente
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetSexo As New Cl_Sexo
    Dim ListadoFinalClientes As IEnumerable(Of Object)

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim Clientes = Await GetCliente.ConsultaCliente()
            Dim Sexo = Await GetSexo.ConsultaSexo()
            Dim ListadoCliente = (From Cli In Clientes
                                  Join Sex In Sexo On
                                       Cli.Id_Sexo Equals Sex.Id_Sexo
                                  Select New NewPersonaModel With {.Id_Cliente = Cli.Id_Persona,
                                                                   .Codigo_Cliente = Cli.Codigo_Cliente,
                                                                    .Documento_Persona = Cli.Documento_Persona,
                                                                    .NombreCompleto_Persona = Cli.NombreCompleto_Persona,
                                                                    .Direccion_Persona = Cli.Direccion_Persona,
                                                                    .Telefono_Persona = Cli.Telefono_Persona,
                                                                    .Correo_Persona = Cli.Correo_Persona,
                                                                    .Edad_Persona = Cli.Edad_Persona,
                                                                    .Nombre_Sexo = Sex.Nombre_Sexo,
                                                                    .NombreEstado_Cliente = If(Cli.Estado_Cliente, "Activo", "Inactivo")})
            ListadoFinalClientes = ListadoCliente.OrderBy(Function(cliente)
                                                              Return cliente.NombreCompleto_Persona
                                                          End Function).ToList()
            Dim ConteoClientes As Integer = ListadoFinalClientes.Count
            Dim SumaEdades As Integer = ListadoFinalClientes.Sum(Function(cliente)
                                                                     Return cliente.Edad_Persona
                                                                 End Function)
            Dim PromedioEdades = SumaEdades / ConteoClientes
            LsvCliente.ItemsSource = ListadoFinalClientes
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub AsbBusueda_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
        Try
            If AsbBusueda.Text.Count = 0 Then
                LsvCliente.ItemsSource = ListadoFinalClientes
            Else
                If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                    Dim ListaClientes = Await GetCliente.ConsultaCliente()
                    Dim RetornoListaClientes = (From x In ListaClientes
                                                Where x.Documento_Persona.Contains(AsbBusueda.Text)
                                                Select x).ToList
                    LsvCliente.ItemsSource = RetornoListaClientes
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LsvCliente_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            Dim GetClienteModel As New NewPersonaModel
            GetClienteModel = e.ClickedItem
            Frame.Navigate(GetType(Frm_DetalleCliente), GetClienteModel)
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            Await GetCliente.CreaExcelCliente
            PgrGeneraExcel.IsActive = False
            GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a excel")
        Catch ex As Exception

        End Try

    End Sub
End Class
