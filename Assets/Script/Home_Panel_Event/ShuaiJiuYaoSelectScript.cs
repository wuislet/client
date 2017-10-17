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

    [System.NonSerialized]
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
        InitTips();
        //遍历所有手牌，选出九幺牌，注册点击事件。黑掉其他牌。
        PoppingCard();
    }
    private void InitTips()   //判断是否庄家显示不同提示
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
        for(int i = 0; i<list.Count;i++)
        {
            bottomScript obj = list[i].GetComponent<bottomScript>();
            obj.isSpecialClick = true;
            int point = obj.getPoint();
            if ((point + 1) % 9 < 2 || point > 26) //幺九字牌。
            {
                OnPickCard(list[i]);
                obj.onSpecialClick += OnPickCard;
                //var eventlist = GlobalDataScript.GetAllEvent(obj, "onSendMessage");
                //bottomScript.EventHandler handler = null;
                //foreach (var e in eventlist)
                //{
                //    print("  for method  name " + e.Method.Name);
                    //handler.
                    // obj.onSendMessage += e.Method.MethodHandle;
                //}
                //EventHandlers.Add(i, handler);
            }
            else
            {
                obj.EnableCard(false);
            }
        }
    }

    public void OnPickCard(GameObject card)
    {
        bottomScript script = card.GetComponent<bottomScript>();
        print("   OnPickCard " + script.isSelect);
        script.SelectCard(!script.isSelect);

        if (script.isSelect)
        {
            CardList.Add(card);
        }
        else
        {
            CardList.Remove(card);
        }
    }

    public void OnConfirm()
    {
       if (CardList.Count != 0)
        {
            //庄家扔幺九牌张数为四、七、十     普通玩家扔幺九牌张数为三、六、九
            if (myScript.IsSelfBanker())
            {
                if (!(CardList.Count == 4 || CardList.Count == 7 || CardList.Count == 10))
                {
                    MsgTxt.text = "扔出的牌数不对，请扔四张、七张或者十张";
                    MsgTxt.color = Color.red;
                    return;
                }
            }
            else
            {
                if (!(CardList.Count == 3 || CardList.Count == 6 || CardList.Count == 9))
                {
                    MsgTxt.text = "扔出的牌数不对，请扔三张、六张或者九张";
                    MsgTxt.color = Color.red;
                    return;
                }
            }
        }
        shuaijiuyaoVO = new ShuaiJiuYaoVo();
        shuaijiuyaoVO.cardList = new List<int>();
        var list = myScript.handerCardList[0];
        for (int i = 0; i < list.Count; i++)
        {
            bottomScript obj = list[i].GetComponent<bottomScript>();
            int point = obj.getPoint();
            shuaijiuyaoVO.cardList.Add(point);
        }
        AfterSelect();
    }

    private void AfterSelect()
    {
        //还原牌的状态
        var list = myScript.handerCardList[0];
        for (int i = 0; i < list.Count; i++)
        {
            bottomScript obj = list[i].GetComponent<bottomScript>();
            int point = obj.getPoint();
            if ((point + 1) % 9 < 2 || point > 26) //幺九字牌。
            {
                obj.SelectCard(false);
                obj.onSpecialClick -= OnPickCard;
            }
            else
            {
                obj.EnableCard(true);
            }
        }

        foreach(var obj in CardList)
        {
            obj.SetActive(false);
        }

        ReadyVO readyVO = new ReadyVO();
        readyVO.phase = 2;
        CustomSocket.getInstance().sendMsg(new GameReadyRequest(readyVO));
        CustomSocket.getInstance().sendMsg(new ShuaiJiuYaoRequest(shuaijiuyaoVO));
        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
}
