using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace AssemblyCSharp
{
    public class XiazuiRequest : ClientRequest
    {
        public XiazuiRequest(XiazuiVO xiazuiVO)
        {
            headCode = APIS.XIAZUI_REQUEST;
            messageContent = JsonMapper.ToJson(xiazuiVO); ;
        }
    }
}