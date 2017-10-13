using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;

/// <summary>/// 甩九幺/// </summary>
public class CreateShuaiJiuYaoScript : MonoBehaviour
{

    private int bankerID;   // 庄
    public List<List<int>> SJYpaiList; //扔掉九幺牌组
   
    public MyMahjongScript cardChange;
    public MyMahjongScript cardSelect;
  
    private Vector3 oldPosition;
    public delegate void EventHandler(GameObject[] obj);
    public event EventHandler SendSJYMessage;
    public event EventHandler ReSetPoisiton;
    public bool dragFlag = false;
    public bool selected = false;

    public void onShuaiJiuYaoCallBack(ClientResponse SJYrespone)
    {
        ShuaiJiuYaoVo SJYvo = JsonMapper.ToObject<ShuaiJiuYaoVo>(SJYrespone.message);
        GlobalDataScript.sjyVo = SJYvo;
    }

    //public void onDealShuaiJiuYaoCallback(ClientResponse SJYrespone) //甩九幺的事件是在发牌之后发生的  TODO 
    //{      
    //    StartGameVO sgvo = JsonMapper.ToObject<StartGameVO>(SJYrespone.message);
    //    SJYpaiList = sgvo.paiArray;
        
    //    for (int i = 0;  i < SJYpaiList[0].Count; i++)
    //    {
    //        int a = SJYpaiList[0][i] % 9; //扔掉幺，九牌型
    //        int b = SJYpaiList[0][i] - 26;//扔掉东南西北中发白
            
    //        if (a == 1 || b > 0 || a == 1 && b > 0)
    //        {
    //            dragFlag = true;
    //            pai[SJYpaiList[0][i]].transform.localPosition = new Vector3 (transform.localPosition.x,-122f,transform.localPosition.z);
    //            Shuaipai[SJYpaiList[0][i]].transform.position = Input.mousePosition;

    //            if (GlobalDataScript.isDrag &&  selected == false)
    //            {
    //                selected = true;
    //                oldPosition = Shuaipai[SJYpaiList[0][i]].transform.localPosition;
    //            }
    //            else if (Shuaipai[SJYpaiList[0][i]].transform.localPosition.y > -122f)
    //            {
    //                if (bankerID == sgvo.bankerId)
    //                {
    //                    GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/SJY_tishi_zhuang"); // 庄家甩4张、7张、10张 九幺牌          
    //                    if (SJYpaiList[0].Count == 4)
    //                    {
    //                        Shuaipai[SJYpaiList[0][i]].onDealShuaiJiuYaoCallback += cardChange.cardChange(gameObject);
    //                        Shuaipai[SJYpaiList[0][i]].onShuaiJiuYaoCallBack += cardSelect.cardChange(gameObject);
    //                    }
    //                    else if (SJYpaiList[0].Count == 7)
    //                    {
                          
    //                    }
    //                    else if (SJYpaiList[0].Count == 10)
    //                    {
                            
    //                    }
    //                }
    //                else
    //                {
    //                    GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/SJY_tishi_player"); // 普通玩家甩3张、6张、9张 九幺牌
    //                    if (SJYpaiList.Count == 3)
    //                    {
                     
    //                    }
    //                    else if (SJYpaiList.Count == 6)
    //                    {
                           
    //                    }
    //                    else if (SJYpaiList.Count == 9)
    //                    {
                           
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (dragFlag)
    //                {
    //                    Shuaipai[i].transform.localPosition = oldPosition;
    //                }
    //            }
    //            dragFlag = false;
    //        }
    //    }
    //}

    public void gameReadyNotice(ClientResponse response)
    {
    }

    public void addListener()
    {
        SocketEventHandle.getInstance().StartGameNotice += gameReadyNotice;
        SocketEventHandle.getInstance().SJYCallBack += onShuaiJiuYaoCallBack;
        //SocketEventHandle.getInstance().DealSJYCallBack += onDealShuaiJiuYaoCallback;
    }

    public void removeListener()
    {
        SocketEventHandle.getInstance().StartGameNotice -= gameReadyNotice;
        SocketEventHandle.getInstance().SJYCallBack -= onShuaiJiuYaoCallBack;
        //SocketEventHandle.getInstance().DealSJYCallBack -= onDealShuaiJiuYaoCallback;
    }
}


            
        