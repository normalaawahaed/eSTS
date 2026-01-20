using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apps.Common
{
    public class LogDB
    {
        private int _logSeq = -1;
        public Int32 LogSeq
        {
            get { return _logSeq; }
            set { _logSeq = value; }
        }

        private string _logActivityType = "";
        public String LogActivityType
        {
            get { return _logActivityType; }
            set { _logActivityType = value; }
        }

        private string _logActivity = "";
        public String LogActivity
        {
            get { return _logActivity; }
            set { _logActivity = value; }
        }

        private DateTime _logDatetime = DateTime.Now;
        public DateTime LogDatetime
        {
            get { return _logDatetime; }
            set { _logDatetime = value; }
        }

        private string _logRemark = "";
        public String LogRemark
        {
            get { return _logRemark; }
            set { _logRemark = value; }
        }

        private string _logger = "";
        public String Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }
    }
}
