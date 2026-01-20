using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class AccessLevel
    {
        private string _accessLevelId = "";
        public String AccessLevelId
        {
            get { return _accessLevelId; }
            set { _accessLevelId = value; }
        }

        private string _description = "";
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private bool _isActive = true;
        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}