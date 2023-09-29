' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.UI.Core
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_DetalleMascota
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetMascota As New Cl_Mascota
    Dim GetImagenMascota As New Cl_ImagenMascota
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim ObtenerImagenMascota As List(Of ImagenMascotaModel)
    Dim IdMascota As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            ObtenerImagenMascota = Await GetImagenMascota.ConsultaImagenMascotaPorIdMascota(IdMascota)
            If ObtenerImagenMascota.Count = 0 Then
                LblmensajeImagen.Visibility = Visibility.Visible
                FlvImagenMascota.Visibility = Visibility.Collapsed
                PspNumeroImagnes.Visibility = Visibility.Collapsed
            Else
                LblmensajeImagen.Visibility = Visibility.Collapsed
                FlvImagenMascota.Visibility = Visibility.Visible
                FlvImagenMascota.ItemsSource = ObtenerImagenMascota
                PspNumeroImagnes.Visibility = Visibility.Visible
                PspNumeroImagnes.NumberOfPages = ObtenerImagenMascota.Count

            End If

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
        Try
            MyBase.OnNavigatedTo(e)
            Dim DatosMascota As NewMascotaModel = TryCast(e.Parameter, NewMascotaModel)
            If DatosMascota IsNot Nothing Then
                IdMascota = DatosMascota.Id_Mascota
                TxtCodigo.Text = DatosMascota.Codigo_Mascota
                TxtRaza.Text = DatosMascota.Nombre_Raza
                TxtNombre.Text = DatosMascota.Nombre_Mascota
                TxtTituloNombreMascota.Text = DatosMascota.Nombre_Mascota
                NbbEdad.Value = DatosMascota.Edad_Mascota
                TxtPropietario.Text = DatosMascota.NombreCompleto_Persona
                TxtObservaciones.Text = DatosMascota.Observaciones_Mascota
                Dim EstadoMascota = DatosMascota.Estado_Mascota
                Select Case EstadoMascota
                    Case 1
                        TgsEstadoMascota.IsOn = True
                    Case 0
                        TgsEstadoMascota.IsOn = False
                End Select
            End If
            Dim currentView = SystemNavigationManager.GetForCurrentView()
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible
            AddHandler currentView.BackRequested, AddressOf backButton_Tapped
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Sub backButton_Tapped(sender As Object, e As BackRequestedEventArgs)
        Try
            If Frame.CanGoBack Then
                Frame.GoBack()
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try

    End Sub

    Private Sub BtnEditarEdad_Click(sender As Object, e As RoutedEventArgs)
        NbbEdad.IsEnabled = True
    End Sub

    Private Sub BtnEditarNombre_Click(sender As Object, e As RoutedEventArgs)
        TxtNombre.IsEnabled = True
    End Sub

    Private Sub BtnEditarObservaciones_Click(sender As Object, e As RoutedEventArgs)
        TxtObservaciones.IsEnabled = True
    End Sub

    Private Async Sub BtnActualizarMascota_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim GetDataToggle As Boolean
            If TgsEstadoMascota.IsOn = True Then
                GetDataToggle = True
            Else
                GetDataToggle = False
            End If
            If Await GetMascota.ActualizarMascota(IdMascota, TxtNombre.Text, NbbEdad.Value, GetDataToggle, TxtObservaciones.Text) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La Mascota ha sido Actualizada.")
                VolverAtras()
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Sub VolverAtras()
        If Frame.CanGoBack Then
            Frame.GoBack()
        End If
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaTextBoxVacio(TxtDescripcionImagen) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La descripcion de la Imagen no puede quedar Vacia", TxtDescripcionImagen)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtUrlImagen) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "La URL de la imagen es Obligatoria", TxtUrlImagen)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub BtnGuardarImagenMascota_Click(sender As Object, e As RoutedEventArgs)
        Try
            If ValidaDatos() = True Then
                If Await GetImagenMascota.InsertImagenMascota(TxtDescripcionImagen.Text, TxtUrlImagen.Text, IdMascota) = True Then
                    ObtenerImagenMascota = Await GetImagenMascota.ConsultaImagenMascotaPorIdMascota(IdMascota)
                    LblmensajeImagen.Visibility = Visibility.Collapsed
                    FlvImagenMascota.Visibility = Visibility.Visible
                    PspNumeroImagnes.Visibility = Visibility.Visible
                    PspNumeroImagnes.NumberOfPages = ObtenerImagenMascota.Count
                    FlvImagenMascota.ItemsSource = ObtenerImagenMascota
                    FlvImagenMascota.UpdateLayout()
                    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La imagen se Guardó con Exito")
                    GetUtilitarios.LimpiarControles(StpImagen)
                Else
                    GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Error al Guardar")
                End If
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
End Class
