using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eSTS.DAL
{
    public class DALMMS
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion
        public DataSet GetBunkerOperatorLic(string LicCompanyID)
        {
            try
            {
                string querystring = null;
                querystring = "select * from dbo.v_LicCompanyAll where LicCompanyID ='" + LicCompanyID + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "License");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }

        public DataSet GetOPAppointAgent(string BOLicCompanyID)
        {
            try
            {
                string querystring = null;
                querystring = @"select b.DtLicExp,a.* from dbo.v_AppointAgentAll a 
                                inner join v_LicCompanyAll b on a.LicCompanyID = b.LicCompanyID
                                where b.LicCompanyID ='" + BOLicCompanyID + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "v_AppointAgent");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }
        public int GetLocation(string LicCompanyID)
        {
            object result = null;
            try
            {
                string querystring = null;
                querystring = "select Location from dbo.v_LicCompanyAll where LicCompanyID ='" + LicCompanyID + "'";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    result = cmd.ExecuteScalar();
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return Convert.ToInt32(result);
        }
        public DataSet GetShipDetails(string filter)
        {
            try
            {
                string querystring = null;
                querystring = "select * from MMSSync.dbo.ShipMaster " + filter;

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "ShipMaster");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }
        public DataSet GetShipFlag(string filter)
        {
            try
            {
                string querystring = null;
                querystring = "select * from MMSSync.dbo.ShipFlag " + filter + " Order by ShipFlag";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "ShipFlag");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }
        public DataSet GetShipType(string filter)
        {
            try
            {
                string querystring = null;
                querystring = "select * from MMSSync.dbo.ShipType " + filter;

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "ShipType");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }
        public DataSet GetPort()
        {
            try
            {
                string querystring = null;
                querystring = "select * from v_port ";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "Port");
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds;
        }
        public bool SaveShipDetails(string filter,string OperationAppID, string compID,string createdBy)
        {
            try
            {
                string querystring = null;
                querystring = @"insert into OpAppointShip(OpAppointShipID,OpAppointAgentID,ShipID,CompID, [OffNo], [ShipName], [PortReg], [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipType], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight],CreatedBy,CreatedDate)
        select NEWID(),@pOperationAppID,ShipID, @pCompID,[OffNo], [ShipName], [PortReg], [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipType], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight], @pCreatedBy,GetDate()
from v_shipmaster " + filter;

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("pOperationAppID", OperationAppID);
                    cmd.Parameters.AddWithValue("pCompID", compID);
                    cmd.Parameters.AddWithValue("pCreatedBy", createdBy);
                    

                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public bool SaveLicCompanyVessel(string filter, string LicCompanyID,string licenseNo, DateTime dtValidFrom, DateTime dtValidTo, DateTime dtDSLValidFrom, DateTime dtDSLValidTo, string createdBy)
        {
            try
            {
                string querystring = null;
                querystring = @"insert into LicCompanyVessel(LicCompanyVesselID,LicCompanyID,ShipLicenseNo,STSPermitValidFrom,STSPermitValidTo,DSLValidFrom,DSLValidTo,ShipID, [OffNo], [ShipName], [PortReg], [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipType], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight],CreatedBy,CreatedDate)
        select NEWID(),@pLicCompanyID,@pShipLicenseNo,@pValidFrom,@pValidTo,@pDSLValidFrom,@pDSLValidTo,ShipID,[OffNo], [ShipName],case when [PortReg] is null then '' else  [PortReg]  end as 'PortReg', [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipTypeID], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight], @pCreatedBy,GetDate()
from v_shipmaster " + filter;

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("pLicCompanyID", LicCompanyID);
                    cmd.Parameters.AddWithValue("pShipLicenseNo", licenseNo);
                    cmd.Parameters.AddWithValue("pValidFrom", dtValidFrom.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("pValidTo", dtValidTo.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("pDSLValidFrom", dtDSLValidFrom.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("pDSLValidTo", dtDSLValidTo.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("pCreatedBy", createdBy);


                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public bool SaveLicCompanyVessel(string filter,string LicCompanyVesselID, string LicCompanyID, string createdBy)
        {
            try
            {
                string querystring = null;
                querystring = @"insert into LicCompanyVessel(LicCompanyVesselID,LicCompanyID,ShipID, [OffNo], [ShipName], [PortReg], [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipType], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight],CreatedBy,CreatedDate)
        select @pLicCompanyVesselID,@pLicCompanyID,ShipID,[OffNo], [ShipName],case when [PortReg] is null then '' else  [PortReg]  end as 'PortReg', [CallSign], [IMONo], [YearReg], [YearBuilt], [GRT], [NRT], [DWT], [ShipTypeID], [VoyageType], [LOA]
, [Status], [OwnerName], [ShipFlag], [Breadth], [Depth], [STDDraft], [ShipCapacity], [ShipBeam], [DispmtWeight], @pCreatedBy,GetDate()
from v_shipmaster " + filter;

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("pLicCompanyVesselID", LicCompanyVesselID);
                    cmd.Parameters.AddWithValue("pLicCompanyID", LicCompanyID);
                    cmd.Parameters.AddWithValue("pCreatedBy", createdBy);


                    int result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
    }
}