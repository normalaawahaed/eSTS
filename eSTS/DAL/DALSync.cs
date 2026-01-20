using Apps.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eSTS.DAL
{
    public class DALSync
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        #region SyncCompanyProfile
        public bool SyncCompanyProfile()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    DataTable sourceTable = getMMSCompanyProfile();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        string sqlCmd = "";
                        if (!CheckRecordExist(dr["orgzid"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO  MMSSync.dbo.CompanyProfile (Orgzid,CompanyName,Address1,Address2,Address3,TelNo,FaxNo,ContactPerson
                                            , EmailAddress, OrgzType, IsLock, IsBlacklist, SyncDate)
                                        VALUES
                                            (@Orgzid, @CompanyName, @Address1, @Address2, @Address3, @TelNo, @FaxNo, @ContactPerson,
                                        @EmailAddress, @OrgzType, @IsLock, @IsBlacklist, @SyncDate)";
                        }
                        else
                        {
                            sqlCmd = @"UPDATE MMSSync.dbo.CompanyProfile SET CompanyName = @CompanyName, Address1 = @Address1
                                              , Address2 = @Address2, Address3 = @Address3
                                              , TelNo = @TelNo, FaxNo = @FaxNo, ContactPerson = @ContactPerson
                                              , EmailAddress = @EmailAddress, OrgzType = @OrgzType, IsLock = @IsLock,
                                               IsBlacklist = @IsBlacklist, SyncDate = @SyncDate
                                         WHERE Orgzid = @Orgzid";
                        }
                      
                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@Orgzid", dr["orgzid"].ToString());
                                cmd.Parameters.AddWithValue("@CompanyName", dr["CompanyName"].ToString());
                                cmd.Parameters.AddWithValue("@Address1", dr["Address1"].ToString());
                                cmd.Parameters.AddWithValue("@Address2", dr["Address2"].ToString());
                                cmd.Parameters.AddWithValue("@Address3", dr["Address3"].ToString());
                                cmd.Parameters.AddWithValue("@TelNo", dr["TelNo"].ToString());
                                cmd.Parameters.AddWithValue("@FaxNo", dr["FaxNo"].ToString());
                                cmd.Parameters.AddWithValue("@ContactPerson", dr["ContactPerson"].ToString());
                                cmd.Parameters.AddWithValue("@EmailAddress", dr["EmailAddress"].ToString());
                                cmd.Parameters.AddWithValue("@OrgzType", dr["OrgzType"].ToString());
                                cmd.Parameters.AddWithValue("@IsLock", dr["IsLock"].ToString());
                                cmd.Parameters.AddWithValue("@IsBlacklist", dr["IsBlacklist"].ToString());
                                cmd.Parameters.AddWithValue("@SyncDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public DataTable getMMSCompanyProfile()
        {
            try
            {
                string querystring = null;
                querystring = @"select orgzid,company_name as CompanyName,address1,address2,address3,tel_no as TelNo,fax_no as FaxNo, contact_person as ContactPerson, email_address as EmailAddress, orgz_type as OrgzType,is_lock as IsLock,is_blacklist as IsBlacklist
                                from 
                                (
                                select roc_no as orgzid,company_name,address1,address2,address3,tel_no,fax_no,contact_person,email_address,'NGOV'  as orgz_type,case when is_lock is null then 0 else 1 end as is_lock,case when is_blacklist is null then 0 else 1 end as is_blacklist  from SRV_NAME4.eRegistration.dbo.company_non_gov
                                union
                                select gov_id as orgzid,company_name,address1,address2,address3,tel_no,fax_no,contact_person,email_address,'NGOV'  as orgz_type,case when is_lock is null then 0 else 1 end as is_lock,case when is_blacklist is null then 0 else 1 end as is_blacklist  from SRV_NAME4.eRegistration.dbo.company_gov
                                ) A
                                Except
                                select Orgzid,CompanyName,Address1,Address2,Address3,TelNo,FaxNo,ContactPerson,EmailAddress,OrgzType,IsLock,IsBlacklist 
                                from MMSSync.dbo.CompanyProfile";


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSCompanyProfile");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public bool CheckRecordExist(string orgzid)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from MMSSync.dbo.CompanyProfile where orgzid='" + orgzid + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        #endregion

        #region Sync License Company
        public bool SyncLicense()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    
                    DataTable sourceTable = getMMSLicCompany();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        Guid stsLicenseID = Guid.NewGuid();
                        string sqlCmd = "";
                        if (!CheckRecordLicExist(dr["LicenseID"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO  LicCompany
                                       (LicCompanyID,CompID,ServiceCode,LicenseID,LicDateIssue,LicDateExp,DtLicIssue
                                       ,DtLicExp,Location,ServiceType,CaseNUm,VesselName,VesselIMO,CreatedDate)
                                        VALUES
                                       (@LicCompanyID,@CompID,@ServiceCode,@LicenseID, @LicDateIssue,@LicDateExp,
                                       @DtLicIssue,@DtLicExp,@Location,@ServiceType,@CaseNUm,@VesselName,@VesselIMO, @CreatedDate)";
                        }
                        else
                        {
                            sqlCmd = @"UPDATE  LicCompany
                                    SET  CompID = @CompID,ServiceCode = @ServiceCode,
                                    LicDateIssue = @LicDateIssue,LicDateExp = @LicDateExp,DtLicIssue = @DtLicIssue, 
                                    DtLicExp = @DtLicExp,Location = @Location,ServiceType = @ServiceType,CaseNUm = @CaseNUm,VesselName=@VesselName,VesselIMO=@VesselIMO,CreatedDate = @CreatedDate 
                                    WHERE LicenseID = @LicenseID";
                        }

                        using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                        {
                            cmd.Parameters.AddWithValue("@LicCompanyID", stsLicenseID);
                            cmd.Parameters.AddWithValue("@CompID", dr["CompID"].ToString());
                            cmd.Parameters.AddWithValue("@ServiceCode", dr["ServiceCode"].ToString());
                            cmd.Parameters.AddWithValue("@LicenseID", dr["LicenseID"].ToString());
                            cmd.Parameters.AddWithValue("@LicDateIssue", dr["LicDateIssue"].ToString());
                            cmd.Parameters.AddWithValue("@LicDateExp", dr["LicDateExp"].ToString());
                            cmd.Parameters.AddWithValue("@DtLicIssue", dr["DtLicIssue"]);
                            cmd.Parameters.AddWithValue("@DtLicExp", dr["DtLicExp"]);
                            cmd.Parameters.AddWithValue("@Location", dr["Location"]);
                            cmd.Parameters.AddWithValue("@ServiceType", dr["ServiceType"].ToString());
                            cmd.Parameters.AddWithValue("@CaseNUm", dr["CASE_NUM"].ToString());
                            cmd.Parameters.AddWithValue("@VesselName", dr["Vessel_Name"].ToString());
                            cmd.Parameters.AddWithValue("@VesselIMO", dr["Vessel_IMO"].ToString());
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    DataTable licTable = getLicCompany();
               
                    foreach (DataRow dr in licTable.Rows)
                    {
                        string a = "";
                        if (dr[1].ToString() == "569838X")
                            a = "1";
                        Guid stsLicenseID = new Guid(dr["LicCompanyID"].ToString());
                     string sqlCmd = "";
                        if (!CheckRecordVesselLicense(dr["LicenseID"].ToString()))
                        {
                            string [] shipName = dr["VesselName"].ToString().Split(' ');

                            string whereShipName = " Where ";
                            foreach (string str in shipName)
                            {
                                whereShipName += "shipname like '%" + str + "%' or ";
                            }
                            whereShipName = whereShipName.Substring(0, whereShipName.Length - 3);
                            DataTable dtShip = getMMSShipByIMO(dr["VesselIMO"].ToString(),whereShipName);
                            if (dtShip != null)
                            {
                                DataRow drShip = dtShip.Rows[0];
                                sqlCmd = @"INSERT INTO LicCompanyVessel (CompID,LicCompanyVesselID,LicCompanyID,ShipLicenseNo,ShipID,OffNo,ShipName,PortReg,CallSign,IMONo,YearReg,ShipType,YearBuilt,LOA,Breadth,Depth,GRT,NRT
   ,DWT,OwnerName,ShipFlag,[Status],VoyageType,STDDraft,ShipCapacity,ShipBeam,DispmtWeight,createdBy,CreatedDate)
                                     VALUES
                                     (@CompID,@LicCompanyVesselID,@LicCompanyID,@ShipLicenseNo,@ShipID,@OffNo,@ShipName,@PortReg,@CallSign,@IMONo,@YearReg,@ShipType,@YearBuilt,@LOA,@Breadth,@Depth,@GRT,@NRT
   ,@DWT,@OwnerName,@ShipFlag,@Status,@VoyageType,@STDDraft,@ShipCapacity,@ShipBeam,@DispmtWeight,@createdBy,@CreatedDate)";


                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    
                                    cmd.Parameters.AddWithValue("@CompID", dr["CompID"].ToString());
                                    cmd.Parameters.AddWithValue("@LicCompanyVesselID", Guid.NewGuid());
                                    cmd.Parameters.AddWithValue("@LicCompanyID", stsLicenseID);
                                    cmd.Parameters.AddWithValue("@ShipLicenseNo", dr["LicenseID"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipID", drShip["ShipID"].ToString());
                                    // cmd.Parameters.AddWithValue("@ShipCategory", drShip["ShipCategory"].ToString());
                                    cmd.Parameters.AddWithValue("@OffNo", drShip["OffNo"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipName", drShip["ShipName"].ToString());
                                    cmd.Parameters.AddWithValue("@PortReg", drShip["PortReg"].ToString());
                                    cmd.Parameters.AddWithValue("@CallSign", drShip["CallSign"].ToString());
                                    cmd.Parameters.AddWithValue("@IMONo", drShip["IMONo"].ToString());
                                    // cmd.Parameters.AddWithValue("@RegNo", drShip["RegNo"].ToString());
                                    cmd.Parameters.AddWithValue("@YearReg", drShip["YearReg"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipType", drShip["ShipTypeID"].ToString());
                                    cmd.Parameters.AddWithValue("@YearBuilt", drShip["YearBuilt"].ToString());
                                    cmd.Parameters.AddWithValue("@LOA", drShip["LOA"].ToString());
                                    cmd.Parameters.AddWithValue("@Breadth", drShip["Breadth"].ToString());
                                    cmd.Parameters.AddWithValue("@Depth", drShip["Depth"].ToString());
                                    cmd.Parameters.AddWithValue("@GRT", drShip["GRT"].ToString());
                                    cmd.Parameters.AddWithValue("@NRT", drShip["NRT"].ToString());
                                    cmd.Parameters.AddWithValue("@DWT", drShip["DWT"].ToString());
                                    //cmd.Parameters.AddWithValue("@IMOCode", drShip["IMOCode"].ToString());
                                    cmd.Parameters.AddWithValue("@OwnerName", drShip["OwnerName"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipFlag", drShip["ShipFlag"].ToString());
                                    // cmd.Parameters.AddWithValue("@BoatLicense", drShip["BoatLicense"].ToString());
                                    cmd.Parameters.AddWithValue("@Status", drShip["Status"].ToString());
                                    cmd.Parameters.AddWithValue("@VoyageType", drShip["VoyageType"].ToString());
                                    cmd.Parameters.AddWithValue("@STDDraft", drShip["STDDraft"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipCapacity", drShip["ShipCapacity"].ToString());
                                    cmd.Parameters.AddWithValue("@ShipBeam", drShip["ShipBeam"].ToString());
                                    cmd.Parameters.AddWithValue("@DispmtWeight", drShip["DispmtWeight"].ToString());
                                    // cmd.Parameters.AddWithValue("@ShipTypeID", drShip["ShipTypeID"].ToString());
                                    cmd.Parameters.AddWithValue("@CreatedBy", "System");
                                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                      //      sqlCmd = @"INSERT INTO LicCompanyVessel(LicCompanyVesselID, LicCompanyID, ShipLicenseNo, ShipID, CompID, OffNo, ShipName, PortReg, CallSign, IMONo, YearReg, YearBuilt, GRT, NRT, DWT, ShipType, VoyageType, LOA,
                      //Status, OwnerName, Owner, ShipFlag, Breadth, Depth, STDDraft, ShipCapacity, ShipBeam, DispmtWeight, DSLValidFrom, DSLValidTo, STSPermitValidFrom, STSPermitValidTo, MMSINo, Latitude,
                      //Longitude, LatDegree, LatMin, LatSec, LongDegree, LongMin, LongSec, CreatedBy, CreatedDate)
                      //  SELECT @LicCompanyVesselID, @LicCompanyID, ShipLicenseNo, ShipID, @CompID, OffNo, ShipName, PortReg, CallSign, IMONo, YearReg, YearBuilt, GRT, NRT, DWT, ShipType, VoyageType, LOA,
                      //                        Status, OwnerName, Owner, ShipFlag, Breadth, Depth, STDDraft, ShipCapacity, ShipBeam, DispmtWeight, DSLValidFrom, DSLValidTo, STSPermitValidFrom, STSPermitValidTo, MMSINo, Latitude,
                      //                        Longitude, LatDegree, LatMin, LatSec, LongDegree, LongMin, LongSec,@createdBy,@CreatedDate
                      //  FROM         LicCompanyVessel
                      //  where ShipID = '" + dr["LicenseID"].ToString()+"'";

                      //      using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                      //      {
                      //          cmd.Parameters.AddWithValue("@CompID", dr["CompID"].ToString());
                      //          cmd.Parameters.AddWithValue("@LicCompanyVesselID", Guid.NewGuid());
                      //          cmd.Parameters.AddWithValue("@LicCompanyID", stsLicenseID);
                      //          cmd.Parameters.AddWithValue("@CreatedBy", "System");
                      //          cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                      //          cmd.ExecuteNonQuery();
                      //      }

                        }
                        if (!CheckRecordAppointExist(dr["CompID"].ToString()))
                        {
                            DataTable activeLicTable = getActiveLicCompany(dr["CompID"].ToString());

                            DataRow drLic = activeLicTable.Rows[0];

                            sqlCmd = @"INSERT INTO  OpAppointAgent
                                       (OpAppointAgentID,SACompID,SOCompID,AppointStartDate,AppointEndDate
                                       ,CreatedBy,CreatedDate)
                                        VALUES (@OpAppointAgentID,@SACompID,@SOCompID,@AppointStartDate,@AppointEndDate
                                       ,@CreatedBy,@CreatedDate)";

                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@OpAppointAgentID", Guid.NewGuid().ToString());
                                cmd.Parameters.AddWithValue("@SACompID", dr["CompID"].ToString());
                                cmd.Parameters.AddWithValue("@SOCompID", dr["CompID"].ToString());
                                //cmd.Parameters.AddWithValue("@SOLicenseID", dr["LicCompanyID"].ToString());
                                cmd.Parameters.AddWithValue("@AppointStartDate", dr["DtLicIssue"]);
                                cmd.Parameters.AddWithValue("@AppointEndDate", dr["DtLicExp"]);
                                cmd.Parameters.AddWithValue("@CreatedBy", "System");
                                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            DataTable activeLicTable = getActiveLicCompany(dr["CompID"].ToString());

                            if (activeLicTable != null)
                            {
                                DataRow drLic = activeLicTable.Rows[0];

                                sqlCmd = @"UPDATE OpAppointAgent SET 
                                       SACompID=@SACompID,AppointStartDate=@AppointStartDate,AppointEndDate=@AppointEndDate
                                       WHERE SOCompID=@SOCompID";

                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    cmd.Parameters.AddWithValue("@SOCompID", drLic["CompID"].ToString());
                                    cmd.Parameters.AddWithValue("@SACompID", drLic["CompID"].ToString());
                                    cmd.Parameters.AddWithValue("@AppointStartDate", drLic["DtLicIssue"]);
                                    cmd.Parameters.AddWithValue("@AppointEndDate", drLic["DtLicExp"]);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public DataTable getMMSLicCompany()
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT A.COMPANY_ID AS CompID,A.SERVICE_CODE AS ServiceCode, A.LICENSE_ID AS LicenseID, B.DATE_ISSUED AS LicDateIssue, B.DATE_EXPIRY AS LicDateExp, CONVERT(DATETIME, 
                      SUBSTRING(B.DATE_ISSUED, 0, 5) + '/' + SUBSTRING(B.DATE_ISSUED, 5, 2) + '/' + SUBSTRING(B.DATE_ISSUED, 7, 2)) AS DtLicIssue, CONVERT(DATETIME, 
                      SUBSTRING(B.DATE_EXPIRY, 0, 5) + '/' + SUBSTRING(B.DATE_EXPIRY, 5, 2) + '/' + SUBSTRING(B.DATE_EXPIRY, 7, 2)) AS DtLicExp, A.LOCATION, 
                      E.SERVICE_TYPE AS ServiceType, A.CASE_NUM,A.vessel_name,A.vessel_IMO
                        FROM         SRV_NAME4.LPJMMS.dbo.ACC_SS_APP AS A INNER JOIN
                      SRV_NAME4.LPJMMS.dbo.MMS_LICENSE_INFO AS B ON A.LICENSE_ID = B.LICENSE_ID LEFT OUTER JOIN
                      SRV_NAME4.LPJMMS.dbo.SS_SERVICE_CODE AS D ON A.SERVICE_CODE = D.SERVICE_CODE LEFT OUTER JOIN
                      SRV_NAME4.LPJMMS.dbo.SS_SERVICE_TYPE AS E ON D.SERVICE_TYPE_ID = E.SERVICE_TYPE_ID 
                      where A.SERVICE_TYPE_ID='C299B09B-79E5-4526-933A-DA4AF5E22138'  
                        EXCEPT
                        SELECT CompID,ServiceCode,LicenseID,LicDateIssue,LicDateExp,DtLicIssue,DtLicExp,Location,ServiceType,CaseNUm,VesselName,VesselIMO
                        from LicCompany";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSLicCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getLicCompany()
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT LicCompanyID,CompID,ServiceCode,LicenseID,LicDateIssue,LicDateExp,DtLicIssue,DtLicExp,Location,ServiceType,CaseNUm,VesselName,VesselIMO
                        from v_LicCompanyActive where DtLicExp > getdate()  ";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "LicCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getActiveLicCompany(string compID)
        {
            try
            {
                string querystring = null;
                querystring = @" select top 1 * from dbo.v_LicCompanyActive
                             where CompID = '"+ compID + "'  and DtLicExp > getdate()   " +
                              "  order by LicDateExp desc";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "LicCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
       
        public bool CheckRecordLicExist(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from LicCompany where LicenseID='" + licenseID + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        public bool CheckRecordAppointExist(string compID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from OpAppointAgent where SACompID='" + compID + "' and SOCompID='"+compID+ "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist >= 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        public bool CheckRecordVesselLicense(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from LicCompanyVessel where ShipLicenseNo='" + licenseID + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        #endregion

        #region Sync License Company
        public bool SyncLicenseEB()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    string sqlCmd = "";
                    //1) Copy from MMS to EB (LicCompanyNew - new table)
                    DataTable sourceTable = getMMSLicCompanyEB();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        if (!CheckRecordLicEBExist(dr["LicenseID"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO  eBunkering_live.dbo.LicCompany
                                       (CompID,ServiceCode,MMSCompLicID,LicDateIssue,LicDateExp,DtLicIssue
                                       ,DtLicExp,Location,ServiceType,CaseNUm,CreatedDate)
                                        VALUES
                                       (@CompID,@ServiceCode,@MMSCompLicID, @LicDateIssue,@LicDateExp,
                                       @DtLicIssue,@DtLicExp,@Location,@ServiceType,@CaseNUm, @CreatedDate)";
                        }
                        else
                        {
                            sqlCmd = @"UPDATE  eBunkering_live.dbo.LicCompany
                                    SET CompID = @CompID,ServiceCode = @ServiceCode,
                                    LicDateIssue = @LicDateIssue,LicDateExp = @LicDateExp,DtLicIssue = @DtLicIssue, 
                                    DtLicExp = @DtLicExp,Location = @Location,ServiceType = @ServiceType,CaseNUm = @CaseNUm,CreatedDate = @CreatedDate 
                                    WHERE MMSCompLicID = @MMSCompLicID";
                        }

                        using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                        {
                            cmd.Parameters.AddWithValue("@CompID", dr["CompID"].ToString());
                            cmd.Parameters.AddWithValue("@ServiceCode", dr["ServiceCode"].ToString());
                            cmd.Parameters.AddWithValue("@MMSCompLicID", dr["LicenseID"].ToString());
                            cmd.Parameters.AddWithValue("@LicDateIssue", dr["LicDateIssue"].ToString());
                            cmd.Parameters.AddWithValue("@LicDateExp", dr["LicDateExp"].ToString());
                            cmd.Parameters.AddWithValue("@DtLicIssue", dr["DtLicIssue"]);
                            cmd.Parameters.AddWithValue("@DtLicExp", dr["DtLicExp"]);
                            cmd.Parameters.AddWithValue("@Location", dr["Location"]);
                            cmd.Parameters.AddWithValue("@ServiceType", dr["ServiceType"].ToString());
                            cmd.Parameters.AddWithValue("@CaseNUm", dr["CASE_NUM"].ToString());
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }


                    }
                    ////2) Copy from LiccompanyNew to LicCompany
                    //DataTable syncTable = getLicCompanyEB();

                    //foreach (DataRow drLic in syncTable.Rows)
                    //{

                    //    DataTable dtLicDet = getLicCompanyNewDet(drLic["LicenseID"].ToString());

                    //    DataRow drLicDet = dtLicDet.Rows[0];

                    //    sqlCmd = @"Insert Into eBunkering_live.dbo.LicCompany (MMSCompLicID,CompID,CreatedBy,CreatedDate) 
                    //                   Values (@BOLicenseID,@CompID,@CreatedBy,@CreatedDate)";

                    //    using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                    //    {
                    //        cmd.Parameters.AddWithValue("@CompID", drLicDet["CompID"].ToString());
                    //        cmd.Parameters.AddWithValue("@BOLicenseID", drLicDet["LicenseID"].ToString());
                    //        cmd.Parameters.AddWithValue("@CreatedBy", "System");
                    //        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}
                    //3) Create appoint agent
                    DataTable syncAppointTable = getAppointEB();

                    foreach (DataRow drApp in syncAppointTable.Rows)
                    {
                        DataTable dtLicDet = getLicCompanyDet(drApp["LicCompanyID"].ToString());

                        DataRow drLicDet = dtLicDet.Rows[0];
                        if (!CheckRecordAppointEBExist(drLicDet["LicCompanyID"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO  eBunkering_live.dbo.OpAppointAgent
                                       (OpAppointAgentID,BACompID,BOLicenseID,BOCompID,AppointStartDate,AppointEndDate
                                       ,CreatedBy,CreatedDate)
                                        VALUES (@OpAppointAgentID,@BACompID,@BOLicenseID,@BOCompID,@AppointStartDate,@AppointEndDate
                                       ,@CreatedBy,@CreatedDate)";
                        }

                        using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                        {
                            cmd.Parameters.AddWithValue("@OpAppointAgentID", Guid.NewGuid().ToString());
                            cmd.Parameters.AddWithValue("@BACompID", drLicDet["CompID"].ToString());
                            cmd.Parameters.AddWithValue("@BOLicenseID", drLicDet["LicCompanyID"].ToString());
                            cmd.Parameters.AddWithValue("@BOCompID", drLicDet["CompID"].ToString());
                            cmd.Parameters.AddWithValue("@AppointStartDate", drLicDet["DtLicIssue"]);
                            cmd.Parameters.AddWithValue("@AppointEndDate", drLicDet["DtLicExp"]);
                            cmd.Parameters.AddWithValue("@CreatedBy", "System");
                            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //Sync Vessel Info


                    //else
                    //{
                    //    DataTable activeLicTable = getActiveLicCompany(dr["CompID"].ToString());

                    //    DataRow drLic = activeLicTable.Rows[0];

                    //    sqlCmd = @"UPDATE OpAppointAgent SET 
                    //               SACompID=@SACompID,AppointStartDate=@AppointStartDate,AppointEndDate=@AppointEndDate
                    //               WHERE SOCompID=@SOCompID";

                    //    using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                    //    {
                    //        cmd.Parameters.AddWithValue("@SOCompID", drLic["CompID"].ToString());
                    //        cmd.Parameters.AddWithValue("@SACompID", drLic["CompID"].ToString());
                    //        cmd.Parameters.AddWithValue("@AppointStartDate", drLic["DtLicIssue"]);
                    //        cmd.Parameters.AddWithValue("@AppointEndDate", drLic["DtLicExp"]);

                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}
                }
                
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public DataTable getMMSLicCompanyEB()
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT    A.COMPANY_ID AS CompID,A.SERVICE_CODE AS ServiceCode, A.LICENSE_ID AS LicenseID, B.DATE_ISSUED AS LicDateIssue, B.DATE_EXPIRY AS LicDateExp, CONVERT(DATETIME, 
                      SUBSTRING(B.DATE_ISSUED, 0, 5) + '/' + SUBSTRING(B.DATE_ISSUED, 5, 2) + '/' + SUBSTRING(B.DATE_ISSUED, 7, 2)) AS DtLicIssue, CONVERT(DATETIME, 
                      SUBSTRING(B.DATE_EXPIRY, 0, 5) + '/' + SUBSTRING(B.DATE_EXPIRY, 5, 2) + '/' + SUBSTRING(B.DATE_EXPIRY, 7, 2)) AS DtLicExp, A.LOCATION, 
                      E.SERVICE_TYPE AS ServiceType, A.CASE_NUM
                        FROM         SRV_NAME4.LPJMMS.dbo.ACC_SS_APP AS A INNER JOIN
                      SRV_NAME4.LPJMMS.dbo.MMS_LICENSE_INFO AS B ON A.LICENSE_ID = B.LICENSE_ID LEFT OUTER JOIN
                      SRV_NAME4.LPJMMS.dbo.SS_SERVICE_CODE AS D ON A.SERVICE_CODE = D.SERVICE_CODE LEFT OUTER JOIN
                      SRV_NAME4.LPJMMS.dbo.SS_SERVICE_TYPE AS E ON D.SERVICE_TYPE_ID = E.SERVICE_TYPE_ID 
                      where A.SERVICE_TYPE_ID='F5C75BB5-12A0-4612-A0EB-E92BA5C3295D' 
                        EXCEPT
                       SELECT CompID,ServiceCode,MMSCompLicID,LicDateIssue,LicDateExp,DtLicIssue,DtLicExp,Location,ServiceType,CaseNUm
from eBunkering_live.dbo.LicCompany";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSLicCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getLicCompanyEB()
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT   LicenseID
                                from eBunkering_live.dbo.LicCompany where DtLicExp >= '2021/01/01'  
                                EXCEPT
                                SELECT  MMSCompLicID 
                                from eBunkering_live.dbo.LicCompany";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "EBLicCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getLicCompanyNewDet(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = @" select * from eBunkering_live.dbo.LicCompanyNew
                             where LicenseID = '" + licenseID + "'";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "LicCompanyDet");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getLicCompanyDet(string licenseID)
        {
            try
            {
                string querystring = null;
                //querystring = @"  select * from eBunkering_live.dbo.LicCompany a
                //                INNER JOIN eBunkering_live.dbo.LicCompanyNew b on a.MMSCompLicID=b.LicenseID
                //             where a.LicCompanyID = '" + licenseID + "'";
                querystring = @"  select * from eBunkering_live.dbo.LicCompany a
                             where a.LicCompanyID = '" + licenseID + "'";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "LicCompanyDet");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getAppointEB()
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT   LicCompanyID
                                from eBunkering_live.dbo.LicCompany  where LicCompanyID in ('D24F5DD7-0BE7-42A2-B3ED-882153B5F8AF','EE041105-7130-46EC-9E7B-F10F353E9CD6') 
                                EXCEPT
                                SELECT  BOLicenseID 
                                from eBunkering_live.dbo.OpAppointAgent";

                //F5C75BB5-12A0-4612-A0EB-E92BA5C3295D - EB 22
                //C299B09B-79E5-4526-933A-DA4AF5E22138 - STS 28


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "EBAppointAgent");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public bool CheckRecordLicEBExist(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from eBunkering_live.dbo.LicCompany where MMSCompLicID='" + licenseID + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        public bool CheckRecordAppointEBExist(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from eBunkering_live.dbo.OpAppointAgent where BOLicenseID='" + licenseID + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }
        #endregion
        #region Sync Users
        public bool SyncUsers()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    DataTable sourceTable = getMMSUsersCompany();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        string sqlCmd = "";
                        if (dr["UserID"].ToString() == "hafiz")
                        {
                            int x = 0;
                        }

                        if (!CheckRecordUsersExist(dr["OrgzID"].ToString(), dr["UserID"].ToString()))
                        {
                           
                            if (dr["OrgzID"].ToString().ToUpper() == "LPJ")
                            {
                                sqlCmd = @"INSERT INTO  MMSSync.dbo.Users (id,OrgzID,UserID,UserPass,UserPass2,FullName
                                    ,EmailAddress,UserType,TelNo,AccessGroupID,STSAccessGroupID,IsActive,CreatedBy,CreatedDate)
                                     VALUES
                                     (@id,@OrgzID,@UserID,@UserPass,@UserPass2,@FullName
                                    ,@EmailAddress,@UserType,@TelNo,@AccessGroupID,@STSAccessGroupID,@IsActive,@CreatedBy,@CreatedDate)";
                               
                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                                    cmd.Parameters.AddWithValue("@CreatedBy", "System");
                                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@OrgzID", dr["OrgzID"].ToString());
                                    cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
                                    cmd.Parameters.AddWithValue("@UserPass", dr["UserPass"].ToString());
                                    cmd.Parameters.AddWithValue("@UserPass2", dr["UserPass2"].ToString());
                                    cmd.Parameters.AddWithValue("@FullName", dr["FullName"].ToString());
                                    cmd.Parameters.AddWithValue("@EmailAddress", dr["EmailAddress"]);
                                    cmd.Parameters.AddWithValue("@UserType", dr["UserType"]);
                                    cmd.Parameters.AddWithValue("@TelNo", dr["PhoneNumber"]);
                                    cmd.Parameters.AddWithValue("@AccessGroupID", new Guid("7a8a35af-8d6c-447f-b0ba-99a8b9b903e2"));
                                    cmd.Parameters.AddWithValue("@STSAccessGroupID", new Guid("4518da4e-7f91-4199-ab17-f3e90247b16c"));
                                    cmd.Parameters.AddWithValue("@IsActive", true);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                sqlCmd = @"INSERT INTO  MMSSync.dbo.Users (id,OrgzID,UserID,UserPass,UserPass2,FullName
                                    ,EmailAddress,UserType,TelNo,IsActive,CreatedBy,CreatedDate)
                                     VALUES
                                     (@id,@OrgzID,@UserID,@UserPass,@UserPass2,@FullName
                                    ,@EmailAddress,@UserType,@TelNo,@IsActive,@CreatedBy,@CreatedDate)";

                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                                    cmd.Parameters.AddWithValue("@CreatedBy", "System");
                                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@OrgzID", dr["OrgzID"].ToString());
                                    cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
                                    cmd.Parameters.AddWithValue("@UserPass", dr["UserPass"].ToString());
                                    cmd.Parameters.AddWithValue("@UserPass2", dr["UserPass2"].ToString());
                                    cmd.Parameters.AddWithValue("@FullName", dr["FullName"].ToString());
                                    cmd.Parameters.AddWithValue("@EmailAddress", dr["EmailAddress"]);
                                    cmd.Parameters.AddWithValue("@UserType", dr["UserType"]);
                                    cmd.Parameters.AddWithValue("@TelNo", dr["PhoneNumber"]);
                                    cmd.Parameters.AddWithValue("@IsActive", true);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            sqlCmd = @"UPDATE MMSSync.dbo.Users
                                       SET UserPass = @UserPass,UserPass2 = @UserPass2,FullName = @FullName 
                                       ,UserType = @UserType,TelNo = @TelNo,IsActive = @IsActive,UpdatedBy = @UpdatedBy,UpdatedDate = @UpdatedDate 
                                     WHERE OrgzID = @OrgzID and UserID = @UserID";


                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@UpdatedBy", "System");
                                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@OrgzID", dr["OrgzID"].ToString());
                                cmd.Parameters.AddWithValue("@UserID", dr["UserID"].ToString());
                                cmd.Parameters.AddWithValue("@UserPass", dr["UserPass"].ToString());
                                cmd.Parameters.AddWithValue("@UserPass2", dr["UserPass2"].ToString());
                                cmd.Parameters.AddWithValue("@FullName", dr["FullName"].ToString());
                                cmd.Parameters.AddWithValue("@EmailAddress", dr["EmailAddress"]);
                                cmd.Parameters.AddWithValue("@UserType", dr["UserType"]);
                                cmd.Parameters.AddWithValue("@TelNo", dr["PhoneNumber"]);
                                cmd.Parameters.AddWithValue("@IsActive", true);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public DataTable getMMSUsersCompany()
        {
            try
            {
                string querystring = null;
                querystring = @"Select * from 
                                (
                                 select A.orgz_id as OrgzId,A.USER_ID as UserID,A.password as UserPass,'' as UserPass2,A.full_name as FullName,A.email_address as EmailAddress,A.user_type as UserType,B.PhoneNumber 
                                FROM    SRV_NAME4.eRegistration.dbo.V_CompanyUser A
                                LEFT OUTER JOIN SRV_NAME4.LPJConsole.dbo.TblUAMUsers B on A.orgz_id=B.UserOrgzId and A.user_id=B.Username
                                where  B.UserOrgzId is  null and A.is_active=1
                                UNION
                                 SELECT  distinct UserOrgzID as OrgzID,UserName as UserID,'' as UserPass,PasswordHash as UserPass2,StaffName as FullName,Email as EmailAddress,Designation as 'UserType',B.PhoneNumber 
                                FROM    SRV_NAME4.LPJConsole.dbo.TblUAMUsers B
                                LEFT JOIN SRV_NAME4.eRegistration.dbo.V_CompanyUser  A
                                on A.orgz_id=B.UserOrgzId and A.user_id=B.Username
                                where B.UserOrgzId is not null and  A.orgz_id is  null  
                                UNION
                                  SELECT  distinct UserOrgzID as OrgzID,UserName as UserID,'' as UserPass,PasswordHash as UserPass2,StaffName as FullName,Email as EmailAddress,Designation as 'UserType',B.PhoneNumber 
                                FROM    SRV_NAME4.LPJConsole.dbo.TblUAMUsers B
                                LEFT JOIN SRV_NAME4.eRegistration.dbo.V_CompanyUser  A
                                on A.orgz_id=B.UserOrgzId and A.user_id=B.Username
                                where  B.UserOrgzId is not  null
                                ) A
                                EXCEPT
                                select Orgzid,UserID,UserPass,UserPass2,FullName,EmailAddress,UserType,TelNo
                                From MMSSync.dbo.Users order by OrgzId,UserID";
                // Where A.UserID='850323025490' 

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSUsersCompany");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public bool CheckRecordUsersExist(string orgzid,string userid)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from MMSSync.dbo.Users where OrgzID='" + orgzid + "' and UserID ='"+ userid + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }

        #endregion
        #region Sync Users
        public bool SyncShip()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    DataTable sourceTable = getMMSShip();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        string sqlCmd = "";
                        if (!CheckRecordShipExist(dr["ShipID"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO MMSSync.dbo.ShipMaster (ShipID,ShipCategory,OffNo,ShipName,PortReg,CallSign,IMONo,RegNo,YearReg,ShipType,YearBuilt,LOA,Breadth,Depth,GRT,NRT
   ,DWT,IMOCode,OwnerName,ShipFlag,BoatLicense,[Status],VoyageType,STDDraft,ShipCapacity,ShipBeam,DispmtWeight,ShipTypeID)
                                     VALUES
                                     (@ShipID,@ShipCategory,@OffNo,@ShipName,@PortReg,@CallSign,@IMONo,@RegNo,@YearReg,@ShipType,@YearBuilt,@LOA,@Breadth,@Depth,@GRT,@NRT
   ,@DWT,@IMOCode,@OwnerName,@ShipFlag,@BoatLicense,@Status,@VoyageType,@STDDraft,@ShipCapacity,@ShipBeam,@DispmtWeight,@ShipTypeID)";

                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@ShipID",  dr["ShipID"].ToString());
                                cmd.Parameters.AddWithValue("@ShipCategory", dr["ShipCategory"].ToString());
                                cmd.Parameters.AddWithValue("@OffNo", dr["OffNo"].ToString());
                                cmd.Parameters.AddWithValue("@ShipName", dr["ShipName"].ToString());
                                cmd.Parameters.AddWithValue("@PortReg", dr["PortReg"].ToString());
                                cmd.Parameters.AddWithValue("@CallSign", dr["CallSign"].ToString());
                                cmd.Parameters.AddWithValue("@IMONo", dr["IMONo"].ToString());
                                cmd.Parameters.AddWithValue("@RegNo", dr["RegNo"].ToString());
                                cmd.Parameters.AddWithValue("@YearReg", dr["YearReg"].ToString());
                                cmd.Parameters.AddWithValue("@ShipType", dr["ShipType"].ToString());
                                cmd.Parameters.AddWithValue("@YearBuilt", dr["YearBuilt"].ToString());
                                cmd.Parameters.AddWithValue("@LOA", dr["LOA"].ToString());
                                cmd.Parameters.AddWithValue("@Breadth", dr["Breadth"].ToString());
                                cmd.Parameters.AddWithValue("@Depth", dr["Depth"].ToString());
                                cmd.Parameters.AddWithValue("@GRT", dr["GRT"].ToString());
                                cmd.Parameters.AddWithValue("@NRT", dr["NRT"].ToString());
                                cmd.Parameters.AddWithValue("@DWT", dr["DWT"].ToString());
                                cmd.Parameters.AddWithValue("@IMOCode", dr["IMOCode"].ToString());
                                cmd.Parameters.AddWithValue("@OwnerName", dr["OwnerName"].ToString());
                                cmd.Parameters.AddWithValue("@ShipFlag", dr["ShipFlag"].ToString());
                                cmd.Parameters.AddWithValue("@BoatLicense", dr["BoatLicense"].ToString());
                                cmd.Parameters.AddWithValue("@Status", dr["Status"].ToString());
                                cmd.Parameters.AddWithValue("@VoyageType", dr["VoyageType"].ToString());
                                cmd.Parameters.AddWithValue("@STDDraft", dr["STDDraft"].ToString());
                                cmd.Parameters.AddWithValue("@ShipCapacity", dr["ShipCapacity"].ToString());
                                cmd.Parameters.AddWithValue("@ShipBeam", dr["ShipBeam"].ToString());
                                cmd.Parameters.AddWithValue("@DispmtWeight", dr["DispmtWeight"].ToString());
                                cmd.Parameters.AddWithValue("@ShipTypeID", dr["ShipTypeID"].ToString());

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            sqlCmd = @"UPDATE MMSSync.dbo.ShipMaster
                                       SET ShipCategory=@ShipCategory,OffNo=@OffNo,ShipName=@ShipName,PortReg=@PortReg,CallSign=@CallSign,IMONo=@IMONo,RegNo=@RegNo,YearReg=@YearReg,ShipType=@ShipType,
                                            YearBuilt=@YearBuilt,LOA=@LOA,Breadth=@Breadth,Depth=@Depth,GRT=@GRT,NRT=@NRT,DWT=@DWT,IMOCode=@IMOCode,OwnerName=@OwnerName,ShipFlag=@ShipFlag,BoatLicense=@BoatLicense
                                            ,Status=@Status,VoyageType=@VoyageType,STDDraft=@STDDraft,ShipCapacity=@ShipCapacity,ShipBeam=@ShipBeam,DispmtWeight=@DispmtWeight,ShipTypeID=@ShipTypeID 
                                     WHERE ShipID = @ShipID";


                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@ShipID", dr["ShipID"].ToString());
                                cmd.Parameters.AddWithValue("@ShipCategory", dr["ShipCategory"].ToString());
                                cmd.Parameters.AddWithValue("@OffNo", dr["OffNo"].ToString());
                                cmd.Parameters.AddWithValue("@ShipName", dr["ShipName"].ToString());
                                cmd.Parameters.AddWithValue("@PortReg", dr["PortReg"].ToString());
                                cmd.Parameters.AddWithValue("@CallSign", dr["CallSign"].ToString());
                                cmd.Parameters.AddWithValue("@IMONo", dr["IMONo"].ToString());
                                cmd.Parameters.AddWithValue("@RegNo", dr["RegNo"].ToString());
                                cmd.Parameters.AddWithValue("@YearReg", dr["YearReg"].ToString());
                                cmd.Parameters.AddWithValue("@ShipType", dr["ShipType"].ToString());
                                cmd.Parameters.AddWithValue("@YearBuilt", dr["YearBuilt"].ToString());
                                cmd.Parameters.AddWithValue("@LOA", dr["LOA"].ToString());
                                cmd.Parameters.AddWithValue("@Breadth", dr["Breadth"].ToString());
                                cmd.Parameters.AddWithValue("@Depth", dr["Depth"].ToString());
                                cmd.Parameters.AddWithValue("@GRT", dr["GRT"].ToString());
                                cmd.Parameters.AddWithValue("@NRT", dr["NRT"].ToString());
                                cmd.Parameters.AddWithValue("@DWT", dr["DWT"].ToString());
                                cmd.Parameters.AddWithValue("@IMOCode", dr["IMOCode"].ToString());
                                cmd.Parameters.AddWithValue("@OwnerName", dr["OwnerName"].ToString());
                                cmd.Parameters.AddWithValue("@ShipFlag", dr["ShipFlag"].ToString());
                                cmd.Parameters.AddWithValue("@BoatLicense", dr["BoatLicense"].ToString());
                                cmd.Parameters.AddWithValue("@Status", dr["Status"].ToString());
                                cmd.Parameters.AddWithValue("@VoyageType", dr["VoyageType"].ToString());
                                cmd.Parameters.AddWithValue("@STDDraft", dr["STDDraft"].ToString());
                                cmd.Parameters.AddWithValue("@ShipCapacity", dr["ShipCapacity"].ToString());
                                cmd.Parameters.AddWithValue("@ShipBeam", dr["ShipBeam"].ToString());
                                cmd.Parameters.AddWithValue("@DispmtWeight", dr["DispmtWeight"].ToString());
                                cmd.Parameters.AddWithValue("@ShipTypeID", dr["ShipTypeID"].ToString());

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                return false;
                throw;
            }

            return true;
        }
        public DataTable getMMSShip()
        {
            try
            {
                string querystring = null;
                querystring = @"  select ShipID,ShipCategory collate SQL_Latin1_General_CP1_CI_AS
,OffNo collate SQL_Latin1_General_CP1_CI_AS,ShipName collate SQL_Latin1_General_CP1_CI_AS,PortReg collate SQL_Latin1_General_CP1_CI_AS
,CallSign collate SQL_Latin1_General_CP1_CI_AS,IMONo collate SQL_Latin1_General_CP1_CI_AS,RegNo collate SQL_Latin1_General_CP1_CI_AS
,YearReg ,ShipType collate SQL_Latin1_General_CP1_CI_AS ,YearBuilt  
,LOA,Breadth,Depth,GRT,NRT
,DWT,IMOCode  collate SQL_Latin1_General_CP1_CI_AS,OwnerName  collate SQL_Latin1_General_CP1_CI_AS,ShipFlag  collate SQL_Latin1_General_CP1_CI_AS
,BoatLicense  collate SQL_Latin1_General_CP1_CI_AS,[Status]  collate SQL_Latin1_General_CP1_CI_AS,VoyageType  collate SQL_Latin1_General_CP1_CI_AS
 ,STDDraft,ShipCapacity,ShipBeam ,DispmtWeight,ShipTypeID   collate SQL_Latin1_General_CP1_CI_AS
from MMSSync.dbo.v_ShipMaster where ShipCategory='L'
EXCEPT
select 
ShipID,ShipCategory
,OffNo,ShipName,PortReg,CallSign,IMONo,RegNo,YearReg,ShipType,YearBuilt,LOA,Breadth,Depth,GRT,NRT
,DWT,IMOCode,OwnerName,ShipFlag
,BoatLicense,[Status],VoyageType
 ,STDDraft,ShipCapacity,ShipBeam,DispmtWeight,ShipTypeID
from MMSSync.dbo.ShipMaster";


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSShip");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
        public DataTable getMMSShipByIMO(string imono,string whereShipName)
        {
            try
            {
                string querystring = null;
                querystring = @"SELECT  * from 
                                     (
                                       select ShipID,ShipCategory,OffNo,ShipName,PortReg,CallSign,IMONo,RegNo,YearReg,ShipType,YearBuilt,LOA,Breadth,Depth,GRT,NRT
                                       ,DWT,IMOCode,OwnerName,ShipFlag,BoatLicense,[Status],VoyageType,STDDraft,ShipCapacity,ShipBeam,DispmtWeight,ShipTypeID
                                       from MMSSync.dbo.ShipMaster Where IMONO='"+ imono + "' and PortReg is not null " +
                                      ") A "+ whereShipName;


                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    cmd.Connection = sqlConn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = querystring;
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    ad.SelectCommand = cmd;

                    if (ds.Tables.Count > 0)
                        ds.Tables.Clear();

                    ad.Fill(ds, "MMSShip");
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                if ((cmd.Connection.State == ConnectionState.Open))
                {
                    cmd.Connection.Close();
                }
                throw;
            }

            return ds.Tables[0];
        }
     
        public bool CheckRecordShipExist(string shipID)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from MMSSync.dbo.ShipMaster where ShipID='" + shipID + "'";

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand(querystring, sqlConn);
                    sqlConn.Open();
                    int recordExist = (int)cmd.ExecuteScalar();

                    if (recordExist == 1) //anything different from 1 should be wrong
                    {
                        return true;
                    }
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

            return false;
        }

        #endregion
    }
}