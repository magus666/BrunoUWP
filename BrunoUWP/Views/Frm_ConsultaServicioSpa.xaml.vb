﻿' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaServicioSpa
    Inherits Page
    Dim GetVenta As New Cl_VentaSpa
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetMascota As New Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetNotificaciones As New Cl_Notificaciones


    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim VentaTotal As Double = Await GetVenta.ConsultaSumatoriaVentaPorTipoTransaccion(1)
            Dim Venta = Await GetVenta.ConsultaVentaPorTipoTransaccion(1)
            Dim TipoServicio = Await GetTipoServicio.ConsultaTipoServicio()
            Dim TipoTransaccion = Await GetTipoTransaccion.ConsultaTipoTransaccion()
            Dim Mascota = Await GetMascota.ConsultaMascotas()
            Dim Cliente = Await GetCliente.ConsultaCliente()
            Dim MetodoPagao = Await GetMetodoPago.ConsultaMetodoPago()
            Dim RetornoFiltroVenta = (From Vtn In Venta
                                      Join Tps In TipoServicio On
                                              Vtn.Id_TipoServicioSpa Equals Tps.Id_TipoSerivicio
                                      Join Ttc In TipoTransaccion On
                                          Vtn.Id_TipoTransaccionSpa Equals Ttc.Id_TipoTransaccion
                                      Join Msc In Mascota On
                                          Vtn.Id_MascotaSpa Equals Msc.Id_Mascota
                                      Join Cli In Cliente On
                                          Msc.Id_Persona Equals Cli.Id_Persona
                                      Join Mdp In MetodoPagao On
                                          Vtn.Id_MetodoPagoSpa Equals Mdp.Id_MetodoPago
                                      Select New With {.Codigo = Vtn.Codigo_VentaSpa,
                                                       .Fecha = Vtn.Fecha_VentaSpa.ToShortDateString,
                                                       .TipoServicio = Tps.Nombre_TipoServicio,
                                                       .Mascota = Msc.Nombre_Mascota,
                                                       .Propietario = Cli.NombreCompleto_Persona,
                                                       .MetodoPago = Mdp.Nombre_MetodoPago,
                                                       .VarlorTotal = Vtn.Valor_VentaSpa.ToString("c")})
            DtgVentaServicioSpa.ItemsSource = RetornoFiltroVenta
                LblVentaTotal.Text = "Ventas Totales: " & VentaTotal.ToString("c")
        Catch ex As Exception

        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            If Await GetVenta.CrearExcelVenta() = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
