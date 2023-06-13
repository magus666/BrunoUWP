Imports SQLite
Imports Windows.Storage

Module Md_ParametrizacionSqlite
    Public Property MarcoTrabajo As Frame
    Public ConexionDB As SQLiteAsyncConnection
    Public Async Function ConfiguraSqlite() As Task(Of Boolean)
        Try
            Dim CreacionBaseDatos = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BrunoUWP.db")
            ConexionDB = New SQLiteAsyncConnection(CreacionBaseDatos)
            Await ConexionDB.CreateTableAsync(Of MascotaModel)()
            Await ConexionDB.CreateTableAsync(Of ClienteModel)()
            Await ConexionDB.CreateTableAsync(Of SexoModel)()
            Await ConexionDB.CreateTableAsync(Of TipoMascotaModel)()
            Await ConexionDB.CreateTableAsync(Of RazaModel)()
            Await ConexionDB.CreateTableAsync(Of CitaModel)()
            Await ConexionDB.CreateTableAsync(Of TipoServicioModel)()
            Await ConexionDB.CreateTableAsync(Of TipoServicioMascotaModel)()
            Await ConexionDB.CreateTableAsync(Of DimensionMascotaModel)()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Module
