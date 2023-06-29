Imports System.Globalization
Imports Windows.ApplicationModel.Appointments

Public Class Cl_DateTime
    Implements In_FechaHora
    Public Function ObtenerHoraActual() As String Implements In_FechaHora.ObtenerHoraActual
        Try
            Dim FechaHoraActual As Date = Date.Now
            Dim HoraActual As String = FechaHoraActual.ToString("hh:mm tt", CultureInfo.InvariantCulture)
            Return HoraActual
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ObtenerFechaActual() As Date Implements In_FechaHora.ObtenerFechaActual
        Try
            Dim FechaActual As Date
            FechaActual = Date.Now.Date
            Return FechaActual
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function MostrarFechaLarga(FechaAconvertir As String) As String
        Try
            Dim FechaSeleccionada = Date.Parse(FechaAconvertir)
            Dim FechaConvertida = FechaSeleccionada.ToString("MMMM d 'de' yyyy")
            Return FechaConvertida
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function SoloFecha(FechaAconvertir As Date) As Date
        Try
            Dim Fecha As Date = Date.Parse(FechaAconvertir)
            Return Fecha.Date
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function PonerFecha() As String
        Dim appointment = New Appointments.Appointment()
        Dim Fecha = Date.Now
        Dim time = Date.Now.TimeOfDay
        Dim timeZoneOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)
        Dim startTime = New DateTimeOffset(Fecha.Year, Fecha.Month, Fecha.Day, time.Hours, time.Minutes, 0, timeZoneOffset)
        appointment.StartTime = startTime
        appointment.Subject = "sfds"
        appointment.Location = "sfds"
        appointment.Details = "dfs"
        appointment.Duration = TimeSpan.FromHours(1)
        appointment.Reminder = TimeSpan.FromMinutes(15)
    End Function

    Public Function GetElementRect(element As FrameworkElement) As Rect
        Dim transform As GeneralTransform = element.TransformToVisual(Nothing)
        Dim point As Point = transform.TransformPoint(New Point())
        Return New Rect(point, New Size(element.ActualWidth, element.ActualHeight))
    End Function
End Class
