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
    public MyMahjongScript script;
    int count = 0;
    private void onStartShuaiJiuYaoCallback(ClientResponse SJYrespone)
    {
        var panel = PrefabManage.loadPerfab("Prefab/ShuaiJiuYao_Panel");
        panel.GetComponent<ShuaiJiuYaoSelectScript>().myScript = script;
    }

    private void gameReadyNotice(ClientResponse response)
    {
        print(" gameReadyNotice 1 " + count);
        count += 1;
        if (count == 4)
        {
            //显示甩牌的结果
            //还原牌的状态
            var list = script.handerCardList[0];
            for (int i = 0; i < list.Count; i++)
            {
                bottomScript obj = list[i].GetComponent<bottomScript>();
                obj.isSpecialClick = false;
            }
        }
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