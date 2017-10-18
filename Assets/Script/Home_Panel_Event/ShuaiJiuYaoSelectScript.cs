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
        GlobalDataScript.isDrag = true; //开始甩九幺的时候允许所有人打牌。
        InitTips();
        //遍历所有手牌，选出九幺牌，注册点击事件。黑掉其他牌。
        PoppingCard();
    }
    private void InitTips()   //判断是否庄家显示不同提示
    {
            MsgTxt.text = "请扔掉三、六、九张幺、九牌\n";
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
            if (!(CardList.Count == 3 || CardList.Count == 6 || CardList.Count == 9))
            {
                //MsgTxt.text = "扔出的牌数不对，请扔三张、六张或者九张";
                MsgTxt.color = Color.red;
                return;     
            }        
        }
        shuaijiuyaoVO = new ShuaiJiuYaoVo();
        shuaijiuyaoVO.cardList = new List<int>();
        for (int i = 0; i < CardList.Count; i++)
        {
            bottomScript obj = CardList[i].GetComponent<bottomScript>();
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
            Destroy(obj);
            myScript.handerCardList[0].Remove(obj);
        }
        myScript.SetPosition(false);      

        ReadyVO readyVO = new ReadyVO();
        readyVO.phase = 2;
        CustomSocket.getInstance().sendMsg(new GameReadyRequest(readyVO));
        CustomSocket.getInstance().sendMsg(new ShuaiJiuYaoRequest(shuaijiuyaoVO));
        gameObject.SetActive(false);
    }
}
