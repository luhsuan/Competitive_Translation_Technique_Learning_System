using System;
using System.Data;
using MySql.Data.MySqlClient;

public class MySQLAccess
{

    private static MySqlConnection mySqlConnection;//連接類對象
     static string host;     //IP地址。如果只是在本地的話，寫localhost就可以。
     static string id;       //用户名。
     static string pwd;      //密碼。
     static string dataBase; //數據庫名稱。

    public MySQLAccess(string _host, string _id, string _pwd, string _dataBase)
    {
        host = _host;
        id = _id;
        pwd = _pwd;
        dataBase = _dataBase;
        OpenSql();
    }

    /// <summary>  
    /// 打開數據庫  
    /// </summary>  
    public static void OpenSql()
    {
        try
        {
            //string.Format是將指定的 String類型的數據中的每個格式項替換為相應對象的值的文本等效項。  
            //string mySqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", dataBase, host, id, pwd, "3306");
            string mySqlString = "Server=" + host + ";Database=" + dataBase + ";User ID=" + id + ";Password=" + pwd + ";Pooling=false;CharSet=utf8";
            mySqlConnection = new MySqlConnection(mySqlString);

            mySqlConnection.Open();
        }
        catch (Exception e)
        {
            throw new Exception("服務器連接失敗，請重新檢查是否打開MySql服務。" + e.Message.ToString());
        }
    }

    /// <summary>  
    /// 插入一條數據，包括所有，不適用自動累加ID。  
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="values">插入值</param>  
    /// <returns></returns>  
    public DataSet InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values[i] + "'";
        }
        query += ")";
        MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
        cmd.ExecuteNonQuery();
        return QuerySet(query);
    }

    /// <summary>  
    /// 插入部分ID  
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="col">屬性列</param>  
    /// <param name="values">屬性值</param>  
    /// <returns></returns>  
    public DataSet InsertInto(string tableName, string[] col, string[] values)
    {
        if (col.Length != values.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "INSERT INTO " + tableName + " (" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i];
        }
        query += ") VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values[i] + "'";
        }
        query += ")";
        return QuerySet(query);
    }

    /// <summary>  
    /// 更新表數據單欄 
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="cols">更新列</param>  
    /// <param name="colsvalues">更新的值</param>  
    /// <param name="selectkey">條件：列</param>  
    /// <param name="selectvalue">條件：值</param>  
    /// <returns></returns>  
    public DataSet UpdateInto(string tableName, string col, string colsvalue, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + col + " = " + colsvalue;
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return QuerySet(query);
    }

    /// <summary>  
    /// 更新表數據 
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="cols">更新列</param>  
    /// <param name="colsvalues">更新的值</param>  
    /// <param name="selectkey">條件：列</param>  
    /// <param name="selectvalue">條件：值</param>  
    /// <returns></returns>  
    public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return QuerySet(query);
    }

    /// <summary>
    /// SELECT MAX(highscore) FROM translation.practice_task where  user_id = '123' and 
    // practice_theme = 'means' and practice_level =1;
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">需要查詢的列</param>
    /// <param name="whereColName">查詢的條件列</param>
    /// <param name="operation">條件操作符</param>
    /// <param name="value">條件的值</param>
    /// <returns></returns>
    public DataSet Select(string tableName, string items, string whereColName, string operation, string selectvalue,
     string whereColName2, string operation2, string selectvalue2, string whereColName3, string operation3, string selectvalue3)
    {
        // if (whereColName.Length != operation.Length || operation.Length != value.Length)
        // {
        //     throw new Exception("輸入不正確：" + "col.Length != operation.Length != values.Length");
        // }
        string query = "SELECT " + items;
        query += "  FROM  " + tableName + "  WHERE " + " " + whereColName + operation + " '" + selectvalue + "' and "
        + whereColName2 + operation2 + " '" + selectvalue2 +  "' and "
        + whereColName3 + operation3 + " '" + selectvalue3 +  "' ;" ;
       
        return QuerySet(query);
    }


    /// <summary>
    /// 釋放
    /// </summary>
    public static void Close()
    {
        if (mySqlConnection != null)
        {
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            mySqlConnection = null;
        }
    }

   
    /// <summary>    
    /// 執行Sql語句  
    /// </summary>  
    /// <param name="sqlString">sql語句</param>  
    /// <returns></returns>  
    public static DataSet QuerySet(string sqlString)
    {
        if (mySqlConnection.State == System.Data.ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, mySqlConnection);
                mySqlDataAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
            finally
            {
            }
            return ds;
        }
        return null;
    }
}
