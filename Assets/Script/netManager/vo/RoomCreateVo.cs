using System;

namespace AssemblyCSharp
{
	[Serializable]
	public class RoomCreateVo
	{
		public  bool hong;
		public int ma;
		public int roomId;
		public int roomType;//1转转；2、划水；3、长沙 ；4、广东    5、亳州
		/**局数**/
		public int roundNumber;
		public bool sevenDouble;
		public int ziMo;//1：自摸胡；2、抢杠胡  
		public int xiaYu;
		public string name;
		public bool addWordCard;
		public int magnification;
        public int gui;//鬼牌 0无鬼；1白板；2翻鬼
        public bool gangHu;//可抢杠胡
        public int guiPai=-1;

       
        public int BozhouHu; // 1.推倒胡   2.断一门       
        public int xiazui;   // 1.不下嘴    2.下嘴
        public bool NolisterToBeard;   //不报听可胡
        public bool angangLiang;  // 暗杠亮
        public int BozhouZimoMagnification;//亳州麻将自摸倍数
        

        public RoomCreateVo()
		{

		}
	}
}

