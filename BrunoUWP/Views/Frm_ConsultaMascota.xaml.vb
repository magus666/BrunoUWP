' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaMascota
    Inherits Page
    Dim GetMascota As New Cl_Mascota
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetRaza As New Cl_RazaMascota
    Dim GetPersona As New Cl_Cliente
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim ListadoFinalMascotas As IEnumerable(Of Object)

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            ListadoFinalMascotas = Await GetListaMascota()
            If ListadoFinalMascotas.Count = 0 Then
                LblTituloCreacionMascota.Visibility = Visibility.Visible
                LsvMascota.Visibility = Visibility.Collapsed
            Else
                LsvMascota.ItemsSource = ListadoFinalMascotas
                LblTituloCreacionMascota.Visibility = Visibility.Collapsed
                LsvMascota.Visibility = Visibility.Visible
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            If Await GetMascota.CrearExcelMascota = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub AsbBusueda_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
        Try
            If AsbBusueda.Text.Count = 0 Then
                LsvMascota.ItemsSource = ListadoFinalMascotas
            Else
                If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                    Dim ListaMascota = Await GetListaMascota()
                    Dim RetornoListaMascotas = (From Mas In ListaMascota
                                                Where Mas.Nombre_Mascota.StartsWith(AsbBusueda.Text,
                                                        StringComparison.OrdinalIgnoreCase)
                                                Select Mas)
                    LsvMascota.ItemsSource = RetornoListaMascotas
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function GetListaMascota() As Task(Of List(Of NewMascotaModel))
        Try
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim TipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            Dim Raza = Await GetRaza.ConsultarRazaMascota()
            Dim Propietario = Await GetPersona.ConsultaCliente()

            Dim RetornoMascota = (From Mas In Mascota
                                  Join Tmc In TipoMascota On
                                           Mas.Id_TipoMascota Equals Tmc.Id_TipoMascota
                                  Join Rza In Raza On
                                           Mas.Id_Raza Equals Rza.Id_Raza
                                  Join Ppo In Propietario On
                                           Mas.Id_Persona Equals Ppo.Id_Persona
                                  Order By Mas.Nombre_Mascota
                                  Select New NewMascotaModel With {.Id_Mascota = Mas.Id_Mascota,
                                          .Codigo_Mascota = Mas.Codigo_Mascota,
                                          .Nombre_TipoMascota = Tmc.Nombre_TipoMascota,
                                          .Nombre_Raza = Rza.Nombre_Raza,
                                          .Nombre_Mascota = Mas.Nombre_Mascota,
                                          .Edad_Mascota = Mas.Edad_Mascota,
                                          .NombreCompleto_Persona = Ppo.NombreCompleto_Persona,
                                          .Observaciones_Mascota = Mas.Observaciones_Mascota,
                                          .FechaRegistro_Mascota = Mas.FechaRegistro_Mascota}).ToList
            Return RetornoMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub LsvMascota_ItemClick(sender As Object, e As ItemClickEventArgs)
        Try
            Dim GetMascota As New NewMascotaModel
            GetMascota = e.ClickedItem
            Frame.Navigate(GetType(Frm_DetalleMascota), GetMascota)
        Catch ex As Exception

        End Try
    End Sub
End Class
