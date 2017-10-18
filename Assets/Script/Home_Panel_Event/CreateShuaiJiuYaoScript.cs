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
    }

    private void onShuaiJiuYaoCallBack(ClientResponse SJYrespone)
    {
        //显示甩牌的结果
        ShuaiJiuYaoVo SJYvo = JsonMapper.ToObject<ShuaiJiuYaoVo>(SJYrespone.message);
        print("  shuai jiu yao result " + SJYvo.cardList.Count);

        if (count == 9)
        {
            PrefabManage.loadPerfab("Prefab/Panel_ShuaiJiuYaoTishi");

        }

        count += 1;
        if (count == 4)
        {
            //还原牌的状态
            var list = script.handerCardList[0];
            for (int i = 0; i < list.Count; i++)
            {
                bottomScript obj = list[i].GetComponent<bottomScript>();
                obj.isSpecialClick = false;
            }
            GlobalDataScript.isDrag = script.IsSelfBanker();//结束甩九幺的时候只有庄家可以打牌。
        }
    }

    public void addListener()
    {
        SocketEventHandle.getInstance().StartShuaiJiuYaoCallback += onStartShuaiJiuYaoCallback;
        SocketEventHandle.getInstance().gameReadyNotice += gameReadyNotice;
        SocketEventHandle.getInstance().ShuaiJiuYaoCallback += onShuaiJiuYaoCallBack;
    }

    public void removeListener()
    {
        SocketEventHandle.getInstance().StartShuaiJiuYaoCallback -= onStartShuaiJiuYaoCallback;
        SocketEventHandle.getInstance().gameReadyNotice -= gameReadyNotice;
        SocketEventHandle.getInstance().ShuaiJiuYaoCallback -= onShuaiJiuYaoCallBack;
    }
}