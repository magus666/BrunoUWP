' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
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
            ListadoFinalClientes = Await GetCliente.ConsultaCliente()
            If ListadoFinalClientes.Count = 0 Then
                LblTituloCreacionCliente.Visibility = Visibility.Visible
                LsvCliente.Visibility = Visibility.Collapsed
            Else
                LsvCliente.ItemsSource = ListadoFinalClientes
                LblTituloCreacionCliente.Visibility = Visibility.Collapsed
                LsvCliente.Visibility = Visibility.Visible
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub AsbBusueda_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
        Try
            Dim GetListaClienteFiltro = Await GetCliente.ConsultaCliente()

            If AsbBusueda.Text.Count = 0 Then
                LsvCliente.ItemsSource = ListadoFinalClientes
            Else
                Dim GetIndexRadioCliente As Integer = RdbFiltroBusquedaCliente.SelectedIndex
                Select Case GetIndexRadioCliente
                    Case 0
                        If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                            Dim RetornoListaBusqueda = (From Cli In GetListaClienteFiltro
                                                        Where Cli.Documento_Persona.Contains(AsbBusueda.Text)
                                                        Select Cli).ToList
                            LsvCliente.ItemsSource = RetornoListaBusqueda
                        End If
                    Case 1
                        If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                            Dim RetornoListaBusqueda = (From Cli In GetListaClienteFiltro
                                                        Where Cli.Codigo_Cliente.StartsWith(AsbBusueda.Text,
                                                        StringComparison.OrdinalIgnoreCase)
                                                        Select Cli).ToList
                            LsvCliente.ItemsSource = RetornoListaBusqueda
                        End If
                    Case 2
                        If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                            Dim RetornoListaBusqueda = (From Cli In GetListaClienteFiltro
                                                        Where Cli.NombreCompleto_Persona.StartsWith(AsbBusueda.Text,
                                                        StringComparison.OrdinalIgnoreCase)
                                                        Select Cli).ToList
                            LsvCliente.ItemsSource = RetornoListaBusqueda
                        End If
                    Case 3
                        If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                            Dim RetornoListaBusqueda = (From Cli In GetListaClienteFiltro
                                                        Where Cli.Telefono_Persona.Contains(AsbBusueda.Text)
                                                        Select Cli).ToList
                            LsvCliente.ItemsSource = RetornoListaBusqueda
                        End If
                End Select

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
            If Await GetCliente.CreaExcelCliente = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub RdbFiltroBusquedaCliente_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Documento"
                    AsbBusueda.PlaceholderText = "Digite Documento"
                Case "Codigo"
                    AsbBusueda.PlaceholderText = "Digite Codigo"
                Case "Nombre"
                    AsbBusueda.PlaceholderText = "Digite Nombre"
                Case "Telefono"
                    AsbBusueda.PlaceholderText = "Digite Telefono"
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
