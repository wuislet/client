using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XZbeishuDisplay : MonoBehaviour {
    // 游戏开始界面  下嘴界面选择/ 跳转
    public GameObject xiazui_147;
    public GameObject xiazui_258;
    public GameObject xiazui_369;
    public GameObject xiazui_beishu;
    Toggle to_147;
    Toggle to_258;
    Toggle to_369;
	void Start () {
        to_147 = xiazui_147.GetComponent<Toggle>();
        to_258 = xiazui_258.GetComponent<Toggle>();
        to_369 = xiazui_369.GetComponent<Toggle>();
	}
	
	void Update () {
        showMultiple();
	}

    void showMultiple()
    {
        if (to_147.isOn == true || to_258.isOn == true || to_369.isOn == true)
        {
            xiazui_beishu.SetActive(true);
        }
        else {
            xiazui_beishu.SetActive(false);
        }
        
    }
}
