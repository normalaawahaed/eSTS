using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Auth
{
    public class Users
    {
        private string _userId = "";
        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _userPass = "";
        public String UserPass
        {
            get { return _userPass; }
            set { _userPass = value; }
        }

        private string _fullName = "";
        public String FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _idNo = "";
        public String IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }

        private string _gender = "";
        public String Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }


        private string _address1 = "";
        public String Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }


        private string _address2 = "";
        public String Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }


        private string _city = "";
        public String City
        {
            get { return _city; }
            set { _city = value; }
        }


        private string _state = "";
        public String State
        {
            get { return _state; }
            set { _state = value; }
        }


        private string _country = "";
        public String Country
        {
            get { return _country; }
            set { _country = value; }
        }


        private string _postcode = "";
        public String Postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        private string _phone = "";
        public String Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _emailAddress = "";
        public String EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        private string _accessGroupId = "";
        public String AccessGroupId
        {
            get { return _accessGroupId; }
            set { _accessGroupId = value; }
        }

        private string _accessLevelId = "";
        public String AccessLevelId
        {
            get { return _accessLevelId; }
            set { _accessLevelId = value; }
        }

        private int _unitId;
        public Int32 UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }

        private string _designation = "";
        public String Designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        private string _remark = "";
        public String Remark
        {
            get { return _remark; }
            set { _remark = value; }
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