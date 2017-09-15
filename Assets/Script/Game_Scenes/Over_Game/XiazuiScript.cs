using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiazuiScript : MonoBehaviour
{
    // 下嘴
    public Text xiazuiText;
    
    void Start()
    {

    }

   
    void Update()
    {

    }

    public void setCount(int count)
    {
        xiazuiText.text = "X" + count;
    }
}

