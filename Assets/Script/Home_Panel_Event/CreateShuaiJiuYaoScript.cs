using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;

/// <summary>
/// 甩九幺
/// </summary>
public class CreateShuaiJiuYaoScript : MonoBehaviour
{
    private void onStartShuaiJiuYaoCallback(ClientResponse SJYrespone)
    {
        GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/ShuaiJiuYao_Panel");
    }

    private void gameReadyNotice(ClientResponse response)
    {
        //显示甩牌的结果
    }

    private void onShuaiJiuYaoCallBack(ClientResponse SJYrespone)
    {
        ShuaiJiuYaoVo SJYvo = JsonMapper.ToObject<ShuaiJiuYaoVo>(SJYrespone.message);
        GlobalDataScript.shuaijiuyaoVo = SJYvo;
    }

    public void addListener()
    {
        SocketEventHandle.getInstance().StartShuaiJiuYaoCallback += onStartShuaiJiuYaoCallback;
        SocketEventHandle.getInstance().StartGameNotice += gameReadyNotice;
        SocketEventHandle.getInstance().ShuaiJiuYaoCallback += onShuaiJiuYaoCallBack;
    }

    public void removeListener()
    {
        SocketEventHandle.getInstance().StartShuaiJiuYaoCallback -= onStartShuaiJiuYaoCallback;
        SocketEventHandle.getInstance().StartGameNotice -= gameReadyNotice;
        SocketEventHandle.getInstance().ShuaiJiuYaoCallback -= onShuaiJiuYaoCallBack;
    }
}