using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TPR_App
{
    public class Dal
    {
        StringBuilder _SbQry;

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
                _SbQry.AppendLine("," + trolley.PackSize + ",'" + trolley.CreatedBy + "'");
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
                _SbQry.AppendLine(",'" + model.CreatedBy + "'," + model.DTB + "");
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
                _SbQry.AppendLine("," + productionPlan.Qty + ",'" + productionPlan.CreatedBy + "'");
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
        public DataSet GetCuttingData()
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetCuttingData] '" + ClsGlobal.UserGroup + "'");
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
                    _SbQry.AppendLine(",'" + cutting.ShiftName + "','" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + cutting.Operators + "'");
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
                _SbQry.AppendLine(",'" + cutting.ShiftName + "','" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + cutting.Operators + "'");
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
                _SbQry = new StringBuilder("Exec [Prc_QA] '" + qA.DbType + "','" + qA.TrolleyCard + "'," + qA.Status + ",'" + qA.CreatedBy + "','" + qA.Shift + "'");
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

        public DataTable GetCuttingReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "",string Id="",string ReportType="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Cutting '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "'");
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

        public DataTable GetQAReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_QA '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'");
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
                    _SbQry.AppendLine(",'" + cutting.LotNo + "','" + cutting.LotNoDate + "','" + NewTrolleyCard + "','" + cutting.CreatedBy + "','" + cutting.IsValueChange + "','" + OldTrolleyCard + "'");
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

        #endregion

        #region Machining Report

        public DataTable GetMachiningReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec Prc_Rpt_Machining '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "'");
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
        public DataTable GetCustomer(string Filter,string CustomerName="")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_GetCustomer] '" + Filter + "','"+CustomerName+"'");
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
                _SbQry = new StringBuilder("Exec [Prc_DispatchOrder] '" + dispatchOrder.DbType + "','" + dispatchOrder.Shift + "','" + dispatchOrder.DispatchDate + "'");
                _SbQry.AppendLine(",'" + dispatchOrder.ModelNo + "','" + dispatchOrder.CustomerId + "','" + dispatchOrder.CustomerName + "'");
                _SbQry.AppendLine("," + dispatchOrder.Qty + "," + dispatchOrder.OldQty + ",'" + dispatchOrder.CreatedBy + "'");
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

        #region Final Packing Report

        public DataTable GetFinalPackingReportData(string FromDate, string ToDate, string ModelNo = "", string LotNo = "", string Id = "", string ReportType = "")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Rpt_FinalPacking] '" + FromDate + "','" + ToDate + "','" + ModelNo + "','" + LotNo + "'," + Id + ",'" + ReportType + "'");
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

        public DataTable GetDispatchReportData(string FromDate, string ToDate, string ModelNo = "", string CustomerId = "0")
        {
            clsDB oDb = new clsDB();
            try
            {
                _SbQry = new StringBuilder("Exec [Prc_Rpt_Dispatch] '" + FromDate + "','" + ToDate + "','" + ModelNo + "'," + CustomerId + "");
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
    }
}
