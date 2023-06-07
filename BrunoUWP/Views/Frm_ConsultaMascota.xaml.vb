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

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await LlenarGrilla()
        Catch ex As Exception

        End Try
    End Sub

    Public Async Function LlenarGrilla() As Task
        Dim Mascota = Await GetMascota.ConsultaMascotas()
        Dim TipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
        Dim Raza = Await GetRaza.ConsultarRazaMascota()
        Dim Propietario = Await GetPersona.ConsultaCliente()

        Dim RetornoMascota = (From Mas In Mascota
                              Join Tmc In TipoMascota On
                                   Mas.Tipo_Mascota Equals Tmc.Id_TipoMascota
                              Join Rza In Raza On
                                   Mas.Raza_Mascota Equals Rza.Id_Raza
                              Join Ppo In Propietario On
                                   Mas.Propietario_Mascota Equals Ppo.Id_Persona
                              Select New With {Tmc.Nombre_TipoMascota,
                                               Rza.Nombre_Raza,
                                               Mas.Nombre_Mascota,
                                               Mas.Edad_Mascota,
                                               Ppo.NombreCompleto_Persona,
                                               Mas.Observaciones_Mascota,
                                               Mas.FechaRegistro_Mascota})

        DtgMovimientoEquipos.ItemsSource = RetornoMascota
    End Function
End Class
