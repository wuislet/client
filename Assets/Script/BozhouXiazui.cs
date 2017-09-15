using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BozhouXiazui : MonoBehaviour {
    public GameObject xiazuiList;
    //public GameObject xiazuiSelect;
   
    void Start () {
 
	}
	
	
	void Update () {
  
	}
    public void showSelect()
    {
        xiazuiList.SetActive(false);
        //xiazuiSelect.SetActive(true);
        GlobalDataScript.gamePlayPanel = PrefabManage.loadPerfab("Prefab/Room_Game_xiazuiSelect");
    }

    public void cancelXiazui()
    {
        xiazuiList.SetActive(false);       
    }
}
