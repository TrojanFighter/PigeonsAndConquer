
public static class GlobalDefine  {
	public enum UnitClass{
		Messenger=0,
		Knight=1,
		Archer=2,
		Pikeman=3,
	}
	public enum Fraction{
		Netrual=0,
		One=1,//默认左边
		Two=2//默认右边
	}
	public enum UnitState
	{
		Born=0,
		Advance,
		Attack,
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
