Imports System.Security.Principal
Imports System.Text
Imports System.Xml.Serialization
Imports Windows.Storage

Public NotInheritable Class AccountHelper
    ' In the real world this would not be needed as there would be a server implemented that would host a user account database.
    ' For this tutorial we will just be storing accounts locally.
    Private Const USER_ACCOUNT_LIST_FILE_NAME As String = "accountlist.txt"
    Private Shared _accountListPath As String = Path.Combine(ApplicationData.Current.LocalFolder.Path, USER_ACCOUNT_LIST_FILE_NAME)
    Public Shared AccountList As New List(Of Usuario)()

    ''' <summary>
    ''' Create and save a useraccount list file. (Updating the old one)
    ''' </summary>
    Private Shared Async Sub SaveAccountListAsync()
        Dim accountsXml As String = SerializeAccountListToXml()
        If File.Exists(_accountListPath) Then
            Dim accountsFile As StorageFile = Await StorageFile.GetFileFromPathAsync(_accountListPath)
            Await FileIO.WriteTextAsync(accountsFile, accountsXml)
        Else
            Dim accountsFile As StorageFile = Await ApplicationData.Current.LocalFolder.CreateFileAsync(USER_ACCOUNT_LIST_FILE_NAME)
            Await FileIO.WriteTextAsync(accountsFile, accountsXml)
        End If
    End Sub

    ''' <summary>
    ''' Gets the useraccount list file and deserializes it from XML to a list of useraccount objects.
    ''' </summary>
    ''' <returns>List of useraccount objects</returns>
    Public Shared Async Function LoadAccountListAsync() As Task(Of List(Of Usuario))
        If File.Exists(_accountListPath) Then
            Dim accountsFile As StorageFile = Await StorageFile.GetFileFromPathAsync(_accountListPath)
            Dim accountsXml As String = Await FileIO.ReadTextAsync(accountsFile)
            DeserializeXmlToAccountList(accountsXml)
        End If

        Return AccountList
    End Function

    ''' <summary>
    ''' Uses the local list of accounts and returns an XML formatted string representing the list
    ''' </summary>
    ''' <returns>XML formatted list of accounts</returns>
    Public Shared Function SerializeAccountListToXml() As String
        Dim xmlizer As New XmlSerializer(GetType(List(Of Usuario)))
        Dim writer As New StringWriter()
        xmlizer.Serialize(writer, AccountList)
        Return writer.ToString()
    End Function

    ''' <summary>
    ''' Takes an XML formatted string representing a list of accounts and returns a list object of accounts
    ''' </summary>
    ''' <param name="listAsXml">XML formatted list of accounts</param>
    ''' <returns>List object of accounts</returns>
    Public Shared Function DeserializeXmlToAccountList(ByVal listAsXml As String) As List(Of Usuario)
        Dim xmlizer As New XmlSerializer(GetType(List(Of Usuario)))
        Dim textreader As TextReader = New StreamReader(New MemoryStream(Encoding.UTF8.GetBytes(listAsXml)))
        Return TryCast(xmlizer.Deserialize(textreader), List(Of Usuario))
    End Function

    Public Shared Function AddAccount(username As String) As Usuario
        ' Create a new account with the username
        Dim account As New Usuario With {.UserName = username}
        ' Add it to the local list of accounts
        AccountList.Add(account)
        ' SaveAccountList and return the account
        SaveAccountListAsync()
        Return account
    End Function

    Public Shared Sub RemoveAccount(account As Usuario)
        ' Remove the account from the accounts list
        AccountList.Remove(account)
        ' Re save the updated list
        SaveAccountListAsync()
    End Sub

    Public Shared Function ValidateAccountCredentials(username As String) As Boolean
        ' In the real world, this method would call the server to authenticate that the account exists and is valid.
        ' For this tutorial however we will just have a existing sample user that is just "sampleUsername"
        ' If the username is null or does not match "sampleUsername" it will fail validation. In which case the user should register a new passport user

        If String.IsNullOrEmpty(username) Then
            Return False
        End If

        If Not String.Equals(username, "sampleUsername") Then
            Return False
        End If

        Return True
    End Function

End Class
