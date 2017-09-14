using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using LitJson;

public class EnterRoomScript : MonoBehaviour
{

    public Button button_sure, button_delete;//确认删除按钮

    private List<String> inputChars;//输入的字符
    public List<Text> inputTexts;

    public List<GameObject> btnList;
    public Image watingPanel;
    // Use this for initialization
    void Start()
    {
        SocketEventHandle.getInstance().JoinRoomCallBack += onJoinRoomCallBack;
        inputChars = new List<String>();
        for (int i = 0; i < btnList.Count; i++)
        {
            GameObject gobj = btnList[i];
            btnList[i].GetComponent<Button>().onClick.AddListener(delegate () {
                this.OnClickHandle(gobj);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cancle()
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        watingPanel.gameObject.SetActive(false);


    }

    public void OnClickHandle(GameObject gobj)
    {
		
        //if(eventData.button)
        MyDebug.Log(gobj);
        clickNumber(gobj.GetComponentInChildren<Text>().text);
    }



    private void clickNumber(string number)
    {

		SoundCtrl.getInstance().playSoundByActionButton(1);

        if (number.Equals("100"))
        {
            clear();
            return;
        }
        MyDebug.Log(number.ToString());
        if (inputChars.Count >= 6)
        {
            return;
        }
        inputChars.Add(number);
        int index = inputChars.Count;
        inputTexts[index - 1].text = number.ToString();
        if (index == inputTexts.Count)
            sureRoomNumber();



    }

    public void number(int number)
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        if (number == 100)
        {
            clear();
            return;
        }
        MyDebug.Log(number.ToString());
        if (inputChars.Count >= 6)
        {
            return;
        }
        inputChars.Add(number + "");
        int index = inputChars.Count;
        inputTexts[index - 1].text = number.ToString();
        if (index == inputTexts.Count)
            sureRoomNumber();

    }



    public void clear()
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        inputChars.Clear();
        for (int i = 0; i < 6; i++)
        {
            inputTexts[i].text = "";
        }




    }





    public void deleteNumber()
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        if (inputChars != null && inputChars.Count > 0)
        {
            inputChars.RemoveAt(inputChars.Count - 1);
            inputTexts[inputChars.Count].text = "";
        }
    }

    public void closeDialog()
    {
		SoundCtrl.getInstance().playSoundByActionButton(1);
        MyDebug.Log("closeDialog");
        //GlobalDataScript.homePanel.SetActive (false);
        removeListener();
        Destroy(this);
        Destroy(gameObject);
    }
    public Image tip1;
    private void removeListener()
    {
        SocketEventHandle.getInstance().JoinRoomCallBack -= onJoinRoomCallBack;
    }
    public Transform parent;
    public void sureRoomNumber()
    {
        if (inputChars.Count != 6)
        {
            MyDebug.Log("请先完整输入房间号码！");
           
            TipsManagerScript.getInstance().setTips("请先完整输入房间号码！");
            return;
        }

        if (watingPanel != null)
        {
            watingPanel.gameObject.SetActive(true);
        }


        String roomNumber = inputChars[0] + inputChars[1] + inputChars[2] + inputChars[3] + inputChars[4] + inputChars[5];
        MyDebug.Log(roomNumber);
        RoomJoinVo roomJoinVo = new RoomJoinVo();
        roomJoinVo.roomId = int.Parse(roomNumber);
        string sendMsg = JsonMapper.ToJson(roomJoinVo);
        StartCoroutine(ConnectTime(4f, 1));
        CustomSocket.getInstance().sendMsg(new JoinRoomRequest(sendMsg));

        SocketEventHandle.getInstance().serviceErrorNotice += serviceErrorNotice;
    }

    public void serviceErrorNotice(ClientResponse response)
    {
        SocketEventHandle.getInstance().serviceErrorNotice -= serviceErrorNotice;
        watingPanel.gameObject.SetActive(false);
        clear();
        TipsManagerScript.getInstance().setTips(response.message);
    }

    bool connectRetruen = false;
    IEnumerator ConnectTime(float time, byte type)
    {
        connectRetruen = false;
        yield return new WaitForSeconds(time);
        if (!connectRetruen)
        {//超过5秒还没连接成功显示失败
            connectRetruen = true;

            watingPanel.gameObject.SetActive(false);
            watingPanel.gameObject.transform.gameObject.SetActive(false);
        }
    }

















    public Transform parent1;

    public void onJoinRoomCallBack(ClientResponse response)
    {
        watingPanel.gameObject.SetActive(false);
        MyDebug.Log(response);
        if (response.status == 1)
        {
            GlobalDataScript.roomJoinResponseData = JsonMapper.ToObject<RoomCreateVo>(response.message);
            GlobalDataScript.roomVo = GlobalDataScript.roomJoinResponseData;
            GlobalDataScript.surplusTimes = GlobalDataScript.roomJoinResponseData.roundNumber;
            GlobalDataScript.loginResponseData.roomId = GlobalDataScript.roomJoinResponseData.roomId;
            //loadPerfab("Prefab/Panel_GamePlay");
			GlobalDataScript.reEnterRoomData = null;

            GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/Panel_GamePlay");
            //SocketEventHandle.getInstance().gameReadyNotice += GlobalDataScript.gamePlayPanel.GetComponent<MyMahjongScript>().gameReadyNotice;
            //GlobalDataScript.gamePlayPanel.GetComponent<MyMahjongScript>().joinToRoom(GlobalDataScript.roomJoinResponseData.playerList);

            connectRetruen = true;
            closeDialog();
        }
        else {
            clear();
            TipsManagerScript.getInstance().setTips2("");
            //TipsManagerScript.getInstance();
            //watingPanel.gameObject.SetActive(true);
            //watingPanel.gameObject.transform.gameObject.SetActive(true);
            //watingPanel.gameObject.transform.FindChild("tip3/Text").GetComponent<Text>().text = response.message;

           
             TipsManagerScript.getInstance().setTips(response.message);
             closeDialog();
             GlobalDataScript.homePanel.GetComponent<HomePanelScript>().openEnterRoomDialog();

        }

    }
  


    private void loadPerfab(string perfabName)
    {
        GameObject panelCreateDialog = Instantiate(Resources.Load(perfabName)) as GameObject;
        panelCreateDialog.transform.parent = GlobalDataScript.getInstance().canvsTransfrom; ;
        panelCreateDialog.transform.localScale = Vector3.one;
        //panelCreateDialog.transform.localPosition = new Vector3 (200f,150f);
        panelCreateDialog.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        panelCreateDialog.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        panelCreateDialog.GetComponent<MyMahjongScript>().joinToRoom(GlobalDataScript.roomJoinResponseData.playerList);
    }
}
