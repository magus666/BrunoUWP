﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a
Imports Windows.UI.Xaml.Navigation
Imports Windows.UI.Xaml.Media
Imports Windows.UI
Imports System.Security.Principal
''' <summary>
''' Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page
    Public Shared PaginaActual As MainPage
    Public GetWindowsHello As New Cl_WindowsHello

    Private Sub NvwBruno_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
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
                        Case "Inventario"
                            ContenFrameMenu.Navigate(GetType(Frm_Inventario))

                    End Select
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            NvwBruno.SelectedItem = NviInicio
            MarcoTrabajo = ContenFrameMenu
            MarcoTrabajo.Navigate(GetType(Frm_Inicio))
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

    Private Sub NvwBruno_BackRequested(sender As NavigationView, args As NavigationViewBackRequestedEventArgs)
        Try
            ContenFrameMenu.GoBack()
            NvwBruno.SelectedItem = NviInicio
        Catch ex As Exception

        End Try
    End Sub
End Class
