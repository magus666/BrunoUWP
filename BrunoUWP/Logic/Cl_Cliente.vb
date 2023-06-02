Public Class Cl_Cliente

    Public Async Function InsertarCliente(DocumentoCliente As String,
                                          NombreCliente As String,
                                          ApellidoCliente As String,
                                          EdadCliente As Integer,
                                          SexoCliente As String,
                                          CodigoCliente As String,
                                          EstadoCliente As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cliente = New ClienteModel With {
                .Documento_Persona = DocumentoCliente,
                .Nombre_Persona = NombreCliente,
                .Apellido_Persona = ApellidoCliente,
                .NombreCompleto_Persona = NombreCliente & " " & ApellidoCliente,
                .Edad_Persona = EdadCliente,
                .Sexo_Persona = SexoCliente,
                .Codigo_Cliente = CodigoCliente,
                .Estado_Cliente = EstadoCliente,
                .FechaCreacion_Persona = Date.Now
            }
            Dim Id = Await ConexionDB.InsertAsync(Cliente)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaCliente() As Task(Of List(Of ClienteModel))
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim ListaPersona = (From x In GetCliente
                                Select New ClienteModel With {
                                    .Id_Persona = x.Id_Persona,
                                    .NombreCompleto_Persona = x.NombreCompleto_Persona
                                }).ToList()
            Return ListaPersona
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
