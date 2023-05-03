Imports Microsoft.Toolkit.Uwp.Notifications

Public Class Cl_Notificaciones

    Public Function NotifiacionToast() As Boolean
        Try
            Dim LlamarNotificacionToast As New ToastContentBuilder()
            LlamarNotificacionToast.SetToastScenario(ToastScenario.Reminder)
            LlamarNotificacionToast.AddArgument("action", "viewEvent")
            LlamarNotificacionToast.AddArgument("eventId", 666)
            LlamarNotificacionToast.AddText("Spa para Perrita Ginebra")
            LlamarNotificacionToast.AddText("Se espera 15 minutos antes")
            LlamarNotificacionToast.AddText(Date.Now)
            LlamarNotificacionToast.AddComboBox("TiempoEspera", "15", ("1", "1 Munto"),
                                                                      ("5", "5 Munutos"),
                                                                      ("10", "10 Munutos"),
                                                                      ("15", "15 Minutos"))
            LlamarNotificacionToast.AddButton(New ToastButton().SetSnoozeActivation("TiempoEspera"))
            LlamarNotificacionToast.AddButton(New ToastButton().SetDismissActivation).Show()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

End Class
