Imports Windows.Security.Credentials

Public Class MicrosoftPassportHelper

    Async Function MicrosoftPassportAvailableCheckAsync() As Task(Of Boolean)
        Dim keyCredentialAvailable As Boolean = Await KeyCredentialManager.IsSupportedAsync()

        If keyCredentialAvailable = False Then
            Debug.WriteLine("Microsoft Passport is not setup!" & vbLf & "Please go to Windows Settings and set up a PIN to use it.")
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Creates a Passport key on the machine using the _account id passed.
    ''' </summary>
    ''' <param name="accountId">The _account id associated with the _account that we are enrolling into Passport</param>
    ''' <returns>Boolean representing if creating the Passport key succeeded</returns>
    Public Shared Async Function CreatePassportKeyAsync(accountId As String) As Task(Of Boolean)
        Dim keyCreationResult As KeyCredentialRetrievalResult = Await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting)

        Select Case keyCreationResult.Status
            Case KeyCredentialStatus.Success
                Debug.WriteLine("Successfully made key")

                ' In the real world authentication would take place on a server.
                ' So every time a user migrates or creates a new Microsoft Passport account Passport details should be pushed to the server.
                ' The details that would be pushed to the server include:
                ' The public key, keyAttesation if available, 
                ' certificate chain for attestation endorsement key if available,  
                ' status code of key attestation result: keyAttestationIncluded or 
                ' keyAttestationCanBeRetrievedLater and keyAttestationRetryType
                ' As this sample has no concept of a server it will be skipped for now
                ' for information on how to do this refer to the second Passport sample

                'For this sample just return true
                Return True
            Case KeyCredentialStatus.UserCanceled
                Debug.WriteLine("User cancelled sign-in process.")
            Case KeyCredentialStatus.NotFound
                ' User needs to setup Microsoft Passport
                Debug.WriteLine("Microsoft Passport is not setup!" & vbLf & "Please go to Windows Settings and set up a PIN to use it.")
            Case Else
        End Select

        Return False
    End Function


End Class
