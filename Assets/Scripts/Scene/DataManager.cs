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
			if (Application.platform == RuntimePlatform.Android) {
				
//				Debug.Log ("ASSET PATH: " + Application.streamingAssetsPath + GlobalDefine.PathDefines.XML_Path +
//					GlobalDefine.FileName.SoldierType);
//				Debug.Log ("FILE EXISTS: " + System.IO.File.Exists (Application.streamingAssetsPath + GlobalDefine.PathDefines.XML_Path +
//					GlobalDefine.FileName.SoldierType));
//				SoldierTypes = XMLReader.ReadSoldierTypeFile("file:///android_asset/XML/" + GlobalDefine.FileName.SoldierType);
//				Fractions = XMLReader.ReadFractionsFile(Application.streamingAssetsPath + GlobalDefine.PathDefines.XML_Path +
//					GlobalDefine.FileName.Fraction);

				SoldierTypes = XMLReader.ReadSoldierTypeFile(Application.streamingAssetsPath + GlobalDefine.PathDefines.XML_Path +
					GlobalDefine.FileName.SoldierType);
				Fractions = XMLReader.ReadFractionsFile(Application.streamingAssetsPath + GlobalDefine.PathDefines.XML_Path +
					GlobalDefine.FileName.Fraction);
			} else {
				SoldierTypes = XMLReader.ReadSoldierTypeFile(Application.dataPath + GlobalDefine.PathDefines.XML_Path +
					GlobalDefine.FileName.SoldierType);
				Fractions = XMLReader.ReadFractionsFile(Application.dataPath + GlobalDefine.PathDefines.XML_Path +
					GlobalDefine.FileName.Fraction);
			}

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
