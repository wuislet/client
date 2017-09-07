using System;
using LitJson;
using System.Collections;

namespace AssemblyCSharp
{
	public class LoginRequest:ClientRequest
	{
		
		public LoginRequest (string data)
		{
            MyDebug.Log("----------------4------------------");
			headCode = APIS.LOGIN_REQUEST;
			/**
			LoginVo loginvo = new LoginVo ();
			if (data != null) {
				MyDebug.Log (data.toJson());
				try {
					
					loginvo.openId = (string)data ["openid"];
					loginvo.nickName = (string)data ["nickname"];
					loginvo.headIcon = (string)data ["headimgurl"];
					loginvo.unionid = (string)data ["unionid"];
					loginvo.province = (string)data ["province"];
					loginvo.city = (string)data ["city"];
					string sex = data ["sex"].ToString();
					loginvo.sex = int.Parse(sex);
					loginvo.IP = GlobalDataScript.getInstance().getIpAddress();
				} catch (Exception e) {
					MyDebug.Log ("微信接口有变动！" + e.Message);
					TipsManagerScript.getInstance ().setTips ("请先打开你的微信客户端");
					return;
				}
			} else {

			}


			MyDebug.Log ("loginvo.IP" + loginvo.IP);

**/

			if (data == null) {
				LoginVo loginvo = new LoginVo ();
				Random ran = new Random();
				string str = ran.Next (100, 1000) + "for" + ran.Next (2000, 5000);
				loginvo.openId = "133333333" ;
                MyDebug.Log("----------------5------------------");

				loginvo.nickName = "11112322444";
				loginvo.headIcon = "imgico221";
				loginvo.unionid = "12732233";
				loginvo.province = "21sfsd";
				loginvo.city = "afafsdf";
				loginvo.sex = 1;
				loginvo.IP = GlobalDataScript.getInstance().getIpAddress();
				data = JsonMapper.ToJson (loginvo);

				GlobalDataScript.loginVo = loginvo;
				GlobalDataScript.loginResponseData = new AvatarVO ();
				GlobalDataScript.loginResponseData.account = new Account ();
				GlobalDataScript.loginResponseData.account.city = loginvo.city;
				GlobalDataScript.loginResponseData.account.openid = loginvo.openId;
				GlobalDataScript.loginResponseData.account.nickname = loginvo.nickName;
				GlobalDataScript.loginResponseData.account.headicon = loginvo.headIcon;
				GlobalDataScript.loginResponseData.account.unionid = loginvo.city;
				GlobalDataScript.loginResponseData.account.sex = loginvo.sex;
				GlobalDataScript.loginResponseData.IP = loginvo.IP;
			}
            MyDebug.Log("----------------6------------------" + messageContent);
			messageContent = data;

		}


        public LoginRequest(int id)
        {
            string data = null;
            MyDebug.Log("----------------4------------------");
            headCode = APIS.LOGIN_REQUEST;
            
            LoginVo loginvo = new LoginVo();
            Random ran = new Random();
            string str = ran.Next(100, 1000) + "for" + ran.Next(2000, 5000);
            loginvo.openId = id.ToString();
            MyDebug.Log("----------------5------------------");

            loginvo.nickName = "test" + id;
            loginvo.headIcon = "imgico221";
            loginvo.unionid = "12732" + id;
            loginvo.province = "21s" + id;
            loginvo.city = "afafsdf";
            loginvo.sex = 1;
            loginvo.IP = GlobalDataScript.getInstance().getIpAddress();
            data = JsonMapper.ToJson(loginvo);

            GlobalDataScript.loginVo = loginvo;
            GlobalDataScript.loginResponseData = new AvatarVO();
            GlobalDataScript.loginResponseData.account = new Account();
            GlobalDataScript.loginResponseData.account.city = loginvo.city;
            GlobalDataScript.loginResponseData.account.openid = loginvo.openId;
            GlobalDataScript.loginResponseData.account.nickname = loginvo.nickName;
            GlobalDataScript.loginResponseData.account.headicon = loginvo.headIcon;
            GlobalDataScript.loginResponseData.account.unionid = loginvo.unionid;
            GlobalDataScript.loginResponseData.account.sex = loginvo.sex;
            GlobalDataScript.loginResponseData.IP = loginvo.IP;

            MyDebug.Log("----------------6------------------" + messageContent);
            messageContent = data;

        }

        /**用于重新登录使用**/


        //退出登录
        public LoginRequest (){
			headCode = APIS.QUITE_LOGIN;
			if (GlobalDataScript.loginResponseData != null) {
				messageContent = GlobalDataScript.loginResponseData.account.uuid + "";
			}

		}


	}
}

