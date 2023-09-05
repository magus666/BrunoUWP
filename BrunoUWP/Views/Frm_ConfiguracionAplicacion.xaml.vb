' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
Imports Windows.Storage
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConfiguracionAplicacion
    Inherits Page
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim localSettings As ApplicationDataContainer = ApplicationData.Current.LocalSettings
            If localSettings.Values.ContainsKey("TemaSeleccionado") Then
                Dim temaSeleccionado As String = localSettings.Values("TemaSeleccionado")
                Select Case temaSeleccionado
                    Case "Claro"
                        RdbTema.SelectedIndex = 0
                    Case "Oscuro"
                        RdbTema.SelectedIndex = 1
                    Case "Configuracion del Sistema"
                        RdbTema.SelectedIndex = 2
                End Select
            Else
                RdbTema.SelectedIndex = 2
            End If
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

            Dim localSettings As ApplicationDataContainer = ApplicationData.Current.LocalSettings
            localSettings.Values("TemaSeleccionado") = SeleccionRadio

        Catch ex As Exception
            Dim MensajeError = ex.Message
        End Try
    End Sub
End Class
