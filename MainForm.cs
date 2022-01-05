using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using Tabula;
using Tabula.Extractors;

namespace School_Calendar_Converter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            pdfFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private static Dictionary<string, Dictionary<DateTime, List<string>>> LoadPDFFile(string PDFFilePath)
        {
            var nonFormattedEvents = new Dictionary<string, Dictionary<DateTime, List<string>>>
            {
                { "MainCourse", new Dictionary<DateTime, List<string>>() }, // 本科
                { "AdvancedCourse", new Dictionary<DateTime, List<string>>() } // 専攻科
            };

            using (PdfDocument document = PdfDocument.Open(PDFFilePath, new ParsingOptions() { ClipPaths = true }))
            {
                ObjectExtractor oe = new ObjectExtractor(document);

                for (int pageNumber = 1; pageNumber <= document.NumberOfPages; pageNumber++)
                {
                    PageArea page = oe.Extract(pageNumber);

                    IExtractionAlgorithm ea = new SpreadsheetExtractionAlgorithm();
                    List<Table> tables = ea.Extract(page);
                    var table = tables[0];

                    // 初めの年・月を取得
                    string startDateStr = "";
                    for (int col = 0; col <= 2; col++)
                    {
                        if ((startDateStr = table[0, col].GetText()) != "") break;
                    }
                    if (startDateStr == "") continue;

                    int startYear = Convert.ToInt32(Regex.Match(startDateStr, @"(?<=\()[0-9]+(?=\))").Value);
                    int startMonth = Convert.ToInt32(Regex.Match(startDateStr, @"[0-9]+(?=月)").Value);
                    DateTime startDateTime = new DateTime(startYear, startMonth, 1);

                    int startCol; bool startColFound = false;
                    for (startCol = 0; startCol <= 2; startCol++)
                    {
                        int num;
                        Int32.TryParse(table[3, startCol].GetText(), out num);
                        if (num == 1)
                        {
                            startColFound = true;
                            break;
                        }
                    }
                    if (!startColFound) continue;

                    for (int m = 0; m < 4; m++)
                    {
                        int daysOfMonth = startDateTime.AddMonths(1).AddDays(-1).Day;

                        for (int day = 1; day <= daysOfMonth; day++)
                        {
                            int row = 3 + day - 1;

                            // 行番号を探索
                            int col; bool colFound = false;
                            for (col = startCol + 6 * m; col <= startCol + 7 * m; col++)
                            {
                                int checkDay;
                                Int32.TryParse(table[row, col].GetText(), out checkDay);
                                string dateStr = table[row, col + 1].GetText();
                                if (checkDay == day && table[row, col + 1].GetText() == startDateTime.AddDays(day - 1).ToString("ddd"))
                                {
                                    colFound = true;
                                    break;
                                }
                            }
                            if (!colFound) continue;

                            string mainCourseStr = table[row, col + 2].GetText();
                            string advancedCourseStr = table[row, col + 3].GetText();

                            char[] separator = { '/', '\r' };
                            var mainCourseEvents = new List<string>(mainCourseStr.Split(separator));
                            var advancedCourseEvents = new List<string>(advancedCourseStr.Split(separator));

                            if (mainCourseStr != "")
                            {
                                nonFormattedEvents["MainCourse"].Add(startDateTime.AddDays(day - 1), mainCourseEvents);
                            }
                            
                            if (!advancedCourseStr.All(char.IsDigit) && advancedCourseStr != "")
                            {
                                nonFormattedEvents["AdvancedCourse"].Add(startDateTime.AddDays(day - 1), advancedCourseEvents);
                            }
                        }

                        startDateTime = startDateTime.AddMonths(1);
                    }
                }
            }

            return nonFormattedEvents;
        }

        private void excelFileButton_Click(object sender, EventArgs e)
        {
            // PDFファイルのパス指定ダイアログを表示
            if (pdfFileDialog.ShowDialog() == DialogResult.OK)
            {
                pdfFileTextBox.Text = pdfFileDialog.FileName;
            }
        }

        private void pdfFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kagoshima-ct.ac.jp/student/schedule/");
        }

        private void PDF2ExcelLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://documentcloud.adobe.com/link/acrobat/pdf-to-excel/");
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            string workspacePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), DateTime.Now.ToString("yyyyMMdd HHhmmmsss"));

            // ワークスペースフォルダを作成
            Directory.CreateDirectory(workspacePath);

            var nonFormattedEvents = LoadPDFFile(pdfFileDialog.FileName);

            /* - CSVファイル書き出し - */
            string mainCourseCSVFilePath = Path.Combine(workspacePath, "本科.csv");
            string advancedCourseCSVFilePath = Path.Combine(workspacePath, "専攻科.csv");

            // 本科
            var mainCourseFormattedEvents = new FormattedEvents(nonFormattedEvents["MainCourse"]);
            mainCourseFormattedEvents.Export2CSV(mainCourseCSVFilePath);

            // 専攻科
            var advancedCourseFormattedEvents = new FormattedEvents(nonFormattedEvents["AdvancedCourse"]);
            advancedCourseFormattedEvents.Export2CSV(advancedCourseCSVFilePath);

            MessageBox.Show($"CSVファイルに変換しました。\nパス:\"{workspacePath}\"", "変換終了", MessageBoxButtons.OK);
        }
    }
}
