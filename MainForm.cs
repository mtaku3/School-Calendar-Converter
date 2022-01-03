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
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace School_Calendar_Converter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            excelFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private static Dictionary<string, Dictionary<DateTime, List<string>>> LoadExcelFile(string ExcelFilePath)
        {
            // Excelクライアントの初期化
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;

            // Excelワークブックからシート読み込み
            Excel.Workbook workbook = excel.Workbooks.Open(ExcelFilePath, Type.Missing, true);
            Excel.Worksheet worksheet = workbook.Sheets[1];

            var nonFormattedEvents = new Dictionary<string, Dictionary<DateTime, List<string>>>
            {
                { "MainCourse", new Dictionary<DateTime, List<string>>() }, // 本科
                { "AdvancedCourse", new Dictionary<DateTime, List<string>>() } // 専攻科
            };

            // 各月のタイトルセルを探す
            string pattern = ".+年.*（[0-9]+）.*月";
            for (int row = 1; row <= 100; row++)
            {
                for (int col = 1; col <= 26; col++)
                {
                    Range cell = worksheet.Cells[row, col];
                    if (cell.Value2 != null && Regex.IsMatch(Convert.ToString(cell.Value2), pattern))
                    {
                        string matchedString = Regex.Match(Convert.ToString(cell.Value2), pattern).Value;
                        int year = Convert.ToInt32(Regex.Match(matchedString, "(?<=（)[0-9]+(?=）)").Value);
                        int month = Convert.ToInt32(Regex.Match(matchedString, "[0-9]+(?=月)").Value);

                        // 各月の最終日
                        int lastDay = DateTime.Parse($"{year}/{month % 12 + 1}/1").AddDays(-1).Day;

                        // 日付ごとのイベントを取得
                        char[] separator = { '／', '\n', ' ', '　' };
                        for (int day = 1; day <= lastDay; day++)
                        {
                            DateTime date = new DateTime(year, month, day);

                            string mainCourseEventsStr = "", advancedCourseEventsStr = "";

                            Range mainCourseCell = worksheet.Cells[row + day + 2, col + 2];
                            mainCourseEventsStr = mainCourseCell.Value2 != null ? Convert.ToString(mainCourseCell.Value2) : "";
                            if (mainCourseEventsStr != "")
                            {
                                var mainCourseEvents = new List<string>(mainCourseEventsStr.Split(separator));
                                nonFormattedEvents["MainCourse"].Add(date, mainCourseEvents);
                            }

                            Range advancedCourseCell = worksheet.Cells[row + day + 2, col + 3];
                            advancedCourseEventsStr = advancedCourseCell.Value2 != null ? Convert.ToString(advancedCourseCell.Value2) : "";
                            if (advancedCourseEventsStr != "")
                            {
                                var advancedCourseEvents = new List<string>(advancedCourseEventsStr.Split(separator));
                                nonFormattedEvents["AdvancedCourse"].Add(date, advancedCourseEvents);
                            }
                        }
                    }
                }
            }

            return nonFormattedEvents;
        }

        private void excelFileButton_Click(object sender, EventArgs e)
        {
            // PDFファイルのパス指定ダイアログを表示
            if (excelFileDialog.ShowDialog() == DialogResult.OK)
            {
                excelFileTextBox.Text = excelFileDialog.FileName;
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

            var nonFormattedEvents = LoadExcelFile(excelFileDialog.FileName);

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
