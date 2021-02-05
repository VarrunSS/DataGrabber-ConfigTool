using DataGrabberConfig.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DataGrabberConfig.Logger
{
    public class LogWriter : ILogWriter
    {
        private readonly IConfigFields _config;

        public LogWriter(IConfigFields config)
        {
            _config = config;
        }
        
        public void Write(string logMessage, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            string m_exePath = _config.PathSetting.LogPath;
            try
            {
                using (StreamWriter txtWriter = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    try
                    {
                        txtWriter.Write("\r\nLog Entry : ");
                        txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                            DateTime.Now.ToLongDateString());
                        txtWriter.WriteLine("  :");
                        txtWriter.WriteLine("  :{0}; Line no: {1}; Method: {2}", logMessage, lineNumber, caller);
                        txtWriter.WriteLine("-------------------------------");
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }

    public interface ILogWriter
    {
        void Write(string logMessage, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
    }
}
