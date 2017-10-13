using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


namespace AssemblyCSharp
{
    public class ShuaiJiuYaoRequest : ClientRequest
    {
        public ShuaiJiuYaoRequest(ShuaiJiuYaoVo ShuaiJiuYaoVO)
        {
            headCode = APIS.SHUAIJIUYAO_REQUEST;
            messageContent = JsonMapper.ToJson(ShuaiJiuYaoVO); ;
        }
    }
}
