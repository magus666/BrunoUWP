' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.UI.Xaml.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CrearRaza
    Inherits Page
    Dim IdTipoMascota As Integer
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetRaza As New Cl_RazaMascota
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetTipoMascota As New CL_TipoMascota

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim ListaTipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            CmbTipoMascota.ItemsSource = ListaTipoMascota
            CmbTipoMascota.DisplayMemberPath = "Nombre_TipoMascota"
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            If ValidaDatos() = True Then
                Await GetRaza.InsertarRaza(TxtNombreRaza.Text, TxtDescripcionRaza.Text, IdTipoMascota)
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La raza se ha Guardado con Exito.")
                Select Case IdTipoMascota
                    Case 1
                        RdbTipoMascota.SelectedIndex = 0
                    Case 2
                        RdbTipoMascota.SelectedIndex = 1
                    Case 3
                        RdbTipoMascota.SelectedIndex = 2
                    Case 4
                        RdbTipoMascota.SelectedIndex = 3
                End Select
                GetUtilitarios.LimpiarControles(StpPrincipal)
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
    Private Sub CmbTipoMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As TipoMascotaModel = CType(comboBox.SelectedItem, TipoMascotaModel)
            If CmbTipoMascota.SelectedIndex = -1 Then
                IdTipoMascota = 0
            Else
                IdTipoMascota = selectedItem.Id_TipoMascota
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Async Function GetRazaMascota() As Task(Of IEnumerable)
        Try
            Dim TipoMascota = Await GetTipoMascota.ConsultarTipoMascota
            Dim Raza = Await GetRaza.ConsultarRazaMascota
            Dim RetornoRaza = (From Rza In Raza
                               Join Tpm In TipoMascota On
                                   Rza.Id_TipoMascota Equals Tpm.Id_TipoMascota
                               Select Tpm.Nombre_TipoMascota,
                                   Rza.Id_TipoMascota,
                                   Rza.Nombre_Raza,
                                   Rza.Descripcion_Raza)
            Return RetornoRaza
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub RdbTipoMascota_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim SeleccionRadio As String = TryCast(TryCast(sender, RadioButtons).SelectedItem, String)
            Select Case SeleccionRadio
                Case "Perros"
                    Dim Raza = Await GetRazaMascota()
                    Dim RetornoRazaPerro = (From Msc In Raza
                                            Where Msc.Id_TipoMascota = 1
                                            Order By Msc.Nombre_Raza
                                            Select Msc)
                    DtgRazaMascota.ItemsSource = RetornoRazaPerro
                Case "Gatos"
                    Dim Raza = Await GetRazaMascota()
                    Dim RetornoRazaGato = (From Msc In Raza
                                           Where Msc.Id_TipoMascota = 2
                                           Order By Msc.Nombre_Raza
                                           Select Msc)
                    DtgRazaMascota.ItemsSource = RetornoRazaGato
                Case "Conejos"
                    Dim Raza = Await GetRazaMascota()
                    Dim RetornoRazaConejo = (From Msc In Raza
                                             Where Msc.Id_TipoMascota = 3
                                             Order By Msc.Nombre_Raza
                                             Select Msc)
                    DtgRazaMascota.ItemsSource = RetornoRazaConejo
                Case "Todas"
                    Dim Raza = Await GetRazaMascota()
                    Dim RetornoRazaTodas = (From Msc In Raza
                                            Order By Msc.Nombre_Raza
                                            Select Msc)
                    DtgRazaMascota.ItemsSource = RetornoRazaTodas
            End Select
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaComboBoxVacio(CmbTipoMascota) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "Seleccione un tipo de Mascota valido", CmbTipoMascota)
                Return False
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtNombreRaza) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El campo Nombre de Raza no puede estar vacio", TxtNombreRaza)
                Return False
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtDescripcionRaza) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Alerta", "El campo de descripcion no puede estar vacio", TxtDescripcionRaza)
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
