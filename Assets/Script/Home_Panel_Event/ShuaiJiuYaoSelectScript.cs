using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ShuaiJiuYaoSelectScript : MonoBehaviour
{
    private Text MsgTxt;
    private Transform CardParent;
    private List<GameObject> CardList;
    private ShuaiJiuYaoVo shuaijiuyaoVO;

    private void Start()
    {
        //TODO SJY 
        //初始化所有变量
        //判断是否庄家显示不同提示
        //注册pickcard
        //For所有手牌，选出九幺牌。 同时黑掉其他牌。
        //屏蔽半个屏幕的点击
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
        //组织VO数据
        AfterSelect();
    }

    public void OnCancel()
    {
        print("  on cancel ");
        //组织VO数据
        AfterSelect();
    }

    private void AfterSelect()
    {
        //恢复牌的选择
        //恢复黑牌

        ReadyVO readyVO = new ReadyVO();
        readyVO.phase = 2;
        CustomSocket.getInstance().sendMsg(new GameReadyRequest(readyVO));
        CustomSocket.getInstance().sendMsg(new XiazuiRequest(null)); //shuaijiuyaoVO
    }
}
