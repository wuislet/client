using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace AssemblyCSharp
{
    [Serializable]
    public class ShuaiJiuYaoVo
    {
        public List<int> cardList;  // 扔九幺（一、九牌和东南西北中发白牌）牌组
        public int avatarIndex;     // L-左，T-上,R-右，B-下（自己的方位永远都是在下方）  根据一个人在数组里的索引，得到这个人所在的方位，

        public ShuaiJiuYaoVo()
        {

        }
    }

}