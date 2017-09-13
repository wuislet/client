using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class GangpaiObj{
	public int cardPiont;//出牌的下标
	public int uuid;//出牌的玩家
	public string type;
}

//public class PengpaiObj{
	//public string cardPoints;//出牌的玩家
//}

public class HuipaiObj{
	public int cardPiont;//出牌的下标
	public int uuid;
	public string type;
}

public class ChipaiObj{
	public string[] cardPionts;//出牌的下标

}
	


/**
 * 
 * 
 */ 
public class SignalOverItemScript : MonoBehaviour {

	public Text nickName;
	public Text resultDes;
	public GameObject huFlagImg;
	public Text totalScroe;
	public Text fanCount;
	public Text gangScore;
	public GameObject paiArrayPanel;
	public Image zhongMaFlag;//中码标记
	public GameObject GenzhuangFlag;


	//public GameObject subContaner ;
	//public GameObject chiContaner;
	//public GameObject pengContaner;
	//public GameObject gangContaner;	
	//public GameObject huContaner;

	private List<GangpaiObj> gangPaiList = new List<GangpaiObj>();//杠牌列表
	private string[] pengpaiList ;//碰牌列表
	private List<ChipaiObj> chipaiList = new List<ChipaiObj>();//吃牌列表
	private List<int> maPais;//码牌数组

	private HuipaiObj hupaiObj = new HuipaiObj();//胡牌列表


	private string mdesCribe = "";//对结果展示字符串
	private int[] paiArray;//牌列表

	private List<int> validMas;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setUI(HupaiResponseItem itemData,List<int> validMasParm ,int mainuuid){
		validMas = validMasParm;
		nickName.text = itemData.nickname;
		totalScroe.text = itemData.totalScore + "";
		gangScore.text = itemData.gangScore + "";
        fanCount.text = (itemData.totalScore - itemData.gangScore) + "";

		paiArray = itemData.paiArray;
		huFlagImg.SetActive(false);
		if (itemData.totalInfo.genzhuang == "1" && itemData.uuid == GlobalDataScript.mainUuid) {
			GenzhuangFlag.SetActive(true);
		} else {
			GenzhuangFlag.SetActive(false);
		}
		analysisPaiInfo(itemData);
	}

    
	private void analysisPaiInfo(HupaiResponseItem parms){
		var itemData = parms.totalInfo;
		string gangpaiStr = itemData.gang;
		if (gangpaiStr != null && gangpaiStr.Length > 0) {
			string[] gangtemps = gangpaiStr.Split(',');
			for (int i = 0; i < gangtemps.Length; i++) {
                var itemList = gangtemps[i].Split(':');
                GangpaiObj gangpaiObj = new GangpaiObj();
                gangpaiObj.uuid = int.Parse(itemList[0]);
                gangpaiObj.cardPiont = int.Parse(itemList[1]);
                gangpaiObj.type = itemList[2];
				//增加判断是否为自己的杠牌的操作

				paiArray [gangpaiObj.cardPiont] -= 4;
				gangPaiList.Add (gangpaiObj);

				if (gangpaiObj.type == "an") {
					mdesCribe += "暗杠  ";
				} else {
					mdesCribe += "明杠  ";
				}
			}
		}


		string pengpaiStr = itemData.peng;
		if (pengpaiStr != null && pengpaiStr.Length > 0) {
			
			pengpaiList = pengpaiStr.Split(',');


			//string[] pengpaiListTTT = pengpaiList;
			List<string> pengpaiListTTT = new List<string>();
			for (int i = 0; i <pengpaiList.Length; i++) {
				if (paiArray [int.Parse (pengpaiList [i])] >= 3) {
					paiArray [int.Parse (pengpaiList [i])] -= 3;
					pengpaiListTTT.Add (pengpaiList [i]);
				}

			}
			pengpaiList = pengpaiListTTT.ToArray ();
		}


		string chipaiStr = itemData.chi;
		if (chipaiStr != null && chipaiStr.Length > 0) {
			string[] chitemps = chipaiStr.Split(',');
			for (int i = 0; i < chitemps.Length; i++) {
				string item = chitemps[i];
				ChipaiObj chipaiObj = new ChipaiObj ();
				string[] pointStr = item.Split(':'); 
				chipaiObj.cardPionts = pointStr;
				chipaiList.Add (chipaiObj);
				paiArray [int.Parse(chipaiObj.cardPionts[0])] -= 1;
				paiArray [int.Parse(chipaiObj.cardPionts[1])] -= 1;
				paiArray [int.Parse(chipaiObj.cardPionts[2])] -= 1;
			}

		}
        

		string hupaiStr = itemData.hu;
		if(!string.IsNullOrEmpty(hupaiStr)){
            var strList = hupaiStr.Split(':');
            hupaiObj.uuid = int.Parse(strList[0]);
			hupaiObj.cardPiont = int.Parse(strList[1]);
			hupaiObj.type = strList[2];
            //增加判断是否是自己胡牌的判断

            var huType = MyMahjongScript.checkAvarHupai(itemData.hu);
            if (huType == 0)
            {
                mdesCribe += "点炮";
            }
            else
            {
                if (huType == 1)
                {
                    mdesCribe += "接炮";
                }
                else if (huType == 2)
                {
                    mdesCribe += "自摸";
                }

                if (hupaiObj.type == "qingyise")
                {
                    mdesCribe += "清一色";
                }
                if (hupaiObj.type == "qixiaodui")
                {
                    mdesCribe += "七小对";
                }
                if (hupaiObj.type == "gangshanghua")
                {
                    mdesCribe += "杠上花";
                }
                huFlagImg.SetActive(true);
                paiArray[hupaiObj.cardPiont] -= 1;
            }
		}
		
		resultDes.text = mdesCribe;
		maPais = parms.getMaPoints ();
		arrangePai (hupaiStr);
	}


	/**整理牌**/
	private void arrangePai(string hu){
		
		float startPosition = 30f;
		GameObject itemTemp;

		int subPaiConut = 0;
		if(gangPaiList!=null){
			for (int i = 0; i < gangPaiList.Count; i++) {
				GangpaiObj itemgangData = gangPaiList [i];
				for (int j = 0; j < 4; j++) {

					itemTemp = Instantiate (Resources.Load ("Prefab/ThrowCard/TopAndBottomCard")) as GameObject;
					itemTemp.transform.parent = paiArrayPanel.transform;
				//	itemTemp.transform.localScale = new Vector3(0.8f,0.8f,1f);
					itemTemp.transform.localScale = Vector3.one;
					itemTemp.GetComponent<TopAndBottomCardScript> ().setPoint (itemgangData.cardPiont);
					itemTemp.transform.localPosition = new Vector3 (startPosition+((i*4)+j)*36f, 0, 0);

				}
			}
			startPosition = startPosition + (gangPaiList.Count > 0 ? (gangPaiList.Count * 4 * 36f +8f) : 0f);
		}



		if (pengpaiList != null) {
			for (int i = 0; i < pengpaiList.Length; i++) {
				string cardPoint = pengpaiList [i];
				for (int j = 0; j < 3; j++) {

					itemTemp = Instantiate (Resources.Load ("Prefab/ThrowCard/TopAndBottomCard")) as GameObject;
					itemTemp.transform.parent = paiArrayPanel.transform;
					//itemTemp.transform.localScale = new Vector3(0.8f,0.8f,1f);
					itemTemp.transform.localScale = Vector3.one;
					itemTemp.GetComponent<TopAndBottomCardScript> ().setPoint (int.Parse(cardPoint));

					itemTemp.transform.localPosition = new Vector3 (startPosition+((i*3)+j)*36f, 0, 0);


				}
			}
			startPosition =startPosition+ (pengpaiList.Length > 0 ? (pengpaiList.Length * 3 * 36f + 8f) : 0f);

		}



		if (chipaiList != null) {
			for (int i = 0; i < chipaiList.Count; i++) {
				ChipaiObj itemgangData = chipaiList [i];
				for (int j = 0; j < 3; j++) {

					itemTemp = Instantiate (Resources.Load ("Prefab/ThrowCard/TopAndBottomCard")) as GameObject;
					itemTemp.transform.parent = paiArrayPanel.transform;
					//itemTemp.transform.localScale = new Vector3(0.8f,0.8f,1f);
					itemTemp.transform.localScale = Vector3.one;
					itemTemp.GetComponent<TopAndBottomCardScript> ().setPoint (int.Parse(itemgangData.cardPionts[j]));

					itemTemp.transform.localPosition = new Vector3 (startPosition+((i*3)+j)*36f, 0, 0);


				}
			}

			startPosition =startPosition+  (chipaiList.Count > 0 ? (chipaiList.Count * 3 * 36f + 8f) : 0f);
		}


		for(int i = 0; i < paiArray.Length; i++){
			
			if (paiArray [i] > 0) {

				for (int j = 0; j < paiArray[i]; j++) {
					itemTemp = Instantiate (Resources.Load ("Prefab/ThrowCard/TopAndBottomCard")) as GameObject;
					itemTemp.transform.parent = paiArrayPanel.transform;
					itemTemp.transform.localScale = Vector3.one;
					itemTemp.GetComponent<TopAndBottomCardScript>().setPoint(i);

					itemTemp.transform.localPosition = new Vector3(startPosition + subPaiConut * 36f, 0, 0);

					subPaiConut += 1;
				}

			}

		}
		MyDebug.Log ("subPaiConut:"+subPaiConut);

		startPosition = startPosition + (subPaiConut * 36f + 8f);
		if (hupaiObj != null) {
			if (hupaiObj.type == "zi_common" || hupaiObj.type == "d_self" || hupaiObj.type == "qingyise"
			   || hupaiObj.type == "qixiaodui" || hupaiObj.type == "gangshanghua") {
				itemTemp = Instantiate (Resources.Load ("Prefab/ThrowCard/TopAndBottomCard")) as GameObject;
				itemTemp.transform.parent = paiArrayPanel.transform;
				//itemTemp.transform.localScale = new Vector3 (0.8f, 0.8f, 1f);
				itemTemp.transform.localScale = Vector3.one;
				itemTemp.GetComponent<TopAndBottomCardScript> ().setPoint (hupaiObj.cardPiont);
				itemTemp.transform.localPosition = new Vector3 (startPosition, 0, 0);
			}
			startPosition = startPosition + 36f + 52f;
		} else {
			startPosition = startPosition  + 52f;
		}


        //显示中码牌信息==>广东麻将单独处理
		if (GlobalDataScript.roomVo.roomType == GameConfig.GAME_TYPE_GUANGDONG && MyMahjongScript.checkAvarHupai(hu) == 2)
        {
            for (int i = 0; i < validMas.Count; i++)
            {
                itemTemp = Instantiate(Resources.Load("Prefab/ThrowCard/ZhongMa")) as GameObject;
                zhongMaFlag.transform.gameObject.SetActive(true);
                itemTemp.transform.parent = paiArrayPanel.transform;
                itemTemp.GetComponent<TopAndBottomCardScript>().setPoint(validMas[i]);
                itemTemp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                itemTemp.transform.localPosition = new Vector3((20 + i) * 36f, 0, 0);                
            }
        }
        else
        {
            if (maPais != null && maPais.Count > 0)
            {
                for (int i = 0; i < maPais.Count; i++)
                {
                    itemTemp = Instantiate(Resources.Load("Prefab/ThrowCard/ZhongMa")) as GameObject;
                    if (isMaValid(maPais[i]))
                    {
                        zhongMaFlag.transform.gameObject.SetActive(true);
                    }
                    else {
                        zhongMaFlag.transform.gameObject.SetActive(false);
                    }

                    itemTemp.transform.parent = paiArrayPanel.transform;
                    itemTemp.GetComponent<TopAndBottomCardScript>().setPoint(maPais[i]);
                    itemTemp.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
                    itemTemp.transform.localPosition = new Vector3((20 + i) * 36f, 0, 0);
                }
            }
		}

		if (GlobalDataScript.roomVo.roomType == GameConfig.GAME_TYPE_HUASHUI) {
			itemTemp = Instantiate (Resources.Load ("Prefab/Image_yu")) as GameObject;
			itemTemp.transform.parent = paiArrayPanel.transform;
			itemTemp.GetComponent<yuSetScript> ().setCount (GlobalDataScript.roomVo.xiaYu);
			itemTemp.transform.localScale =  Vector3.one;
			itemTemp.transform.localPosition = new Vector3 (20*36f, 0, 0);
		}
	}
    

	private bool isMaValid(int cardPonit){
		for (int i = 0; i < validMas.Count; i++) {
			if (cardPonit == validMas [i]) {
				return true;		
			}
		}
		return false;
	}
}
