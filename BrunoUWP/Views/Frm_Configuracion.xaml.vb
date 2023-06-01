' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
Imports Windows.UI
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Configuracion
    Inherits Page

    Private Async Sub TglInicioAplicacion_Toggled(sender As Object, e As RoutedEventArgs)
        Try
            Dim startupTask As StartupTask = Await StartupTask.GetAsync("LaunchOnStartupTaskId")
            Dim EstadoStartup = startupTask.State.ToString
            If TglInicioAplicacion.IsOn = True Then
                Select Case EstadoStartup
                    Case "Disabled"
                        Dim newState As StartupTaskState = Await startupTask.RequestEnableAsync()
                        Debug.WriteLine("Request to enable startup, result = {0}", newState)
                End Select
            End If
            If TglInicioAplicacion.IsOn = False Then
                Select Case EstadoStartup
                    Case "Enabled"
                        startupTask.Disable()
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim startupTask As StartupTask = Await StartupTask.GetAsync("LaunchOnStartupTaskId")
            Dim Retorno = startupTask.State.ToString
            Select Case Retorno
                Case "Disabled"
                    TglInicioAplicacion.IsOn = False
                Case "Enabled"
                    TglInicioAplicacion.IsOn = True
                Case Else
                    TglInicioAplicacion.IsOn = False
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RdbTema_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Claro"
                    CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Light
                Case "Oscuro"
                    CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Dark
                Case "Configuracion del Sistema"
                    CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Default
            End Select

        Catch ex As Exception
            Dim MensajeError = ex.Message
        End Try
    End Sub
End Class
