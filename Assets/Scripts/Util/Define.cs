
public class Define
{
    public const float SCREEN_WIDTH = 10.8f;
    public const float SCREEN_HEIGHT = 19.2f;
    
    public struct PELAGIC
	{
		public struct NAME
		{
			public const string EPIPELAGIC = "표해수층";
			public const string MESOPELAGIC = "중심해층";
			public const string BATHYPELAGIC = "점심해수층";
			public const string ABYSSOPELAGIC = "심해저대";
			public const string HADALPELAGIC = "초심해저대";
		}

		public struct DEPTH
		{
			public const int EPIPELAGIC = 200;
			public const int MESOPELAGIC = 1000;
			public const int BATHYPELAGIC = 3000;
			public const int ABYSSOPELAGIC = 6000;
			public const int HADALPELAGIC = int.MaxValue;
		}

		public static string GetName(int depth)
		{
			if (depth < DEPTH.EPIPELAGIC)
				return NAME.EPIPELAGIC;
			else if (depth < DEPTH.MESOPELAGIC)
				return NAME.MESOPELAGIC;
			else if (depth < DEPTH.BATHYPELAGIC)
				return NAME.BATHYPELAGIC;
			else if (depth < DEPTH.ABYSSOPELAGIC)
				return NAME.ABYSSOPELAGIC;
			else
				return NAME.HADALPELAGIC;
		}
	}

	public static string DepthToName(int depth)
    {
		if (depth <= PELAGIC.DEPTH.EPIPELAGIC) {
			return PELAGIC.NAME.EPIPELAGIC;
		} else if (depth <= PELAGIC.DEPTH.MESOPELAGIC) {
			return PELAGIC.NAME.MESOPELAGIC;
		} else if (depth <= PELAGIC.DEPTH.BATHYPELAGIC) {
			return PELAGIC.NAME.BATHYPELAGIC;
		} else if (depth <= PELAGIC.DEPTH.ABYSSOPELAGIC) {
			return PELAGIC.NAME.ABYSSOPELAGIC;
		} else if (depth <= PELAGIC.DEPTH.HADALPELAGIC) {
			return PELAGIC.NAME.HADALPELAGIC;
		} 

		return "";
    }

}

