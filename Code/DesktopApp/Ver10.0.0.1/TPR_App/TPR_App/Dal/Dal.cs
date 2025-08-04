using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatoLib;

namespace TPR_App
{
    public class Dal
    {
        SqlHelper _SqlHelper = new SqlHelper();
        

        StringBuilder _SbQry;

        #region AppVersion Update

        public DataTable GetAppVersion()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetAppVersion] '" + EnumAppType.DESKTOPAPP + "'");
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

        #endregion

        #region GroupMaster

        public DataSet GetGroup(Group group)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_GroupMaster '" + group.DbType + "','" + group.GroupName + "'");
                oDb.Connect();
                return oDb.GetDataSet(_SbQry.ToString());
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        public void SaveGroup(Group group, DataGridView dgv)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_GroupMaster '" + EnumDbType.INSERT + "','" + group.GroupName + "','','" + group.CreatedBy + "'");
                oDb.Connect();
                oDb.BeginTran();
                //First Insert Group Name 
                oDb.GetDataTable(_SbQry.ToString());
                //Now Insert Group Rights
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["HasRight"].Value) == true)
                    {
                        _SbQry.Length = 0;
                        _SbQry.AppendLine("Exec Prc_GroupMaster 'INSERT_GROUP_RIGHT','" + group.GroupName + "','" + row.Cells["ModuleId"].Value.ToString() + "','" + group.CreatedBy + "'");
                        oDb.GetDataTable(_SbQry.ToString());
                    }
                }
                oDb.CommitTran();
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        public void UpdateGroup(Group group, DataGridView dgv)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_GroupMaster 'DELETE_GROUP_RIGHT','" + group.GroupName + "','','" + group.CreatedBy + "'");
                oDb.Connect();
                oDb.BeginTran();
                //First Insert Group Name 
                oDb.GetDataTable(_SbQry.ToString());
                //Now Insert Group Rights
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["HasRight"].Value) == true)
                    {
                        _SbQry.Length = 0;
                        _SbQry.AppendLine("Exec Prc_GroupMaster 'INSERT_GROUP_RIGHT','" + group.GroupName + "','" + row.Cells["ModuleId"].Value.ToString() + "','" + group.CreatedBy + "'");
                        oDb.GetDataTable(_SbQry.ToString());
                    }
                }
                oDb.CommitTran();
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        #endregion

        #region User Master

        public DataTable GetGroupName()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("SELECT GroupName FROM GROUPMASTER Order By GroupName");
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

        public DataTable ManageUser(User user)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_UserMaster '" + user.DbType + "','" + user.UserId + "','" + user.Name + "'");
                _SbQry.AppendLine(",'" + user.Password + "','" + user.Group + "','" + user.CreatedBy + "','" + user.NewPassword + "'");
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

        #endregion

        #region Menu

        public DataTable GetUserRight(string UserGroup)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("SELECT ModuleId FROM GroupRight Where GroupName = '" + UserGroup + "'");
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

        public DataTable GetShift()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetShift]");
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

        public DataTable GetTimerTime(string Type = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetTimerTime_PendingReOiling] '" + Type + "'");
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

        public DataTable GetDashBoardData(EnumDashBoardName dashBoardName, bool IsPreviousDay = false, int ProdFlag = 1, string ModelNo = "", bool ShowNotUsedTrolley = false)
        {
            clsDB oDb = new clsDB();
            try
            {
                // string Shift = GetShift().Rows[0]["ShiftName"].ToString();
                _SbQry = new StringBuilder("Exec [Prc_GetDashBoardData] '" + dashBoardName + "','" + IsPreviousDay + "'," + ProdFlag + ",'" + ModelNo + "','" + ShowNotUsedTrolley + "'");
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

        #endregion

        #region Machine Master

        public DataTable ManageMachine(Machine machine)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_MachineMaster] '" + machine.DbType + "','" + machine.MachineNo + "','" + machine.Description + "'");
                _SbQry.AppendLine(",'" + machine.CreatedBy + "'");
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

        #endregion

        #region Trolley Master

        public DataTable ManageTrolley(Trolley trolley)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_TrolleyMaster] '" + trolley.DbType + "','" + trolley.TrolleyNo + "','" + trolley.Description + "'");
                _SbQry.AppendLine(",'" + trolley.IsReturnAble + "'," + trolley.PackSize + ",'" + trolley.CreatedBy + "','" + trolley.IsNG + "'");
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

        #endregion

        #region Line Master

        public DataTable ManageLine(Line line)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_LineMaster] '" + line.DbType + "','" + line.LineNo + "','" + line.Description + "'");
                _SbQry.AppendLine(",'" + line.CreatedBy + "'");
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

        #endregion

        #region Customer Master

        public DataTable ManageCustomer(Customer customer)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_CustomerMaster] '" + customer.DbType + "','" + customer.Name + "','" + customer.Address + "','" + customer.Location + "'");
                _SbQry.AppendLine(",'" + customer.CreatedBy + "'");
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

        #endregion

        #region Model Master

        public DataTable ManageModel(Model model)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ModelMaster] '" + model.DbType + "','" + model.ModelNo + "','" + model.Description + "'");
                _SbQry.AppendLine(",'" + model.CreatedBy + "'," + model.DTB + "," + model.Qty + "");
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

        #endregion

        #region Shift Master

        public DataTable ManageShift(Shift shift)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ShiftMaster] '" + shift.DbType + "','" + shift.ShiftName + "','" + shift.StartTime + "'");
                _SbQry.AppendLine(",'" + shift.EndTime + "','" + shift.CreatedBy + "'");
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

        #endregion

        #region Production Plan

        public DataTable ManageProductionPlan(ProductionPlan productionPlan)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ProductionPlan] '" + productionPlan.DbType + "','" + productionPlan.Month + "'," + productionPlan.MonthNo + "");
                _SbQry.AppendLine("," + productionPlan.Year + "," + productionPlan.OrderNo + ",'" + productionPlan.ModelNo + "'");
                _SbQry.AppendLine("," + productionPlan.Qty + ",'" + productionPlan.CreatedBy + "','" + productionPlan.Remarks + "','" + productionPlan.LineNo + "'");
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

        #endregion

        #region Cutting
        public int GetModelDTB(string ModelNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Select DTB From Modelmaster Where ModelNo='" + ModelNo + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                int dtb = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
                return dtb;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }
        //It will return multiple datatable while loading cutting form
        public DataSet GetCuttingData(bool IsPreviousMonth = false, string PreviousMonth = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetCuttingData] '" + ClsGlobal.UserGroup + "','" + ClsGlobal.LineNo + "','" + IsPreviousMonth + "','" + PreviousMonth + "'");
                oDb.Connect();
                return oDb.GetDataSet(_SbQry.ToString());
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }
        public DataTable GetChargePalletNo(string ModelNo, string LotNoDate, string LineNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetChargePalletNo] '" + ModelNo + "','" + LotNoDate + "','" + LineNo + "'");
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
        public bool IsLotNoExist(string LotNo, string ModelNo)
        {
            clsDB oDb = new clsDB();
            try
            {
                bool IsExist = false;
                _SbQry = new StringBuilder("SELECT LotNo FROM CUTTING Where LotNo = '" + LotNo + "' And ModelNo = '" + ModelNo + "'");
                oDb.Connect();
                DataTable dt = oDb.GetDataTable(_SbQry.ToString());
                if (dt.Rows.Count > 0) IsExist = true;
                return IsExist;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }
        public string SaveCuttingMixedTrollyData(List<Cutting> cuttings)
        {
            clsDB oDb = new clsDB();
            try
            {
                string Msg = "Not Saved";
                _SbQry = new StringBuilder();
                oDb.Connect();
                oDb.BeginTran();
                foreach (var cutting in cuttings)
                {
                    _SbQry.AppendLine("Exec [Prc_SaveCuttingData] " + cutting.ProductionPlanId + ",'" + cutting.ModelNo + "'");
                    _SbQry.AppendLine(",'" + cutting.ShiftName + "'," + cutting.ChargeNo + "," + cutting.PalletNo + ",'" + cutting.LineNo + "','" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + cutting.Operators + "'");
                    _SbQry.AppendLine(",'" + cutting.Leaders + "'," + cutting.TotalQty + "," + cutting.OkQty + "," + cutting.NgQty + "");
                    _SbQry.AppendLine(",'" + cutting.TrolleyCard + "'," + cutting.Status + ",'" + cutting.CreatedBy + "'");
                    _SbQry.AppendLine("," + cutting.InnerCavity + "," + cutting.OuterCavity + "," + cutting.PenetrationPorosity + "," + cutting.Slag + "");
                    _SbQry.AppendLine("," + cutting.Dent + "," + cutting.Spine + "," + cutting.ForeignSubstance + "," + cutting.Crack + "");
                    _SbQry.AppendLine("," + cutting.MaterialLD + "," + cutting.OD + "," + cutting.Cuttings + "," + cutting.Other + "");
                    _SbQry.AppendLine("," + cutting.DTBQty + "," + cutting.Sample + "," + cutting.PowerCut + "," + cutting.Length + "," + cutting.ExtraParam5 + "");

                    Msg = oDb.GetDataTable(_SbQry.ToString()).Rows[0]["Result"].ToString();
                    _SbQry.Length = 0;
                }
                oDb.CommitTran();
                return Msg;
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        public DataTable SaveCuttingData(Cutting cutting)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_SaveCuttingData] " + cutting.ProductionPlanId + ",'" + cutting.ModelNo + "'");
                _SbQry.AppendLine(",'" + cutting.ShiftName + "'," + cutting.ChargeNo + "," + cutting.PalletNo + ",'" + cutting.LineNo + "','" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + cutting.Operators + "'");
                _SbQry.AppendLine(",'" + cutting.Leaders + "'," + cutting.TotalQty + "," + cutting.OkQty + "," + cutting.NgQty + "");
                _SbQry.AppendLine(",'" + cutting.TrolleyCard + "'," + cutting.Status + ",'" + cutting.CreatedBy + "'");
                _SbQry.AppendLine("," + cutting.InnerCavity + "," + cutting.OuterCavity + "," + cutting.PenetrationPorosity + "," + cutting.Slag + "");
                _SbQry.AppendLine("," + cutting.Dent + "," + cutting.Spine + "," + cutting.ForeignSubstance + "," + cutting.Crack + "");
                _SbQry.AppendLine("," + cutting.MaterialLD + "," + cutting.OD + "," + cutting.Cuttings + "," + cutting.Other + "");
                _SbQry.AppendLine("," + cutting.DTBQty + "," + cutting.Sample + "," + cutting.PowerCut + "," + cutting.Length + "," + cutting.ExtraParam5 + "");
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

        #endregion

        #region QA

        public DataTable ManageQA(QA qA)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_QA] '" + qA.DbType + "','" + qA.TrolleyCard + "'," + qA.Status + ",'" + qA.CreatedBy + "','" + qA.Shift + "'," + qA.PartialNgQty + ",'" + qA.PartialNgReason + "','" + qA.LotNo + "'");
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

        #endregion

        #region After Machining QA

        public DataTable ManageMachiningQA(QA qA)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_QA_Machining] '" + qA.DbType + "','" + qA.TrolleyCard + "','" + qA.IsOnHold + "','" + qA.CreatedBy + "'," + qA.PartialNgQty + ",'" + qA.PartialNgReason + "','" + qA.LotNo + "'");
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

        #endregion

        #region After Machining Hold/UnHold

        public DataTable ManageMachiningHoldUnHold(EnumDbType dbType, string TrolleyCard, bool IsHold, string CreatedBy)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_MachiningHold] '" + dbType + "','" + TrolleyCard + "','" + IsHold + "','" + CreatedBy + "'");
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

        #endregion

        #region QASampling

        public DataTable ManageQASample(QA qA)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_QASample] '" + qA.DbType + "','" + qA.TrolleyCard + "'," + qA.Status + "," + qA.PickedQty + ",'" + qA.CreatedBy + "'");
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

        #endregion

        #region Cutting Report

        public DataTable GetModel()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_GetModel");
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
    

        public DataTable GetCuttingReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "", string LineNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Cutting '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "','" + LineNo + "'");
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

        #endregion

        #region Cutting Report

        public DataTable GetPickingReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Picking '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "'");
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

        #endregion

        #region QA Report

        public DataTable GetQAReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string LineNo = "",string status="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_QA '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "','" + LineNo + "','" + status + "'");
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

        #endregion

        #region Cutting TrolleyCard Update

        public DataTable GetOldCuttingTrolleyData(string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Update_Cutting_Trolley '" + EnumDbType.SELECT + "',0,'','','" + TrolleyCard + "',''");
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

        public string UpdateCuttingTrolleyCard(List<CuttingTrolleyUpdate> cuttingTrolleys, string NewTrolleyCard, string OldTrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                string Msg = "Not Saved";
                _SbQry = new StringBuilder();
                oDb.Connect();
                oDb.BeginTran();
                foreach (var cutting in cuttingTrolleys)
                {
                    _SbQry.AppendLine("Exec Prc_Update_Cutting_Trolley '" + EnumDbType.UPDATE + "'," + cutting.Id + "");
                    _SbQry.AppendLine(",'" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + NewTrolleyCard + "','" + cutting.CreatedBy + "','" + cutting.IsValueChange + "','" + OldTrolleyCard + "',''");
                    Msg = oDb.GetDataTable(_SbQry.ToString()).Rows[0]["Result"].ToString();
                    _SbQry.Length = 0;
                    if (Msg != "Y")
                    {
                        throw new Exception(Msg);
                    }
                }
                oDb.CommitTran();
                return Msg;
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }
        public string UpdateCuttingTrolleyCardModel(string NewTrolleyCard, string OldTrolleyCard, string NewModelNo, int UpdateType = 2, int TotalQty = 0, int OkQty = 0, int NgQty = 0,string LotNo="")
        {
            clsDB oDb = new clsDB();
            try
            {
                string Msg = "Not Saved";
                _SbQry = new StringBuilder();
                oDb.Connect();

                _SbQry.AppendLine("Exec Prc_Update_Cutting_Trolley '" + EnumDbType.UPDATE + "',0");
                _SbQry.AppendLine(",'"+ LotNo + "','','" + NewTrolleyCard + "','" + ClsGlobal.UserId + "','False','" + OldTrolleyCard + "','" + NewModelNo + "'," + UpdateType + "," + TotalQty + "," + OkQty + "," + NgQty + "");
                Msg = oDb.GetDataTable(_SbQry.ToString()).Rows[0]["Result"].ToString();
                _SbQry.Length = 0;
                if (Msg != "Y")
                {
                    throw new Exception(Msg);
                }
                return Msg;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        #endregion

        #region ReOiling Report

        public DataTable GetReOilingReportData(string FromDate, string ToDate, string ModelNo = "", bool IsPending = false)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Rpt_ReOiling] '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + IsPending + "'");
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

        #endregion

        #region RePrint

        public DataTable GetRePrintData(string RePrintType, string TrolleyCard)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetRePrintData] '" + RePrintType + "','" + TrolleyCard + "'");
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

        public DataTable ReprintHistory(string Type, string RePrintType, string TrolleyCard,int OkQty,string Reason,string CreatedBy,string FromDate,string Todate)
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

        public DataTable ExchangeTrolleyHistory(string Type, string FromDate, string Todate)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ExchangeTrolleyHistory] '" + Type + "','" + FromDate + "','" + Todate + "'");
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


        #endregion

        #region Machining Report

        public DataTable GetMachiningReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "", string LineNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Machining '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "','" + LineNo + "'");
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

        public DataTable GetPendingMachiningPacking(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "", string LineNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Pending_Machining_Packing '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "','" + LineNo + "'");
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


        #endregion

        #region Dispatch Order
        public int GetSrNo()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Select IsNull(Max(SrNo),0) As SrNo From DispatchOrder Where Cast(CreatedOn As Date) =  '" + DateTime.Now.ToString("dd-MMM-yyyy") + "'");
                oDb.Connect();
                int SrNo = Convert.ToInt32(oDb.GetDataTable(_SbQry.ToString()).Rows[0]["SrNo"]);
                return ++SrNo;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }
        public DataTable GetCustomer(string Filter, string CustomerName = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetCustomer] '" + Filter + "','" + CustomerName + "'");
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
        public DataTable ManageDispatchOrder(DispatchOrder dispatchOrder)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_DispatchOrder] '" + dispatchOrder.DbType + "','" + dispatchOrder.Shift + "'," + dispatchOrder.SrNo + ",'" + dispatchOrder.DispatchOrderNo + "','" + dispatchOrder.DispatchDate + "'");
                _SbQry.AppendLine(",'" + dispatchOrder.ModelNo + "','" + dispatchOrder.CustomerId + "','" + dispatchOrder.CustomerName + "'");
                _SbQry.AppendLine("," + dispatchOrder.Qty + "," + dispatchOrder.OldQty + "," + dispatchOrder.TrolleyType + ",'" + dispatchOrder.CreatedBy + "'");
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
        public DataTable GetAvailableStockForDispatch(string Filter, string ModelNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetDispatchAvailableQty] '" + Filter + "','" + ModelNo + "'");
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

        #endregion

        #region Dispatch Order Cancel

        public DataTable GetDispatchDataForCancel(string DispatchDate, string ModelNo = "", string CustomerId = "0")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Dispatch_Cancel] '" + EnumDbType.SELECT + "','" + DispatchDate + "','" + ModelNo + "'," + CustomerId + "");
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

        public void CancelDispatch(DataGridView dgv, string Reason)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                oDb.Connect();
                oDb.BeginTran();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                    {
                        _SbQry.AppendLine("Exec [Prc_Dispatch_Cancel] '" + EnumDbType.DELETE + "','','',0");
                        _SbQry.AppendLine("," + row.Cells["DispatchOrderId"].Value.ToString() + ",'" + row.Cells["TrolleyNo"].Value.ToString() + "'");
                        _SbQry.AppendLine("," + row.Cells["DispatchQty"].Value.ToString() + ",'" + row.Cells["ModelNo"].Value.ToString() + "'");
                        _SbQry.AppendLine(",'" + Reason + "','" + ClsGlobal.UserId + "'," + Convert.ToInt32(EnumFinalPackingStatus.FinalPacking) + "");
                        _SbQry.AppendLine("," + Convert.ToInt32(EnumTrolleyStatus.Receive) + "");
                        oDb.GetDataTable(_SbQry.ToString());
                        _SbQry.Length = 0;
                    }
                }
                oDb.CommitTran();
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        #endregion

        #region Delete TrolleyCard

        public DataTable DeleteTrolleyCard(EnumDbType enumDbType, EnumProcess Process, string TrolleyCard = "", string TrolleyNo = "", string Reason = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Delete_TrolleyCard] '" + enumDbType + "','" + Process + "','" + TrolleyCard + "','" + TrolleyNo + "','" + Reason + "','" + ClsGlobal.UserId + "'");
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

        #endregion

        #region Customer Return

        public DataTable GetDispatchDataForReturn(string DispatchDate, string ModelNo = "", string CustomerId = "0")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_CustomerReturn] '" + EnumDbType.SELECT + "','" + DispatchDate + "','" + ModelNo + "'," + CustomerId + "");
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

        public void SaveCustomerReturn(DataGridView dgv, string Reason,string Status)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder();
                oDb.Connect();
                oDb.BeginTran();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                    {
                        _SbQry.AppendLine("Exec [Prc_CustomerReturn] '" + EnumDbType.DELETE + "','','',0");
                        _SbQry.AppendLine("," + row.Cells["DispatchOrderId"].Value.ToString() + ",'" + row.Cells["TrolleyNo"].Value.ToString() + "'");
                        _SbQry.AppendLine("," + row.Cells["DispatchQty"].Value.ToString() + ",'" + row.Cells["ModelNo"].Value.ToString() + "'");
                        _SbQry.AppendLine(",'" + Reason + "','" + ClsGlobal.UserId + "'," + Convert.ToInt32(EnumFinalPackingStatus.FinalPacking) + "");
                        _SbQry.AppendLine("," + Convert.ToInt32(EnumTrolleyStatus.Receive) + "");
                        _SbQry.AppendLine(",'" + Status + "'");
                        oDb.GetDataTable(_SbQry.ToString());
                        _SbQry.Length = 0;
                    }
                }
                oDb.CommitTran();
            }
            catch (Exception ex) { oDb.RollBackTran(); throw ex; }
            finally
            {
                oDb.DisConnect();
                oDb = null;
            }
        }

        #endregion

        #region Color Master

        public DataTable ManageColor(Colors customer)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_ColorMaster] '" + customer.DbType + "','" + customer.ColorName + "','" + customer.RowId + "'");
                _SbQry.AppendLine(",'" + customer.CreatedBy + "'");
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

        #endregion

        #region Ng Percentage

        public DataTable ManageNgPerentage(EnumDbType dbType, decimal PercentageValue = 0)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_NGPercentage] '" + dbType + "','" + PercentageValue + "'");
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

        #endregion

        #region Final Packing Report

        public DataTable GetFinalPackingReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "",string LineNo="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Rpt_FinalPacking] '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "','" + LineNo + "'");
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

        #endregion

        #region Dispatch Report
        public DataTable GetDispatchReportData(string FromDate, string ToDate, string ModelNo = "", string CustomerId = "0", string TrolleyNo = "", string isTrolleyRec = "", string customerName = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@FromDate", SqlDbType.VarChar, 100);
                param[0].Value = FromDate;
                param[1] = new SqlParameter("@ToDate", SqlDbType.VarChar, 100);
                param[1].Value = ToDate;
                param[2] = new SqlParameter("@ModelNo", SqlDbType.VarChar, 50);
                param[2].Value = ModelNo;
                param[3] = new SqlParameter("@CustomerId", SqlDbType.VarChar, 50);
                param[3].Value = CustomerId;
                param[4] = new SqlParameter("@TrolleyNo", SqlDbType.VarChar, 100);
                param[4].Value = TrolleyNo;
                param[5] = new SqlParameter("@IsTrolley", SqlDbType.VarChar, 100);
                param[5].Value = isTrolleyRec;
                param[6] = new SqlParameter("@CustomerName", SqlDbType.VarChar, 100);
                param[6].Value = customerName;
                return oDb.ExecuteDataset(ClsGlobal.mMainSqlConString, CommandType.StoredProcedure, "[Prc_Rpt_Dispatch]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DataTable GetDispatchReportData(string FromDate, string ToDate, string ModelNo = "", string CustomerId = "0", string TrolleyNo = "",string isTrolleyRec="",string customerName="")
        //{
        //    clsDB oDb = new clsDB();
        //    try
        //    {
        //        if (customerName == "")
        //        {
        //            customerName = "''";
        //        }

        //        _SbQry = new StringBuilder("Exec [Prc_Rpt_Dispatch] '" + FromDate + "','" + ToDate + "','" + ModelNo + "'," + CustomerId + ",'" + TrolleyNo + "','" + isTrolleyRec + "'," + customerName + "");
        //        oDb.Connect();
        //        return oDb.GetDataTable(_SbQry.ToString());
        //    }
        //    catch (Exception ex) { throw ex; }
        //    finally
        //    {
        //        oDb.DisConnect();
        //        oDb = null;
        //    }
        //}

        #endregion

        #region Inventory Report

        public DataTable GetInventoryReport(string ModelNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Rpt_Inventory] '" + ModelNo + "'");
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

        #endregion

        #region TrolleyReceiving Report
        public DataTable GetTrolleyReceivingReportData(string Type, string FromDate = "", string ToDate = "", string TrolleyNo = "", string CustomerId = "0", int TrayQty = 0, string ColorName = "", string Id = "0", string customerName = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter("@Type", SqlDbType.VarChar, 100);
                param[0].Value = Type;
                param[1] = new SqlParameter("@FromDate", SqlDbType.VarChar, 100);
                param[1].Value = FromDate;
                param[2] = new SqlParameter("@ToDate", SqlDbType.VarChar, 50);
                param[2].Value = ToDate;
                param[3] = new SqlParameter("@TrolleyNo", SqlDbType.VarChar, 50);
                param[3].Value = TrolleyNo;
                param[4] = new SqlParameter("@CustomerId", SqlDbType.VarChar, 100);
                param[4].Value = CustomerId;
                param[5] = new SqlParameter("@TrayQty", SqlDbType.VarChar, 100);
                param[5].Value = TrayQty;
                param[6] = new SqlParameter("@ColorName", SqlDbType.VarChar, 100);
                param[6].Value = ColorName;
                param[7] = new SqlParameter("@Id", SqlDbType.VarChar, 100);
                param[7].Value = Id;
                param[8] = new SqlParameter("@CustomerName", SqlDbType.VarChar, 100);
                param[8].Value = customerName;
                return oDb.ExecuteDataset(ClsGlobal.mMainSqlConString, CommandType.StoredProcedure, "[Prc_Rpt_TrolleyReceiving]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DataTable GetTrolleyReceivingReportData(string Type, string FromDate = "", string ToDate = "", string TrolleyNo = "", string CustomerId = "0", int TrayQty = 0, string ColorName = "", string Id = "0",string customerName="")
        //{
        //    clsDB oDb = new clsDB();
        //    try
        //    {
        //        if (customerName == "")
        //        {
        //            customerName = "''";
        //        }
        //        _SbQry = new StringBuilder("Exec [Prc_Rpt_TrolleyReceiving] '" + Type + "','" + FromDate + "','" + ToDate + "','" + TrolleyNo + "'," + CustomerId + "," + TrayQty + ",'" + ColorName + "'," + Id + ","+customerName+"");
        //        oDb.Connect();
        //        return oDb.GetDataTable(_SbQry.ToString());
        //    }
        //    catch (Exception ex) { throw ex; }
        //    finally
        //    {
        //        oDb.DisConnect();
        //        oDb = null;
        //    }
        //}

        #endregion

        #region FIFO ENABLE AND DISABLE
        public DataTable SaveFifoDisableEnable(string type,string code="",bool action=false)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [PRC_FIFO_ACTION] '" + type + "','" + code + "','" + action + "'");
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
        public DataTable SaveFifosku(string type, string code = "", bool action = false, string SKU = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [PRC_FIFO_ACTION] '" + type + "','" + code +"','" + action + "' ,'" + SKU + "'");
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

        #endregion

        #region After Packing Hold/UnHold

        public DataTable ManagePackingHoldUnHold(EnumDbType dbType, string Trolley, bool IsHold, string CreatedBy)
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_PackingHold] '" + dbType + "','" + Trolley + "','" + IsHold + "','" + CreatedBy + "'");
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

        #endregion

        #region Trolley Exchange
        public DataTable SaveTrolleyExchange(string type, string OldTrolleyNo = "", string NewTrolleyNo = "",string createdBy="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [PRC_TROLLEY_EXCHANGE] '" + type + "','" + OldTrolleyNo + "','" + NewTrolleyNo + "','" + createdBy + "'");
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
        #endregion

        #region TrolleyDelete History Report

        public DataTable GetTrolleyDeleteHistoryReportData(string Type, string FromDate = "", string ToDate = "", string TrolleyNo = "",string Process="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [PRC_RPT_TROLLEY_DELETE_HISTORY] '" + Type + "','" + FromDate + "','" + ToDate + "','" + TrolleyNo + "','" + Process + "'");
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

        #endregion

        #region Warehouse Master
       
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable WarehouseMasterExecuteTask(PL_WH_MASTER obj)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[6];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = obj.DbType;
                param[1] = new SqlParameter("@WH_CODE", SqlDbType.VarChar, 50);
                param[1].Value = obj.WHCode;
                param[2] = new SqlParameter("@WH_NAME", SqlDbType.VarChar, 50);
                param[2].Value = obj.WHName;
                param[3] = new SqlParameter("@WH_LOCATION", SqlDbType.VarChar, 50);
                param[3].Value = obj.WHLocation;
                param[4] = new SqlParameter("@STATUS", SqlDbType.VarChar, 50);
                param[4].Value = obj.Status;
                param[5] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[5].Value = obj.CreatedBy;
                return _SqlHelper.ExecuteDataset(ClsGlobal.mMainSqlConString, CommandType.StoredProcedure, "[PRC_WH_MASTER]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        #endregion

        #region Warehouse InOut Operation Report

        public DataTable GetWarehouseOperationInOutReport(string Type, string FromDate = "", string ToDate = "", string Process = "",string Operation="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [PRC_RPT_WHAREHOUSE_IN_OUT_OPREATION] '" + Type + "','" + FromDate + "','" + ToDate + "','" + Process + "','"+ Operation + "'");
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
        #endregion
        #region InOut Delete Trolley

        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public DataTable DeleteInOutTrolleyCard(string strDbType,string strPorcess,string strTrolleyBarocde="")
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[6];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = strDbType;
                param[1] = new SqlParameter("@PROCESS", SqlDbType.VarChar, 50);
                param[1].Value = strPorcess;
                param[2] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 50);
                param[2].Value = strTrolleyBarocde;
                param[3] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[3].Value = ClsGlobal.UserId;
                return _SqlHelper.ExecuteDataset(ClsGlobal.mMainSqlConString, CommandType.StoredProcedure, "[PRC_IN_OUT_WARD]", param).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

}
