using Apps.Common;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.UI;
using eSTS.DAL;
using eSTS.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eSTS.Operation
{
    public partial class printPermit : System.Web.UI.Page
    {
        ReportDocument oRpt = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Check Session
                if ((Session["UserID"] == null) && (Session["UserGroup"] == null))//&& (Session["UserLevel"] != null))
                {
                    Response.Redirect("~//SignIn.aspx", true);
                }
                if (Request.QueryString.Count > 0)
                {
                    Session["OperationAppID"] = Request.QueryString["sno"].ToString();
                }
                //GenerateDO();
               // GenerateLPJPermit();
               GeneratePetrolPermit();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void GenerateNotis()
        {
            try
            {
                string operationAppID = Session["OperationAppID"].ToString();
                string permitFilePath = "~/Operation/Upload/"+ "Notis.pdf";
                ReportDocument oRpt = new ReportDocument();
                string dbServer = WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];


                DALOperation objMain = new DALOperation();
                objMain.Get_OperationAppList(operationAppID);

                oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);

                oRpt.Load(Server.MapPath("~/Operation/rptNotisPermi.rpt"));

                oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);
            
                //this.CrystalReportViewer1.ReportSource = oRpt;

                //string fileName ="DO_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");

                string permitFullPath = Server.MapPath(permitFilePath);
                string contentType = "application/pdf";

                CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                dfo.DiskFileName = permitFullPath;
                oRpt.ExportOptions.DestinationOptions = dfo;
                oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                oRpt.Export();
                oRpt.Close();

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = contentType;

               // Response.WriteFile(permitFullPath);
                Response.Flush();
                Response.Close();

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        protected void GenerateLampiran()
        {
            try
            {
                string operationAppID = Session["OperationAppID"].ToString();
                string permitFilePath = "~/Operation/Upload/" + "LPJPermit.pdf";
                ReportDocument oRpt = new ReportDocument();
                string dbServer = WebConfigurationManager.AppSettings["DBServer"];
                string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                string dbPass = WebConfigurationManager.AppSettings["DBPass"];


                oRpt.Load(Server.MapPath("~/Operation/rptLampiran1.rpt"));
                oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                DALOperation objMain = new DALOperation();
                objMain.Get_OperationAppList(operationAppID);

                oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);
               

                //// Export to PDF
                string fileName = "LPJLampiran_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string permitFullPath = Server.MapPath(permitFilePath);
                string contentType = "application/pdf";
 
                CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                dfo.DiskFileName = permitFullPath;
                oRpt.ExportOptions.DestinationOptions = dfo;
                oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, permitFullPath);
                oRpt.Close();

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = contentType;
//Response.WriteFile(permitFullPath);

                Response.Flush();
                Response.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void GeneratePetrolPermit()
        {
            try
            {
                DALOperation objMain = new DALOperation();
                Guid operationAppID = new Guid(Session["OperationAppID"].ToString());
                using (eSTS_StagEntities dbContext = new eSTS_StagEntities())
                {
                    OperationApp item = dbContext.OperationApps.Find(operationAppID);

                    string folderDirectory = Server.MapPath("Upload/" + item.CompID + "/" + item.OperationAppID.ToString());
                    string fileName = "Permit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string PermitQRFilePath = "~/Operation/Upload/" + item.CompID + "/" + item.OperationAppID.ToString() + "/" + "qrcode_" + fileName + ".jpg";

                    item.PermitDocLink = objMain.GenerateQRCode(item.OperationAppID.ToString(), item.CompID, folderDirectory, fileName, Server.MapPath(PermitQRFilePath)); //GeneratePermit(operationAppID.ToString(),item.CompID, item.RefID,ref QRPhysicalPath);
                    item.PermitQRCode = Server.MapPath(PermitQRFilePath);
                    dbContext.SaveChanges();
              
                    ReportDocument oRpt = new ReportDocument();
                    string dbServer = WebConfigurationManager.AppSettings["DBServer"];
                    string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
                    string dbUser = WebConfigurationManager.AppSettings["DBUser"];
                    string dbPass = WebConfigurationManager.AppSettings["DBPass"];


                    oRpt.Load(Server.MapPath("~/Operation/PetrolPermitNew.rpt"));// = new eBunkering.Operation.PetrolPermit();
                    oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);

                    objMain.Get_OperationAppList(operationAppID.ToString());

                    oRpt.SetDataSource(objMain.ds.Tables["v_permit"]);

                    //   string fileName ="PermitPetrol_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string permitFilePath = "~/Operation/Upload/" + objMain.ds.Tables["v_Permit"].Rows[0]["CompID"].ToString() + "/" + operationAppID + "/" + fileName + ".pdf";
                    string permitFullPath = Server.MapPath(permitFilePath);
                    string contentType = "application/pdf";


                    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
                    dfo.DiskFileName = permitFullPath;
                    oRpt.ExportOptions.DestinationOptions = dfo;
                    oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                    oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                    oRpt.Export();
                    oRpt.Close();

                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = contentType;

                    Response.WriteFile(permitFullPath);
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void ShowReport()
        {
            //try
            //{
            //    string dbServer = WebConfigurationManager.AppSettings["DBServer"];
            //    string dbCatalog = WebConfigurationManager.AppSettings["DBCatalog"];
            //    string dbUser = WebConfigurationManager.AppSettings["DBUser"];
            //    string dbPass = WebConfigurationManager.AppSettings["DBPass"];


            //    string reportPath = @"D:\Workspace\Project\eBunkering\Source Code\eBunkering\Operation\LPJDO.rpt";
            //    oRpt.Load(reportPath);
            //    oRpt.SetDatabaseLogon(dbUser, dbPass, dbServer, dbCatalog);
            //    int ttlLorry = 0;
            //    //oRpt = new Operation.PetrolPermit();

            //    //oRpt.SetDataSource(objReport.ds.Tables["company"]);
            //    DALOperation objMain = new DALOperation();
            //    objMain.Get_OperationAppList(Session["OperationAppID"].ToString());
            //    objMain.Get_OperationAppLorryList(Session["OperationAppID"].ToString());

            //    DALUsers objUser = new DALUsers();
            //    objUser.LoadUserVTMS(objMain.ds.Tables["v_Permit"].Rows[0]["Location"].ToString());

            //    DataRow drVTMS = objUser.ds.Tables[0].Rows[0];

            //    if (objMain.ds.Tables["OperationAppLorry"] != null)
            //    {
            //        ttlLorry = objMain.ds.Tables["OperationAppLorry"].Rows.Count;
            //    }

            //    oRpt.SetDataSource(objMain.ds);
            //    oRpt.Subreports["subrptLorry"].SetDataSource(objMain.ds.Tables["OperationAppLorry"]);
            //    oRpt.SetParameterValue("ttlLorry", ttlLorry);

            //    oRpt.SetParameterValue("pCompName", drVTMS["CompanyName"].ToString());
            //    oRpt.SetParameterValue("pAdd1", drVTMS["Add1"].ToString());
            //    oRpt.SetParameterValue("pAdd2", drVTMS["Add2"].ToString());
            //    oRpt.SetParameterValue("pAdd3", drVTMS["Add3"].ToString());
            //    oRpt.SetParameterValue("pTelNo", drVTMS["TelNo"].ToString());
            //    oRpt.SetParameterValue("pNoFax", drVTMS["FaxNo"].ToString());
            //    oRpt.SetParameterValue("pEmail", drVTMS["EmailAddress"].ToString());

            //    //// Export to PDF
            //    string fileName = "LPJPermit_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //    string permitFullPath = Server.MapPath(permitFilePath);
            //    string contentType = "application/pdf";

            //    CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
            //    dfo.DiskFileName = permitFullPath;
            //    oRpt.ExportOptions.DestinationOptions = dfo;
            //    oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
            //    oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
            //    oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, permitFullPath);
            //    oRpt.Close();

            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.ContentType = contentType;

            //    //Response.WriteFile(permitFullPath);
            //    Response.Flush();
            //    Response.Close();

            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }
    }
}