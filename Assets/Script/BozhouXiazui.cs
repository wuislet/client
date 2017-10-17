using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BozhouXiazui : MonoBehaviour {

    public void showSelect()
    {
        gameObject.SetActive(false);
        PrefabManage.loadPerfab("Prefab/Room_Game_xiazuiSelect");
    }

    public void cancelXiazui()
    {
        gameObject.SetActive(false);       
    }
}
