using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

namespace AssemblyCSharp
{
    [Serializable]
    public class ReadyVO 
    {
        public int phase; //准备阶段
        public int tmp = 0;
        public ReadyVO()
        {

        }
    }
}
