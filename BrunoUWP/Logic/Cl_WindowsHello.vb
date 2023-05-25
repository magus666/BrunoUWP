Imports Windows.Security.Credentials
Imports Windows.Security.Cryptography

Public Class Cl_WindowsHello
    Public Async Function VerifiacionWindowsHello() As Task
        Dim userName As String = Security.Principal.WindowsIdentity.GetCurrent().Name
        ' Check if Windows Hello is set up on the computer
        If Await KeyCredentialManager.IsSupportedAsync() Then
            ' Sign in to an account using Windows Hello
            Dim openKeyResult As KeyCredentialRetrievalResult = Await KeyCredentialManager.OpenAsync("black_death_metalero@outlook.com")
            If openKeyResult.Credential IsNot Nothing Then
                Dim signResult As KeyCredentialOperationResult = Await openKeyResult.Credential.RequestSignAsync(CryptographicBuffer.ConvertStringToBinary("LoginAuth", BinaryStringEncoding.Utf8))
                If signResult.Status = KeyCredentialStatus.Success Then
                    ' Successful sign in
                End If
            End If
        End If

    End Function
End Class
