using PGproject.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
///===============================================================
/// <summary>
/// FileName : AccountDAO.cs
/// Description : 로그인/회원가입 데이터베이스 접근 클래스
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-20
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace PGproject.Dao
{
    public class AccountDAO
    {
        SqlConnection pl_objSqlCon     = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand    pl_objSqlCommand = null; // SqlCommand 객체
        SqlParameter  pl_objSqlParam   = null; // SqlParameter 객체
        AESEncrypt    pl_objAesEncrypt = null; // AES256방식 암호화 객체
        
        private int   pl_intRetValue   = 0; // 성공 여부 결과값 성공:0,실패:-1

        /// -------------------------------------------------------
        /// <summary>
        /// 사용자 로그인
        /// </summary>
        /// <param>성공 pl_intRetValue:0, 아이디 미존재 pl_intRetValue:-1, 아이디 미존재 pl_intRetValue:-2</param>
        /// -------------------------------------------------------
        public int LoginUser(string strUserID, string strUserPwd)
        {
            pl_objAesEncrypt = new AESEncrypt();

            string strEncript = pl_objAesEncrypt.Encrypt(strUserPwd);

            try
            {
                pl_objSqlCommand = new SqlCommand("dbo.UP_ACCT_INFO_NT_GET", pl_objSqlCon);
                pl_objSqlCommand.CommandType = CommandType.StoredProcedure;

                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserID", strUserID);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserPwd", strEncript);

                pl_objSqlParam = new SqlParameter();
                pl_objSqlParam.ParameterName = "@po_intRetVal";
                pl_objSqlParam.SqlDbType = SqlDbType.Int;
                pl_objSqlParam.Direction = ParameterDirection.Output;
                pl_objSqlCommand.Parameters.Add(pl_objSqlParam);

                pl_objSqlCon.Open();
                pl_objSqlCommand.ExecuteNonQuery();

                pl_intRetValue = Convert.ToInt32(pl_objSqlParam.Value);
                return pl_intRetValue;
            }
            catch (Exception pl_objEx)
            {
                Console.WriteLine(pl_objEx);
                return 1;
            }
            finally
            {
                pl_objSqlCon.Close();
            }
        }


        /// -------------------------------------------------------
        /// <summary>
        /// 사용자 회원가입 
        /// </summary>
        /// <returns> 0:성공, <> 0 실패, 중복 ID 존재: -1</returns>
        /// -------------------------------------------------------
        public int RegisterUser(TAcctMst objTAcctMst)
        {
            pl_objAesEncrypt = new AESEncrypt();
            
            try
            {
                pl_objSqlCommand = new SqlCommand("dbo.UP_ACCT_INFO_NT_INS", pl_objSqlCon);
                pl_objSqlCommand.CommandType = CommandType.StoredProcedure;

                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserID", objTAcctMst.UserID);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserName", objTAcctMst.UserName);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strUserPwd", pl_objAesEncrypt.Encrypt(objTAcctMst.UserPwd));

                pl_objSqlCommand.Parameters.AddWithValue("@pi_dtBirthDay", objTAcctMst.BirthDay);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_intGenderCode", objTAcctMst.GenderCode);
                pl_objSqlCommand.Parameters.AddWithValue("@pi_strEmailAddr", objTAcctMst.EmailAddr);

                pl_objSqlParam = new SqlParameter();
                pl_objSqlParam.ParameterName = "@po_intRetVal";
                pl_objSqlParam.SqlDbType = SqlDbType.Int;
                pl_objSqlParam.Direction = ParameterDirection.Output;
                pl_objSqlCommand.Parameters.Add(pl_objSqlParam);

                pl_objSqlCon.Open();
                pl_objSqlCommand.ExecuteNonQuery();
                pl_intRetValue = Convert.ToInt32(pl_objSqlParam.Value);

                return pl_intRetValue;
            }
            catch(Exception pl_objEx)
            {
                Console.WriteLine(pl_objEx);
                return 1;
            }
            finally
            {
                pl_objSqlCon.Close();
            }
        }
    }
}