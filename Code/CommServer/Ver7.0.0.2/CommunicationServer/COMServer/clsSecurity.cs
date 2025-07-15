using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TPR_App;
using System.IO;
//using UmsDLL;
using System.Windows.Forms;
using SatoLib;
using System.Data.SqlClient;
namespace COMServer.Classes
{
    class clsSecurity
    {
        StringBuilder _SbQry = null;
        clsMsgRule oRule = new clsMsgRule();

        #region App New Version Update

        internal string GetAppVersion()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetAppVersion] '" + EnumAppType.ANDROIDAPP + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["Version"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~App new version information not found,please check";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string GetNewExeDevice()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\NewApp\\AndroidApp"))
                {
                    throw new Exception("Location not defined for new app");
                }
                string[] AllFiles = Directory.GetFiles(Application.StartupPath + "\\NewApp\\AndroidApp");
                string FileName = Path.GetFileName(AllFiles[0]);
                byte[] FileNewExe = File.ReadAllBytes(Application.StartupPath + "\\NewApp\\AndroidApp\\" + FileName);

                string exestring = Convert.ToBase64String(FileNewExe);
                oRule.sResponse = clsMsgRule.sValid + "~" + exestring + "~" + FileName;
            }
            catch (Exception ex) { throw ex; }
            return oRule.sResponse + "}";
        }
        internal string GetNewExeDesktop()
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "\\NewApp\\DesktopApp"))
                {
                    throw new Exception("Location not defined for new app");
                }
                string[] AllFiles = Directory.GetFiles(Application.StartupPath + "\\NewApp\\DesktopApp");
                string FileName = Path.GetFileName(AllFiles[0]);
                byte[] FileNewExe = File.ReadAllBytes(Application.StartupPath + "\\NewApp\\DesktopApp\\" + FileName);

                string exestring = Convert.ToBase64String(FileNewExe);
                oRule.sResponse = clsMsgRule.sValid + "~" + exestring + "~" + FileName;
            }
            catch (Exception ex) { throw ex; }
            return oRule.sResponse;
        }

        #endregion

        #region Login & Menu
        internal string ManageUser(User user)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_UserMaster '" + EnumDbType.VALIDATEUSER + "','" + user.UserId + "','" + user.Name + "'");
                _SbQry.AppendLine(",'" + user.Password + "','" + user.Group + "','" + user.CreatedBy + "','" + user.NewPassword + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["USERID"].ToString() + "~" + dt.Rows[0]["UserName"].ToString() + "~" + dt.Rows[0]["GroupName"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Wrong UserId / Password";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string GetUserRights(string UserGroup)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("SELECT ModuleId FROM GroupRight Where GroupName = '" + UserGroup + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["ModuleId"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region Picking For Machining
        internal string GetPickingForMachiningData(string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Picking_For_Machining_Data] '" + EnumDbType.SELECT + "','" + TrolleyCard + "','" + Convert.ToInt32(EnumCuttingStatus.Picking) + "',''");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "Y")
                        oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["Qty"].ToString() + "~" + dt.Rows[0]["ModelNo"].ToString();
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string SavePickingForMachiningData(string TrolleyCard, string UserId)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Picking_For_Machining_Data] '" + EnumDbType.UPDATE + "','" + TrolleyCard + "','" + Convert.ToInt32(EnumCuttingStatus.Picking) + "','" + UserId + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "Y")
                        oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["Msg"].ToString();
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region Machining

        internal string GeLine()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                oDb.Connect();
                //Now Get Line Details
                _SbQry.AppendLine("Exec [Prc_LineMaster] 'SELECT','',''");
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["Line_No"].ToString() + "-" + row["Description"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string GetShift()
        {
            clsDB oDb = new clsDB();
            string Shift = "";
            try
            {
                _SbQry = new StringBuilder();
                oDb.Connect();
                _SbQry.AppendLine("Exec [Prc_GetShift]");
                Shift = oDb.GetDataTable(_SbQry.ToString()).Rows[0]["ShiftName"].ToString();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return Shift;
        }

        internal string GetMachiningData(string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Get_Machining_Data '" + TrolleyCard + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            if (Convert.ToInt32(row["Status"].ToString()) != Convert.ToInt32(EnumCuttingStatus.Picking))
                            {
                                string Message = Convert.ToInt32(row["Status"].ToString()) == Convert.ToInt32(EnumCuttingStatus.After_Machining) ? "Machining is already done" : "Picking pending, current status " + Enum.GetName(typeof(EnumCuttingStatus), Convert.ToInt32(row["Status"]));
                                oRule.sResponse = clsMsgRule.sInValid + "~" + Message;
                                break;
                            }
                            else
                            {
                                oRule.sResponse += row["ModelNo"].ToString() + "$" + row["LotNo"].ToString() + "$" + Convert.ToDateTime(row["LotNoDate"]).ToString("dd-MMM-yyyy") + "$" + row["TotalQty"].ToString() + "#";
                            }
                        }
                        catch (Exception)
                        {
                            oRule.sResponse = clsMsgRule.sInValid + "~" + row["Status"].ToString();
                            break;
                        }
                        
                        
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string SaveMachiningMixedData(string TrolleyCard, int TotalOkQty, List<Machining> ListMachining, string OperatorName, string InspectorName, string printerIp)
        {
            clsDB oDb = new clsDB();
            try
            {
                //Check Prn File Exist Or Not
                if (File.Exists(Application.StartupPath + "\\" + Program.MachiningPrnName))
                {
                    //fetch the current shift from the server
                    //added by dipak 18_01_22 for multiple Machining Printer
                    Program.MachiningPrinterIP = printerIp;
                    string Shift = GetShift();
                    _SbQry = new StringBuilder();
                    oDb.Connect();
                    oDb.BeginTran();
                    foreach (var machining in ListMachining)
                    {
                        _SbQry.AppendLine("Exec Prc_SaveMachiningData '" + Shift + "','" + machining.ModelNo + "','" + machining.LotNo + "','" + machining.LotNoDate + "','" + machining.LineNo + "','" + machining.CuttingTrolleyCard + "'");
                        _SbQry.AppendLine("," + machining.TotalQty + "," + machining.OkQty + "," + machining.NgQty + "");
                        _SbQry.AppendLine(",'" + TrolleyCard + "'," + machining.Status + "," + machining.MachiningStatus + ",'" + machining.CreatedBy + "','" + OperatorName + "','" + InspectorName + "'");
                        _SbQry.AppendLine("," + machining.InnerCavity + "," + machining.OuterCavity + "," + machining.Slag + "," + machining.Dent + "");
                        _SbQry.AppendLine("," + machining.Spine + "," + machining.ForgMat + "," + machining.Rust + "," + machining.PinHole + "," + machining.MachineOutsideCavity + "");
                        _SbQry.AppendLine("," + machining.Other + "," + machining.IDPlus + "");
                        _SbQry.AppendLine("," + machining.IDMinus + "," + machining.PowerCut + "," + machining.ExtraParam4 + "," + machining.ExtraParam5 + "");

                        _SbQry.AppendLine("," + machining.TotalLengthMinus + "," + machining.TotalLengthPlus + "," + machining.RCenterMinus + "," + machining.RCenterPlus + "");
                        _SbQry.AppendLine("," + machining.NoCleanUp + "," + machining.Crack + "");

                        oDb.GetDataTable(_SbQry.ToString());
                        _SbQry.Length = 0;
                    }
                    oDb.CommitTran();
                    //Now Print The Trolley Card
                    oRule.sResponse = clsMsgRule.sValid + "~ Data saved successfully";
                    StreamReader sr = null;
                    try
                    {
                        sr = new StreamReader(Application.StartupPath + "\\" + Program.MachiningPrnName);
                        string PrnFile = sr.ReadToEnd();
                        sr.Close();
                        sr = null;
                        SatoPrinter satoPrinter = new SatoPrinter();
                        satoPrinter.PrintMachiningLabel(TrolleyCard, ListMachining[0].ModelNo, TrolleyCard.Split('-')[1], TotalOkQty.ToString(), PrnFile);
                        oRule.sResponse += ", trolley card print successfully.";
                    }
                    catch (Exception PrintEx)
                    {
                        oRule.sResponse += ", " + PrintEx.Message;
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close();
                            sr = null;
                        }
                    }
                }
                else
                    oRule.sResponse = clsMsgRule.sInValid + "~Request could not process,Prn File " + Program.MachiningPrnName + " not found";
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse + "~" + TrolleyCard;
        }



        public DataTable ReprintHistory(string Type, string RePrintType, string TrolleyCard, int OkQty, string Reason, string CreatedBy, string FromDate, string Todate)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ReprintHistory] '" + Type + "','" + RePrintType + "','" + TrolleyCard + "' ," + OkQty + ",'" + Reason + "' ,'" + CreatedBy + "','" + FromDate + "','" + Todate + "'");
                oDb.Connect();
                return oDb.GetDataTable(_SbQry.ToString());
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }


        internal string ReprintMachiningTrolleyCard(string TrolleyCard,string printerIp,string createdby)
        {
            clsDB oDb = new clsDB();
            try
            { 
                //added by dipak 18_01_22 for multiple Machining Printer
                Program.MachiningPrinterIP = printerIp;
                _SbQry = new StringBuilder("Exec [Prc_GetRePrintData] 'MACHINING','" + TrolleyCard + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    StreamReader sr = null;
                    try
                    {
                        sr = new StreamReader(Application.StartupPath + "\\" + Program.MachiningPrnName);
                        string PrnFile = sr.ReadToEnd();
                        sr.Close();
                        sr = null;
                        SatoPrinter satoPrinter = new SatoPrinter();
                        satoPrinter.PrintMachiningLabel(TrolleyCard, dt.Rows[0]["ModelNo"].ToString(), TrolleyCard.Split('-')[1], dt.Rows[0]["OkQty"].ToString().ToString(), PrnFile);
                        DataTable dtReprint = ReprintHistory("INSERT", "MACHINING", TrolleyCard, Convert.ToInt32( dt.Rows[0]["OkQty"].ToString()), "MACHINING", createdby, "","");
                        oRule.sResponse = clsMsgRule.sValid + "~Trolley card print successfully.";
                    }
                    catch (Exception PrintEx)
                    {
                        oRule.sResponse = clsMsgRule.sError + "~" + PrintEx.Message;
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close();
                            sr = null;
                        }
                    }
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Wrong Trolley Card,Data Not Found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region Machining Split
        internal string GetModelForMachiningSplit()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [Prc_GetModel_ForMachiningSplit]");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["ModelNo"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Model details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string GetMachiningSplitData(string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Get_Machining_Split_Data '" + TrolleyCard + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["Status"].ToString()) != Convert.ToInt32(EnumCuttingStatus.Picking))
                        {
                            string Message = Convert.ToInt32(row["Status"].ToString()) == Convert.ToInt32(EnumCuttingStatus.After_Machining) ? "Machining is already done" : "Picking pending, current status " + Enum.GetName(typeof(EnumCuttingStatus), Convert.ToInt32(row["Status"]));
                            oRule.sResponse = clsMsgRule.sInValid + "~" + Message;
                            break;
                        }
                        else
                        {
                            oRule.sResponse += row["ModelNo"].ToString() + "$" + row["LotNo"].ToString() + "$" + Convert.ToDateTime(row["LotNoDate"]).ToString("dd-MMM-yyyy") + "$" + row["TotalQty"].ToString() + "#";
                        }
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found or Machining is already done.";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string SaveMachiningSplitMixedData(string TrolleyCard, int TotalOkQty, List<Machining> ListMachining, string OperatorName, string InspectorName, string printerIp)
        {
            clsDB oDb = new clsDB();
            try
            {
                //Check Prn File Exist Or Not
                if (File.Exists(Application.StartupPath + "\\" + Program.MachiningPrnName))
                {
                    //fetch the current shift from the server
                    //added by dipak 18_01_22 for multiple Machining Printer
                    Program.MachiningPrinterIP = printerIp;
                    string Shift = GetShift();
                    _SbQry = new StringBuilder();
                    oDb.Connect();
                    oDb.BeginTran();
                    foreach (var machining in ListMachining)
                    {
                        _SbQry.AppendLine("Exec Prc_SaveMachiningSplitData '" + Shift + "','" + machining.ModelNo + "','" + machining.LotNo + "','" + machining.LotNoDate + "','" + machining.LineNo + "','" + machining.CuttingTrolleyCard + "'");
                        _SbQry.AppendLine("," + machining.TotalQty + "," + machining.OkQty + "," + machining.NgQty + "");
                        _SbQry.AppendLine(",'" + TrolleyCard + "'," + machining.Status + "," + machining.MachiningStatus + ",'" + machining.CreatedBy + "','" + OperatorName + "','" + InspectorName + "'");
                        _SbQry.AppendLine("," + machining.InnerCavity + "," + machining.OuterCavity + "," + machining.Slag + "," + machining.Dent + "");
                        _SbQry.AppendLine("," + machining.Spine + "," + machining.ForgMat + "," + machining.Rust + "," + machining.PinHole + "," + machining.MachineOutsideCavity + "");
                        _SbQry.AppendLine("," + machining.Other + "," + machining.IDPlus + "");
                        _SbQry.AppendLine("," + machining.IDMinus + "," + machining.PowerCut + "," + machining.ExtraParam4 + "," + machining.ExtraParam5 + "");
                        _SbQry.AppendLine("," + machining.TotalLengthMinus + "," + machining.TotalLengthPlus + "," + machining.RCenterMinus + "," + machining.RCenterPlus + "");
                        _SbQry.AppendLine("," + machining.NoCleanUp + "," + machining.Crack + "");

                        oDb.GetDataTable(_SbQry.ToString());
                        _SbQry.Length = 0;
                    }
                    oDb.CommitTran();
                    //Now Print The Trolley Card
                    oRule.sResponse = clsMsgRule.sValid + "~ Data saved successfully";
                    StreamReader sr = null;
                    try
                    {
                        sr = new StreamReader(Application.StartupPath + "\\" + Program.MachiningPrnName);
                        string PrnFile = sr.ReadToEnd();
                        sr.Close();
                        sr = null;
                        SatoPrinter satoPrinter = new SatoPrinter();
                        satoPrinter.PrintMachiningLabel(TrolleyCard, ListMachining[0].ModelNo, TrolleyCard.Split('-')[1], TotalOkQty.ToString(), PrnFile);
                        oRule.sResponse += ", trolley card print successfully.";
                    }
                    catch (Exception PrintEx)
                    {
                        oRule.sResponse += ", " + PrintEx.Message;
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close();
                            sr = null;
                        }
                    }
                }
                else
                    oRule.sResponse = clsMsgRule.sInValid + "~Request could not process,Prn File " + Program.MachiningPrnName + " not found";
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse + "~" + TrolleyCard;
        }

        #endregion

        #region ReOiling

        internal string GetReOilingData(string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Get_ReOiling_Data] '" + TrolleyCard + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "Y")
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~";
                        foreach (DataRow row in dt.Rows)
                        {
                            oRule.sResponse += row["ModelNo"].ToString() + "$" + row["LotNo"].ToString() + "$" + Convert.ToDateTime(row["LotNoDate"]).ToString("dd-MMM-yyyy") + "$" + row["Qty"].ToString() + "#";
                        }
                        oRule.sResponse = oRule.sResponse.TrimEnd('#');
                    }
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string SaveReOilingData(List<ReOiling> ListReOiling)
        {
            clsDB oDb = new clsDB();
            try
            {

                //fetch the current shift from the server
                string Shift = GetShift();
                _SbQry = new StringBuilder();
                oDb.Connect();
                oDb.BeginTran();
                foreach (var Item in ListReOiling)
                {
                    _SbQry.AppendLine("Exec Prc_SaveReOilingData '" + Shift + "','" + Item.ModelNo + "','" + Item.LotNo + "','" + Item.LotNoDate + "','" + Item.MachiningTrolleyCard + "'");
                    _SbQry.AppendLine("," + Item.TotalQty + "," + Item.OkQty + "," + Item.NgQty + ",'" + Item.CreatedBy + "'");
                    _SbQry.AppendLine("," + Item.InnerCavity + "," + Item.OuterCavity + "," + Item.Slag + "," + Item.Dent + "");
                    _SbQry.AppendLine("," + Item.Spine + "," + Item.RONg + "," + Item.Other + "," + Item.ExtraParam1 + "");
                    _SbQry.AppendLine("," + Item.ExtraParam2 + "," + Item.ExtraParam3 + "," + Item.ExtraParam4 + "," + Item.ExtraParam5 + "");

                    oDb.GetDataTable(_SbQry.ToString());
                    _SbQry.Length = 0;
                }
                oDb.CommitTran();
                oRule.sResponse = clsMsgRule.sValid + "~ Data saved successfully";
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region Final Packing

        internal string GetCustomerTrolley(string TrolleyNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [Prc_ValidateTrolleyNo] '" + TrolleyNo + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "Y")
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["PackQty"].ToString() + "~" + dt.Rows[0]["PackSize"].ToString();
                    }
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley no details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string GetFinalPackingData(string TrolleyCard, string TrolleyNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Get_FinalPacking_Data '" + TrolleyCard + "','" + TrolleyNo + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Result"].ToString() != "Y")
                        {
                            oRule.sResponse = clsMsgRule.sInValid + "~" + row["Result"].ToString();
                            break;
                        }
                        else
                        {
                            oRule.sResponse += row["ModelNo"].ToString() + "$" + row["LotNo"].ToString() + "$" + Convert.ToDateTime(row["LotNoDate"]).ToString("dd-MMM-yyyy") + "$" + row["Qty"].ToString() + "#";
                        }
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley card details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string SaveFinalPackingData(FinalPacking Item)
        {
            clsDB oDb = new clsDB();
            try
            {
                //fetch the current shift from the server
                string Shift = GetShift();
                _SbQry = new StringBuilder();
                oDb.Connect();

                _SbQry.AppendLine("Exec Prc_SaveFinalPackingData '" + Shift + "','" + Item.ModelNo + "','" + Item.TrolleyNo + "'," + Item.TrolleyPackSize + ",'" + Item.LotNo + "','" + Item.LotNoDate + "','" + Item.MachiningTrolleyCard + "'");
                _SbQry.AppendLine("," + Convert.ToInt32(EnumFinalPackingStatus.FinalPacking) + "," + Convert.ToInt32(EnumMachiningStatus.FinalPacking) + "," + Item.TotalQty + "," + Item.OkQty + "," + Item.NgQty + ",'" + Item.CreatedBy + "'");
                _SbQry.AppendLine("," + Item.InnerCavity + "," + Item.OuterCavity + "," + Item.Slag + "," + Item.Dent + "");
                _SbQry.AppendLine("," + Item.Spine + "," + Item.PackNg + "," + Item.Other + "," + Item.Rust + "");
                _SbQry.AppendLine("," + Item.ExtraParam2 + "," + Item.ExtraParam3 + "," + Item.ExtraParam4 + "," + Item.ExtraParam5 + "," + Item.Color + "");

                oDb.GetDataTable(_SbQry.ToString());
                _SbQry.Length = 0;

                oRule.sResponse = clsMsgRule.sValid + "~ Data saved successfully";
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region Add/Print Trolley

        internal string GetModelForTrolley()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [Prc_GetModel]");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["ModelNo"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Model details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string SaveTrolleyForPrint(string TrolleyNo, string ModelNo, string PackSize, string UserId)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec Prc_TrolleyMaster_FromHHT '" + EnumDbType.INSERT + "','" + TrolleyNo + "','" + ModelNo + "'," + PackSize + ",'" + UserId + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RESULT"].ToString() == "Y")
                        oRule.sResponse = clsMsgRule.sValid + "~Save successfully.Now click on print button to print";
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["RESULT"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~no information return from database";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string GetTrolleyData(string TrolleyNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec Prc_TrolleyMaster_FromHHT '" + EnumDbType.SEARCH + "','" + TrolleyNo + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["PackSize"].ToString() + "~" + dt.Rows[0]["ModelNo"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~TrolleyNo details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        internal string PrintTrolley(string TrolleyNo)
        {
            try
            {
                string Msg = GetTrolleyData(TrolleyNo);
                string[] ArrayMsg = Msg.Split('~');
                if (ArrayMsg[0] == clsMsgRule.sValid)
                {
                    StreamReader sr = null;
                    try
                    {
                        sr = new StreamReader(Application.StartupPath + "\\" + Program.TrolleyBox);
                        string PrnFile = sr.ReadToEnd();
                        sr.Close();
                        sr = null;
                        SatoPrinter satoPrinter = new SatoPrinter();
                        satoPrinter.PrintTrolleyBoxLabel(TrolleyNo, ArrayMsg[2], PrnFile);
                        oRule.sResponse = clsMsgRule.sValid;
                    }
                    catch (Exception PrintEx)
                    {
                        oRule.sResponse = clsMsgRule.sInValid + "~" + PrintEx.Message;
                    }
                    finally
                    {
                        if (sr != null)
                        {
                            sr.Close();
                            sr = null;
                        }
                    }
                }
                else
                    oRule.sResponse = Msg;
            }
            catch (Exception ex) { throw ex; }
            finally
            {

            }
            return oRule.sResponse;
        }

        #endregion

        #region Trolley Receiving (Created by dipak 12July19)
        internal string GetColorName()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [PRC_TROLLEY_REC] 'GET_COLOR' ");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        oRule.sResponse += row["ColorName"].ToString() + "#";
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Color data not found,please check";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string ValidateTrolley(string Trolley)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [PRC_TROLLEY_REC] 'VALID_TROLLEY','" + Trolley + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    oRule.sResponse += dt.Rows[0]["RESULT"].ToString() + "#";
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string SaveTrolleyRec(string trolley, string trayQty, string colorName, string createdBy)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [PRC_TROLLEY_REC] 'SAVE_TROLLEY_REC','" + trolley + "','" + trayQty + "','" + colorName + "','" + createdBy + "' ");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "Y")
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0][1].ToString();
                    }
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["Result"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley no details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        #endregion

        #region Trolley Dispatch (Created by dipak 16July19)

        internal string GetDispatchOrderData(string DispatchOrderNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [Prc_GetDispatchData] '1','" + DispatchOrderNo + "','','0',''");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["Id"].ToString() + "~" + dt.Rows[0]["ModelNo"].ToString() + "~" + dt.Rows[0]["Qty"].ToString() + "~" + dt.Rows[0]["DispatchQty"].ToString() + "~" + dt.Rows[0]["CustomerName"].ToString() + "~" + dt.Rows[0]["Location"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Wrong dispatch order no or dispatch already completed";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string GetTrolleyQty(string sModel, string sTrolley)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec [Prc_GetDispatchData] '2','','" + sModel + "'," + "NULL" + ",'" + sTrolley + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    oRule.sResponse = clsMsgRule.sValid + "~";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Result"].ToString() != "Y")
                        {
                            oRule.sResponse = clsMsgRule.sInValid + "~" + row["Result"].ToString();
                            break;
                        }
                        else
                        {
                            oRule.sResponse += row["Qty"].ToString() + "#";
                        }
                    }
                    oRule.sResponse = oRule.sResponse.TrimEnd('#');
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~Trolley details not found";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        internal string SaveDispatchData(string sDisOrderId, string sModel, string sTrolleyNo, string sQty, string sCreatedBy)
        {
            clsDB oDb = new clsDB();
            try
            {
                //fetch the current shift from the server
                string Shift = GetShift();
                _SbQry = new StringBuilder();
                oDb.Connect();

                _SbQry.AppendLine("Exec Prc_SaveDispatchData '" + sDisOrderId + "','" + Shift + "','" + sModel + "','" + sTrolleyNo + "'");
                _SbQry.AppendLine("," + Convert.ToInt32(EnumFinalPackingStatus.Dispatch) + "," + Convert.ToInt32(EnumTrolleyStatus.Dispatch) + ",'" + sQty + "','" + sCreatedBy + "'");

                oDb.GetDataTable(_SbQry.ToString());
                _SbQry.Length = 0;

                oRule.sResponse = clsMsgRule.sValid + "~ Data saved successfully";
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }
        #endregion

        #region Exchange Trolley
        internal string ExcangeTrolley(string dbType,string oldTrolleyNo, string newTrolleyNo, string userId)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                _SbQry.AppendLine("Exec PRC_TROLLEY_EXCHANGE '" + dbType + "','" + oldTrolleyNo + "','" + newTrolleyNo + "','" + userId + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["RESULT"].ToString() == "Y")
                        oRule.sResponse = clsMsgRule.sValid + "~"+ dt.Rows[0]["RESULT"].ToString();
                    else
                        oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["RESULT"].ToString();
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~no information return from database";
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
            return oRule.sResponse;
        }

        #endregion

        #region IN_OUT_WARD
        public string In_Out_Ward(InOutWard obj)
        {
           SqlHelper _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@PROCESS", SqlDbType.VarChar, 50);
                param[1].Value = obj.Process;
                param[2] = new SqlParameter("@WH_CODE", SqlDbType.VarChar, 50);
                param[2].Value = obj.WarehouseCode;
                param[3] = new SqlParameter("@MODEL_NO", SqlDbType.VarChar, 50);
                param[3].Value = obj.ModelNo;
                param[4] = new SqlParameter("@TRIP", SqlDbType.VarChar, 50);
                param[4].Value = obj.Trip;
                param[5] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 50);
                param[5].Value = obj.Barcode;
                param[6] = new SqlParameter("@IN_STATUS", SqlDbType.VarChar, 50);
                param[6].Value = obj.InStatus;
                param[7] = new SqlParameter("@OUT_STATUS", SqlDbType.VarChar, 50);
                param[7].Value = obj.OutStatus;
                param[8] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[8].Value = obj.CreatedBy;
                DataTable dt = _SqlHelper.ExecuteDataset(clsMsgRule.mMainSqlConString, CommandType.StoredProcedure, "[PRC_IN_OUT_WARD]", param).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns[0].ColumnName == "RESULT")
                    {
                        if (dt.Rows[0]["RESULT"].ToString() == "Y")
                            oRule.sResponse = clsMsgRule.sValid + "~" + dt.Rows[0]["RESULT"].ToString();
                        else
                            oRule.sResponse = clsMsgRule.sInValid + "~" + dt.Rows[0]["RESULT"].ToString();
                    }
                    else
                    {
                        oRule.sResponse = clsMsgRule.sValid + "~";
                        foreach (DataRow row in dt.Rows)
                        {
                            oRule.sResponse += row[0].ToString() + "#";
                        }
                        oRule.sResponse = oRule.sResponse.TrimEnd('#');
                    }
                }
                else
                {
                    oRule.sResponse = clsMsgRule.sInValid + "~no information return from database";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oRule.sResponse;
        }
        #endregion

    }
}
