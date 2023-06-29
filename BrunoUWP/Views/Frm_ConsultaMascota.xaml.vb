' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI.Controls
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

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await LlenarListview()
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function LlenarListview() As Task
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
                                  Select New With {Mas.Codigo_Mascota,
                                                   Tmc.Nombre_TipoMascota,
                                                   Rza.Nombre_Raza,
                                                   Mas.Nombre_Mascota,
                                                   Mas.Edad_Mascota,
                                                   Ppo.NombreCompleto_Persona,
                                                   Mas.Observaciones_Mascota,
                                                   .FechaRegistro = Mas.FechaRegistro_Mascota.ToShortDateString}).ToList
            LsvMascota.ItemsSource = RetornoMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

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

    Private Sub AsbBusueda_TextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)

    End Sub

    Private Sub LsvMascota_ItemClick(sender As Object, e As ItemClickEventArgs)

    End Sub
End Class
