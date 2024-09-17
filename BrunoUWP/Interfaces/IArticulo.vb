Public Interface IArticulo
    Function InsertarArticulo(CodigoArticulo As String,
                                           NombreArticulo As String,
                                           MarcaArticulo As String,
                                           DescripcionArticulo As String,
                                           ValorArticulo As Double,
                                           CantidadArticulo As Integer,
                                           CantidadTotalVentaArticulo As Integer,
                                           FechaCreacionArticulo As Date,
                                           IdMaestroArticulo As Integer) As Task(Of Boolean)
    Function ActualizarArticulo(IdArticulo As Integer,
                                             ValorArticulo As Double,
                                             CantidadArticulo As Integer,
                                             CantidadTotalVentaArticulo As Integer) As Task(Of Boolean)
    Function ConsultaArticulos() As Task(Of List(Of ArticuloModel))
    Function CountArticulosUltimoDia() As Task(Of Integer)
    Function CountArticulosUltimaSemana() As Task(Of Integer)
    Function CountArticulosUltimoMes() As Task(Of Integer)
    Function CreaExcelArticulo() As Task(Of Boolean)




End Interface
