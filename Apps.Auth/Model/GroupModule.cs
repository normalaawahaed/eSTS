using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class GroupModule
    {
        private string _accessGroupId = "";
        public String AccessGroupId
        {
            get { return _accessGroupId; }
            set { _accessGroupId = value; }
        }

        private string _accessModuleId = "";
        public String AccessModuleId
        {
            get { return _accessModuleId; }
            set { _accessModuleId = value; }
        }


        //private bool _accessAdd = false;
        //public Boolean AccessAdd
        //{
        //    get { return _accessAdd; }
        //    set { _accessAdd = value; }
        //}

        //private bool _accessEdit = false;
        //public Boolean AccessEdit
        //{
        //    get { return _accessEdit; }
        //    set { _accessEdit = value; }
        //}

        //private bool _accessDelete = false;
        //public Boolean AccessDelete
        //{
        //    get { return _accessDelete; }
        //    set { _accessDelete = value; }
        //}
    }
}