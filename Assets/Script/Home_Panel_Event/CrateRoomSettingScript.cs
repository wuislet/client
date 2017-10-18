using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine.SceneManagement;


public class CrateRoomSettingScript : MonoBehaviour {
	
	public GameObject panelZhuanzhuanSetting;
	public GameObject panelChangshaSetting;
	public GameObject panelHuashuiSetting;
    public GameObject panelGuangDongSetting;
    public GameObject panelDevoloping;

    public GameObject panelBoZhouSeeting;  //亳州


    /// <summary>    /// 西凉金昌麻将界面    /// </summary>
    public GameObject panelJinChang;
    public GameObject panelJinChang_shuaijiuyao;
    public GameObject panelJinChang_tuidaohu;


    public GameObject Button_zhuanzhuan1;
    public GameObject Button_zhuanzhuan;
    public GameObject Button_huashui1;
    public GameObject Button_huashui;
    public GameObject Button_changsha1;
    public GameObject Button_changsha;
    public GameObject Button_BoZhou1; // 亳州麻
    public GameObject Button_BoZhou;


    /// <summary>    ///西凉金昌麻将按钮    /// </summary>
    public GameObject Button_JinChang1;
    public GameObject Button_JinChang;
    public GameObject Button_JinChang_shuaijiuyao1;
    public GameObject Button_JinChang_shuaijiuyao;
    public GameObject Button_JinChang_tuidaohu1;
    public GameObject Button_JinChang_tuidaohu;


    public GameObject Btn_JinChang_liang;  //public GameObject Btn_zhuanZ_liang
    public GameObject Btn_JinChang_dark;   //public GameObject Btn_zhuanZ_dark
    public GameObject Btn_shuaijiuyao_liang;    //public GameObject Btn_huaS_liang
    public GameObject Btn_shuaijiuyao_dark;     //public GameObject Btn_huaS_dark
    public GameObject Btn_tuidaohu_liang;     //public GameObject Btn_run_liang
    public GameObject Btn_tuidaohu_dark;      //public GameObject Btn_run_dark

    public GameObject Btn_BoZhou_liang;//亳州麻将 按钮亮
    public GameObject Btn_BoZhou_dark;

    public Image watingPanel;
    public Image button_Create_Sure;

    public List<Toggle> zhuanzhuanRoomCards;//转转麻将房卡数
	public List<Toggle> changshaRoomCards;//长沙麻将房卡数
	public List<Toggle> huashuiRoomCards;//划水麻将房卡数
    public List<Toggle> guangdongRoomCards;//广东麻将房卡数
    public List<Toggle> BoZhouRoomCards;//亳州麻将房卡数


    public List<Toggle> JinChangRoomCards; //金昌麻将房卡数
    public List<Toggle> JinChang_shuaijiuyaoRoomCards; // 金昌麻将甩九幺房卡数
    public List<Toggle> JinChang_tuidaohuRoomCards; //金昌麻将推倒胡房卡数　


    public List<Toggle> zhuanzhuanGameRule;//转转麻将玩法
	public List<Toggle> changshaGameRule;//长沙麻将玩法
	public List<Toggle> huashuiGameRule;//划水麻将玩法
    public List<Toggle> guangdongGameRule;//广东麻将玩法
    public List<Toggle> BoZhouGameRule;//亳州麻将玩法


    public List<Toggle> JinChangGameRule; //金昌麻将玩法
    public List<Toggle> JinChang_shuaijiuyaoGameRule; // 金昌麻将甩九幺玩法
    public List<Toggle> JinChang_tuidaohuGameRule; //金昌麻将推倒胡玩法　


    public List<Toggle> zhuanzhuanZhuama;//转转麻将抓码个数
	public List<Toggle> changshaZhuama;//长沙麻将抓码个数
	public List<Toggle> huashuixiayu;//划水麻将下鱼条数
    public List<Toggle> guangdongZhuama;//广东麻将抓码个数
    public List<Toggle> bozhouselfDrawn; //亳州自摸加倍
    public List<Toggle> bozhouxiazui; // 亳州下嘴个数


    //public List<Toggle> JinChangdifen; //金昌麻将选择下底分
    public List<Toggle> JinChang_shuaijiuyaodifen; // 金昌麻将甩九幺选择下底分
    //public List<Toggle> JinChang_tuidaohudifen; //金昌麻将推倒胡选择下底分


    public List<Toggle> guangdongGui;//广东麻将鬼牌


#pragma warning disable CS0169 // 从不使用字段“CrateRoomSettingScript.roomCardCount”
    private int roomCardCount;//房卡数
#pragma warning restore CS0169 // 从不使用字段“CrateRoomSettingScript.roomCardCount”
#pragma warning disable CS0169 // 从不使用字段“CrateRoomSettingScript.gameSence”
	private GameObject gameSence;
#pragma warning restore CS0169 // 从不使用字段“CrateRoomSettingScript.gameSence”

	private RoomCreateVo sendVo;//创建房间的信息

	void Start () {
        int x = PlayerPrefs.GetInt("userDefaultMJ");
        if (x == 0)
        {
            //openZhuanzhuanSeetingPanel();
           openJinChangPanel();
        }
        else if(x == 1)
        {
            //openHuashuiSeetingPanel();
            openshuaijiuyaoPanel();
        }
        else if (x == 2)
        {
            //openBoZhouSeetingPanel();
            opentuidaohuPanel();    
        }
        else
        {
            openDevloping();
        }

        SocketEventHandle.getInstance ().CreateRoomCallBack += onCreateRoomCallback;
	}
	

	void Update () {
	
	}

    public void cancle() {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        watingPanel.gameObject.SetActive(false);
    }

     /// <summary>     /// 打开金昌麻将的界面     /// </summary>
    public void openJinChangPanel()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        PlayerPrefs.SetInt("userDefaultMJ", 0);

        Btn_JinChang_liang.SetActive(true);
        Btn_JinChang_dark.SetActive(false);
        Btn_shuaijiuyao_liang.SetActive(false);
        Btn_shuaijiuyao_dark.SetActive(true);
        Btn_tuidaohu_liang.SetActive(false);
        Btn_tuidaohu_dark.SetActive(true);

        panelJinChang.SetActive(true);
        panelJinChang_shuaijiuyao.SetActive(false);
        panelJinChang_tuidaohu.SetActive(false);
        panelDevoloping.SetActive(false);
    }
    /// <summary>     /// 打开金昌麻将甩九幺界面     /// </summary>
    public void openshuaijiuyaoPanel()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        PlayerPrefs.SetInt("userDefaultMJ", 1);

        Btn_JinChang_liang.SetActive(false);
        Btn_JinChang_dark.SetActive(true);
        Btn_shuaijiuyao_liang.SetActive(true);
        Btn_shuaijiuyao_dark.SetActive(false);
        Btn_tuidaohu_liang.SetActive(false);
        Btn_tuidaohu_dark.SetActive(true);

        panelJinChang.SetActive(false);
        panelJinChang_shuaijiuyao.SetActive(true);
        panelJinChang_tuidaohu.SetActive(false);
        panelDevoloping.SetActive(false);
    }
    /// <summary>     /// 打开金昌麻将推倒胡界面     /// </summary>
    public void opentuidaohuPanel()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        PlayerPrefs.SetInt("userDefaultMJ", 2);

        Btn_JinChang_liang.SetActive(false);
        Btn_JinChang_dark.SetActive(true);
        Btn_shuaijiuyao_liang.SetActive(false);
        Btn_shuaijiuyao_dark.SetActive(true);
        Btn_tuidaohu_liang.SetActive(true);
        Btn_tuidaohu_dark.SetActive(false);

        panelJinChang.SetActive(false);
        panelJinChang_shuaijiuyao.SetActive(false);
        panelJinChang_tuidaohu.SetActive(true);
        panelDevoloping.SetActive(false);
    }

    /***
	 * 打开转转麻将设置面板
	 */
    //public void openZhuanzhuanSeetingPanel(){

    //	SoundCtrl.getInstance().playSoundByActionButton(1);
    //       PlayerPrefs.SetInt("userDefaultMJ",0);

    //       Btn_zhuanZ_liang.SetActive(true);
    //       Btn_zhuanZ_dark.SetActive(false);
    //       Btn_huaS_liang.SetActive(false);
    //       Btn_huaS_dark.SetActive(true);
    //       Btn_run_liang.SetActive(false);
    //       Btn_run_dark.SetActive(true);

    //       panelZhuanzhuanSetting.SetActive (true);
    //	panelChangshaSetting.SetActive (false);
    //	panelHuashuiSetting.SetActive (false);
    //       panelGuangDongSetting.SetActive(false);
    //       panelDevoloping.SetActive (false);

    //       Btn_BoZhou_liang.SetActive(false);
    //       Btn_BoZhou_dark.SetActive(true);
    //       panelBoZhouSeeting.SetActive(false);
    //}

    /***
	 * 打开长沙麻将设置面板
     * 打开亳州麻将设置面板
	 */
    /*public void openChangshaSeetingPanel()*/
    //  public void openBoZhouSeetingPanel()
    //  {
    //SoundCtrl.getInstance().playSoundByActionButton(1);
    //      PlayerPrefs.SetInt("userDefaultMJ", 2);
    //      Btn_zhuanZ_liang.SetActive(false);
    //      Btn_zhuanZ_dark.SetActive(true);
    //      Btn_huaS_liang.SetActive(false);
    //      Btn_huaS_dark.SetActive(true);
    //      Btn_run_liang.SetActive(false);
    //      Btn_run_dark.SetActive(true);

    //      panelZhuanzhuanSetting.SetActive (false);
    //panelChangshaSetting.SetActive (false);
    //panelHuashuiSetting.SetActive (false);
    //      panelGuangDongSetting.SetActive(false);
    //      panelDevoloping.SetActive (false);

    //      Btn_BoZhou_liang.SetActive(true);
    //      Btn_BoZhou_dark.SetActive(false);
    //      panelBoZhouSeeting.SetActive(true);
    //  }

    /***
	 * 打开划水麻将设置面板
	 */
    //public void openHuashuiSeetingPanel(){
    //	SoundCtrl.getInstance().playSoundByActionButton(1);
    //       PlayerPrefs.SetInt("userDefaultMJ", 1);
    //       Btn_zhuanZ_liang.SetActive(false);
    //       Btn_zhuanZ_dark.SetActive(true);
    //       Btn_huaS_liang.SetActive(true);
    //       Btn_huaS_dark.SetActive(false);
    //       Btn_run_liang.SetActive(false);
    //       Btn_run_dark.SetActive(true);

    //       panelZhuanzhuanSetting.SetActive (false);
    //	panelChangshaSetting.SetActive (false);
    //	panelHuashuiSetting.SetActive (true);
    //       panelGuangDongSetting.SetActive(false);
    //       panelDevoloping.SetActive (false);

    //       Btn_BoZhou_liang.SetActive(false);
    //       Btn_BoZhou_dark.SetActive(true);
    //       panelBoZhouSeeting.SetActive(false);
    //   }

    /***
	 * 打开广东麻将设置面板
	 */
    //public void openGuangDongSeetingPanel()
    //{

    //    SoundCtrl.getInstance().playSoundByActionButton(1);
    //    PlayerPrefs.SetInt("userDefaultMJ", 3);

    //    Btn_zhuanZ_liang.SetActive(false);
    //    Btn_zhuanZ_dark.SetActive(true);
    //    Btn_huaS_liang.SetActive(false);
    //    Btn_huaS_dark.SetActive(true);
    //    Btn_run_liang.SetActive(true);
    //    Btn_run_dark.SetActive(false);

    //    panelZhuanzhuanSetting.SetActive(false);
    //    panelChangshaSetting.SetActive(false);
    //    panelHuashuiSetting.SetActive(false);
    //    panelGuangDongSetting.SetActive(true);
    //    panelDevoloping.SetActive(false);

    //    Btn_BoZhou_liang.SetActive(false);
    //    Btn_BoZhou_dark.SetActive(true);
    //    panelBoZhouSeeting.SetActive(false);

    //}

    public void Button_down()
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        Application.OpenURL("http://a.app.qq.com/o/simple.jsp?pkgname=com.pengyoupdk.poker");

    }

    public void openDevloping(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		panelZhuanzhuanSetting.SetActive (false);
		panelChangshaSetting.SetActive (false);
		panelHuashuiSetting.SetActive (false);
        panelGuangDongSetting.SetActive(false);
		panelDevoloping.SetActive (true);

        panelBoZhouSeeting.SetActive(false);

        panelJinChang.SetActive(false);
        panelJinChang_shuaijiuyao.SetActive(false);
        panelJinChang_tuidaohu.SetActive(false);
    }

	public void closeDialog(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		MyDebug.Log ("closeDialog");
		SocketEventHandle.getInstance ().CreateRoomCallBack -= onCreateRoomCallback;
		Destroy (this);
		Destroy (gameObject);
	}

	/**
	 * 创建转转麻将房间
	 */ 
	public void createZhuanzhuanRoom(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		int roundNumber = 4;//房卡数量
		bool isZimo=false;//自摸
		int gui = 0;//红中赖子
		bool isSevenDoube =false;//七小对
		int maCount = 0;
		for (int i = 0; i < zhuanzhuanRoomCards.Count; i++) {
			Toggle item = zhuanzhuanRoomCards [i];
			if (item.isOn) {
				if (i == 0) {
					roundNumber = 8;
				} else if (i == 1) {
					roundNumber = 16;
				} 
				break;
			}
		}

		if (zhuanzhuanGameRule [0].isOn) {
			isZimo = true;
		}

		if (zhuanzhuanGameRule [2].isOn) {
			gui = 3;
		}

		if (zhuanzhuanGameRule [3].isOn) {
			isSevenDoube = true;
		}


		for (int i = 0; i < zhuanzhuanZhuama.Count; i++) {
			if (zhuanzhuanZhuama [i].isOn) {
				maCount = 2 * (i + 1);
				break;
			}
		}
		sendVo = new RoomCreateVo ();
		sendVo.ma = maCount;
		sendVo.roundNumber = roundNumber;
		sendVo.huXianzhi = isZimo?1:0;
		sendVo.gui = gui;
		sendVo.sevenDouble = isSevenDoube;
		sendVo.roomType = GameConfig.GAME_TYPE_ZHUANZHUAN;
		string sendmsgstr = JsonMapper.ToJson (sendVo);
		if (GlobalDataScript.loginResponseData.account.roomcard > 0) {
            watingPanel.gameObject.SetActive(true);

            CustomSocket.getInstance ().sendMsg (new CreateRoomRequest (sendmsgstr));
		} else {
			TipsManagerScript.getInstance ().setTips ("你的房卡数量不足，不能创建房间");
		}


	}

    /**
	 * 创建广东麻将房间
	 */
    public void createGuangDongRoom()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        int roundNumber = 8;//房卡数量
        int huXianZhi = 1;//胡法限制
        bool isSevenDoube = false;//七小对
        bool isFengpai = false;//风牌
        int gui = 0; //鬼牌
                               //bool isGang = false;
        int maCount = 0;
        for (int i = 0; i < guangdongRoomCards.Count; i++)
        {
            Toggle item = guangdongRoomCards[i];
            if (item.isOn)
            {
                if (i == 0)
                {
                    roundNumber = 8;
                }
                else if (i == 1)
                {
                    roundNumber = 16;
                }
                break;
            }
        }

        if (guangdongGameRule[0].isOn)
        {
            isSevenDoube = true;
        }

        if (guangdongGameRule[1].isOn)
        {
            isFengpai = true;
        }

        if (guangdongGameRule[2].isOn)
        {
            huXianZhi = 2;
        }


        for (int i = 0; i < guangdongZhuama.Count; i++)
        {
            if (guangdongZhuama[i].isOn)
            {
                maCount = 2 * (i + 1);
                break;
            }
        }

        for (int i = 0; i < guangdongGui.Count; i++)
        {
            if (guangdongGui[i].isOn)
            {
                gui = i;
                break;
            }
        }

        sendVo = new RoomCreateVo();
        sendVo.ma = maCount;
        sendVo.roundNumber = roundNumber;
        sendVo.huXianzhi = huXianZhi;
        sendVo.addWordCard = isFengpai;
        sendVo.sevenDouble = isSevenDoube;
        sendVo.gui = gui;
        sendVo.roomType = GameConfig.GAME_TYPE_GUANGDONG;
        print(" creatre  guangdong " + sendVo);
        string sendmsgstr = JsonMapper.ToJson(sendVo);
        if (GlobalDataScript.loginResponseData.account.roomcard > 0)
        {
            watingPanel.gameObject.SetActive(true);

            CustomSocket.getInstance().sendMsg(new CreateRoomRequest(sendmsgstr));
        }
        else {
            TipsManagerScript.getInstance().setTips("你的房卡数量不足，不能创建房间");
        }


    }

    /**
	 * 创建长沙麻将房间
	 */
    public void createChangshaRoom(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		int roundNumber = 4;//房卡数量
		bool isZimo=false;//自摸
		int maCount = 0;
		for (int i = 0; i < changshaRoomCards.Count; i++) {
			Toggle item = changshaRoomCards [i];
			if (item.isOn) {
				if (i == 0) {
					roundNumber = 8;
				} else if (i == 1) {
					roundNumber = 16;
				} 			
				break;
			}
		}
		if (changshaGameRule [0].isOn) {
			isZimo = true;
		}

		for (int i = 0; i <changshaZhuama.Count; i++) {
			if (changshaZhuama [i].isOn) {
				maCount = 2 * (i + 1);
				break;
			}
		}

		sendVo = new RoomCreateVo ();
		sendVo.ma = maCount;
		sendVo.roundNumber = roundNumber;
		sendVo.huXianzhi = isZimo?1:0;
		sendVo.roomType = GameConfig.GAME_TYPE_CHANGSHA;
		string sendmsgstr = JsonMapper.ToJson (sendVo);
		if (GlobalDataScript.loginResponseData.account.roomcard > 0) {
            watingPanel.gameObject.SetActive(true);
            CustomSocket.getInstance ().sendMsg (new CreateRoomRequest (sendmsgstr));
		} else {
			TipsManagerScript.getInstance ().setTips ("你的房卡数量不足，不能创建房间");
		}

	}

	/**
	 * 创建划水麻将房间
	 */
	public void createHuashuiRoom(){
		SoundCtrl.getInstance().playSoundByActionButton(1);
		int roundNumber = 4;//房卡数量
		bool isZimo=false;//自摸
		bool isFengpai =false;//七小对
		int maCount = 0;
		for (int i = 0; i < huashuiRoomCards.Count; i++) {
			Toggle item = huashuiRoomCards [i];
			if (item.isOn) {
				if (i == 0) {
					roundNumber = 8;
				} else if (i == 1) {
					roundNumber = 16;
				} 
				break;
			}
		}
		if (huashuiGameRule [0].isOn) {
			isFengpai = true;
		}
		if (huashuiGameRule [1].isOn) {
			isZimo = true;
		}
	

		for (int i = 0; i <huashuixiayu.Count; i++) {
			if (huashuixiayu [i].isOn) {
				maCount = 2 * (i + 1)+i;
				break;
			}
		}

		sendVo = new RoomCreateVo ();
		sendVo.xiaYu = maCount;
		sendVo.roundNumber = roundNumber;
		sendVo.huXianzhi = isZimo?1:0;
		sendVo.roomType = GameConfig.GAME_TYPE_HUASHUI;
		sendVo.addWordCard = isFengpai;
		sendVo.sevenDouble = true;
		string sendmsgstr = JsonMapper.ToJson (sendVo);
		if (GlobalDataScript.loginResponseData.account.roomcard > 0) {
            watingPanel.gameObject.SetActive(true);
            CustomSocket.getInstance ().sendMsg (new CreateRoomRequest (sendmsgstr));
		} else {
			TipsManagerScript.getInstance ().setTips ("你的房卡数量不足，不能创建房间");
		}

    }

    /**
	 * 创建亳州麻将房间
	 */
    public void createBoZhouRoom()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        int roundNumber = 4;//房卡数量
        bool bozhouZiMo = false;  // 亳州麻将胡法
        bool bubaotingkehu = false;  //不报听可胡
        bool anhangliang = false;   // 暗杠亮 
        bool xiazui = false;   //下嘴
        int zimojiabei=0;
        for (int i = 0; i < BoZhouRoomCards.Count; i++)
        {
            Toggle item = BoZhouRoomCards[i];
            if (item.isOn)
            {
                if (i == 0)
                {
                    roundNumber = 4;
                }
                else if (i == 1)
                {
                    roundNumber = 8;
                }
                else if (i == 2)
                {
                    roundNumber = 16;
                }
                break;
            }
        }
        
            if(BoZhouGameRule[0].isOn){
                bozhouZiMo = true;
            }

            if (BoZhouGameRule[2].isOn){
                bubaotingkehu = true;
            }
            if(BoZhouGameRule[3].isOn){
                anhangliang = true;
            }
            
            if(bozhouxiazui[0].isOn)  {
                 xiazui = true;
            }
  
            for (int i = 0; i <bozhouselfDrawn.Count; i++) {
                if (bozhouselfDrawn[i].isOn) {
                    zimojiabei = 2 * i;
                    break;
                }
            }
        sendVo = new RoomCreateVo();
        sendVo.roundNumber = roundNumber;
        sendVo.bozhouHu = bozhouZiMo ? 1 : 0;
        sendVo.xiazui = xiazui ? 1 : 0;
        sendVo.nolisterToBeard = bubaotingkehu;
        sendVo.angangLiang = anhangliang;
        sendVo.bozhouZimoMagnification = zimojiabei;
        sendVo.roomType = GameConfig.GAME_TYPE_BOZHOU;
        sendVo.addWordCard = true;
        sendVo.sevenDouble = true;
        string sendmsgstr = JsonMapper.ToJson(sendVo);
        if (GlobalDataScript.loginResponseData.account.roomcard > 0){
            watingPanel.gameObject.SetActive(true);
            CustomSocket.getInstance().sendMsg(new CreateRoomRequest(sendmsgstr));
        } else{
            TipsManagerScript.getInstance().setTips("你的房卡数量不足，不能创建房间");
        }

    }

    /**
   * 创建金昌麻将房间
   */
    public void createJinChangRoom()
    {
         SoundCtrl.getInstance().playSoundByActionButton(1);
    }
    /**
   * 创建甩九幺麻将房间
   */
    public void createShuaiJiuYaoRoom()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
        int roundNumber = 4;//房卡数量
        int bottomScore = 1;//默认底分：1分 
        int ShuaidaihuiHu =1;  // 甩九幺胡法限制  自摸  收炮(不算胡)
        bool OneAndOneColorTrain = false;   // 清一色、一条龙

        for (int i = 0; i < JinChang_shuaijiuyaoRoomCards.Count; i++)
        {
            Toggle item = JinChang_shuaijiuyaoRoomCards[i];
            if (item.isOn){
                if (i == 0)
                    roundNumber = 4;
                else if (i == 1)
                    roundNumber = 8;
                else if (i == 2)
                    roundNumber = 16;
                break;
            }
        }
            if (JinChang_shuaijiuyaoGameRule[0].isOn)
                OneAndOneColorTrain = true;

        for (int i = 0; i < JinChang_shuaijiuyaoRoomCards.Count; i++)
        {
            Toggle var = JinChang_shuaijiuyaoRoomCards[i];
            if (var.isOn){
                if (i == 0)
                    bottomScore = 1;
                else if (i == 1)
                    bottomScore = 2;
                else if (i == 2)
                    bottomScore = 5;
                else if (i == 2)
                    bottomScore = 10;
                break;
            }
        }
            sendVo = new RoomCreateVo();
            sendVo.roundNumber = roundNumber;
            sendVo.BottomScore = bottomScore;
            sendVo.huXianzhi = ShuaidaihuiHu;
            sendVo.OneAndOneColorTrain = OneAndOneColorTrain ?1:0;
            sendVo.sevenDouble = true;
            sendVo.addWordCard = true;
            sendVo.roomType = GameConfig.GAME_TYPE_ShuaiJiuyao;
            string sendmsgstr = JsonMapper.ToJson(sendVo);
            if (GlobalDataScript.loginResponseData.account.roomcard > 0)
            {
                watingPanel.gameObject.SetActive(true);
                CustomSocket.getInstance().sendMsg(new CreateRoomRequest(sendmsgstr));
            }else{
                TipsManagerScript.getInstance().setTips("你的房卡数量不足，不能创建房间");
            }    
    }
    /**
   * 创建推倒胡麻将房间
   */
    public void createTuiDaoHuRoom()
    {
        SoundCtrl.getInstance().playSoundByActionButton(1);
    }


    //	public void toggleHongClick(){
    //
    //		if (zhuanzhuanGameRule [2].isOn) {
    //			zhuanzhuanGameRule [0].isOn = true;
    //		}
    //	}
    //
    //	public void toggleQiangGangHuClick(){
    //		if (zhuanzhuanGameRule [1].isOn) {
    //			zhuanzhuanGameRule [2].isOn = false;
    //		}
    //	}

    public void onCreateRoomCallback(ClientResponse response){
        if (watingPanel != null) {
        watingPanel.gameObject.SetActive(false);
        }
        MyDebug.Log (response.message);
		if (response.status == 1) {
            print(sendVo);
			//RoomCreateResponseVo responseVO = JsonMapper.ToObject<RoomCreateResponseVo> (response.message);
			int roomid = Int32.Parse(response.message);
			sendVo.roomId = roomid;
			GlobalDataScript.roomVo = sendVo;
			GlobalDataScript.loginResponseData.roomId = roomid;
			GlobalDataScript.loginResponseData.main = true;
			GlobalDataScript.loginResponseData.isOnLine = true;
			GlobalDataScript.reEnterRoomData = null;
			//SceneManager.LoadSceneAsync(1);
			/**
			if (gameSence == null) {
				gameSence = Instantiate (Resources.Load ("Prefab/Panel_GamePlay")) as GameObject;
				gameSence.transform.parent = GlobalDataScript.getInstance ().canvsTransfrom;
				gameSence.transform.localScale = Vector3.one;
				gameSence.GetComponent<RectTransform> ().offsetMax = new Vector2 (0f, 0f);
				gameSence.GetComponent<RectTransform> ().offsetMin = new Vector2 (0f, 0f);
				gameSence.GetComponent<MyMahjongScript> ().createRoomAddAvatarVO (GlobalDataScript.loginResponseData);
			}*/
			GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab ("Prefab/Panel_GamePlay");

			//GlobalDataScript.gamePlayPanel.GetComponent<MyMahjongScript> ().createRoomAddAvatarVO (GlobalDataScript.loginResponseData);
		
			closeDialog ();
            //closeXiazui();

		} else {
			TipsManagerScript.getInstance ().setTips (response.message);
		}
	}

}
