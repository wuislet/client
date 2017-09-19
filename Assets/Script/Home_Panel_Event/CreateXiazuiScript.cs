using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;



public class CreateXiazuiScript : MonoBehaviour {

    public void onXiazuiCallback(ClientResponse Xiazuirespone)
    {
        print(">>>>>>>>>>>>>>>>>>>>>>  信息 <<<<<<<<<<<<<<<<<" + Xiazuirespone.message);
        XiazuiVO xiazuivo = JsonMapper.ToObject<XiazuiVO>(Xiazuirespone.message);
        GlobalDataScript.xiazuiVo = xiazuivo;
    }

    public void onStartXiazuiCallback(ClientResponse StartXiazuiRespone)
    {    
        GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/xiazui_btnList");   
    }

    public void gameReadyNotice(ClientResponse response)
    {
    }

    public void addListener()
    {
        SocketEventHandle.getInstance().gameReadyNotice += gameReadyNotice;
        SocketEventHandle.getInstance().XiazuiCallBack += onXiazuiCallback;
        SocketEventHandle.getInstance().StartXiazuiCallBack += onStartXiazuiCallback;
    }

    public void removeListener()
    {
        SocketEventHandle.getInstance().gameReadyNotice -= gameReadyNotice;
        SocketEventHandle.getInstance().XiazuiCallBack -= onXiazuiCallback;
        SocketEventHandle.getInstance().StartXiazuiCallBack -= onStartXiazuiCallback;
    }
}
