using LitJson;
using System;

namespace AssemblyCSharp
{
	public class GameReadyRequest:ClientRequest
	{
		public GameReadyRequest(ReadyVO readyVO)
		{
			headCode = APIS.PrepareGame_MSG_REQUEST;
			messageContent = JsonMapper.ToJson(readyVO);
		}
	}
}

