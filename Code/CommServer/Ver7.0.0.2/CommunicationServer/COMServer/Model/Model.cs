using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPR_App
{
    public enum EnumDbType { SELECT, INSERT, UPDATE, DELETE, SELECTBYID, SEARCH, VALIDATEUSER };
    public enum EnumCuttingStatus { Cutting = 1, QC_Ok = 2, QC_Ng = 3, QC_Hold = 4, QC_Sample = 5, Picking = 6, After_Machining = 7 };
    public enum EnumTrolleyStatus { Receive = 0, Dispatch = 1};
    public enum EnumMachiningStatus { Machining = 1, FinalPacking = 2 };
    public enum EnumFinalPackingStatus { FinalPacking = 1, Dispatch = 2 };

    #region User Master
    public class User : Common
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Group { get; set; }
    }
    #endregion

    #region Machining
    public class Machining : Common
    {
        public string ModelNo { get; set; }
        public string LotNo { get; set; }
        public string LotNoDate { get; set; }
        public string LineNo { get; set; }
        public string CuttingTrolleyCard { get; set; }
        public int TotalQty { get; set; }
        public int OkQty { get; set; }
        public int NgQty { get; set; }
        public int DefectQty { get; set; }
        public string TrolleyNo { get; set; }
        public string TrolleyCard { get; set; }
        public int Status { get; set; }
        public int TrolleyStatus { get; set; }
        public int MachiningStatus { get; set; }

        //Defect
        public int InnerCavity { get; set; }
        public int OuterCavity { get; set; }
        public int Slag { get; set; }
        public int Dent { get; set; }
        public int Spine { get; set; }
        public int ForgMat { get; set; }
        public int Rust { get; set; }
        public int PinHole { get; set; }
        public int MachineOutsideCavity { get; set; }
        public int Other { get; set; }
        public int IDPlus { get; set; }
        public int IDMinus { get; set; }
        public int PowerCut { get; set; }
        public int ExtraParam4 { get; set; }
        public int ExtraParam5 { get; set; }

        public int TotalLengthMinus { get; set; }
        public int TotalLengthPlus { get; set; }
        public int RCenterMinus { get; set; }
        public int RCenterPlus { get; set; }
        public int NoCleanUp { get; set; }
        public int Crack { get; set; }
    }

    #endregion

    #region Final Packing
    public class FinalPacking
    {
        public string Shift { get; set; }
        public string ModelNo { get; set; }
        public string TrolleyNo { get; set; }
        public string LotNo { get; set; }
        public string LotNoDate { get; set; }
        public string MachiningTrolleyCard { get; set; }
        public int TotalQty { get; set; }
        public int OkQty { get; set; }
        public int NgQty { get; set; }
        public int DefectQty { get; set; }
        public int TrolleyPackSize { get; set; }
        public int MachiningStatus { get; set; }
        public int Status { get; set; }
        public string Color { get; set; }
        public string CreatedBy { get; set; }

        //Defect
        public int InnerCavity { get; set; }
        public int OuterCavity { get; set; }
        public int Slag { get; set; }
        public int Dent { get; set; }
        public int Spine { get; set; }
        public int PackNg { get; set; }
        public int Other { get; set; }
        public int Rust { get; set; }
        public int ExtraParam2 { get; set; }
        public int ExtraParam3 { get; set; }
        public int ExtraParam4 { get; set; }
        public int ExtraParam5 { get; set; }
    }

    #endregion

    #region ReOiling
    public class ReOiling
    {
        public string ModelNo { get; set; }
        public string LotNo { get; set; }
        public string LotNoDate { get; set; }
        public string MachiningTrolleyCard { get; set; }
        public int TotalQty { get; set; }
        public int OkQty { get; set; }
        public int NgQty { get; set; }
        public int DefectQty { get; set; }
        public int MachiningStatus { get; set; }
        public string CreatedBy { get; set; }

        //Defect
        public int InnerCavity { get; set; }
        public int OuterCavity { get; set; }
        public int Slag { get; set; }
        public int Dent { get; set; }
        public int Spine { get; set; }
        public int RONg { get; set; }
        public int Other { get; set; }
        public int ExtraParam1 { get; set; }
        public int ExtraParam2 { get; set; }
        public int ExtraParam3 { get; set; }
        public int ExtraParam4 { get; set; }
        public int ExtraParam5 { get; set; }
    }

    #endregion

    #region IN_OUT_WARD

    public class InOutWard
    {
        public string DbType { get; set; }
        public string Process { get; set; }
        public string WarehouseCode { get; set; }
        public string ModelNo { get; set; }
        public string Barcode { get; set; }
        public string Trip { get; set; }
        public bool? InStatus { get; set; }
        public bool? OutStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    #endregion
}
