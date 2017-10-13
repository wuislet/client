using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	[Serializable]
	public class RoomCreateVo
	{
		public int roomId;
        public string name;
        public int roomType;//1转转；2、划水；3、长沙；4、广东；5、亳州
		public int roundNumber;//局数

        public bool addWordCard;
		public int huXianzhi;//胡法的限制 0：可以点炮；1：只能自摸；2：自摸 + 抢杠胡
        public bool sevenDouble; //能否胡七小对
        public int gui;   //鬼牌（或叫赖子，即万能牌） 0无鬼；1白板；2翻鬼；3红中
        public int guiPai = -1;

        //划水麻将
        public int xiaYu;

        //广东麻将
        public int ma;

        //亳州专属规则
        public int bozhouHu; // 1.推倒胡   2.断一门
        public int xiazui;   // 1.不下嘴   2.下嘴
        public bool nolisterToBeard;   //不报听可胡
        public bool angangLiang;  // 暗杠亮
        public int bozhouZimoMagnification;//亳州麻将自摸倍数

        public List<AvatarVO> playerList;

        public RoomCreateVo()
		{

		}

        public RoomCreateVo(RoomCreateVo other)
        {
            this.roomId = other.roomId;
            this.name = other.name;
            this.roomType = other.roomType;
            this.roundNumber = other.roundNumber;

            this.addWordCard = other.addWordCard;
            this.huXianzhi = other.huXianzhi;
            this.sevenDouble = other.sevenDouble;
            this.gui = other.gui;
            this.guiPai = other.guiPai;

            this.xiaYu = other.xiaYu;

            this.ma = other.ma;

            this.bozhouHu = other.bozhouHu;
            this.xiazui = other.xiazui;
            this.nolisterToBeard = other.nolisterToBeard;
            this.angangLiang = other.angangLiang;
            this.bozhouZimoMagnification = other.bozhouZimoMagnification;
        }

	}
}

