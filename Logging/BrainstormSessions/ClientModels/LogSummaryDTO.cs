using BrainstormSessions.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainstormSessions.ViewModels
{
    public class LogSummaryDTO
    {
        public List<Log> Errors { get; set; }
        public int ErrorCount { get; set; }
        public int InfoCount { get; set; }
        public int WarnCount { get; set; }
        public int DebugCount { get; set; }
    }
}
