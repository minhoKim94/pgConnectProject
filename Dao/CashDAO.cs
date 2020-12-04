using PGproject.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PGproject.Dao
{
    public class CashDAO
    {
        SqlConnection pl_objSqlCon     = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand    pl_objSqlCommand = null; // SqlCommand 객체
        SqlParameter  pl_objSqlParam   = null; // SqlParameter 객체

        /// -------------------------------------------------------
        /// <summary>
        /// 캐쉬 충전 시 로그 입력 
        /// </summary>
        /// -------------------------------------------------------
        public void InsertPGLog(TPGPayLogMst objTPGPayLogMst)
        {
            try
            {
                pl_objSqlCommand = new SqlCommand("dbo.UP_PG_PAY_LOG_TX_INS", pl_objSqlCon);
                pl_objSqlCommand.CommandType = CommandType.StoredProcedure;

                pl_objSqlCommand.Parameters.AddWithValue("@pi_intOrderNo", objTPGPayLogMst.OrderNo);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserID", objTPGPayLogMst.UserID);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserName", objTPGPayLogMst.UserName);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strPGCode", objTPGPayLogMst.PGCode);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strMallID", objTPGPayLogMst.MallID);

                pl_objSqlCommand.Parameters.AddWithValue("@pi_intPayAmt", objTPGPayLogMst.PayAmt);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_intVATAmt", objTPGPayLogMst.VATAmt);

                pl_objSqlCon.Open();
                pl_objSqlCommand.ExecuteNonQuery();

                return;
            }
            catch (Exception pl_objExp)
            {
                Console.WriteLine(pl_objExp);
                return;
            }
            finally
            {
                pl_objSqlCon.Close();
            }
        }

        public void InsertCash(TCashMst objTCashMst)
        {
            try
            {
                pl_objSqlCommand = new SqlCommand("dbo.UP_CASH_NT_INS", pl_objSqlCon);
                pl_objSqlCommand.CommandType = CommandType.StoredProcedure;

                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserID", objTCashMst.UserID);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserName", objTCashMst.UserName);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strCashAttrCode", objTCashMst.OrderNo);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strTID", objTCashMst.TID);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strCID", objTCashMst.CID);

                pl_objSqlCommand.Parameters.AddWithValue("@pi_intPayAmt", objTCashMst.PayAmt);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strPGCode", objTCashMst.PGCode);

                pl_objSqlCon.Open();
                pl_objSqlCommand.ExecuteNonQuery();

                return;
            }
            catch (Exception pl_objExp)
            {
                Console.WriteLine(pl_objExp);
                return;
            }
            finally
            {
                pl_objSqlCon.Close();
            }
        }
    }
}