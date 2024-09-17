''' <summary>
''' Interface para la gestión de artículos.
''' </summary>
Public Interface IArticulo

    ''' <summary>
    ''' Inserta un nuevo artículo en el sistema.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la inserción fue exitosa.</returns>
    Function InsertarArticulo(CodigoArticulo As String,
                              NombreArticulo As String,
                              MarcaArticulo As String,
                              DescripcionArticulo As String,
                              ValorArticulo As Double,
                              CantidadArticulo As Integer,
                              CantidadTotalVentaArticulo As Integer,
                              FechaCreacionArticulo As Date,
                              IdMaestroArticulo As Integer) As Task(Of Boolean)

    ''' <summary>
    ''' Actualiza los detalles de un artículo existente.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la actualización fue exitosa.</returns>
    Function ActualizarArticulo(IdArticulo As Integer,
                                ValorArticulo As Double,
                                CantidadArticulo As Integer,
                                CantidadTotalVentaArticulo As Integer) As Task(Of Boolean)

    ''' <summary>
    ''' Consulta la lista de todos los artículos.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene una lista de objetos ArticuloModel.</returns>
    Function ConsultaArticulos() As Task(Of List(Of ArticuloModel))

    ''' <summary>
    ''' Cuenta el número de artículos creados en el último día.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene el número de artículos creados en el último día.</returns>
    Function CountArticulosUltimoDia() As Task(Of Integer)

    ''' <summary>
    ''' Cuenta el número de artículos creados en la última semana.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene el número de artículos creados en la última semana.</returns>
    Function CountArticulosUltimaSemana() As Task(Of Integer)

    ''' <summary>
    ''' Cuenta el número de artículos creados en el último mes.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene el número de artículos creados en el último mes.</returns>
    Function CountArticulosUltimoMes() As Task(Of Integer)

    ''' <summary>
    ''' Crea un archivo Excel con los detalles de los artículos.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la creación del archivo Excel fue exitosa.</returns>
    Function CreaExcelArticulo() As Task(Of Boolean)

End Interface
