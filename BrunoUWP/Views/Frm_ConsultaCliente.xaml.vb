﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaCliente
    Inherits Page
    Dim GetCliente As New Cl_Cliente
    Dim GetSexo As New Cl_Sexo
    Dim ListadoFinalClientes As IEnumerable(Of Object)

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
            ListadoFinalClientes = ListadoCliente.OrderBy(Function(cliente)
                                                              Return cliente.NombreCompleto_Persona
                                                          End Function).ToList()
            LsvCliente.ItemsSource = ListadoFinalClientes
        Catch ex As Exception

        End Try

    End Sub
    Private Sub LsvCliente_DoubleTapped(sender As Object, e As DoubleTappedRoutedEventArgs)
        Try
            Dim frame As Frame = FindParent(Of Frame)(Me)

            ' Navega a la página de detalles del cliente
            frame.Navigate(GetType(Frm_DetalleCliente))
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

    Private Function FindParent(Of T As DependencyObject)(child As DependencyObject) As T
        ' Obtiene el padre del objeto actual
        Dim parent As DependencyObject = VisualTreeHelper.GetParent(child)

        ' Si el padre es del tipo buscado, lo devuelve
        If TypeOf parent Is T Then
            Return CType(parent, T)
        End If

        ' Si no se ha encontrado el padre buscado, busca en el siguiente nivel
        Return FindParent(Of T)(parent)
    End Function
End Class
