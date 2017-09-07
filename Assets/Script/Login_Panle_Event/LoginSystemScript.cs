using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using LitJson;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using System.Threading;

public class LoginSystemScript : MonoBehaviour {
	

	//public ShareSDK shareSdk;
	private GameObject panelCreateDialog;

	public Toggle agreeProtocol;

	public Text versionText;

    public InputField testloginID;

	private int tapCount = 0;//点击次数
	public GameObject watingPanel;


	void Start () {

		//shareSdk.showUserHandler = getUserInforCallback;//注册获取用户信息回调
		CustomSocket.hasStartTimer = false;
        SoundCtrl.getInstance().playBGM(1);
		SocketEventHandle.getInstance ().LoginCallBack += LoginCallBack;
		SocketEventHandle.getInstance ().RoomBackResponse += RoomBackResponse;

    
        GlobalDataScript.isonLoginPage = true;
		versionText.text ="版本号：" +Application.version;
		//WxPayImpl test = new WxPayImpl(gameObject);
		//test.callTest ("dddddddddddddddddddddddddddd");
        if (watingPanel != null)
        {
            //watingPanel.SetActive(false);
            watingPanel.GetComponentInChildren<Text>().text = "正在连接服务器...";
        }
        StartCoroutine(ConnectTime1(1f, 1));

        //每隔0.1秒執行一次定時器
        InvokeRepeating("isConnected", 1, 0.1f);

    }

    private void isConnected()
    {
        if (CustomSocket.getInstance().isConnected) {
            watingPanel.SetActive(false);
            this.CancelInvoke();//取消定时器的执行
                                //如果已经授权自动登录
            #if UNITY_ANDROID
            if (GlobalDataScript.getInstance().wechatOperate.shareSdk.IsAuthorized(PlatformType.WeChat)) {
                login();
            }
            #elif UNITY_IPHONE
            if (GlobalDataScript.getInstance().wechatOperate.shareSdk.IsAuthorized(PlatformType.WechatPlatform)) {
                login();
            }
            #endif
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKey(KeyCode.Escape)){ //Android系统监听返回键，由于只有Android和ios系统所以无需对系统做判断
			if (panelCreateDialog == null) {
				panelCreateDialog = Instantiate (Resources.Load("Prefab/Panel_Exit")) as GameObject;
				panelCreateDialog.transform.parent = gameObject.transform;
				panelCreateDialog.transform.localScale = Vector3.one;
				//panelCreateDialog.transform.localPosition = new Vector3 (200f,150f);
				panelCreateDialog.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
				panelCreateDialog.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
			}
			
		} 

	}

    int count = 0;

    IEnumerator ConnectTime1(float time, byte type)
    {
        connectRetruen = false;
        yield return new WaitForSeconds(time);
        if (!connectRetruen)
        {//超过5秒还没连接成功显示失败          
            if (type == 1)
            {                
                CustomSocket.hasStartTimer = false;
                CustomSocket.getInstance().Connect();
                ChatSocket.getInstance().Connect();
                GlobalDataScript.isonLoginPage = true;
            }
            else if (type == 2)
            {
                 
            }
        }
    }
   

	public void login(){
        MyDebug.Log("----------------1-------------------");
		if (!CustomSocket.getInstance ().isConnected) {
			CustomSocket.getInstance ().Connect ();
			ChatSocket.getInstance ().Connect();            
            tapCount = 0;
            MyDebug.Log("----------------2------------------");
			return;
		}

		GlobalDataScript.reinitData ();//初始化界面数据
		if (agreeProtocol.isOn) {
            MyDebug.Log("----------------3------------------");
			doLogin ();
            watingPanel.GetComponentInChildren<Text>().text = "进入游戏中";
            watingPanel.SetActive(true);
		} else {
			MyDebug.Log ("请先同意用户使用协议");
			TipsManagerScript.getInstance ().setTips ("请先同意用户使用协议");
		}

		tapCount += 1;
		Invoke ("resetClickNum", 10f);

	}

    bool connectRetruen = false;

  
    IEnumerator ConnectTime(float time, byte type)
    {
        connectRetruen = false;
        yield return new WaitForSeconds(time);
        if (!connectRetruen)
        {//超过5秒还没连接成功显示失败

            if (watingPanel != null)
            {
                watingPanel.SetActive(false);
            }
        }
    }
	public void doLogin(){
        StartCoroutine(ConnectTime(10, 0));
		#if UNITY_EDITOR
        	//用于测试 不用微信登录
        	//CustomSocket.getInstance().sendMsg(new LoginRequest(null));
		#else
        	//GlobalDataScript.getInstance ().wechatOperate.login ();
		#endif
        if (testloginID.text == "null")
        {
            CustomSocket.getInstance().sendMsg(new LoginRequest(null));
        }
        else if(!string.IsNullOrEmpty(testloginID.text))
        {
            int num = int.Parse(testloginID.text);
            CustomSocket.getInstance().sendMsg(new LoginRequest(num));
        }
        else
        {
            GlobalDataScript.getInstance().wechatOperate.login();
        }
	}

	public void LoginCallBack(ClientResponse response){
		if (watingPanel != null) {
			watingPanel.SetActive(false);
		}

        SoundCtrl.getInstance().playBGM(1);
		if (response.status == 1) {
			if (GlobalDataScript.homePanel != null) {
				GlobalDataScript.homePanel.GetComponent<HomePanelScript> ().removeListener ();
				Destroy (GlobalDataScript.homePanel);
			}


			if (GlobalDataScript.gamePlayPanel != null) {
				GlobalDataScript.gamePlayPanel.GetComponent<MyMahjongScript> ().exitOrDissoliveRoom ();
			}

			GlobalDataScript.loginResponseData = JsonMapper.ToObject<AvatarVO> (response.message);
			ChatSocket.getInstance ().sendMsg (new LoginChatRequest(GlobalDataScript.loginResponseData.account.uuid));
			panelCreateDialog = Instantiate (Resources.Load("Prefab/Panel_Home")) as GameObject;
			panelCreateDialog.transform.parent = GlobalDataScript.getInstance().canvsTransfrom;
			panelCreateDialog.transform.localScale = Vector3.one;
			panelCreateDialog.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
			panelCreateDialog.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
			GlobalDataScript.homePanel = panelCreateDialog;
			removeListener ();
			Destroy (this);
			Destroy (gameObject);
		}
	}


    GameObject Panel_xieyi;
    public void xieyi()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        Panel_xieyi = PrefabManage.loadPerfab("Prefab/Panel_xieyi");



    }

    public void closexieyi()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        if (Panel_xieyi != null)
            Panel_xieyi.SetActive(false);


    }
	private void removeListener(){
		SocketEventHandle.getInstance ().LoginCallBack -= LoginCallBack;
		SocketEventHandle.getInstance ().RoomBackResponse -= RoomBackResponse;
	}

	private void RoomBackResponse(ClientResponse response){

		watingPanel.SetActive(false);

		if (GlobalDataScript.homePanel != null) {
			GlobalDataScript.homePanel.GetComponent<HomePanelScript> ().removeListener ();
			Destroy (GlobalDataScript.homePanel);
		}


		if (GlobalDataScript.gamePlayPanel != null) {
			GlobalDataScript.gamePlayPanel.GetComponent<MyMahjongScript> ().exitOrDissoliveRoom ();
		}
		GlobalDataScript.reEnterRoomData = JsonMapper.ToObject<RoomJoinResponseVo> (response.message);

		for (int i = 0; i < GlobalDataScript.reEnterRoomData.playerList.Count; i++) {
			AvatarVO itemData =	GlobalDataScript.reEnterRoomData.playerList [i];
			if (itemData.account.openid == GlobalDataScript.loginResponseData.account.openid) {
				GlobalDataScript.loginResponseData.account.uuid = itemData.account.uuid;
				ChatSocket.getInstance ().sendMsg (new LoginChatRequest(GlobalDataScript.loginResponseData.account.uuid));
				break;
			}
		}

		GlobalDataScript.gamePlayPanel =  PrefabManage.loadPerfab ("Prefab/Panel_GamePlay");
		removeListener ();
		Destroy (this);
		Destroy (gameObject);
	
	}


	private void resetClickNum(){
		tapCount = 0;
	}


}
