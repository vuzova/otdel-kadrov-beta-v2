using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

public class ReportGenerator
{
    private string _filePath;
    private string _reportName;
    private DataGridView _dataGridView;
    private DateTime _reportDate;
    private string _preparedBy;
    private string _dateFrom;
    private string _dateTo;
    private string _employeeName;

    public ReportGenerator(
        string reportName,
        DataGridView dataGridView, 
        DateTime reportDate, 
        string preparedBy,
        string employeeName = "",
        string dateFrom = "", 
        string dateTo = ""
        )
    {
        _filePath = "C:\\Users\\vasil\\OneDrive\\Рабочий стол\\report.docx";
        _reportName = reportName;
        _dataGridView = dataGridView;
        _reportDate = reportDate;
        _preparedBy = preparedBy;
        _employeeName = employeeName;
        _dateFrom = dateFrom;
        _dateTo = dateTo;
        
    }

    public void CreateReport()
    {
        using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(_filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        {
            // Добавляем основную часть документа.
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = new Body();

            // Добавляем заголовок отчета
            body.Append(CreateParagraph($"Отчет «{_reportName}»", true));
            body.Append(CreateParagraph(""));

            if (_dateTo != "" && _dateFrom != "") 
            {
                body.Append(CreateParagraph($"Период: {_dateTo} -  {_dateFrom}"));
            }


            // Добавляем информацию об отчете


            // Добавляем таблицу с данными
            if (_dataGridView != null)
            {
                body.Append(CreateTable(_dataGridView));
            }
            body.Append(CreateParagraph(""));

            body.Append(CreateParagraph($"Дата создания отчета: {_reportDate.ToString("dd.MM.yyyy")}"));

            body.Append(CreateParagraph(""));
            body.Append(CreateParagraph(""));
            body.Append(CreateParagraph(""));
            body.Append(CreateParagraph($"Подготовил: {_preparedBy} _____________ / {_employeeName} "));

            mainPart.Document.Append(body);
            mainPart.Document.Save();

            Process.Start(new ProcessStartInfo
            {
                FileName = _filePath,
                UseShellExecute = true // Используем оболочку для открытия файла
            });
        }
    }

    private Paragraph CreateParagraph(string text, bool isHeading = false)
    {
        Run run = new Run(new Text(text));
        Paragraph paragraph = new Paragraph(run);
        if (isHeading)
        {
            RunProperties runProperties = new RunProperties();
            runProperties.Append(new Bold()); // Устанавливаем жирное выделение
            run.PrependChild(runProperties); // Добавляем свойства к Run

            paragraph.ParagraphProperties = new ParagraphProperties(
            new Justification() { Val = JustificationValues.Center },
            new ParagraphStyleId() { Val = "Heading1" }
        );

            paragraph.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Heading1" });
        }
        return paragraph;
    }

    private Table CreateTable(DataGridView dataGridView)
    {
        Table table = new Table();

        // Создаем строку заголовков
        TableRow headerRow = new TableRow();
        for (int i = 0; i < dataGridView.ColumnCount; i++)
        {
            // Создаем текст с жирным шрифтом
            Run run = new Run(new Text(dataGridView.Columns[i].HeaderText));
            RunProperties runProperties = new RunProperties();
            runProperties.Append(new Bold()); // Устанавливаем жирное выделение
            run.PrependChild(runProperties); // Добавляем свойства к Run

            TableCell cell = new TableCell(new Paragraph(run));
            headerRow.Append(cell);
        }
        table.Append(headerRow);

        // Заполняем таблицу данными
        for (int i = 0; i < dataGridView.RowCount; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < dataGridView.ColumnCount; j++)
            {
                string cellValue;

                // Проверяем, является ли значение датой
                if (dataGridView.Rows[i].Cells[j].Value is DateTime dateValue)
                {
                    // Форматируем дату в нужный формат "дд.мм.гггг"
                    cellValue = dateValue.ToString("dd.MM.yyyy");
                }
                else
                {
                    // Если не дата, просто берем строковое представление
                    cellValue = dataGridView.Rows[i].Cells[j].Value?.ToString() ?? "";
                }

                TableCell cell = new TableCell(new Paragraph(new Run(new Text(cellValue))));
                row.Append(cell);
            }
            table.Append(row);
        }

        return table;
    }
}