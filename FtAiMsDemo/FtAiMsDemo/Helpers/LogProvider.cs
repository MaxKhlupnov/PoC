using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace FtAiMsDemo.Helpers
{
    public class LogProvider : ObservableCollection<LogRecord>
    {
        

        public void WriteInfo(string message)
        {
            this.Add(new LogRecord() { RecordType = RecordType.Info, Message = message });
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void WriteError(Exception ex)
        {
            LogRecord record = new LogRecord() { RecordType = RecordType.Error, Message = String.Format("{0} {1}", ex.Message, ex.StackTrace) };
            this.Add(record);
            System.Diagnostics.Debug.WriteLine(record.Message);
        }

    }

    public enum RecordType
    {
        Info,
        Warning,
        Error,
    }

    public class LogRecord{
        public RecordType RecordType { get; set; }
        public string Message { get; set; }
    }
}
