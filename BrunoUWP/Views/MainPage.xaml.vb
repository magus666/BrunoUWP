' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

Imports System.Security
Imports System.Security.Cryptography.X509Certificates
''' <summary>
''' Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public Shared Current As MainPage
    Private Sub NvwBruno_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.IsSettingsInvoked
                Case True
                    sender.Content = New Frm_Configuracion()
                Case False
                    Select Case args.InvokedItem
                        Case "Clientes"
                            sender.Content = New Frm_Persona()
                        Case "Inicio"
                            sender.Content = New Frm_Inicio()
                        Case "Mascotas"
                            sender.Content = New Frm_Mascota()
                        Case "Inventario"
                            sender.Content = New Frm_Inventario()
                    End Select
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            MarcoTrabajo = ContenFrameMenu
            ContenFrameMenu.Navigate(GetType(Frm_Inicio))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NvwBruno_PaneOpening(sender As NavigationView, args As Object)
        Try
            NvwBruno.PaneCustomContent.Visibility = Visibility.Visible
        Catch ex As Exception

        End Try

    End Sub

    Private Sub NvwBruno_PaneClosing(sender As NavigationView, args As NavigationViewPaneClosingEventArgs)
        Try
            NvwBruno.PaneCustomContent.Visibility = Visibility.Collapsed
        Catch ex As Exception

        End Try

    End Sub
End Class
