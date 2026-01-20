using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class LevelModule
    {
        private string _accessLevelId = "";
        public String AccessLevelId
        {
            get { return _accessLevelId; }
            set { _accessLevelId = value; }
        }

        private string _accessModuleId = "";
        public String AccessModuleId
        {
            get { return _accessModuleId; }
            set { _accessModuleId = value; }
        }

        private bool _accessView = false;
        public Boolean AccessView
        {
            get { return _accessView; }
            set { _accessView = value; }
        }

        private bool _accessAdd = false;
        public Boolean AccessAdd
        {
            get { return _accessAdd; }
            set { _accessAdd = value; }
        }

        private bool _accessEdit = false;
        public Boolean AccessEdit
        {
            get { return _accessEdit; }
            set { _accessEdit = value; }
        }

        private bool _accessDelete = false;
        public Boolean AccessDelete
        {
            get { return _accessDelete; }
            set { _accessDelete = value; }
        }
    }
}