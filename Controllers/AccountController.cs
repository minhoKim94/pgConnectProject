using PGproject.Dao;
using PGproject.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;

///===============================================================
/// <summary>
/// FileName : AccountController.cs
/// Description : 로그인/회원가입
/// Copyright : 2020 by PayLetter Inc. All rights reserved.
/// Author : mh_kim@payletter.com, 2020-11-20
/// Modify History : Just Created.
/// </summary>
///================================================================
namespace PGproject.Controllers
{
    public class AccountController : Controller
    {
        AccountDAO  pl_objAcctDao          = null;

        private int pl_intRegsterChkRetVal = 0;  // 회원가입 등록 결과 값
        private int pl_intLoginChkRetVal   = 0;  // 로그인 등록 결과 값

        /// -------------------------------------------------------
        /// <summary>
        /// 로그인
        /// </summary>
        /// <param>pl_intLoginChkRetVal 성공:0, 아이디 미존재:-1</param>
        /// -------------------------------------------------------
        public ActionResult LoginFrm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginFrm(FormCollection objFormCollect)
        {
            pl_objAcctDao = new AccountDAO();
            pl_intLoginChkRetVal = pl_objAcctDao.LoginUser(objFormCollect["UserID"], objFormCollect["UserPwd"]);
            switch (pl_intLoginChkRetVal) // 로그인 성공 : 0 , 아이디 오류 : -1
            {
                case 0:
                    ViewBag.Message = "login Success...!";
                    FormsAuthentication.SetAuthCookie(objFormCollect["UserID"], false);
                    return RedirectToAction("Index", "Home");
                case -1:
                    ViewBag.Message = "Admin is not valid...!";
                    return View();
                default:
                    ViewBag.Message = "password is not correct...!";
                    return View();
            }
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 로그아웃
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("LoginFrm", "Account");
        }

        /// -------------------------------------------------------
        /// <summary>
        /// 회원가입
        /// </summary>
        /// -------------------------------------------------------
        public ActionResult RegisterFrm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterFrm(TAcctMst objTAcctMst, FormCollection objFormCollect)
        {
            if (ModelState.IsValid)
            {
                pl_objAcctDao = new AccountDAO();

                objTAcctMst.UserID     = objFormCollect["UserID"];
                objTAcctMst.UserName   = objFormCollect["UserName"];
                objTAcctMst.UserPwd    = objFormCollect["UserPwd"];

                objTAcctMst.BirthDay   = Convert.ToDateTime(objFormCollect["BirthDay"]);
                objTAcctMst.GenderCode = Convert.ToByte(objFormCollect["GenderCode"]);
                objTAcctMst.EmailAddr  = objFormCollect["EmailAddr"];

                pl_intRegsterChkRetVal = pl_objAcctDao.RegisterUser(objTAcctMst);
                if (pl_intRegsterChkRetVal.Equals(0))
                {
                    ViewBag.Message = "User Details Saved";
                    return RedirectToAction("LoginFrm", "Account");
                }
                else if (pl_intRegsterChkRetVal.Equals(-1))
                {
                    ViewBag.Message = "Duplicated ID";
                    return View(objTAcctMst);
                }
                else
                {
                    ViewBag.Message = "Login Fail";
                    return View(objTAcctMst);
                }
            }
            else
            {
                return View("RegisterFrm", objTAcctMst);
            }
        }
        /// -------------------------------------------------------
        /// <summary>
        /// 아이디 중복 체크 추가
        /// </summary>
        /// -------------------------------------------------------
       
    }
}