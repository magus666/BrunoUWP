' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConfiguracionGlobal
    Inherits Page
    Dim GetPickers As New Cl_Pickers
    Dim GetNotificaciones As New Cl_Notificaciones

    Private Async Sub BtnRespaldar_Click(sender As Object, e As RoutedEventArgs)
        Try
            If Await BackUpDatabase() = True Then
                PgrBackUpBd.IsActive = True
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Base de Datos se Importó Correctamnete")
            Else
                PgrBackUpBd.IsActive = True
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "´La Base de Datos no se Importó.")
            End If
            PgrBackUpBd.IsActive = False
        Catch ex As Exception

        End Try

    End Sub
End Class
