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

    private ShuaiJiuYaoVo shuaijiuyaoVO;
    private StartGameVO gameVO;

    private List<GameObject> CardList;
    private Dictionary<int, bottomScript.EventHandler> EventHandlers;

    private void Start()
    {
        CardList = new List<GameObject>();
        EventHandlers = new Dictionary<int, bottomScript.EventHandler>();
        //判断是否庄家显示不同提示
        ThrowingPrompt();
        //注册pickcard
        //遍历所有手牌，选出九幺牌，注册点击事件。黑掉其他牌。
        PoppingCard();
    }
    private void ThrowingPrompt()   //扔牌提示
    {
        if (myScript.IsSelfBanker())
        {
            MsgTxt.text = "庄家请扔掉四、七、十张幺、九牌";
        }
        else
        {
            MsgTxt.text = "请扔掉三、六、九张幺、九牌";
        }
    }

    private void PoppingCard()      //幺、九牌向上弹出
    {
        var list = myScript.handerCardList[0];
        if (list.Count > 0)
        {
            for(int i =0; i<list.Count;i++)
            {
                bottomScript obj = list[i].GetComponent<bottomScript>();
                int point = obj.getPoint();
                if (point % 9 < 2 || point > 26) //幺九字牌。
                {
                    obj.SelectCard(true);
                    //EventHandlers.Add(i, obj.onSendMessage);
                }
                else
                {
                    obj.EnableCard(false);
                }
            }
        }
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
        if (myScript.IsSelfBanker())
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
        shuaijiuyaoVO.JiuYaoList.Add(CardList.Count);
        AfterSelect();
    }

    public void OnCancel()
    {
        print("  on cancel ");
        gameObject.SetActive(false);
        shuaijiuyaoVO = new ShuaiJiuYaoVo();
        shuaijiuyaoVO.JiuYaoList.Add(0);
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
