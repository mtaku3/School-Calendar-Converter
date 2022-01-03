using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Calendar_Converter
{
    public class FormattedEvent
    {
        // https://support.google.com/a/users/answer/37118?hl=ja
        private string subject; // 予定の名前（必須）
        private DateTime startDateTime; // 予定の開始日（必須）
        private DateTime? endDateTime; // 予定の終了日
        private bool allDayEvent; // 終日の予定であるかどうかを指定します。終日の予定の場合は True、そうでない場合は False と入力します。
        private string description; // 予定の説明やメモ
        private string location; // 予定の場所
        private bool privateEnabled; // 予定を限定公開にするかどうかを指定します。予定が限定公開の場合は True、そうでない場合は False と入力します

        public FormattedEvent(string Subject, DateTime StartDateTime, DateTime? EndDateTime = null, bool AllDayEvent = true, string Description = "", string Location = "", bool PrivateEnabled = false)
        {
            this.subject = Subject;
            this.startDateTime = StartDateTime;
            this.endDateTime = EndDateTime;
            this.allDayEvent = AllDayEvent;
            this.description = Description;
            this.location = Location;
            this.privateEnabled = PrivateEnabled;
        }

        public void write(StreamWriter sw)
        {
            // CSVファイルに書き出し（１行）
            var stringList = new List<string>();

            stringList.Add(subject);
            stringList.Add(startDateTime.ToString("MM/dd/yyyy")); // 例: 05/30/2020
            if (startDateTime == endDateTime)
            {
                stringList.AddRange(new List<string> { "", "", "" });
            }
            else
            {
                // stringList.Add(startDateTime.ToString("hh:mm tt")); // 例: 10:00 AM
                stringList.Add("");
                stringList.Add(endDateTime != null ? ((DateTime)endDateTime).ToString("MM/dd/yyyy") : ""); // 例: 05/30/2020
                // stringList.Add(endDateTime != null ? ((DateTime)endDateTime).ToString("hh:mm tt") : ""); // 例: 1:00 PM
                stringList.Add("");
            }
            stringList.Add(allDayEvent.ToString());
            stringList.Add(description);
            stringList.Add(location);
            stringList.Add(privateEnabled.ToString());

            var str = string.Join(",", stringList);
            sw.WriteLine(str);
        }
    }

    public class FormattedEvents
    {
        public List<FormattedEvent> formattedEvents = new List<FormattedEvent>();

        public FormattedEvents(Dictionary<DateTime, List<string>> nonFormattedEvents)
        {
            for (int d = 0; d < nonFormattedEvents.Keys.Count; d++)
            {
                DateTime startDateTime = nonFormattedEvents.Keys.ElementAt(d);

                foreach (string eventSubject in nonFormattedEvents[startDateTime])
                {
                    DateTime endDateTime = startDateTime;
                    // 同じイベントが連日行われていた場合、一つのイベントにまとめる
                    while (true)
                    {
                        if (!nonFormattedEvents.ContainsKey(endDateTime.AddDays(1))) break;
                        else
                        {
                            if (nonFormattedEvents[endDateTime.AddDays(1)].Contains(eventSubject))
                            {
                                nonFormattedEvents[endDateTime.AddDays(1)].Remove(eventSubject);
                                endDateTime = endDateTime.AddDays(1);
                            }
                            else break;
                        }
                    }

                    var formattedEvent = new FormattedEvent(eventSubject, startDateTime, endDateTime);
                    formattedEvents.Add(formattedEvent);
                }
            }
        }

        public void Export2CSV(string CSVFilePath)
        {
            // CSVファイルに書き出し
            StreamWriter sw = new StreamWriter(CSVFilePath, false, Encoding.UTF8);

            sw.WriteLine("Subject,Start Date,Start Time,End Date,End Time,All Day Event,Description,Location,Private");

            foreach (var formattedEvent in formattedEvents)
            {
                formattedEvent.write(sw);
            }

            sw.Close();
        }
    }
}
