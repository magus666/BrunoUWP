' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaVentaArticulo
    Inherits Page
    Dim GetVentaArticulo As New Cl_VentaArticulo
    Dim GetMetodoPago As New Cl_MetodoPago
    Dim GetTipoTransaccion As New Cl_TipoTransaccion
    Dim GetArticulo As New Cl_Articulo
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ObtenerVentaArticulo = Await GetVentaArticulo.ConsultaVenta()
            Dim ObtenerMetodoPago = Await GetMetodoPago.ConsultaMetodoPago()
            Dim ObtenerCategoriaArticulo = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            Dim ObtenerTipoTransaccion = Await GetTipoTransaccion.ConsultaTipoTransaccion()
            Dim ObtenerArticulo = Await GetArticulo.ConsultaArticulos()
            Dim RetornoVentaArticulo = (From Var In ObtenerVentaArticulo
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
                                                         .ValorTotal = Var.Valor_VentaArticulo.ToString("c")})
            DtgVentaArticulo.ItemsSource = RetornoVentaArticulo
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
