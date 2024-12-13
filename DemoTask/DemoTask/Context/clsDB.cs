using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

public class clsDB
{
    SqlConnection con;
    public IConfiguration _configuration { get; }

    public clsDB()
    {
        con = new SqlConnection(Config.ConStr);

    }
    public clsDB(string conStr)
    {
        con = new SqlConnection(conStr);
    }

    public DataSet ExecuteDataSet(string storedProcedure, SqlParameter[] p)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(p);
            if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            cmd.Dispose();
            da.Dispose();
            ds.Dispose();
        }
    }
    public DataSet ExecuteDataSet(string command)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = command;

            if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            da.Dispose();
            ds.Dispose();
        }
    }
    public DataTable ExecuteDataTable(string command)
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = command;
            if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            da.Dispose();
            dt.Dispose();
        }
    }
    private void CloseConn()
    {
        try
        {
            con.Close();
        }
        catch
        {
        }
    }
}
public static class Config
{
    public static string ConStr = "";
   
}

