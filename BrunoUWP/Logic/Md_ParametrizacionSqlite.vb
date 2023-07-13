Imports SQLite
Imports Windows.Storage
Imports Windows.Storage.Pickers

Module Md_ParametrizacionSqlite
    Public Property MarcoTrabajo As Frame
    Public ConexionDB As SQLiteAsyncConnection
    Dim GetPickers As New Cl_Pickers
    Dim GetAuditoria As New Cl_Auditoria

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
            Await ConexionDB.CreateTableAsync(Of MetodoPagoModel)()
            Await ConexionDB.CreateTableAsync(Of PagosModel)()
            Await ConexionDB.CreateTableAsync(Of TipoTransaccionModel)()
            Await ConexionDB.CreateTableAsync(Of VentaSpaModel)()
            Await ConexionDB.CreateTableAsync(Of AuditoriaModel)()
            Await ConexionDB.CreateTableAsync(Of ArticuloModel)()
            Await ConexionDB.CreateTableAsync(Of VentaArticuloModel)()
            Await ConexionDB.CreateTableAsync(Of CategoriaArticuloModel)()
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function BackUpDatabaseOpenFolder() As Task(Of Boolean)
        Try
            Await GetAuditoria.InsertDataBaseStamp(Date.Now)
            Dim fileToCopy As StorageFile = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appdata:///local/BrunoUWP.db"))
            Dim RutaCarpeta = Await GetPickers.CrearCarpetaBackUpBd("BackUpBd")
            If RutaCarpeta IsNot Nothing Then
                Await fileToCopy.CopyAsync(RutaCarpeta, "BrunoUWP.db", NameCollisionOption.ReplaceExisting)
                Dim success As Boolean = Await Windows.System.Launcher.LaunchFolderAsync(RutaCarpeta)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function BackUpDatabase() As Task(Of Boolean)
        Try
            Await GetAuditoria.InsertDataBaseStamp(Date.Now)
            Dim fileToCopy As StorageFile = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appdata:///local/BrunoUWP.db"))
            Dim RutaCarpeta = Await GetPickers.CrearCarpetaBackUpBd("BackUpBd")
            If RutaCarpeta IsNot Nothing Then
                Await fileToCopy.CopyAsync(RutaCarpeta, "BrunoUWP.db", NameCollisionOption.ReplaceExisting)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function BackUpDatabaseV2() As Task(Of Boolean)
        Try
            Dim fileToCopy As StorageFile = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appdata:///local/BrunoUWP.db"))
            Dim folderPicker As New FolderPicker()
            folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            folderPicker.FileTypeFilter.Add("*")
            Dim destinationFolder As StorageFolder = Await folderPicker.PickSingleFolderAsync()
            If destinationFolder IsNot Nothing Then
                Await fileToCopy.CopyAsync(destinationFolder, "BrunoUWP.db", NameCollisionOption.ReplaceExisting)
                Return True
            Else
                Return False
            End If
            'Dim DirectorioDestino = "C:\Users\ivan.marin\Pictures\BrunoUWP.db"
            'Await ConexionDB.BackupAsync(DirectorioDestino)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



End Module
