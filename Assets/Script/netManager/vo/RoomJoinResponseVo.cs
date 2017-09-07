using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	[Serializable]
	public class RoomJoinResponseVo
	{
		public bool addWordCard;
		public bool hong;
		public int ma;
		public string name;
		public int roomId;
		public int roomType;
		public int roundNumber;
		public bool sevenDouble;
		public int xiaYu;
		public int ziMo;
		public int magnification;
        public int gui;//鬼牌 0无鬼；1白板；2翻鬼
        public bool gangHu;//可抢杠胡
        public List<AvatarVO> playerList;
        public int guiPai;
		//public LastOperationVo lastOperationVo;
		public RoomJoinResponseVo ()
		{
		}
	}
}

