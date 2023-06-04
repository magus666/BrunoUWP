Imports Microsoft.Toolkit.Uwp.Notifications
Imports Microsoft.UI.Xaml.Controls

Public Class Cl_Notificaciones
    Dim GetMetodosDateTime As New Cl_DateTime()

    Public Function NotifiacionToast() As Boolean
        Try
            Dim LlamarNotificacionToast As New ToastContentBuilder()
            LlamarNotificacionToast.SetToastScenario(ToastScenario.Reminder)
            LlamarNotificacionToast.AddArgument("action", "viewEvent")
            LlamarNotificacionToast.AddArgument("eventId", 666)
            LlamarNotificacionToast.AddText("Spa para Perrita Ginebra")
            LlamarNotificacionToast.AddText("Se espera 15 minutos antes")
            LlamarNotificacionToast.AddText(GetMetodosDateTime.ObtenerHoraActual)
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

    Public Function AlertaTeachingTip(TechTipAlerta As TeachingTip, Titulo As String,
                                            Subtitulo As String, Objetivo As FrameworkElement) As TeachingTip
        Try
            TechTipAlerta.Title = Titulo
            TechTipAlerta.Subtitle = Subtitulo
            TechTipAlerta.Target = Objetivo
            TechTipAlerta.IsOpen = True
            Return TechTipAlerta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function AlertaExitoInfoBar(BarraInfo As InfoBar, Titulo As String, Mensaje As String) As InfoBar
        Try
            BarraInfo.IsOpen = True
            BarraInfo.Title = Titulo
            BarraInfo.Message = Mensaje
            BarraInfo.Severity = InfoBarSeverity.Success
            Return BarraInfo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function AlertaErrorInfoBar(BarraInfo As InfoBar, Titulo As String, Mensaje As String) As InfoBar
        Try
            BarraInfo.IsOpen = True
            BarraInfo.Title = Titulo
            BarraInfo.Message = Mensaje
            BarraInfo.Severity = InfoBarSeverity.Error
            Return BarraInfo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function AlertaAdvertenciaInfoBar(BarraInfo As InfoBar, Titulo As String, Mensaje As String) As InfoBar
        Try
            BarraInfo.IsOpen = True
            BarraInfo.Title = Titulo
            BarraInfo.Message = Mensaje
            BarraInfo.Severity = InfoBarSeverity.Warning
            Return BarraInfo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
