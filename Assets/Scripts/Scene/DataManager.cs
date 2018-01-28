using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lords
{
	public class DataManager :MonoSingleton<DataManager>
	{
		//public static GameManager Instance;
		public static Dictionary<int, SoldierType> SoldierTypes;
		public static Dictionary<int, Fraction> Fractions;


		protected override void Init()
		{
			base.Init();
			SoldierTypes = XMLReader.ReadSoldierTypeFile(Application.dataPath + GlobalDefine.PathDefines.XML_Path +
			                                             GlobalDefine.FileName.SoldierType);
			Fractions = XMLReader.ReadFractionsFile(Application.dataPath + GlobalDefine.PathDefines.XML_Path +
			                                        GlobalDefine.FileName.Fraction);
			//BasePoints = new Dictionary<GlobalDefine.Fraction, BasePoint>();
			//BasePoints.Add(GlobalDefine.Fraction.One, BasePointArray[0]);
			//BasePoints.Add(GlobalDefine.Fraction.Two, BasePointArray[1]);
		}

		public override void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
