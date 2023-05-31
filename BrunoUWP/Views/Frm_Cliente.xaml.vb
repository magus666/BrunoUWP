' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.ApplicationModel.Core
Imports Windows.UI.Core
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Cliente
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim newViewId As Integer = 0

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Dim CodigoAleatrorio As String = GetUtilitarios.GenerarCodigoAleatorio()
        TxtCodigo.Text = CodigoAleatrorio
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim newView As CoreApplicationView = CoreApplication.CreateNewView()

            Await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                                 Dim frame As Frame = New Frame()
                                                                                 frame.Navigate(GetType(Frm_ConsultaMascota), Nothing)
                                                                                 Window.Current.Content = frame
                                                                                 ' You have to activate the window in order to show it later.
                                                                                 Window.Current.Activate()
                                                                                 newViewId = ApplicationView.GetForCurrentView().Id
                                                                             End Sub)
            Dim viewShown As Boolean = Await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId)
        Catch ex As Exception

        End Try
    End Sub
End Class
