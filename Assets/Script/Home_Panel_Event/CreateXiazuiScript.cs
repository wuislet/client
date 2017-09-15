using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;

public class CreateXiazuiScript : MonoBehaviour {
    public void onXiazuiCallback(ClientResponse Xiazuirespone)
    {
    }

    public void onStartXiazuiCallback(ClientResponse StartXiazuiRespone)
    {
        print("   wxd>>>   onStartXiazuiCallback");     
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
        SocketEventHandle.getInstance().StartXiazuiCallBack += onStartXiazuiCallback;
    }
}
