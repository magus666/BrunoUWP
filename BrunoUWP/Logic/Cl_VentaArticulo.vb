Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Table
Imports Windows.Storage

Public Class Cl_VentaArticulo
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetArticulo As New Cl_Articulo
    Dim GetPickers As New Cl_Pickers


    Public Async Function InsertVenta(CodigoVentaArticulo As String,
                                      FechaVentaArticulo As Date,
                                      IdTIpoTransaccion As Integer,
                                      CantidadVentaArticulo As Integer,
                                      IdMetodoPago As Integer,
                                      ValorVentaArticulo As Double,
                                      IdArticulo As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cita = New VentaArticuloModel With {
                .Codigo_VentaArticulo = CodigoVentaArticulo,
                .Fecha_VentaArticulo = FechaVentaArticulo,
                .Id_TipoTransaccion = IdTIpoTransaccion,
                .Cantidad_VentaArticulo = CantidadVentaArticulo,
                .Id_MetodoPago = IdMetodoPago,
                .Valor_VentaArticulo = ValorVentaArticulo,
                .Id_Articulo = IdArticulo
            }
            Dim Id = Await ConexionDB.InsertAsync(Cita)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaVenta() As Task(Of List(Of VentaArticuloModel))
        Try
            Await ConfiguraSqlite()
            Dim GetVentaArticulo = Await ConexionDB.Table(Of VentaArticuloModel)().ToListAsync()
            Dim ListaVentaArticulo = (From x In GetVentaArticulo
                                      Select x).ToList()
            Return ListaVentaArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CreaExcelVentaArticulo() As Task(Of Boolean)
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim xlPackage As New ExcelPackage()
            Dim oBook As ExcelWorkbook = xlPackage.Workbook
            Dim ws As ExcelWorksheet = oBook.Worksheets.Add("Venta Articulos")

            Dim ObtenerVentaArticulo = Await ConsultaVenta()
            Dim ObtenerMetodoPago = Await GetMetodoPago.ConsultaMetodoPago()
            Dim ObtenerCategoriaArticulo = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            Dim ObtenerTipoTransaccion = Await GetTipoTransaccion.ConsultaTipoTransaccion()
            Dim ObtenerArticulo = Await GetArticulo.ConsultaArticulos()
            Dim ListaFiltrada = (From Var In ObtenerVentaArticulo
                                 Join Mpg In ObtenerMetodoPago On
                                            Var.Id_MetodoPago Equals Mpg.Id_MetodoPago
                                 Join Ttc In ObtenerTipoTransaccion On
                                            Var.Id_TipoTransaccion Equals Ttc.Id_TipoTransaccion
                                 Join Art In ObtenerArticulo On
                                            Var.Id_Articulo Equals Art.Id_Articulo
                                 Join Cat In ObtenerCategoriaArticulo On
                                            Art.Id_MaestroArticulo Equals Cat.Id_MaestroArticulo
                                 Select New With {.Codigo = Var.Codigo_VentaArticulo,
                                                         .Fecha = Var.Fecha_VentaArticulo.ToShortDateString,
                                                         .Categoria = Cat.Nombre_MaestroArticulo,
                                                         .NombreArticulo = Art.Nombre_Articulo,
                                                         .Cantidad = Var.Cantidad_VentaArticulo,
                                                         .MetodoPago = Mpg.Nombre_MetodoPago,
                                                         .ValorTotal = CDbl(Var.Valor_VentaArticulo)})
            ws.InsertRow(1, 1)
            ws.Cells("A1:G1").Merge = True
            ws.Cells("A1").Value = "Venta Articulos Bruno Spa"
            With ws.Cells("A1")
                .Style.Font.Bold = True
                .Style.Font.Size = 22
                .Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            End With

            ws.Cells("A2").LoadFromCollection(ListaFiltrada, True, TableStyles.Medium1)
            Dim GuardarArchvo = GetPickers.CrearFileSavePicker("ListadoVentaArticulos")

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
