Imports Azure.Storage.Blobs

Public Class Cl_BlobStorage

    Public Async Function UploadFile(containerClient As BlobContainerClient, localFilePath As String) As Task
        Try
            Dim fileName As String = Path.GetFileName(localFilePath)
            Dim blobClient As BlobClient = containerClient.GetBlobClient(fileName)
            Await blobClient.UploadAsync(localFilePath, True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Function

End Class
