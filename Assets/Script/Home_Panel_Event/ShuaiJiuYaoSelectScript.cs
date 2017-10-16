using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

/// <summary>
/// 
/// </summary>
public class ShuaiJiuYaoSelectScript : MonoBehaviour
{
    public Text MsgTxt;
    public MyMahjongScript myScript;
    private Transform CardParent;
    private List<GameObject> CardList;
    private ShuaiJiuYaoVo shuaijiuyaoVO;
    private StartGameVO gameVO ;
    

    private void Start()
    {
        //TODO SJY 
        //初始化所有变量
        CardList = new List<GameObject>();
        //判断是否庄家显示不同提示
        ThrowingPrompt();
        //注册pickcard
        //For所有手牌，选出九幺牌。 同时黑掉其他牌。
        PoppingCard();
    }
    private void ThrowingPrompt()   //扔牌提示
    {
        if (isBank())
        {
            MsgTxt.text = "庄家请扔掉四、七、十张幺、九牌";
        }
        else
            MsgTxt.text = "请扔掉三、六、九张幺、九牌";
    }

    private void PoppingCard()      //幺、九牌向上弹出
    {
        CardList = GetComponent<MyMahjongScript>().handerCardList[0];
        if (CardList.Count > 0)
        {
            for(int i =0; i<CardList.Count;i++)
            {
                bottomScript obj = CardList[i].GetComponent<bottomScript>();
                int point = obj.getPoint();
                if (point % 9 < 2 || point > 26) //幺九字牌。
                {
                    obj.SelectCard(true);
                }
                else
                {
                    obj.EnableCard(false);
                }
            }
        }        
    }

    private bool isBank()  //判断是否为庄
    {
        int bankid = myScript.bankerId;
        print("   zhuangjia  id  " + bankid);
        int bankuuid = myScript.avatarList[bankid].account.uuid;
        print("    zhuangjia  uuid" + bankuuid);
        print("    my   uuid " + GlobalDataScript.loginResponseData.account.uuid);
        if (GlobalDataScript.loginResponseData.account.uuid == bankuuid)
        {        
            return true;
        }
        else           
            return false;
    }

    public void OnBackCard()
    {
        print("   back card ");
        //返回之后，选出来
    }

    public void OnPickCard()
    {
        print("   pick card ");
    }

    public void OnConfirm()
    {
        print("  on confirm ");
        //庄家扔幺九牌张数为四、七、十     普通玩家扔幺九牌张数为三、六、九
        if (isBank())
        {
            if (CardList.Count != 4 || CardList.Count != 7 || CardList.Count != 10)
            {
                MsgTxt.text = "扔出的牌数不对，请扔四张、七张或者十张";
                MsgTxt.color = Color.red;
            }
        }
        else
        {
            if (CardList.Count != 3 || CardList.Count != 6 || CardList.Count != 9)
            {
                MsgTxt.text = "扔出的牌数不对，请扔三张、六张或者九张";
                MsgTxt.color = Color.red;
            }
        }
        gameObject.SetActive(false);
        shuaijiuyaoVO = new ShuaiJiuYaoVo();
        shuaijiuyaoVO.JiuYaoList = null;
        AfterSelect();
    }

    public void OnCancel()
    {
        print("  on cancel ");
        gameObject.SetActive(false);
        shuaijiuyaoVO = new ShuaiJiuYaoVo();
        shuaijiuyaoVO.JiuYaoList.Add(CardList.Count);
        AfterSelect();
    }

    private void AfterSelect()
    {
        //恢复牌的选择
        //恢复黑牌
        //注销pickcard

        ReadyVO readyVO = new ReadyVO();
        readyVO.phase = 2;
        CustomSocket.getInstance().sendMsg(new GameReadyRequest(readyVO));
        CustomSocket.getInstance().sendMsg(new ShuaiJiuYaoRequest(shuaijiuyaoVO));
    }
}
