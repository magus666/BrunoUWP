Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Windows.Storage
Imports Windows.System

Public Class Cl_GenerarWord
    Public Async Function CrearWord() As Task(Of Boolean)
        Try
            Dim filePath As String = Path.Combine(ApplicationData.Current.LocalFolder.Path, "MeloCaramelo.docx")

            Using wordDocument As WordprocessingDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document)
                Dim mainPart As MainDocumentPart = wordDocument.AddMainDocumentPart()
                mainPart.Document = New Document()

                Dim body As New Body()

                ' Agregar el título y centrarlo"
                Dim titulo As New Paragraph()
                Dim runProperties As New RunProperties()
                runProperties.Append(New Bold(), New Italic())
                Dim tituloRun As New Run()
                tituloRun.Append(runProperties, New Text("Factura Electronica"))
                titulo.Append(tituloRun)
                titulo.ParagraphProperties = New ParagraphProperties(New Justification() With {.Val = JustificationValues.Center})

                ' Agregar los párrafos existentes
                Dim paragraph1 As New Paragraph(New Run(New Text("Su factura para El mes de Agosoto es de:")))


                Dim tabla As New Table()
                Dim tableProperties As New TableProperties(New TableBorders(New TopBorder(), New BottomBorder(), New LeftBorder(), New RightBorder(), New InsideHorizontalBorder(), New InsideVerticalBorder()))
                tabla.AppendChild(tableProperties)

                ' Crear una fila para los encabezados
                Dim encabezados As New TableRow()

                ' Agregar celdas para nombre, descripción y valor
                Dim celdaNombre As New TableCell(New Paragraph(New Run(New Text("Nombre"))))
                Dim celdaDescripcion As New TableCell(New Paragraph(New Run(New Text("Descripción"))))
                Dim celdaValor As New TableCell(New Paragraph(New Run(New Text("Valor"))))

                ' Agregar las celdas a la fila de encabezados
                encabezados.Append(celdaNombre)
                encabezados.Append(celdaDescripcion)
                encabezados.Append(celdaValor)

                ' Agregar la fila de encabezados a la tabla
                tabla.Append(encabezados)

                For i As Integer = 1 To 3
                    Dim fila As New TableRow()
                    For j As Integer = 1 To 3 ' Cambio el límite superior a 3
                        Dim celda As New TableCell(New Paragraph(New Run(New Text($"Fila {i}, Columna {j}"))))
                        fila.Append(celda)
                    Next
                    tabla.Append(fila)
                Next

                Dim paragraph2 As New Paragraph(New Run(New Table(tabla)))
                body.Append(titulo, paragraph1, paragraph2)
                mainPart.Document.Append(body)
            End Using

            ' Abrir la carpeta local y el archivo recién creado
            Await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
