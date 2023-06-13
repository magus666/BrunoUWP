' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a
''' <summary>
''' Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public Shared PaginaActual As MainPage
    Public GetWindowsHello As New Cl_WindowsHello

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
                        Case "Inventario"
                            ContenFrameMenu.Navigate(GetType(Frm_Inventario))
                    End Select
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvwBruno.IsBackEnabled = False
            NvwBruno.SelectedItem = NviInicio
            MarcoTrabajo = ContenFrameMenu
            MarcoTrabajo.Navigate(GetType(Frm_Inicio))
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
End Class
