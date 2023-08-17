Public Interface ICliente
    Function ConsultaCliente() As Task(Of List(Of ClienteModel))
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
    Function ActualizarCliente(IdCliente As Integer,
                               DocumentoCliente As String,
                               NombreCliente As String,
                               ApellidoCliente As String,
                               DireccionCLiente As String,
                               TelefonoCliente As String,
                               CorreoCliente As String,
                               EdadCliente As Integer,
                               EstadoCliente As Boolean) As Task(Of Boolean)
    Function EliminarCliente(IdCliente As Integer) As Task(Of Boolean)

End Interface
