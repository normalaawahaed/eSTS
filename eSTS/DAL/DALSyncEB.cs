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
    public class DALSyncEB
    {
        #region Declaration
        string connectionstring = ConfigurationManager.ConnectionStrings["STSConnectionString"].ToString();
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        #endregion

        #region Sync License Company
        public bool SyncLicenseEB()
        {
            try
            {

                using (SqlConnection sqlConn = new SqlConnection(connectionstring))
                {
                   
                    string sqlCmd = "";
                    //1) Copy from MMS to EB 
                    DataTable sourceTable = getMMSLicCompanyEB();
                    sqlConn.Open();
                    foreach (DataRow dr in sourceTable.Rows)
                    {
                        Guid ebLicenseID = Guid.NewGuid();
                        if (!CheckRecordLicEBExist(dr["LicenseID"].ToString()))
                        {
                            sqlCmd = @"INSERT INTO  eBunkering_live.dbo.LicCompany
                                       (LicCompanyID,CompID,ServiceCode,MMSCompLicID,LicDateIssue,LicDateExp,DtLicIssue
                                       ,DtLicExp,Location,ServiceType,CaseNUm,ModeOperation,CreatedDate)
                                        VALUES
                                       (@LicCompanyID,@CompID,@ServiceCode,@MMSCompLicID, @LicDateIssue,@LicDateExp,
                                       @DtLicIssue,@DtLicExp,@Location,@ServiceType,@CaseNUm,@ModeOperation, @CreatedDate)";
                            #region Temporarily Hide - Supposed to be not update for existing license
                            //}
                            //else
                            //{
                            //    sqlCmd = @"UPDATE  eBunkering_live.dbo.LicCompany
                            //            SET CompID = @CompID,ServiceCode = @ServiceCode,
                            //            LicDateIssue = @LicDateIssue,LicDateExp = @LicDateExp,DtLicIssue = @DtLicIssue, 
                            //            DtLicExp = @DtLicExp,Location = @Location,ServiceType = @ServiceType,CaseNUm = @CaseNUm,CreatedDate = @CreatedDate 
                            //            WHERE MMSCompLicID = @MMSCompLicID";
                            //}
                            #endregion
                            using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                            {
                                cmd.Parameters.AddWithValue("@LicCompanyID", ebLicenseID);
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
                                cmd.Parameters.AddWithValue("@ModeOperation", dr["ModeOperation"].ToString());
                                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                            if (dr["ModeOperation"].ToString() == "1")
                            {
                                //4) Sync Vessel Info for Mode Operation = 1

                                sqlCmd = @"INSERT INTO eBunkering_live.dbo.LicCompanyVessel (LICCOMPANYID,CompID,MMSVesselProfileID,SHIPID,OFFNO,SHIPNAME,
                                    PORTREG,CALLSIGN,YEARBUILT,GRT,LOA,OWNERNAME,SHIPFLAG,BREADTH,CREATEDBY,CREATEDDATE)
                                    SELECT @LICCOMPANYID,@CompID,a.VESSEL_PROFILE_ID as MMSVesselProfileID ,a.VESSEL_PROFILE_ID AS SHIPID,OFF_NO AS OFFNO,b.VESSEL_NAME AS SHIPNAME
                                    ,PORT_REG AS PORTREG,CALL_SIGN AS CALLSIGN,YEAR_BUILT AS YEARBUILT,GRT,LOA,NAME_OWNER AS OWNERNAME,SHIP_FLAG AS SHIPFLAG,BREADTH AS BREADTH,'system',@CREATEDDATE
                                    FROM  SRV_NAME4.sLPJMMSv2.dbo.ACC_SS_APP a 
                                    INNER JOIN SRV_NAME4.sLPJMMSV2.dbo.[SS_VESSEL_PROFILE] b on a.VESSEL_PROFILE_ID=b.VESSEL_PROFILE_ID
                                    WHERE a.LICENSE_ID=@MMSLICENSEID";
                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    cmd.Parameters.AddWithValue("@LICCOMPANYID", ebLicenseID);
                                    cmd.Parameters.AddWithValue("@MMSLICENSEID", dr["LicenseID"].ToString());
                                    cmd.Parameters.AddWithValue("@CompID", dr["CompID"].ToString());
                                    // cmd.Parameters.AddWithValue("@LICCOMPANYVESSELID", Guid.NewGuid());
                                    cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            //3) Create appoint agent
                            DataTable dtLicDet = getLicCompanyDet(ebLicenseID.ToString());

                            DataRow drLicDet = dtLicDet.Rows[0];
                            if (!CheckRecordAppointEBExist(drLicDet["CompID"].ToString(), Convert.ToInt32(drLicDet["Location"])))
                            {
                                sqlCmd = @"INSERT INTO  eBunkering_live.dbo.OpAppointAgent
                                       (OpAppointAgentID,BACompID,BOLicenseID,BOCompID,AppointStartDate,AppointEndDate,Location
                                       ,CreatedBy,CreatedDate)
                                        VALUES (@OpAppointAgentID,@BACompID,@BOLicenseID,@BOCompID,@AppointStartDate,@AppointEndDate,@Location
                                       ,@CreatedBy,@CreatedDate)";

                                using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                                {
                                    cmd.Parameters.AddWithValue("@OpAppointAgentID", Guid.NewGuid().ToString());
                                    cmd.Parameters.AddWithValue("@BACompID", drLicDet["CompID"].ToString());
                                    cmd.Parameters.AddWithValue("@BOLicenseID", drLicDet["LicCompanyID"].ToString());
                                    cmd.Parameters.AddWithValue("@BOCompID", drLicDet["CompID"].ToString());
                                    cmd.Parameters.AddWithValue("@AppointStartDate", drLicDet["DtLicIssue"]);
                                    cmd.Parameters.AddWithValue("@AppointEndDate", drLicDet["DtLicExp"]);
                                    cmd.Parameters.AddWithValue("@Location", drLicDet["Location"]);
                                    cmd.Parameters.AddWithValue("@CreatedBy", "System");
                                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        ////    DataTable syncAppointTable = getAppointEB();

                        ////    foreach (DataRow drApp in syncAppointTable.Rows)
                        ////    {
                        ////        DataTable dtLicDet = getLicCompanyDet(drApp["LicCompanyID"].ToString());

                        ////        DataRow drLicDet = dtLicDet.Rows[0];
                        ////        if (!CheckRecordAppointEBExist(drLicDet["LicCompanyID"].ToString()))
                        ////        {
                        ////            sqlCmd = @"INSERT INTO  eBunkering_live.dbo.OpAppointAgent
                        ////               (OpAppointAgentID,BACompID,BOLicenseID,BOCompID,AppointStartDate,AppointEndDate,Location
                        ////               ,CreatedBy,CreatedDate)
                        ////                VALUES (@OpAppointAgentID,@BACompID,@BOLicenseID,@BOCompID,@AppointStartDate,@AppointEndDate,@Location
                        ////               ,@CreatedBy,@CreatedDate)";
                        ////        }

                        ////        using (SqlCommand cmd = new SqlCommand(sqlCmd, sqlConn))
                        ////        {
                        ////            cmd.Parameters.AddWithValue("@OpAppointAgentID", Guid.NewGuid().ToString());
                        ////            cmd.Parameters.AddWithValue("@BACompID", drLicDet["CompID"].ToString());
                        ////            cmd.Parameters.AddWithValue("@BOLicenseID", drLicDet["LicCompanyID"].ToString());
                        ////            cmd.Parameters.AddWithValue("@BOCompID", drLicDet["CompID"].ToString());
                        ////            cmd.Parameters.AddWithValue("@AppointStartDate", drLicDet["DtLicIssue"]);
                        ////            cmd.Parameters.AddWithValue("@AppointEndDate", drLicDet["DtLicExp"]);
                        ////            cmd.Parameters.AddWithValue("@Location", drLicDet["Location"]);
                        ////            cmd.Parameters.AddWithValue("@CreatedBy", "System");
                        ////            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        ////            cmd.ExecuteNonQuery();
                        ////        }
                        ////    }
                        ////}



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
                querystring = @"SELECT A.COMPANY_ID AS CompID,A.SERVICE_CODE AS ServiceCode, A.LICENSE_ID AS LicenseID, B.DATE_ISSUED AS LicDateIssue, B.DATE_EXPIRY AS LicDateExp, CONVERT(DATETIME, 
                            SUBSTRING(B.DATE_ISSUED, 0, 5) + '/' + SUBSTRING(B.DATE_ISSUED, 5, 2) + '/' + SUBSTRING(B.DATE_ISSUED, 7, 2)) AS DtLicIssue, CONVERT(DATETIME, 
                            SUBSTRING(B.DATE_EXPIRY, 0, 5) + '/' + SUBSTRING(B.DATE_EXPIRY, 5, 2) + '/' + SUBSTRING(B.DATE_EXPIRY, 7, 2)) AS DtLicExp, A.LOCATION, 
                            E.SERVICE_TYPE AS ServiceType, A.CASE_NUM,A.Mode_Operation as ModeOperation 
                            FROM         SRV_NAME4.sLPJMMSv2.dbo.ACC_SS_APP AS A INNER JOIN
                            SRV_NAME4.sLPJMMSv2.dbo.MMS_LICENSE_INFO AS B ON A.LICENSE_ID = B.LICENSE_ID LEFT OUTER JOIN
                            SRV_NAME4.sLPJMMSv2.dbo.SS_SERVICE_CODE AS D ON A.SERVICE_CODE = D.SERVICE_CODE LEFT OUTER JOIN
                            SRV_NAME4.sLPJMMSv2.dbo.SS_SERVICE_TYPE AS E ON D.SERVICE_TYPE_ID = E.SERVICE_TYPE_ID 
                            where A.SERVICE_TYPE_ID='F5C75BB5-12A0-4612-A0EB-E92BA5C3295D' and a.license_id is not null
                            EXCEPT
                            SELECT CompID,ServiceCode,MMSCompLicID,LicDateIssue,LicDateExp,DtLicIssue,DtLicExp,Location,ServiceType,CaseNUm,ModeOperation
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
        public DataTable getMMSVessel(string licenseID)
        {
            try
            {
                string querystring = null;
                querystring = @"select * FROM SRV_NAME4.sLPJMMSv2.dbo.ACC_SS_APP a 
                INNER JOIN SRV_NAME4.sLPJMMSV2.dbo.[SS_VESSEL_PROFILE] b on a.VESSEL_PROFILE_ID=b.VESSEL_PROFILE_ID
                WHERE a.LICENSE_ID='FCFEE934-881D-417C-8707-E4734C79B245'";

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
                querystring = @"SELECT   LicCompanyID,CompID 
                                from eBunkering_live.dbo.LicCompany where DtLicExp > getdate()
                                EXCEPT
                                SELECT  BOLicenseID,BACompID 
                                from eBunkering_live.dbo.OpAppointAgent ";
                // where LicCompanyID in ('D24F5DD7-0BE7-42A2-B3ED-882153B5F8AF','EE041105-7130-46EC-9E7B-F10F353E9CD6') 
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
        public bool CheckRecordAppointEBExist(string BOCompID,int Location)
        {
            try
            {
                string querystring = null;
                querystring = " Select COUNT(*) from eBunkering_live.dbo.OpAppointAgent where BOCompID='" + BOCompID + "' and Location="+Location;

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