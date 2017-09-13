using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

namespace AssemblyCSharp
{
    [Serializable]
    public class XiazuiVO 
    {
        //public bool xaizui;// 是否下嘴
        public int xiazuiList; // 不同下嘴组合的列表  1. 147     2. 258    3. 369
        public int xiazuiMultiple; // 下嘴倍数   1.  5倍   2.   10倍    3. 20倍

        public XiazuiVO()
        {

        }
    }
}
