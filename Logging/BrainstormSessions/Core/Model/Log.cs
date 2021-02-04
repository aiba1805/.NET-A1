using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainstormSessions.Core.Model
{
    public class Log
    {
        public int Id { get; set; }
        public string  Message { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }

        public string Logger { get; set; }

    }
}
