' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI.Controls

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaVentaArticulo
    Inherits Page
    Dim GetVentaArticulo As New Cl_VentaArticulo
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetArticulo As New Cl_Articulo
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo
    Private RetornoVentaArticulo As IEnumerable(Of Object)

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ObtenerVentaArticulo = Await GetVentaArticulo.ConsultaListaVentasTotales()
            Dim ObtenerMetodoPago = Await GetMetodoPago.ConsultaMetodoPago()
            Dim ObtenerCategoriaArticulo = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            Dim ObtenerTipoTransaccion = Await GetTipoTransaccion.ConsultaTipoTransaccion()
            Dim ObtenerArticulo = Await GetArticulo.IArticulo_ConsultaArticulos()
            Dim ListaArticulos = (From Var In ObtenerVentaArticulo
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
                                                         .TipoServicio = Ttc.Nombre_TipoTransaccion,
                                                         .Categoria = Cat.Nombre_MaestroArticulo,
                                                         .NombreArticulo = Art.Nombre_Articulo,
                                                         .Cantidad = Var.Cantidad_VentaArticulo,
                                                         .MetodoPago = Mpg.Nombre_MetodoPago,
                                                         .ValorTotal = Var.Valor_VentaArticulo.ToString("c")}).ToList
            RetornoVentaArticulo = (From item In ListaArticulos
                                    Order By DateTime.Parse(item.Fecha) Descending).ToList()
            DtgVentaArticulo.ItemsSource = RetornoVentaArticulo
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            If Await GetVentaArticulo.CreaExcelVentaArticulo = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub DtgVentaArticulo_Sorting(sender As Object, e As Microsoft.Toolkit.Uwp.UI.Controls.DataGridColumnEventArgs)
        Try
            Dim Columna As String = e.Column.Tag.ToString
            Select Case Columna
                Case "Codigo"
                    If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By item.Codigo Ascending)
                        e.Column.SortDirection = DataGridSortDirection.Ascending
                    Else
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By item.Codigo Descending)
                        e.Column.SortDirection = DataGridSortDirection.Descending
                    End If
                Case "FechaVenta"
                    If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By DateTime.Parse(item.Fecha) Ascending)
                        e.Column.SortDirection = DataGridSortDirection.Ascending
                    Else
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By DateTime.Parse(item.Fecha) Descending)
                        e.Column.SortDirection = DataGridSortDirection.Descending
                    End If
                Case "NombreArticulo"
                    If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By item.NombreArticulo Ascending)
                        e.Column.SortDirection = DataGridSortDirection.Ascending
                    Else
                        DtgVentaArticulo.ItemsSource = (From item In RetornoVentaArticulo
                                                        Order By item.NombreArticulo Descending)
                        e.Column.SortDirection = DataGridSortDirection.Descending
                    End If
            End Select

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
End Class
