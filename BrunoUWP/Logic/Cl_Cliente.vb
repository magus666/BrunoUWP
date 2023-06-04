﻿Public Class Cl_Cliente

    Public Async Function InsertarCliente(DocumentoCliente As String,
                                          NombreCliente As String,
                                          ApellidoCliente As String,
                                          DireccionCliente As String,
                                          TelefonoCliente As String,
                                          EdadCliente As Integer,
                                          SexoCliente As Integer,
                                          CodigoCliente As String,
                                          EstadoCliente As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cliente = New ClienteModel With {
                .Documento_Persona = DocumentoCliente,
                .Nombre_Persona = NombreCliente,
                .Apellido_Persona = ApellidoCliente,
                .Direccion_Persona = DireccionCliente,
                .Telefono_Persona = TelefonoCliente,
                .NombreCompleto_Persona = NombreCliente & " " & ApellidoCliente,
                .Edad_Persona = EdadCliente,
                .Id_Sexo = SexoCliente,
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
                                Select x).ToList()
            Return ListaPersona
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteTotal() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().CountAsync()
            Return GetCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimoDia() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddDays(-1)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimaSemana() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddDays(-7)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimoMes() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddMonths(-1)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class