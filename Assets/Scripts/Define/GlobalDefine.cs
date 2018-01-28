
public static class GlobalDefine  {
	public enum UnitClass{
		Messenger=0,
		General=1,
		Cavalry=2,
		Archer=3,
		Pikeman=4,
	}
	public enum Fraction{
		Netrual=0,
		One=1,//默认左边
		Two=2//默认右边
	}
	public enum UnitState
	{
		Standing=0,
		Patrolling,
		BeingPiked,
		PursuingTarget
	}

	public enum CommandState
	{
		idle=0,
		Executing=1
	}

	public enum CommandType
	{
		Unable=0,
		DirectControl=1,
		MessengerControl=2
	}


	public static class PathDefines{
		public const string XML_Path="/StreamingAssets/XML/";
		public const string TROOP_PREFAB="Prefab/Units/";
		public const string UI_PREFAB="Prefab/UI/";
	}
	public static class FileName{
		public const string Fraction="Fractions.XML";
		public const string SoldierType="Soldiers.XML";

	}
	public static class ObjectTag{
		public const string SpawnPointTag="SpawnPoint";
		public const string Fraction0Tag="Fraction0Unit";
		public const string Fraction1Tag="Fraction1Unit";
		public const string Fraction2Tag="Fraction2Unit";

	}
}
