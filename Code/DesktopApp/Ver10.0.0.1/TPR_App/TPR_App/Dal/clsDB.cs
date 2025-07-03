using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Data.Common;
using System.Collections;
using TPR_App;

public class clsDB
{
    SqlConnection conn;
    SqlTransaction tran;
    SqlCommand cmd;

    #region DataBase Connection Property
    private enum SqlConnectionOwnership
    {
        Internal,
        External,
    }
    // public string ConnString { get; set; }
    private string m_DataSource;
    public string DataSource
    {
        get { return m_DataSource; }
        set { m_DataSource = value; }
    }
    private string m_UserID;
    public string UserID
    {
        get { return m_UserID; }
        set { m_UserID = value; }
    }
    private string m_Password;
    public string Password
    {
        get { return m_Password; }
        set { m_Password = value; }
    }
    private string m_InitialCatalog;
    public string InitialCatalog
    {
        get { return m_InitialCatalog; }
        set { m_InitialCatalog = value; }
    }

    private bool m_IsConnected;
    public bool IsConnected
    {
        get { return m_IsConnected; }
        set { m_IsConnected = value; }
    }
    #endregion

    void ReadSetting()
    {
        try
        {
            StreamReader sr = new StreamReader(Application.StartupPath + "\\DBSettings.txt");
            this.DataSource = sr.ReadLine();
            this.InitialCatalog = sr.ReadLine();
            this.UserID = sr.ReadLine();
            this.Password = sr.ReadLine();
            sr.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public clsDB()
    {
        try
        {
            conn = new SqlConnection();
            ReadSetting();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Connect To DataBase
    /// </summary>
    public void Connect()
    {
        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                // conn.ConnectionString = ConnString;
                conn.ConnectionString = "data source=" + DataSource + ";Initial Catalog=" + InitialCatalog + ";User Id=" + UserID + ";Password=" + Password;
                ClsGlobal.mMainSqlConString = conn.ConnectionString;
                conn.Open();
                cmd = new SqlCommand();
                cmd = conn.CreateCommand();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// DisConnect From DataBase
    /// </summary>
    public void DisConnect()
    {
        try
        {
            if (conn.State == ConnectionState.Open && conn != null)
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Return Data If transaction start then within transaction if tran is not start then without transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query</param>
    /// <returns>DataSet</returns>
    public DataSet GetDataSet(string sQry) //can be called in transaction
    {
        try
        {
            cmd.CommandText = sQry;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Dispose();
            da.Dispose();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Return Data If transaction start then within transaction if tran is not start then without transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query</param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(string sQry) //this method can be called in transaction also
    {
        try
        {
            cmd.CommandText = sQry;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Dispose();
            da.Dispose();
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Insert,Update,Delete Data Wihin or Without Transaction
    /// </summary>
    /// <param name="sQry">Pass Sql Query For Inserion,Updation,Deletion</param>
    /// <returns>Affected No Of Rows</returns>
    public int ExecuteNonQuery(string sQry)
    {
        try
        {
            int iCount = 0;
            cmd.CommandText = sQry;
            iCount = cmd.ExecuteNonQuery();
            return iCount;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public object ExecuteScalar(string sQry)
    {
        try
        {
            cmd.CommandText = sQry;
            var vObj = cmd.ExecuteScalar();
            return vObj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

    private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        if (commandParameters == null)
            return;
        foreach (SqlParameter commandParameter in commandParameters)
        {
            if (commandParameter != null)
            {
                if ((commandParameter.Direction == ParameterDirection.InputOutput || commandParameter.Direction == ParameterDirection.Input) && commandParameter.Value == null)
                    commandParameter.Value = (object)DBNull.Value;
                command.Parameters.Add(commandParameter);
            }
        }
    }

    private void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
    {
        if (commandParameters == null || dataRow == null)
            return;
        int num = 0;
        foreach (SqlParameter commandParameter in commandParameters)
        {
            if (commandParameter.ParameterName == null || commandParameter.ParameterName.Length <= 1)
                throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", (object)num, (object)commandParameter.ParameterName));
            if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
            ++num;
        }
    }

    private void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
    {
        if (commandParameters == null || parameterValues == null)
            return;
        if (commandParameters.Length != parameterValues.Length)
            throw new ArgumentException("Parameter count does not match Parameter Value count.");
        int index = 0;
        for (int length = commandParameters.Length; index < length; ++index)
        {
            if (parameterValues[index] is IDbDataParameter)
            {
                IDbDataParameter parameterValue = (IDbDataParameter)parameterValues[index];
                if (parameterValue.Value == null)
                    commandParameters[index].Value = (object)DBNull.Value;
                else
                    commandParameters[index].Value = parameterValue.Value;
            }
            else if (parameterValues[index] == null)
                commandParameters[index].Value = (object)DBNull.Value;
            else
                commandParameters[index].Value = parameterValues[index];
        }
    }

    private void PrepareCommand(
      SqlCommand command,
      SqlConnection connection,
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      SqlParameter[] commandParameters,
      ref bool mustCloseConnection)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        if (commandText == null || commandText.Length == 0)
            throw new ArgumentNullException(nameof(commandText));
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
            mustCloseConnection = false;
        command.Connection = connection;
        command.CommandText = commandText;
        if (transaction != null)
        {
            if (transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
            command.Transaction = transaction;
        }
        command.CommandType = commandType;
        if (commandParameters == null)
            return;
        this.AttachParameters(command, commandParameters);
    }

    public int ExecuteNonQuery(
      string connectionString,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public int ExecuteNonQuery(
      string connectionString,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return this.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }
    }

    public int ExecuteNonQuery(
      string connectionString,
      string spName,
      params object[] parameterValues)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public int ExecuteNonQuery(
      SqlConnection connection,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
    }

    public int ExecuteNonQuery(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        SqlCommand command = new SqlCommand();
        command.CommandTimeout = 5000;
        bool mustCloseConnection = false;
        this.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);
        int num = command.ExecuteNonQuery();
        command.Parameters.Clear();
        if (mustCloseConnection)
            connection.Close();
        return num;
    }

    public int ExecuteNonQuery(
      SqlConnection connection,
      string spName,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public int ExecuteNonQuery(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public int ExecuteNonQuery(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        SqlCommand command = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
        int num = command.ExecuteNonQuery();
        command.Parameters.Clear();
        return num;
    }

    public int ExecuteNonQuery(
      SqlTransaction transaction,
      string spName,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDataset(
      string connectionString,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public DataSet ExecuteDataset(
      string connectionString,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return this.ExecuteDataset(connection, commandType, commandText, commandParameters);
        }
    }

    public DataSet ExecuteDataset(
      string connectionString,
      string spName,
      params object[] parameterValues)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDataset(
      SqlConnection connection,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
    }

    public DataSet ExecuteDataset(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.CommandTimeout = 5000;
        bool mustCloseConnection = false;
        this.PrepareCommand(sqlCommand, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
        {
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            sqlCommand.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return dataSet;
        }
    }

    public DataSet ExecuteDataset(
      SqlConnection connection,
      string spName,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDataset(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteDataset(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public DataSet ExecuteDataset(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        SqlCommand sqlCommand = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
        {
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            sqlCommand.Parameters.Clear();
            return dataSet;
        }
    }

    public DataSet ExecuteDataset(
      SqlTransaction transaction,
      string spName,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    private SqlDataReader ExecuteReader(
      SqlConnection connection,
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      SqlParameter[] commandParameters,
      SqlConnectionOwnership connectionOwnership)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        bool mustCloseConnection = false;
        SqlCommand command = new SqlCommand();
        try
        {
            this.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
            SqlDataReader sqlDataReader = connectionOwnership != SqlConnectionOwnership.External ? command.ExecuteReader(CommandBehavior.CloseConnection) : command.ExecuteReader();
            bool flag = true;
            foreach (DbParameter parameter in (DbParameterCollection)command.Parameters)
            {
                if (parameter.Direction != ParameterDirection.Input)
                    flag = false;
            }
            if (flag)
                command.Parameters.Clear();
            return sqlDataReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public SqlDataReader ExecuteReader(
      string connectionString,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteReader(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public SqlDataReader ExecuteReader(
      string connectionString,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        SqlConnection connection = (SqlConnection)null;
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            return this.ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
        }
        catch
        {
            connection?.Close();
            throw;
        }
    }

    public SqlDataReader ExecuteReader(
      string connectionString,
      string spName,
      params object[] parameterValues)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public SqlDataReader ExecuteReader(
      SqlConnection connection,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteReader(connection, commandType, commandText, (SqlParameter[])null);
    }

    public SqlDataReader ExecuteReader(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        return this.ExecuteReader(connection, (SqlTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
    }

    public SqlDataReader ExecuteReader(
      SqlConnection connection,
      string spName,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteReader(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public SqlDataReader ExecuteReader(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteReader(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public SqlDataReader ExecuteReader(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        return this.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
    }

    public SqlDataReader ExecuteReader(
      SqlTransaction transaction,
      string spName,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalar(
      string connectionString,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
    }

    public object ExecuteScalar(
      string connectionString,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return this.ExecuteScalar(connection, commandType, commandText, commandParameters);
        }
    }

    public object ExecuteScalar(
      string connectionString,
      string spName,
      params object[] parameterValues)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalar(
      SqlConnection connection,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
    }

    public object ExecuteScalar(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        SqlCommand command = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);
        object obj = command.ExecuteScalar();
        command.Parameters.Clear();
        if (mustCloseConnection)
            connection.Close();
        return obj;
    }

    public object ExecuteScalar(
      SqlConnection connection,
      string spName,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalar(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public object ExecuteScalar(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        SqlCommand command = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
        object obj = command.ExecuteScalar();
        command.Parameters.Clear();
        return obj;
    }

    public object ExecuteScalar(
      SqlTransaction transaction,
      string spName,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public XmlReader ExecuteXmlReader(
      SqlConnection connection,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
    }

    public XmlReader ExecuteXmlReader(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        bool mustCloseConnection = false;
        SqlCommand command = new SqlCommand();
        try
        {
            this.PrepareCommand(command, connection, (SqlTransaction)null, commandType, commandText, commandParameters, ref mustCloseConnection);
            XmlReader xmlReader = command.ExecuteXmlReader();
            command.Parameters.Clear();
            return xmlReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public XmlReader ExecuteXmlReader(
      SqlConnection connection,
      string spName,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public XmlReader ExecuteXmlReader(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText)
    {
        return this.ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);
    }

    public XmlReader ExecuteXmlReader(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        SqlCommand command = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
        XmlReader xmlReader = command.ExecuteXmlReader();
        command.Parameters.Clear();
        return xmlReader;
    }

    public XmlReader ExecuteXmlReader(
      SqlTransaction transaction,
      string spName,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues == null || parameterValues.Length <= 0)
            return this.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, parameterValues);
        return this.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public void FillDataset(
      string connectionString,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            this.FillDataset(connection, commandType, commandText, dataSet, tableNames);
        }
    }

    public void FillDataset(
      string connectionString,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            this.FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
        }
    }

    public void FillDataset(
      string connectionString,
      string spName,
      DataSet dataSet,
      string[] tableNames,
      params object[] parameterValues)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            this.FillDataset(connection, spName, dataSet, tableNames, parameterValues);
        }
    }

    public void FillDataset(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames)
    {
        this.FillDataset(connection, commandType, commandText, dataSet, tableNames, (SqlParameter[])null);
    }

    public void FillDataset(
      SqlConnection connection,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames,
      params SqlParameter[] commandParameters)
    {
        this.FillDataset(connection, (SqlTransaction)null, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public void FillDataset(
      SqlConnection connection,
      string spName,
      DataSet dataSet,
      string[] tableNames,
      params object[] parameterValues)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues != null && parameterValues.Length > 0)
        {
            SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
            this.AssignParameterValues(spParameterSet, parameterValues);
            this.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
        }
        else
            this.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
    }

    public void FillDataset(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames)
    {
        this.FillDataset(transaction, commandType, commandText, dataSet, tableNames, (SqlParameter[])null);
    }

    public void FillDataset(
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames,
      params SqlParameter[] commandParameters)
    {
        this.FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public void FillDataset(
      SqlTransaction transaction,
      string spName,
      DataSet dataSet,
      string[] tableNames,
      params object[] parameterValues)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (parameterValues != null && parameterValues.Length > 0)
        {
            SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
            this.AssignParameterValues(spParameterSet, parameterValues);
            this.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
        }
        else
            this.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
    }

    private void FillDataset(
      SqlConnection connection,
      SqlTransaction transaction,
      CommandType commandType,
      string commandText,
      DataSet dataSet,
      string[] tableNames,
      params SqlParameter[] commandParameters)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (dataSet == null)
            throw new ArgumentNullException(nameof(dataSet));
        SqlCommand sqlCommand = new SqlCommand();
        bool mustCloseConnection = false;
        this.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters, ref mustCloseConnection);
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
        {
            if (tableNames != null && tableNames.Length > 0)
            {
                string sourceTable = "Table";
                for (int index = 0; index <= tableNames.Length - 1; ++index)
                {
                    if (tableNames[index] == null || tableNames[index].Length == 0)
                        throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", nameof(tableNames));
                    sqlDataAdapter.TableMappings.Add(sourceTable, tableNames[index]);
                    sourceTable += (index + 1).ToString();
                }
            }
            sqlDataAdapter.Fill(dataSet);
            sqlCommand.Parameters.Clear();
        }
        if (!mustCloseConnection)
            return;
        connection.Close();
    }

    public void UpdateDataset(
      SqlCommand insertCommand,
      SqlCommand deleteCommand,
      SqlCommand updateCommand,
      DataSet dataSet,
      string tableName)
    {
        if (insertCommand == null)
            throw new ArgumentNullException(nameof(insertCommand));
        if (deleteCommand == null)
            throw new ArgumentNullException(nameof(deleteCommand));
        if (updateCommand == null)
            throw new ArgumentNullException(nameof(updateCommand));
        if (tableName == null || tableName.Length == 0)
            throw new ArgumentNullException(nameof(tableName));
        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
        {
            sqlDataAdapter.UpdateCommand = updateCommand;
            sqlDataAdapter.InsertCommand = insertCommand;
            sqlDataAdapter.DeleteCommand = deleteCommand;
            sqlDataAdapter.Update(dataSet, tableName);
            dataSet.AcceptChanges();
        }
    }

    public SqlCommand CreateCommand(
      SqlConnection connection,
      string spName,
      params string[] sourceColumns)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        SqlCommand command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;
        if (sourceColumns != null && sourceColumns.Length > 0)
        {
            SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
            for (int index = 0; index <= sourceColumns.Length - 1; ++index)
                spParameterSet[index].SourceColumn = sourceColumns[index];
            this.AttachParameters(command, spParameterSet);
        }
        return command;
    }

    public int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public int ExecuteNonQueryTypedParams(
      SqlTransaction transaction,
      string spName,
      DataRow dataRow)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDatasetTypedParams(
      string connectionString,
      string spName,
      DataRow dataRow)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDatasetTypedParams(
      SqlConnection connection,
      string spName,
      DataRow dataRow)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public DataSet ExecuteDatasetTypedParams(
      SqlTransaction transaction,
      string spName,
      DataRow dataRow)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public SqlDataReader ExecuteReaderTypedParams(
      string connectionString,
      string spName,
      DataRow dataRow)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public SqlDataReader ExecuteReaderTypedParams(
      SqlConnection connection,
      string spName,
      DataRow dataRow)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteReader(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public SqlDataReader ExecuteReaderTypedParams(
      SqlTransaction transaction,
      string spName,
      DataRow dataRow)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connectionString, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalarTypedParams(
      SqlConnection connection,
      string spName,
      DataRow dataRow)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public object ExecuteScalarTypedParams(
      SqlTransaction transaction,
      string spName,
      DataRow dataRow)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public XmlReader ExecuteXmlReaderTypedParams(
      SqlConnection connection,
      string spName,
      DataRow dataRow)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
    }

    public XmlReader ExecuteXmlReaderTypedParams(
      SqlTransaction transaction,
      string spName,
      DataRow dataRow)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", nameof(transaction));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return this.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
        SqlParameter[] spParameterSet = this.GetSpParameterSet(transaction.Connection, spName);
        this.AssignParameterValues(spParameterSet, dataRow);
        return this.ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
    }

    private SqlParameter[] DiscoverSpParameterSet(
      SqlConnection connection,
      string spName,
      bool includeReturnValueParameter)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        SqlCommand command = new SqlCommand(spName, connection);
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
        SqlCommandBuilder.DeriveParameters(command);
        connection.Close();
        if (!includeReturnValueParameter)
            command.Parameters.RemoveAt(0);
        SqlParameter[] array = new SqlParameter[command.Parameters.Count];
        command.Parameters.CopyTo(array, 0);
        foreach (DbParameter dbParameter in array)
            dbParameter.Value = (object)DBNull.Value;
        return array;
    }

    private SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
    {
        SqlParameter[] sqlParameterArray = new SqlParameter[originalParameters.Length];
        int index = 0;
        for (int length = originalParameters.Length; index < length; ++index)
            sqlParameterArray[index] = (SqlParameter)((ICloneable)originalParameters[index]).Clone();
        return sqlParameterArray;
    }

    public void CacheParameterSet(
      string connectionString,
      string commandText,
      params SqlParameter[] commandParameters)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (commandText == null || commandText.Length == 0)
            throw new ArgumentNullException(nameof(commandText));
        this.paramCache[(object)(connectionString + ":" + commandText)] = (object)commandParameters;
    }

    public SqlParameter[] GetCachedParameterSet(
      string connectionString,
      string commandText)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (commandText == null || commandText.Length == 0)
            throw new ArgumentNullException(nameof(commandText));
        SqlParameter[] originalParameters = this.paramCache[(object)(connectionString + ":" + commandText)] as SqlParameter[];
        if (originalParameters == null)
            return (SqlParameter[])null;
        return this.CloneParameters(originalParameters);
    }

    public SqlParameter[] GetSpParameterSet(string connectionString, string spName)
    {
        return this.GetSpParameterSet(connectionString, spName, false);
    }

    public SqlParameter[] GetSpParameterSet(
      string connectionString,
      string spName,
      bool includeReturnValueParameter)
    {
        if (connectionString == null || connectionString.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        using (SqlConnection connection = new SqlConnection(connectionString))
            return this.GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
    }

    internal SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
    {
        return this.GetSpParameterSet(connection, spName, false);
    }

    internal SqlParameter[] GetSpParameterSet(
      SqlConnection connection,
      string spName,
      bool includeReturnValueParameter)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        using (SqlConnection connection1 = (SqlConnection)((ICloneable)connection).Clone())
            return this.GetSpParameterSetInternal(connection1, spName, includeReturnValueParameter);
    }

    private SqlParameter[] GetSpParameterSetInternal(
      SqlConnection connection,
      string spName,
      bool includeReturnValueParameter)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));
        if (spName == null || spName.Length == 0)
            throw new ArgumentNullException(nameof(spName));
        string str = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
        SqlParameter[] originalParameters = this.paramCache[(object)str] as SqlParameter[];
        if (originalParameters == null)
        {
            SqlParameter[] sqlParameterArray = this.DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
            this.paramCache[(object)str] = (object)sqlParameterArray;
            originalParameters = sqlParameterArray;
        }
        return this.CloneParameters(originalParameters);
    }

    internal object ExecuteDataset(string p, CommandType commandType, SqlParameter[] param)
    {
        throw new NotImplementedException();
    }

   

    #region Transaction

    /// <summary>
    /// Begin Transaction
    /// </summary>
    public void BeginTran()
    {
        try
        {
            Connect();
            tran = conn.BeginTransaction();
            cmd.Transaction = tran;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Commit the transaction 
    /// </summary>
    public void CommitTran()
    {
        try
        {
            if (tran != null)
            {
                tran.Commit();
                tran.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// RollBackTran the transaction
    /// </summary>
    public void RollBackTran()
    {
        try
        {
            if (tran != null)
            {
                tran.Rollback();
                tran.Dispose();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}

