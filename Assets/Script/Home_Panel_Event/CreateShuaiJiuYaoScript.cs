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
        print("  >>>>> + <<<<< " + SJYvo.avatarIndex);
        var curAvatarIndex = SJYvo.avatarIndex;// 0  1  2  3
        string currentDir = script.getDirection(curAvatarIndex);

        if (currentDir == DirectionEnum.Bottom)
        {
            for (int i = 0; i < SJYvo.cardList.Count; i++)
            {
                GameObject temp = script.handerCardList[0][i];
                int tempCardPoint = temp.GetComponent<bottomScript>().getPoint();
                if (tempCardPoint == script.putOutCardPoint)
                {
                    script.handerCardList[0].RemoveAt(i);
                    Destroy(temp);
                }
            }
        }
        else
        {
            List<GameObject> tempCardList = script.handerCardList[script.getIndexByDir(currentDir)];
            if (tempCardList != null)
            {
                for (int i = 0; i < SJYvo.cardList.Count; i++)//消除其他的人牌碰牌长度
                {
                    GameObject temp = tempCardList[0];
                    Destroy(temp);
                    tempCardList.RemoveAt(0);
                }
            }
        }

        if (SJYvo.cardList.Count == 3)
        {
            var panel_shuiajiuyao = PrefabManage.loadPerfab("Prefab/Panel_ShuaiJiuYaoTishi");
            if (currentDir == DirectionEnum.Bottom)
            {
                panel_shuiajiuyao.transform.localPosition = new Vector3(-480, -190);
            }
            else if (currentDir == DirectionEnum.Right)
            {
                panel_shuiajiuyao.transform.localPosition = new Vector3(480, -150);
            }

            else if (currentDir == DirectionEnum.Top)
            {
                panel_shuiajiuyao.transform.localPosition = new Vector3(320, 250);
            }

            else if (currentDir == DirectionEnum.Left)
            {
                panel_shuiajiuyao.transform.localPosition = new Vector3(-480, 300);
            }
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