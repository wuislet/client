using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssemblyCSharp;
using UnityEngine.UI;
using LitJson;

public class XiazuiSelectScript : MonoBehaviour {
    public List<Toggle> xiazuiList; // 下嘴组合的列表  0.不下嘴   1. 147     2. 258    3. 369
    public List<Toggle> beishu;  // 1.  5倍   2.   10倍    3. 20倍

    private XiazuiVO xiazuiVO;

    public void createXiazui()
    {
        gameObject.SetActive(false);
        int XZList = 0;  // 下嘴的组合  147  258  369
        int XZbeishu = 0;  // 下嘴的倍数  5  10   20
        for(int i=0; i<xiazuiList.Count;i++)
        {
            Toggle item = xiazuiList[i];
            if(item.isOn)
            {
                if(i == 0 ){
                    XZList = 1;   // 表示 147 组合
                }
                if(i == 1) {
                    XZList = 2;   // 表示 258 组合
                }
                if(i ==2){
                    XZList = 3;    // 表示 369组合
                }
                break;
            }
        }

        for(int i=0;i<beishu.Count;i++)
        {
            Toggle bs = beishu[i];
            if(bs.isOn)
            {
                if(i== 0){
                    XZbeishu = 5;  // 5倍
                }
                if (i == 1){
                    XZbeishu = 10;   // 10倍
                }
                if (i == 2) {
                    XZbeishu = 20;    // 20倍
                }
            }
        }

        xiazuiVO = new XiazuiVO();
        xiazuiVO.xiazuiList = XZList;
        xiazuiVO.xiazuiMultiple = XZbeishu;
        AfterSelect();
    }
 
    public void quxiaoXiazui()
    {
        gameObject.SetActive(false);
        xiazuiVO = new XiazuiVO();
        xiazuiVO.xiazuiList = 0;
        xiazuiVO.xiazuiMultiple = 0;
        AfterSelect();
    }

    private void AfterSelect()
    {
        ReadyVO readyVO = new ReadyVO();
        readyVO.phase = 1;
        CustomSocket.getInstance().sendMsg(new GameReadyRequest(readyVO));
        CustomSocket.getInstance().sendMsg(new XiazuiRequest(xiazuiVO));
    }
}
