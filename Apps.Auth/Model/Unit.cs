using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class Unit
    {
        private int _unitID;
        public Int32 UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        private string _unitName = "";
        public String UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }

        private bool _isActive = true;
        public Boolean IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        private string _createdBy;
        public String CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        private DateTime _createdOn;
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        private string _modifiedBy;
        public String ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private DateTime _modifiedOn;
        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }
    }
}