using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class AccessModule
    {
        private string _accessModuleId = "";
        public String AccessModuleId
        {
            get { return _accessModuleId; }
            set { _accessModuleId = value; }
        }

        private string _description = "";
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _parentId = "";
        public String ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private int _moduleLevel = 0;
        public Int32 ModuleLevel
        {
            get { return _moduleLevel; }
            set { _moduleLevel = value; }
        }

        private int _moduleSeq = 0;
        public Int32 ModuleSeq
        {
            get { return _moduleSeq; }
            set { _moduleSeq = value; }
        }

        private string _moduleLink = "";
        public String ModuleLink
        {
            get { return _moduleLink; }
            set { _moduleLink = value; }
        }

        private bool _isActive = true;
        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}