﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Mascota
    Inherits Page

    Private Sub NvvMascota_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)
        Try
            Select Case args.InvokedItem
                Case "Gestion de Mascotas"
                    sender.Content = New Frm_GestionMascota()
                Case "Consulta de Mascotas"
                    sender.Content = New Frm_ConsultaMascota()
                Case "Parametrizacion de Razas"
                    sender.Content = New Frm_ParametrizacionRazas()
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
