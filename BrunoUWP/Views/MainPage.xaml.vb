' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a
Imports Windows.Storage
''' <summary>
''' Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public Shared PaginaActual As MainPage
    Public GetWindowsHello As New Cl_WindowsHello
    Dim GetNotificaciones As New Cl_Notificaciones
    Public NombreUsuario As String = Security.Principal.WindowsIdentity.GetCurrent().Name

    Private Sub NvwBruno_ItemInvoked(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs)
        Try
            NvwBruno.IsBackEnabled = True
            Select Case args.IsSettingsInvoked
                Case True
                    ContenFrameMenu.Navigate(GetType(Frm_Configuracion))
                Case False
                    Select Case args.InvokedItem
                        Case "Clientes"
                            ContenFrameMenu.Navigate(GetType(Frm_Cliente))
                        Case "Inicio"
                            ContenFrameMenu.Navigate(GetType(Frm_Inicio))
                        Case "Mascotas"
                            ContenFrameMenu.Navigate(GetType(Frm_Mascota))
                        Case "Servicio Spa"
                            ContenFrameMenu.Navigate(GetType(Frm_ServicioSpa))
                        Case "Citas"
                            ContenFrameMenu.Navigate(GetType(Frm_Citas))
                        Case "Articulos"
                            ContenFrameMenu.Navigate(GetType(Frm_Articulo))
                    End Select
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            IniciarTimerBackUp()
            NvwBruno.IsBackEnabled = False
            NvwBruno.SelectedItem = NviInicio
            MarcoTrabajo = ContenFrameMenu
            MarcoTrabajo.Navigate(GetType(Frm_Inicio))
            Dim localSettings As ApplicationDataContainer = ApplicationData.Current.LocalSettings
            If localSettings.Values.ContainsKey("TemaSeleccionado") Then
                Dim temaSeleccionado As String = localSettings.Values("TemaSeleccionado")
                Select Case temaSeleccionado
                    Case "Claro"
                        CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Light
                    Case "Oscuro"
                        CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Dark
                    Case "Configuracion del Sistema"
                        CType(Window.Current.Content, FrameworkElement).RequestedTheme = ElementTheme.Default
                End Select
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NvwBruno_PaneOpening(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Object)
        Try
            NvwBruno.PaneCustomContent.Visibility = Visibility.Visible
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NvwBruno_PaneClosing(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewPaneClosingEventArgs)
        Try
            NvwBruno.PaneCustomContent.Visibility = Visibility.Collapsed
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NvwBruno_BackRequested(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs)
        Try
            If ContenFrameMenu.CanGoBack Then
                ContenFrameMenu.GoBack()
                NvwBruno.SelectedItem = NviInicio
                If Not ContenFrameMenu.CanGoBack Then
                    NvwBruno.IsBackEnabled = False
                End If
            Else
                NvwBruno.IsBackEnabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub IniciarTimerBackUp()
        Dim Timer As New DispatcherTimer()
        AddHandler Timer.Tick, AddressOf Timer_Tick
        Timer.Interval = TimeSpan.FromHours(1)
        Timer.Start()
    End Sub

    Private Async Sub Timer_Tick(sender As Object, e As Object)
        Try
            Await BackUpDatabase()
        Catch ex As Exception

        End Try
    End Sub
End Class
