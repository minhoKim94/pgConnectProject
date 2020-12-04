using Newtonsoft.Json;
using PGproject.Dao;
using PGproject.Models;
using PGproject.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace PGproject.Controllers
{
    public class HomeController : Controller
    {
        string strURL = string.Empty;
        CashDAO objCashDao = new CashDAO();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult POQRequest(POQSuccessResp objPOQSuccResp)
        {
            string strURL = "https://testpgapi.payletter.com/v1.0/payments/request";

            string strPostData = "{\"pgcode\" : \"" + objPOQSuccResp.pgcode + "\"," +
                              "\"user_id\":\"" + objPOQSuccResp.uid + "\"," +
                              "\"user_name\":\"mh_kim\"," +
                              "\"service_name\":\"페이레터\"," +
                              "\"client_id\":\"pay_test\"," +
                              "\"order_no\":\"" + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) + "\"," +
                              "\"amount\":" + objPOQSuccResp.amount + "," +
                              "\"tax_amount\":\"20\"," +
                              "\"product_name\":\"만두\"," +
                              "\"email_flag\":\"Y\"," +
                              "\"email_addr\":\"payletter@payletter.com\"," +
                              "\"autopay_flag\":\"N\"," +
                              "\"receipt_flag\":\"Y\"," +
                              "\"custom_parameter\":\"this is custom parameter\"," +
                              "\"return_url\":\"" + "http://usrpjtmhk.payletter.co.kr/Home/POQReturn" + "\"," +
                              "\"callback_url\":\"" + "http://usrpjtmhk.payletter.co.kr/Home/POQCallback" + "\"," +
                              "\"cancel_url\":\"" + "http://usrpjtmhk.payletter.co.kr/Home/POQCancel" + "\"}";

            var inputJsonData = JsonConvert.DeserializeObject<POQCashLog>(strPostData);

            TPGPayLogMst objTPGPayLogMst = new TPGPayLogMst();

            objTPGPayLogMst.OrderNo = inputJsonData.order_no;
            objTPGPayLogMst.UserID = inputJsonData.user_id;
            objTPGPayLogMst.UserName = inputJsonData.user_name;
            objTPGPayLogMst.PGCode = inputJsonData.pgcode;
            objTPGPayLogMst.MallID = inputJsonData.client_id;

            objTPGPayLogMst.PayAmt = inputJsonData.amount;
            objTPGPayLogMst.VATAmt = inputJsonData.tax_amount;

            try
            {
                HttpWebRequest objWRequest = (HttpWebRequest)WebRequest.Create(strURL);
                objWRequest.Method = "POST";
                objWRequest.ContentType = "application/json";
                objWRequest.Headers.Add("Authorization", "PLKEY MTFBNTAzNTEwNDAxQUIyMjlCQzgwNTg1MkU4MkZENDA="); // Authorization 설정

                byte[] objResultByte = Encoding.UTF8.GetBytes(strPostData);
                objWRequest.ContentLength = objResultByte.Length;

                Stream objStream = objWRequest.GetRequestStream();
                objStream.Write(objResultByte, 0, objResultByte.Length);
                objStream.Close();

                objWRequest.Timeout = 20000;
                //-----------------------------------------------------------------------------------------
                // Description : API 요청에 대한 성공/실패 여부 (오류코드) 
                //               HTTP StatusCode 200 OK 인 경우에만 요청 처리 성공이며, 성공이 아닌 경우에는 아래 StatusCode를 참고하시기 바랍니다.          
                //              - 401 : [998] Authentication token is missing or incorrect. (인증 오류)
                //              - 403 : [993] Yon do not have authorization. (인증 오류)
                //              - 405 : [995] 요청된 메소드는 권한이 없습니다. (POST / GET 등 메소드 오류)
                //              - 406 : [2000]~[5000] 오류 상세 메시지 (비즈니스 로직 처리중 오류 발생)
                //              - 500 : [999] Internal server error (System 오류)
                //-----------------------------------------------------------------------------------------

                // 요청 처리 성공인 경우                                       
                // Response Parameters (성공시) : token, online_url, mobile_url
                HttpWebResponse objWResponse = (HttpWebResponse)objWRequest.GetResponse();

                StreamReader objStreamReader = new StreamReader(objWResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                string strResponse = objStreamReader.ReadToEnd();

                var JsonDeseReponse = JsonConvert.DeserializeObject<POQResp>(strResponse);

                objStreamReader.Close();
                objWResponse.Close();

                // 로그 입력
                objCashDao.InsertPGLog(objTPGPayLogMst);

                return Json(JsonDeseReponse);
            }
            // 성공이 아닌 경우
            // Response Parameters (실패시) : code, message
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    return Json(reader);
                }
            }
        }

        // 리턴url 쪽에서는 결제 결과 받아서 사용자한테 뿌리고
        public ActionResult POQReturn()
        {
            return View();
        }

        //-----------------------------------------------------------------------------------------
        // Description    : Callback URL로 반환된 결제 결과 받아오기 (json)
        //                - 결제가 성공한 경우 callback_url로 결제 결과가 반환됩니다. (json)
        //                - 전달받은 Callback URL 을 통해서 결과값을 받아서 가맹점에 맞는 충전, 구매 등의 로직을 수행하도록 합니다.
        //-----------------------------------------------------------------------------------------
        public ActionResult POQCallback()
        {
            StreamReader objStreamReader = new StreamReader(Request.GetBufferlessInputStream(), Encoding.GetEncoding("utf-8"));
            string strResponse = objStreamReader.ReadToEnd();

            objStreamReader.Close();

            //-- Callback URL로 반환된 결제 결과 받기
            if (string.IsNullOrEmpty(strResponse))
            {
                Response.Write("{\"code\":9999, \"message\":\"Response data is empty\"}");
            }
            //-- JSON 파싱
            PayResult objJsonData = JsonConvert.DeserializeObject<PayResult>(strResponse);

            string strResponseCode = "{\"code\":0, \"message\":\"success\"}";

            POQResp objRespData = JsonConvert.DeserializeObject<POQResp>(strResponseCode);

            if (objRespData.code.Equals(0))
            {
                TCashMst objTCashMst = new TCashMst();

                objTCashMst.UserID = objJsonData.user_id;                        //--가맹점 결제자(회원) 아이디(email, 영문 및 숫자 가능)
                objTCashMst.UserName = objJsonData.user_name;                    //--가맹점 결제자(회원) 이름
                objTCashMst.OrderNo = objJsonData.order_no;                      //--가맹점의 주문 번호
                objTCashMst.TID = objJsonData.tid;                               //--결제고유번호

                objTCashMst.CID = objJsonData.cid;                             //--승인번호
                objTCashMst.PayAmt = objJsonData.amount;                       //--결제요청 금액
                objTCashMst.PGCode = objJsonData.pgcode;                       //--결제요청한 pg명
                string strPayHash = objJsonData.payhash;                       //--파라메터 검증을 위한 SHA256 hash 값 SHA256(user_id +amount + tid +결제용 API Key) * 일부 결제 수단은 전달되지 않습니다.(가상계좌 등)
                objCashDao.InsertCash(objTCashMst);

                return RedirectToAction("POQCallback", "Home");
            }
            else
            {
                return RedirectToAction("POQCancel", "Home");
            }
        }

        public ActionResult POQCancel()
        {
            return View();
        }
    }
}


