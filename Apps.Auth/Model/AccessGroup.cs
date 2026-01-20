using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class AccessGroup
    {
        private string _accessGroupId = "";
        public String AccessGroupId
        {
            get { return _accessGroupId; }
            set { _accessGroupId = value; }
        }

        private string _description = "";
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private bool _isWIBS = false;
        public Boolean IsWIBS
        {
            get { return _isWIBS; }
            set { _isWIBS = value; }
        }
        private bool _isWCMS = false;
        public Boolean IsWCMS
        {
            get { return _isWCMS; }
            set { _isWCMS = value; }
        }
        private bool _isKhairat = false;
        public Boolean IsKhairat
        {
            get { return _isKhairat; }
            set { _isKhairat = value; }
        }

        private bool _isAdmin = false;
        public Boolean IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        private bool _isActive = true;
        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}