' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleCliente
    Inherits Page

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        MyBase.OnNavigatedTo(e)

        Dim data As NewPersonaModel = TryCast(e.Parameter, NewPersonaModel)
        If data IsNot Nothing Then
            TxtNombreCliente.Text = data.NombreCompleto_Persona
        End If
    End Sub
End Class
