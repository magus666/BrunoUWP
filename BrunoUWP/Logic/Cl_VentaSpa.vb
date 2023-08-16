Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Table
Imports Windows.Storage

Public Class Cl_VentaSpa
    Dim GetCliente As New Cl_Cliente
    Dim GetTipoServicio As New Cl_TipoServicio
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetMascota As New Cl_Mascota
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetPickers As New Cl_Pickers

    Public Async Function InsertVentaSpa(CodigoVenta As String,
                                         FechaVenta As Date,
                                         IdTipoServicio As Integer,
                                         IdTipoTransaccion As Integer,
                                         IdMascota As Integer,
                                         IdMetodoPago As Integer,
                                         ValorVenta As Double) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New VentaSpaModel With {
                .Codigo_VentaSpa = CodigoVenta,
                .Fecha_VentaSpa = FechaVenta,
                .Id_TipoServicioSpa = IdTipoServicio,
                .Id_TipoTransaccionSpa = IdTipoTransaccion,
                .Id_MascotaSpa = IdMascota,
                .Id_MetodoPagoSpa = IdMetodoPago,
                .Valor_VentaSpa = ValorVenta
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaSpa() As Task(Of List(Of VentaSpaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Select x).ToList()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaTotalSpa() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Select x.Valor_VentaSpa).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaUltimoDiaSpa() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Fecha_VentaSpa.Date = Date.Today
                              Select x.Valor_VentaSpa).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaUltimaSemana() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Fecha_VentaSpa.Date >= Date.Now.AddDays(-7)
                              Select x.Valor_VentaSpa).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaUltimoMes() As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Fecha_VentaSpa.Date >= Date.Now.AddMonths(-1)
                              Select x.Valor_VentaSpa).Sum
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVentaPorTipoTransaccionSpa(IdTipoTransaccion As Integer) As Task(Of List(Of VentaSpaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Id_TipoTransaccionSpa = IdTipoTransaccion
                              Select x).ToList()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaSumatoriaVentaPorTipoTransaccionSpa(IdTipoTransaccion As Integer) As Task(Of Double)
        Try
            Await ConfiguraSqlite()
            Dim GetVenta = Await ConexionDB.Table(Of VentaSpaModel)().ToListAsync()
            Dim ListaVenta = (From x In GetVenta
                              Where x.Id_TipoTransaccionSpa = IdTipoTransaccion
                              Select x.Valor_VentaSpa).Sum()
            Return ListaVenta
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CrearExcelVentaSpa() As Task(Of Boolean)
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim xlPackage As New ExcelPackage()
            Dim oBook As ExcelWorkbook = xlPackage.Workbook
            Dim ws As ExcelWorksheet = oBook.Worksheets.Add("Ventas")

            Dim VentaTotal As Double = Await ConsultaSumatoriaVentaPorTipoTransaccionSpa(1)
            Dim Venta = Await ConsultaVentaPorTipoTransaccionSpa(1)
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
                                                       .Tipo_de_Servicio = Tps.Nombre_TipoServicio,
                                                       .Mascota = Msc.Nombre_Mascota,
                                                       .Propietario = Cli.NombreCompleto_Persona,
                                                       .Metodo_de_Pago = Mdp.Nombre_MetodoPago,
                                                       .Varlor_Total = CDbl(Vtn.Valor_VentaSpa)})
            ws.InsertRow(1, 1)
            ws.Cells("A1:G1").Merge = True
            ws.Cells("A1").Value = "Ventas Bruno Spa"
            With ws.Cells("A1")
                .Style.Font.Bold = True
                .Style.Font.Size = 22
                .Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            End With

            ws.Cells("A2").LoadFromCollection(RetornoFiltroVenta, True, TableStyles.Medium1)
            Dim lastRow = ws.Dimension.End.Row
            ws.Cells(lastRow + 1, 7).Formula = "=SUM(G2:G" & lastRow & ")"
            ws.Cells(lastRow + 1, 6).Value = "Total"


            Dim GuardarArchvo = GetPickers.CrearFileSavePicker("ListadoVentas")
            Dim file = Await GuardarArchvo.PickSaveFileAsync()
            If file IsNot Nothing Then
                CachedFileManager.DeferUpdates(file)
                Using stream = Await file.OpenAsync(FileAccessMode.ReadWrite)
                    Await xlPackage.SaveAsAsync(stream.AsStream())
                End Using
                Dim status = Await CachedFileManager.CompleteUpdatesAsync(file)
                Dim success = Await Windows.System.Launcher.LaunchFileAsync(file)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
