﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.Media.Protection.PlayReady
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleMascota
    Inherits Page
    Dim GetMascota As New Cl_Mascota
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim IdMascota As Integer

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)
        Dim DatosMascota As NewMascotaModel = TryCast(e.Parameter, NewMascotaModel)
        If DatosMascota IsNot Nothing Then
            IdMascota = DatosMascota.Id_Mascota
            TxtCodigo.Text = DatosMascota.Codigo_Mascota
            TxtRaza.Text = DatosMascota.Nombre_Raza
            TxtNombre.Text = DatosMascota.Nombre_Mascota
            NbbEdad.Value = DatosMascota.Edad_Mascota
            TxtPropietario.Text = DatosMascota.NombreCompleto_Persona
            TxtObservaciones.Text = DatosMascota.Observaciones_Mascota
        End If
    End Sub

    Private Sub BtnEditarEdad_Click(sender As Object, e As RoutedEventArgs)
        NbbEdad.IsEnabled = True
    End Sub

    Private Sub BtnEditarNombre_Click(sender As Object, e As RoutedEventArgs)
        TxtNombre.IsEnabled = True
    End Sub

    Private Sub BtnEditarObservaciones_Click(sender As Object, e As RoutedEventArgs)
        TxtObservaciones.IsEnabled = True
    End Sub

    Private Async Sub BtnActualizarMascota_Click(sender As Object, e As RoutedEventArgs)
        Try
            If Await GetMascota.ActualizarMascota(IdMascota, TxtNombre.Text, NbbEdad.Value, TxtObservaciones.Text) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Mascota ha sido Actualizada.")
                VolverAtras()
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Sub VolverAtras()
        If Frame.CanGoBack Then
            Frame.GoBack()
        End If
    End Sub


End Class