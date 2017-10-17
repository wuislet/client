using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;

public class CreateXiazuiScript : MonoBehaviour {

    private void onStartXiazuiCallback(ClientResponse StartXiazuiRespone)
    {
        PrefabManage.loadPerfab("Prefab/xiazui_btnList");
    }

    private void gameReadyNotice(ClientResponse response)
    {
    }

    private void onXiazuiCallback(ClientResponse Xiazuirespone)
    {
        XiazuiVO xiazuivo = JsonMapper.ToObject<XiazuiVO>(Xiazuirespone.message);
        GlobalDataScript.xiazuiVo = xiazuivo;
    }

    public void addListener()
    {
        SocketEventHandle.getInstance().StartXiazuiCallBack += onStartXiazuiCallback;
        SocketEventHandle.getInstance().gameReadyNotice += gameReadyNotice;
        SocketEventHandle.getInstance().XiazuiCallBack += onXiazuiCallback;
    }

    public void removeListener()
    {
        SocketEventHandle.getInstance().StartXiazuiCallBack -= onStartXiazuiCallback;
        SocketEventHandle.getInstance().gameReadyNotice -= gameReadyNotice;
        SocketEventHandle.getInstance().XiazuiCallBack -= onXiazuiCallback;
    }
}
