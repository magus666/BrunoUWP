Public Class Cl_Utilitarios

    Public Function GenerarCodigoAleatorio() As String
        Dim Iniciales As String = "BS-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function

End Class
