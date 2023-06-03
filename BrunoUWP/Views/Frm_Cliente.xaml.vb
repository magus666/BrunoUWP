' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
Imports Windows.ApplicationModel.Core
Imports Windows.Devices.Enumeration
Imports Windows.UI.Core
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Cliente
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetCliente As New Cl_Cliente
    Dim newViewId As Integer = 0
    Public GetSexo As New Cl_Sexo
    Dim IdSexoSeleccionado As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Await GetSexo.InsertaActualizaSexo()
            CmbSexo.ItemsSource = Await GetSexo.ConsultaSexo()
            CmbSexo.DisplayMemberPath = "Nombre_Sexo"
            'Dim localSettings = Windows.Storage.ApplicationData.Current.LocalSettings
            'Dim Value = localSettings.Values("NombreMascota")
            'If Value IsNot Nothing Then
            '    TxtNombres.Text = Value
            'End If
            Dim CodigoAleatrorio As String = GetUtilitarios.GenerarCodigoAleatorio()
            TxtCodigo.Text = CodigoAleatrorio
        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim IndexCmbSexo As String = CmbSexo.SelectedIndex
            If IndexCmbSexo >= 0 Then
                If Await GetCliente.InsertarCliente(TxtDocumento.Text, TxtNombres.Text, TxtApellidos.Text,
                                                 TxtDireccion.Text, TxtTelefono.Text, NmbEdad.Text,
                                                 IdSexoSeleccionado, TxtCodigo.Text, True) = True Then
                    LimpiarTextbox()
                End If
            Else
                Dim Mensaje = "Paila Perri"
            End If


            'Dim newView As CoreApplicationView = CoreApplication.CreateNewView()

            'Await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
            '                                                                     Dim frame As Frame = New Frame()
            '                                                                     frame.Navigate(GetType(Frm_GestionMascota), Nothing)
            '                                                                     Window.Current.Content = frame
            '                                                                     ' You have to activate the window in order to show it later.
            '                                                                     Window.Current.Activate()
            '                                                                     newViewId = ApplicationView.GetForCurrentView().Id
            '                                                                 End Sub)
            'Dim viewShown As Boolean = Await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbSexo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim ComboBoxSexo As ComboBox = CType(sender, ComboBox)
            Dim ItemSeleccionado As SexoModel = CType(ComboBoxSexo.SelectedItem, SexoModel)
            IdSexoSeleccionado = ItemSeleccionado.Id_Sexo
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LimpiarTextbox()
        Try
            For Each element As UIElement In StpDatosCliente.Children
                If (TypeOf element Is TextBox) Then
                    DirectCast(element, TextBox).Text = String.Empty
                End If
            Next
            TxtCodigo.Text = GetUtilitarios.GenerarCodigoAleatorio()
            NmbEdad.Text = 1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


    End Sub
End Class
