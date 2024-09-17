''' <summary>
''' Interface para la gestión de clientes.
''' </summary>
Public Interface ICliente

    ''' <summary>
    ''' Consulta la lista de todos los clientes.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene una lista de objetos ClienteModel.</returns>
    Function ConsultaCliente() As Task(Of List(Of ClienteModel))

    ''' <summary>
    ''' Inserta un nuevo cliente en el sistema.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la inserción fue exitosa.</returns>
    Function InsertarCliente(DocumentoCliente As String,
                             NombreCliente As String,
                             ApellidoCliente As String,
                             DireccionCliente As String,
                             TelefonoCliente As String,
                             CorreoCliente As String,
                             EdadCliente As Integer,
                             SexoCliente As Integer,
                             CodigoCliente As String,
                             EstadoCliente As Boolean) As Task(Of Boolean)

    ''' <summary>
    ''' Actualiza los detalles de un cliente existente.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la actualización fue exitosa.</returns>
    Function ActualizarCliente(IdCliente As Integer,
                               DocumentoCliente As String,
                               NombreCliente As String,
                               ApellidoCliente As String,
                               DireccionCLiente As String,
                               TelefonoCliente As String,
                               CorreoCliente As String,
                               EdadCliente As Integer,
                               EstadoCliente As Boolean) As Task(Of Boolean)

    ''' <summary>
    ''' Elimina un cliente del sistema.
    ''' </summary>
    ''' <returns>Una tarea que representa la operación asincrónica. El resultado contiene un valor booleano que indica si la eliminación fue exitosa.</returns>
    Function EliminarCliente(IdCliente As Integer) As Task(Of Boolean)

End Interface
