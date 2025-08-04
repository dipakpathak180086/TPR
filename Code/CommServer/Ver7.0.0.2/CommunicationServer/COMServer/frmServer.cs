using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using BCILCommServer;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
//using UmsDLL;
using System.Net.NetworkInformation;
using System.Net;
//using NTF_COM_SERVER.Classes;
using SILCommServer;
using COMServer.Classes;
using TPR_App;

namespace COMServer
{
    public partial class frmServer : Form
    {
        #region local variables

        ContextMenu cMenu;
        NotifyIcon m_notifyicon;
        SILSocketServer oServer;
        bool isStart = false;
        public static string strCnn;
        static string strConfig = Application.StartupPath + "\\dbSetting.ini";
        static string strPort = Application.StartupPath + "\\Port.txt";
        static string IsRunning = Application.StartupPath + "\\IsRunning.txt";
        public string DataSource { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string InitialCatalog { get; set; }


        #endregion

        #region Constructor

        public frmServer()
        {
            try
            {
                InitializeComponent();
                defaultDisplay();
                disConnectUI();
                ReadSetting();
                //clsMain.SetLogger();
                lblVersion.Text = "Version: " + Application.ProductVersion.Trim();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region local methods
        void ReadSetting()
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\DBSettings.txt"))
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\DBSettings.txt");
                    this.DataSource = sr.ReadLine();
                    this.InitialCatalog = sr.ReadLine();
                    this.UserID = sr.ReadLine();
                    this.Password = sr.ReadLine();
                    clsMsgRule.mMainSqlConString = "data source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";User Id=" + UserID + ";Password=" + Password;
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Open Socket to communicate Client MC.
        /// </summary>
        private void connectServer()
        {
            //oServer = new BCILSocketServer(5150, 50);
            oServer = new SILSocketServer(clsMsgRule.sPort, 500);
            oServer.EOMChar = "}";
            oServer.SessionTimeOut = 5000;
            oServer.OnClientConnect += new SILSocketServer.NewClientHandler(oServer_OnClientConnect);
            LogFile oLog = oServer.ActiveLog;
            oLog.EnableLogFiles = true;
            oLog.ChangeInterval = LogFile.ChangeIntervals.ciDaily;
            oLog.LogLevel = EventNotice.EventTypes.evtAll;
            oLog.LogFilesPrefix = "SIL";
            string strLogPath = Application.StartupPath + "/Log";
            DirectoryInfo oDir = new DirectoryInfo(strLogPath);
            if (!oDir.Exists)
            {
                oDir.Create();
            }
            oLog.LogFilesPath = strLogPath;
            oLog.LogDays = 15;

            oServer.StartService();
            connectUI();
        }
        private void connectUI()
        {
            picConnect.BringToFront();
            picDisconnect.SendToBack();
            picViewConnection.Image = Properties.Resources.com_server;
            lblConnect.ForeColor = Color.Green;
            cmdConnect.Enabled = false;
            cmdDisconnect.Enabled = true;
            isStart = true;
        }
        /// <summary>
        /// To stop Socket Communication Service.
        /// </summary>
        private void disConnectServer()
        {
            //clsSetting.sCon = string.Empty;
            //clsSetting.sCon1 = string.Empty;
            oServer.StopService();
            oServer = null;
            disConnectUI();
        }
        private void disConnectUI()
        {
            picConnect.SendToBack();
            picDisconnect.BringToFront();
            lblConnect.Text = "Server Disconnected.";
            picViewConnection.Image = Properties.Resources.com_server_disconnect;
            lblConnect.ForeColor = Color.Red;
            cmdDisconnect.Enabled = false;
            cmdConnect.Enabled = true;
            isStart = false;
        }
        private void FillComboBox(ComboBox cbo, DataTable dt, bool isSelect)
        {
            if (isSelect)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "--Select--";
                dr[1] = "";
                dt.Rows.InsertAt(dr, 0);
            }
            cbo.DisplayMember = dt.Columns[0].ToString();
            cbo.ValueMember = dt.Columns[1].ToString();
            cbo.DataSource = dt;
        }
        private DataTable getDBServer()
        {
            DataTable dtDbServer = new DataTable();
            dtDbServer.Columns.Add("Display");
            dtDbServer.Columns.Add("Value");
            DataTable dtResults = SqlDataSourceEnumerator.Instance.GetDataSources();

            string strInstance;
            foreach (DataRow dr in dtResults.Rows)
            {
                if (dr["InstanceName"].ToString() != string.Empty)
                {
                    strInstance = "\\" + dr["InstanceName"].ToString();
                }
                else
                {
                    strInstance = string.Empty;
                }

                DataRow drRow = dtDbServer.NewRow();
                drRow["Display"] = dr["ServerName"].ToString() + strInstance;
                drRow["Value"] = dr["ServerName"].ToString() + strInstance;
                dtDbServer.Rows.Add(drRow);
            }

            return dtDbServer;
        }
        private DataTable getDBSchema(string strSource, string strUser, string strPwd)
        {
            try
            {
                DataTable dtSchema = new DataTable();
                dtSchema.Columns.Add("Display");
                dtSchema.Columns.Add("Value");
                string strCon = "Data Source=" + strSource + ";";
                strCon = strCon + " User ID=" + strUser + "; Password=" + strPwd + ";";

                SqlConnection oCon = new SqlConnection(strCon);
                oCon.Open();
                DataTable dtResults = oCon.GetSchema("Databases"); ;
                oCon.Close();
                foreach (DataRow dr in dtResults.Rows)
                {
                    DataRow drRow = dtSchema.NewRow();
                    drRow["Display"] = dr["database_name"].ToString();
                    drRow["Value"] = dr["database_name"].ToString();
                    dtSchema.Rows.Add(drRow);
                }
                return dtSchema;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        private void defaultDisplay()
        {
            pnlClients.SetBounds(39, 313, 10, 20);
            pnlLog.SetBounds(10, 313, 10, 20);
            pnlDBSeting.SetBounds(23, 313, 10, 20);
            pnlClients.Visible = false;
            pnlDBSeting.Visible = false;
            pnlLog.Visible = false;
        }
        private void setDBSettingPnl()
        {
            cmbServer.Text = string.Empty;
            cmbSchema.Text = string.Empty;
            txtUserID.Text = string.Empty;
            txtPwd.Text = string.Empty;
        }

        delegate void delegateAddClient(string data, string strFlag);

        delegate void delegateRemoveClient(string data);

        public void AddClient(string data, string strFlag)
        {
            if (strFlag == "LOG")
            {
                if (lvLog.InvokeRequired)
                {
                    lvLog.Invoke(new delegateAddClient(AddClient), data, strFlag);
                }
                else
                {
                    string[] dd = data.Split(';');
                    lvLog.Items.Add(dd[0]);
                    lvLog.Items[lvLog.Items.Count - 1].SubItems.Add(dd[1]);
                    lvLog.Items[lvLog.Items.Count - 1].SubItems.Add(System.DateTime.Now.ToString());
                }
            }
            else if (strFlag == "CLIENT")
            {
                if (lvClient.InvokeRequired)
                {
                    lvClient.Invoke(new delegateAddClient(AddClient), data, strFlag);
                }
                else
                {
                    string[] dd = data.Split(';');
                    lvClient.Items.Add(dd[0]);
                    lvClient.Items[lvClient.Items.Count - 1].SubItems.Add(System.DateTime.Now.ToString());
                }
            }
        }

        public void removeClient(string data)
        {
            if (lvClient.InvokeRequired)
            {
                lvClient.Invoke(new delegateRemoveClient(removeClient), data);
            }
            else
            {
                for (int i = 0; i < lvClient.Items.Count; i++)
                {
                    if (lvClient.Items[i].Text.Trim() == data)
                    {
                        lvClient.Items.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Form

        private void cmdExit_Click(object sender, EventArgs e)
        {
            try
            {
                oServer.StopService();
            }
            catch (Exception ex)
            {
            }
            Application.Exit();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(strPort))
                {
                    StreamReader oSrPort = new StreamReader(strPort);
                    while (!oSrPort.EndOfStream)
                    { clsMsgRule.sPort = Convert.ToInt32(oSrPort.ReadLine()); }
                }

                connectServer();

                lblConnect.Text = "Server Connected : Port : " + clsMsgRule.sPort;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                disConnectServer();
            }
            catch (Exception ex)
            {

            }
        }

        private void oServer_OnClientConnect(ClientHandler RemoteClient)
        {
            try
            {
                RemoteClient.OnDataArrival += new ClientHandler.DataArrivalHandler(RemoteClient_OnDataArrival);
            }
            catch (Exception ex)
            {

            }
        }

        private void RemoteClient_OnDataArrival(ClientHandler RemoteClient)
        {
            string Response = "";
            clsSecurity objSecurity = null;
            DataTable dt = new DataTable();
            try
            {
                AddClient(RemoteClient.ClientIP, "CLIENT");
                AddClient(RemoteClient.ClientIP + ";" + RemoteClient.Message, "LOG");
                //   string data = "RM_QC_VALIDATE_BARCODE~lkj~";
                string[] Data = RemoteClient.Message.Split('~');//data.Split('~'); //
                try
                {
                    switch (Data[0])
                    {
                        case "GET_APP_VERSION":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetAppVersion();
                            objSecurity = null;
                            break;

                        case "GET_NEWEXE_DESKTOP":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetNewExeDesktop();
                            objSecurity = null;
                            break;

                        case "GET_NEWEXE_DEVICE":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetNewExeDevice();
                            objSecurity = null;
                            break;

                        case "V_USER":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.ManageUser(new User { UserId = Data[1], Password = Data[2] });
                            objSecurity = null;
                            break;

                        case "GET_USER_RIGHT":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetUserRights(Data[1]);
                            objSecurity = null;
                            break;

                        case "GET_LINE":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GeLine();
                            objSecurity = null;
                            break;

                        case "GET_PICKING_FOR_MACHINING_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetPickingForMachiningData(Data[1]);
                            objSecurity = null;
                            break;

                        case "SAVE_PICKING_FOR_MACHINING_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.SavePickingForMachiningData(Data[1], Data[2]);
                            objSecurity = null;
                            break;

                        case "GET_MACHINING_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetMachiningData(Data[1]);
                            objSecurity = null;
                            break;

                        case "SAVE_MACHINING_DATA":
                            objSecurity = new clsSecurity();
                            List<Machining> _ListMachining = new List<Machining>();
                            //It will split multiple row data
                            string[] sArr = Data[1].Split('#');
                            for (int i = 0; i < sArr.Length; i++)
                            {
                                //it will split the properties
                                string[] sArrObj = sArr[i].Split('$');
                                //Add Value to List
                                _ListMachining.Add(new Machining
                                {
                                    LineNo = sArrObj[0],
                                    CuttingTrolleyCard = sArrObj[1],
                                    TotalQty = int.Parse(sArrObj[2]),
                                    OkQty = int.Parse(sArrObj[3]),
                                    NgQty = int.Parse(sArrObj[4]),
                                    CreatedBy = sArrObj[5],
                                    ModelNo = sArrObj[6],

                                    InnerCavity = int.Parse(sArrObj[7]),
                                    OuterCavity = int.Parse(sArrObj[8]),
                                    Slag = int.Parse(sArrObj[9]),
                                    Dent = int.Parse(sArrObj[10]),
                                    Spine = int.Parse(sArrObj[11]),
                                    ForgMat = int.Parse(sArrObj[12]),
                                    Rust = int.Parse(sArrObj[13]),
                                    PinHole = int.Parse(sArrObj[14]),
                                    MachineOutsideCavity = int.Parse(sArrObj[15]),
                                    Other = int.Parse(sArrObj[16]),
                                    IDPlus = int.Parse(sArrObj[17]),
                                    IDMinus = int.Parse(sArrObj[18]),
                                    PowerCut = int.Parse(sArrObj[19]),
                                    ExtraParam4 = int.Parse(sArrObj[20]),
                                    ExtraParam5 = int.Parse(sArrObj[21]),

                                    TotalLengthMinus = int.Parse(sArrObj[22]),
                                    TotalLengthPlus = int.Parse(sArrObj[23]),
                                    RCenterMinus = int.Parse(sArrObj[24]),
                                    RCenterPlus = int.Parse(sArrObj[25]),
                                    NoCleanUp = int.Parse(sArrObj[26]),
                                    Crack = int.Parse(sArrObj[27]),

                                    LotNo = sArrObj[28],
                                    LotNoDate = sArrObj[29],

                                    Status = Convert.ToInt32(EnumCuttingStatus.After_Machining),
                                    MachiningStatus = Convert.ToInt32(EnumMachiningStatus.Machining)
                                });
                            }

                            string TrolleyCard = _ListMachining[0].CuttingTrolleyCard;
                            int OkQty = _ListMachining.Sum(x => x.OkQty);

                            Response = objSecurity.SaveMachiningMixedData(TrolleyCard, OkQty, _ListMachining, Data[2], Data[3], Data[4]);
                            objSecurity = null;

                            break;

                        case "SAVE_MACHINING_DATA_MIXED":
                            objSecurity = new clsSecurity();
                            List<Machining> _ListMixMachining = new List<Machining>();
                            //It will split multiple row data
                            string[] sArrMix = Data[1].Split('#');
                            for (int i = 0; i < sArrMix.Length; i++)
                            {
                                //it will split the properties
                                string[] sArrObj = sArrMix[i].Split('$');
                                //Add Value to List
                                _ListMixMachining.Add(new Machining
                                {
                                    LineNo = sArrObj[0],
                                    CuttingTrolleyCard = sArrObj[1],
                                    TotalQty = int.Parse(sArrObj[2]),
                                    OkQty = int.Parse(sArrObj[3]),
                                    NgQty = int.Parse(sArrObj[4]),
                                    CreatedBy = sArrObj[5],
                                    ModelNo = sArrObj[6],

                                    InnerCavity = int.Parse(sArrObj[7]),
                                    OuterCavity = int.Parse(sArrObj[8]),
                                    Slag = int.Parse(sArrObj[9]),
                                    Dent = int.Parse(sArrObj[10]),
                                    Spine = int.Parse(sArrObj[11]),
                                    ForgMat = int.Parse(sArrObj[12]),
                                    Rust = int.Parse(sArrObj[13]),
                                    PinHole = int.Parse(sArrObj[14]),
                                    MachineOutsideCavity = int.Parse(sArrObj[15]),
                                    Other = int.Parse(sArrObj[16]),
                                    IDPlus = int.Parse(sArrObj[17]),
                                    IDMinus = int.Parse(sArrObj[18]),
                                    PowerCut = int.Parse(sArrObj[19]),
                                    ExtraParam4 = int.Parse(sArrObj[20]),
                                    ExtraParam5 = int.Parse(sArrObj[21]),

                                    TotalLengthMinus = int.Parse(sArrObj[22]),
                                    TotalLengthPlus = int.Parse(sArrObj[23]),
                                    RCenterMinus = int.Parse(sArrObj[24]),
                                    RCenterPlus = int.Parse(sArrObj[25]),
                                    NoCleanUp = int.Parse(sArrObj[26]),
                                    Crack = int.Parse(sArrObj[27]),

                                    LotNo = sArrObj[28],
                                    LotNoDate = sArrObj[29],


                                    Status = Convert.ToInt32(EnumCuttingStatus.After_Machining),
                                    MachiningStatus = Convert.ToInt32(EnumMachiningStatus.Machining)
                                });
                            }

                            string TrolleyCardMix = _ListMixMachining[0].CuttingTrolleyCard;
                            List<string> _cuttingTrolleyCard = new List<string>();
                            _cuttingTrolleyCard.Add(TrolleyCardMix);
                            for (int i = 1; i < _ListMixMachining.Count; i++)
                            {
                                if (!_cuttingTrolleyCard.Contains(_ListMixMachining[i].CuttingTrolleyCard))
                                {
                                    _cuttingTrolleyCard.Add(_ListMixMachining[i].CuttingTrolleyCard);
                                    string[] ArrLot = _ListMixMachining[i].CuttingTrolleyCard.Split('-');
                                    for (int j = 1; j < ArrLot.Length; j++)
                                    {
                                        TrolleyCardMix += "-" + ArrLot[j].Substring(ArrLot[j].Length - 4, 4);
                                    }
                                }
                            }

                            int OkQtyMix = _ListMixMachining.Sum(x => x.OkQty);

                            Response = objSecurity.SaveMachiningMixedData(TrolleyCardMix, OkQtyMix, _ListMixMachining, Data[2], Data[3], Data[4]);
                            objSecurity = null;
                            break;

                        case "GET_MODEL_FOR_MACHINING_SPLIT":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetModelForMachiningSplit();
                            objSecurity = null;
                            break;

                        case "GET_MACHINING_SPLIT_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetMachiningSplitData(Data[1]);
                            objSecurity = null;
                            break;

                        case "SAVE_MACHINING_SPLIT_DATA":
                            objSecurity = new clsSecurity();
                            List<Machining> _ListMachiningSplit = new List<Machining>();
                            //It will split multiple row data
                            string[] sArrSplit = Data[1].Split('#');
                            for (int i = 0; i < sArrSplit.Length; i++)
                            {
                                //it will split the properties
                                string[] sArrObj = sArrSplit[i].Split('$');
                                //Add Value to List
                                _ListMachiningSplit.Add(new Machining
                                {
                                    LineNo = sArrObj[0],
                                    CuttingTrolleyCard = sArrObj[1],
                                    TotalQty = int.Parse(sArrObj[2]),
                                    OkQty = int.Parse(sArrObj[3]),
                                    NgQty = int.Parse(sArrObj[4]),
                                    CreatedBy = sArrObj[5],
                                    ModelNo = sArrObj[6],

                                    InnerCavity = int.Parse(sArrObj[7]),
                                    OuterCavity = int.Parse(sArrObj[8]),
                                    Slag = int.Parse(sArrObj[9]),
                                    Dent = int.Parse(sArrObj[10]),
                                    Spine = int.Parse(sArrObj[11]),
                                    ForgMat = int.Parse(sArrObj[12]),
                                    Rust = int.Parse(sArrObj[13]),
                                    PinHole = int.Parse(sArrObj[14]),
                                    MachineOutsideCavity = int.Parse(sArrObj[15]),
                                    Other = int.Parse(sArrObj[16]),
                                    IDPlus = int.Parse(sArrObj[17]),
                                    IDMinus = int.Parse(sArrObj[18]),
                                    PowerCut = int.Parse(sArrObj[19]),
                                    ExtraParam4 = int.Parse(sArrObj[20]),
                                    ExtraParam5 = int.Parse(sArrObj[21]),

                                    TotalLengthMinus = int.Parse(sArrObj[22]),
                                    TotalLengthPlus = int.Parse(sArrObj[23]),
                                    RCenterMinus = int.Parse(sArrObj[24]),
                                    RCenterPlus = int.Parse(sArrObj[25]),
                                    NoCleanUp = int.Parse(sArrObj[26]),
                                    Crack = int.Parse(sArrObj[27]),

                                    LotNo = sArrObj[28],
                                    LotNoDate = sArrObj[29],


                                    Status = Convert.ToInt32(EnumCuttingStatus.After_Machining),
                                    MachiningStatus = Convert.ToInt32(EnumMachiningStatus.Machining)
                                });
                            }

                            string NewTrolleyCard = _ListMachiningSplit[0].ModelNo + "-" + _ListMachiningSplit[0].LotNo;
                            //If more than one lot no then add other lot no last 4 digit
                            if (_ListMachiningSplit.Count > 1)
                            {
                                for (int k = 1; k < _ListMachiningSplit.Count; k++)
                                {
                                    string SplitLotNo = _ListMachiningSplit[k].LotNo;
                                    NewTrolleyCard += "-" + SplitLotNo.Substring(SplitLotNo.Length - 4, 4);
                                }
                            }
                            int NewOkQty = _ListMachiningSplit.Sum(x => x.OkQty);

                            Response = objSecurity.SaveMachiningSplitMixedData(NewTrolleyCard, NewOkQty, _ListMachiningSplit, Data[2], Data[3], Data[4]);
                            objSecurity = null;

                            break;

                        case "REPRINT_MACHINING":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.ReprintMachiningTrolleyCard(Data[1], Data[2], Data[3]);
                            objSecurity = null;
                            break;

                        case "GET_REOILING_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetReOilingData(Data[1]);
                            objSecurity = null;
                            break;

                        case "SAVE_REOILING_DATA":
                            objSecurity = new clsSecurity();

                            List<ReOiling> _ListReOiling = new List<ReOiling>();
                            //It will split multiple row data
                            string[] sArrReOil = Data[1].Split('#');
                            for (int i = 0; i < sArrReOil.Length; i++)
                            {
                                //it will split the properties
                                string[] sArrObj = sArrReOil[i].Split('$');
                                //Add Value to List
                                _ListReOiling.Add(new ReOiling
                                {
                                    MachiningTrolleyCard = sArrObj[0],
                                    TotalQty = int.Parse(sArrObj[1]),
                                    OkQty = int.Parse(sArrObj[2]),
                                    NgQty = int.Parse(sArrObj[3]),
                                    CreatedBy = sArrObj[4],
                                    ModelNo = sArrObj[5],

                                    InnerCavity = int.Parse(sArrObj[6]),
                                    OuterCavity = int.Parse(sArrObj[7]),
                                    Slag = int.Parse(sArrObj[8]),
                                    Dent = int.Parse(sArrObj[9]),
                                    Spine = int.Parse(sArrObj[10]),
                                    RONg = int.Parse(sArrObj[11]),
                                    Other = int.Parse(sArrObj[12]),
                                    ExtraParam1 = int.Parse(sArrObj[13]),
                                    ExtraParam2 = int.Parse(sArrObj[14]),
                                    ExtraParam3 = int.Parse(sArrObj[15]),
                                    ExtraParam4 = int.Parse(sArrObj[16]),
                                    ExtraParam5 = int.Parse(sArrObj[17]),
                                    LotNo = sArrObj[18],
                                    LotNoDate = sArrObj[19],

                                });
                            }


                            Response = objSecurity.SaveReOilingData(_ListReOiling);
                            objSecurity = null;
                            break;

                        case "GET_FINALPACKING_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetCustomerTrolley(Data[1]);
                            objSecurity = null;
                            break;

                        case "GET_FINALPACKING_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetFinalPackingData(Data[1], Data[2]);
                            objSecurity = null;
                            break;

                        case "SAVE_FINALPACKING_DATA":
                            objSecurity = new clsSecurity();
                            FinalPacking finalPacking = new FinalPacking();

                            finalPacking.MachiningTrolleyCard = Data[1];
                            finalPacking.TotalQty = int.Parse(Data[2]);
                            finalPacking.OkQty = int.Parse(Data[3]);
                            finalPacking.NgQty = int.Parse(Data[4]);
                            finalPacking.TrolleyNo = Data[5];

                            finalPacking.InnerCavity = int.Parse(Data[6]);
                            finalPacking.OuterCavity = int.Parse(Data[7]);
                            finalPacking.Slag = int.Parse(Data[8]);
                            finalPacking.Dent = int.Parse(Data[9]);
                            finalPacking.Spine = int.Parse(Data[10]);
                            finalPacking.PackNg = int.Parse(Data[11]);
                            finalPacking.Other = int.Parse(Data[12]);
                            finalPacking.Rust = int.Parse(Data[13]);
                            finalPacking.ExtraParam2 = int.Parse(Data[14]);
                            finalPacking.ExtraParam3 = int.Parse(Data[15]);
                            finalPacking.ExtraParam4 = int.Parse(Data[16]);
                            finalPacking.ExtraParam5 = int.Parse(Data[17]);
                            finalPacking.CreatedBy = Data[18];
                            finalPacking.ModelNo = Data[19];
                            finalPacking.LotNo = Data[20];
                            finalPacking.LotNoDate = Data[21];
                            finalPacking.TrolleyPackSize = int.Parse(Data[22]);
                            if (Data.Length > 23)
                            {
                                finalPacking.Color = Data[23];
                            }

                            Response = objSecurity.SaveFinalPackingData(finalPacking);
                            objSecurity = null;
                            break;

                        #region Trolley Receiving Created by Dipak 12 July 2019
                        case "GET_COLOR":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetColorName();
                            objSecurity = null;
                            break;
                        case "VALID_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.ValidateTrolley(Data[1]);
                            objSecurity = null;
                            break;
                        case "SAVE_TROLLEY_REC":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.SaveTrolleyRec(Data[1], Data[2], Data[3], Data[4]);
                            objSecurity = null;
                            break;
                        #endregion

                        #region Trolley Dispatch Created by dipak 16July2019

                        case "GET_DIS_DATA":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetDispatchOrderData(Data[1]);
                            objSecurity = null;
                            break;
                        case "DIS_VALID_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetTrolleyQty(Data[1], Data[2]);
                            objSecurity = null;
                            break;
                        case "SAVE_TROLLEY_DIS":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.SaveDispatchData(Data[1], Data[2], Data[3], Data[4], Data[5]);
                            objSecurity = null;
                            break;
                        #endregion

                        case "GET_MODEL_FOR_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetModelForTrolley();
                            objSecurity = null;
                            break;
                        case "SAVE_TROLLEY_FOR_PRINT":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.SaveTrolleyForPrint(Data[1], Data[2], Data[3], Data[4]);
                            objSecurity = null;
                            break;

                        case "GET_TROLLEY_DATA_FOR_PRINT":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.GetTrolleyData(Data[1]);
                            objSecurity = null;
                            break;

                        case "PRINT_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.PrintTrolley(Data[1]);
                            objSecurity = null;
                            break;
                        #region Exchange Trolley --Added by dipak 17_08_21 for pc option into HHT
                        case "EXCHANGE_TROLLEY":
                            objSecurity = new clsSecurity();
                            Response = objSecurity.ExcangeTrolley(Data[1], Data[2], Data[3], Data[4]);
                            objSecurity = null;
                            break;
                        #endregion
                        #region IN_OUT_WARD --Added by dipak 07_07_25 for HHT IN_OUT_WARD Operation
                        case "IN_OUT_WARD":
                            objSecurity = new clsSecurity();
                            InOutWard objPl = new InOutWard();
                            if (Data.Length == 6)
                            {
                                objPl.DbType = Data[1];
                                objPl.Process = Data[2];
                                objPl.WarehouseCode = Data[3];
                                objPl.ModelNo = Data[4];
                                objPl.Trip = Data[5];
                            }

                            else if (Data.Length >6)
                            {
                                objPl.DbType = Data[1];
                                objPl.Process = Data[2];
                                objPl.WarehouseCode = Data[3];
                                objPl.ModelNo = Data[4];
                                objPl.Trip = Data[5];
                                objPl.Barcode = Data[6];
                                objPl.InStatus = Convert.IsDBNull(Data[7]) ? (bool?)null : Convert.ToBoolean(Data[7]);
                                objPl.OutStatus = Convert.IsDBNull(Data[8]) ? (bool?)null : Convert.ToBoolean(Data[8]);
                                objPl.CreatedBy = Data[9];
                            }
                            else
                            {
                                objPl.DbType = Data[1];
                            }
                            Response = objSecurity.In_Out_Ward(objPl);
                            objSecurity = null;
                            break;
                        #endregion
                        default:
                            break;
                    }
                }

                catch (Exception ex)
                { Response = "ERROR" + "~" + ex.Message; }
                AddClient(RemoteClient.ClientIP + ";RESPONSE~", "LOG");
                removeClient(RemoteClient.ClientIP);
                RemoteClient.Response = Response;
            }
            catch (Exception ex1)
            { Response = "ERROR" + "~" + ex1.Message; }
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            try
            {
                cMenu = new System.Windows.Forms.ContextMenu();
                cMenu.MenuItems.Add(0, new MenuItem("Show", new EventHandler(this.Show_Click)));
                cMenu.MenuItems.Add(1, new MenuItem("Close", new EventHandler(this.cmdExit_Click)));
                m_notifyicon = new NotifyIcon();
                m_notifyicon.Text = "Right click for context menu";
                m_notifyicon.Visible = true;
                m_notifyicon.Icon = new Icon(Application.StartupPath + "\\Network.ico");
                m_notifyicon.ContextMenu = cMenu;
                ReadPrinter();
                //Connect button auto click
                cmdConnect_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReadPrinter()
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\PrinterSetting.txt"))
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\PrinterSetting.txt");
                    Program.MachiningPrinterIP = sr.ReadLine().Split('=')[1].Trim();
                    Program.MachiningPrinterPort = sr.ReadLine().Split('=')[1].Trim();
                    Program.FinalPackingPrinterIP = sr.ReadLine().Split('=')[1].Trim();
                    Program.FinalPackingPrinterPort = sr.ReadLine().Split('=')[1].Trim();
                    Program.PrinterName = sr.ReadLine().Split('=')[1].Trim();
                    sr.Close();
                }
                else
                    MessageBox.Show("Printer setting file not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Show_Click(Object sender, EventArgs e)
        {
            this.Show();
        }

        private void cmdHide_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region DBSetting Panel

        private void cmdDbServer_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                setDBSettingPnl();
                pnlDBSeting.Visible = true;
                pnlDBSeting.SetBounds(0, 0, 382, 341);
                pnlDBSeting.Dock = DockStyle.Fill;
                // pnlImage.SendToBack();
                pnlDBSeting.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;

                if (File.Exists(strConfig))
                {
                    using (StreamReader oSr = new StreamReader(strConfig))
                    {
                        //while (!oSr.EndOfStream)
                        //{
                        txtConString.Text = oSr.ReadLine();
                        txtPrinterName.Text = oSr.ReadLine();
                        oSr.Close();
                    }
                    // }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void cmbServer_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cmbServer.Items.Count == 0)
                {
                    FillComboBox(cmbServer, getDBServer(), true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sever Not Found!");
            }
        }

        private void cmbSchema_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cmbSchema.Items.Count == 0)
                {
                    FillComboBox(cmbSchema, getDBSchema(cmbServer.Text.ToString(), txtUserID.Text.Trim(), txtPwd.Text.Trim()), true);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdTestCon_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbServer.Text.Trim() != string.Empty)
                {
                    string strCon = "Data Source=" + cmbServer.Text.Trim() + ";Initial Catalog=" + cmbSchema.SelectedValue.ToString() + ";";
                    strCon = strCon + " User ID=" + txtUserID.Text.Trim() + ";";
                    txtConString.Text = strCon + "Password = ******;";
                    strCon = strCon + "Password = " + txtPwd.Text.Trim() + ";";
                    SqlConnection oCon = new SqlConnection(strCon);
                    oCon.Open();
                    oCon.Close();
                    MessageBox.Show("Test Connection Sucessfully!");
                }
            }
            catch (SqlException oSql)
            {
                MessageBox.Show(oSql.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strCon = "Data Source=" + cmbServer.Text.Trim() + ";Initial Catalog=" + cmbSchema.SelectedValue.ToString() + ";";
                strCon = strCon + " User ID=" + txtUserID.Text.Trim() + ";";
                txtConString.Text = strCon + "Password = ******;";
                strCnn = strCon + "Password = " + txtPwd.Text.Trim() + ";";
                SqlConnection oCon = new SqlConnection(strCnn);
                oCon.Open();
                oCon.Close();
                FileInfo oFile = new FileInfo(strConfig);
                if (txtConString.Text == "")
                {
                    MessageBox.Show("Please configure database setting.");
                }
                else
                {
                    using (StreamWriter oSw = new StreamWriter(strConfig))
                    {
                        //if (!oFile.Exists)
                        //{ oSw = new StreamWriter(strConfig); }
                        //else
                        //{ oSw = new StreamWriter(strConfig1); }
                        oSw.WriteLine(strCnn);
                        oSw.WriteLine(txtPrinterName.Text.Trim());
                        oSw.Close();
                        setDBSettingPnl();
                        MessageBox.Show("Operation Successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();

                if (isStart)
                {
                    cmdDisconnect.Enabled = true;
                }
                else
                {
                    cmdConnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region View Clients Panel

        private void cmdClients_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlClients.Visible = true;
                pnlClients.SetBounds(0, 0, 382, 341);
                pnlClients.Dock = DockStyle.Fill;
                // pnlImage.SendToBack();
                pnlClients.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdClientBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();

                if (isStart)
                {
                    cmdDisconnect.Enabled = true;
                }
                else
                {
                    cmdConnect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region View Logs Panel

        private void cmdLog_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlLog.Visible = true;
                pnlLog.SetBounds(0, 0, 382, 341);
                pnlLog.BringToFront();
                pnlLog.Dock = DockStyle.Fill;

                cmdBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                // pnlImage.SendToBack();
                pnlLog.BringToFront();
                cmdConnect.Enabled = false;
                cmdDisconnect.Enabled = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void cmdLogBack_Click(object sender, EventArgs e)
        {
            try
            {
                defaultDisplay();
                pnlImage.BringToFront();
                if (isStart)
                    cmdDisconnect.Enabled = true;
                else
                    cmdConnect.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string Mesg = "SAVE_MACHINING_SPLIT_DATA~3$HSPP-Z4M/Z4V-14101910101$150$100$2$1$Z4V$1$1$0$0$0$0$0$0$0$0$0$0$14101910101$14-Oct-2019~as~fg";
                string[] Data = Mesg.Split('~');
                clsSecurity objSecurity = new clsSecurity();
                List<Machining> _ListMachiningSplit = new List<Machining>();
                //It will split multiple row data
                string[] sArrSplit = Data[1].Split('#');
                for (int i = 0; i < sArrSplit.Length; i++)
                {
                    //it will split the properties
                    string[] sArrObj = sArrSplit[i].Split('$');
                    //Add Value to List
                    _ListMachiningSplit.Add(new Machining
                    {
                        LineNo = sArrObj[0],
                        CuttingTrolleyCard = sArrObj[1],
                        TotalQty = int.Parse(sArrObj[2]),
                        OkQty = int.Parse(sArrObj[3]),
                        NgQty = int.Parse(sArrObj[4]),
                        CreatedBy = sArrObj[5],
                        ModelNo = sArrObj[6],

                        InnerCavity = int.Parse(sArrObj[7]),
                        OuterCavity = int.Parse(sArrObj[8]),
                        Slag = int.Parse(sArrObj[9]),
                        Dent = int.Parse(sArrObj[10]),
                        Spine = int.Parse(sArrObj[11]),
                        ForgMat = int.Parse(sArrObj[12]),
                        Other = int.Parse(sArrObj[13]),
                        IDPlus = int.Parse(sArrObj[14]),
                        IDMinus = int.Parse(sArrObj[15]),
                        PowerCut = int.Parse(sArrObj[16]),
                        ExtraParam4 = int.Parse(sArrObj[17]),
                        ExtraParam5 = int.Parse(sArrObj[18]),
                        LotNo = sArrObj[19],
                        LotNoDate = sArrObj[20],

                        Status = Convert.ToInt32(EnumCuttingStatus.After_Machining),
                        MachiningStatus = Convert.ToInt32(EnumMachiningStatus.Machining)
                    });
                }

                string NewTrolleyCard = _ListMachiningSplit[0].ModelNo + "-" + _ListMachiningSplit[0].LotNo;
                int NewOkQty = _ListMachiningSplit.Sum(x => x.OkQty);

                string Response = objSecurity.SaveMachiningSplitMixedData(NewTrolleyCard, NewOkQty, _ListMachiningSplit, Data[2], Data[3], Data[4]);
                objSecurity = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }
}
