using UnityEngine;
using System.Collections.Generic;
using System.Xml;

namespace Lords
{

public static class XMLReader{
	public static Dictionary<int,SoldierType> ReadSoldierTypeFile(string path)
	{
		Dictionary<int,SoldierType> soldierTypeList = new Dictionary<int, SoldierType>();
		XmlDocument xDoc = new XmlDocument();
			// Android hack fix that doesn't require filepath
			if (Application.platform == RuntimePlatform.Android) {
				TextAsset soldierXML = Resources.Load<TextAsset> ("XML/Soldiers");
				xDoc.LoadXml (soldierXML.text);
			} else {
				xDoc.Load (path);
			}
		XmlNamespaceManager xnm = new XmlNamespaceManager(xDoc.NameTable);
		xnm.AddNamespace("WB", "urn:schemas-microsoft-com:office:spreadsheet");
		XmlElement root = xDoc.DocumentElement;
		XmlNodeList rows = root.SelectNodes("/WB:Workbook/WB:Worksheet/WB:Table/WB:Row", xnm);

		for (int i = 3; i < rows.Count; i++)
		{
			XmlElement rowNode = rows[i] as XmlElement;
			if (rowNode != null)
			{
				SoldierType newSoldierType = new SoldierType();
				//评论ID
				//Debug.LogWarning(GetInnerData(rowNode.ChildNodes[0]));
				newSoldierType.SoldierTypeID = int.Parse(GetInnerData(rowNode.ChildNodes[0]));

				newSoldierType.SoldierTypeName = GetInnerData(rowNode.ChildNodes[1]);

				newSoldierType.AttackType= int.Parse(GetInnerData(rowNode.ChildNodes[2]));

				newSoldierType.CommandType= int.Parse(GetInnerData(rowNode.ChildNodes[3]));
				
				newSoldierType.NormalMoveSpeed=float.Parse(GetInnerData(rowNode.ChildNodes[4]));

				newSoldierType.NormalHP=int.Parse( GetInnerData(rowNode.ChildNodes[5]));

				newSoldierType.AttackRange =float.Parse( GetInnerData (rowNode.ChildNodes [6]));
				newSoldierType.AttackTime=float.Parse(GetInnerData(rowNode.ChildNodes[7]));



				newSoldierType.NormalAttackPower= int.Parse(GetInnerData(rowNode.ChildNodes[8]));
				newSoldierType.ArmorAgainstNormalAttack =int.Parse( GetInnerData(rowNode.ChildNodes[9]));

				int MakePikeEffect=int.Parse(GetInnerData(rowNode.ChildNodes[10]));
				newSoldierType.MakePikeEffect= MakePikeEffect == 1 ? true : false;
				int BePikeAffected=int.Parse(GetInnerData(rowNode.ChildNodes[11]));
				newSoldierType.BePikeAffected= BePikeAffected == 1 ? true : false;
				int JoinCombatOrNotInt=int.Parse(GetInnerData(rowNode.ChildNodes[12]));
				newSoldierType.JoinCombat = JoinCombatOrNotInt == 1 ? true : false;
				newSoldierType.LifeTime=float.Parse(GetInnerData(rowNode.ChildNodes[13]));

				/*string GapTimeString = GetInnerData(rowNode.ChildNodes[10]);
                string[] GapTimeStringArray = GapTimeString.Split('+');

                for (int f=0;f < GapTimeStringArray.Length;f++) 
                  {
                    newComment.CharacterGapTime[f] = float.Parse(GapTimeStringArray[f]);
                }*/


				soldierTypeList.Add(newSoldierType.SoldierTypeID,newSoldierType);
			}
		}
		return soldierTypeList;
	}

	public static Dictionary<int,Fraction> ReadFractionsFile(string path)
	{
		Dictionary<int,Fraction> fractionsList = new Dictionary<int, Fraction>();
		XmlDocument xDoc = new XmlDocument();
			if (Application.platform == RuntimePlatform.Android) {
				TextAsset fractionXML = Resources.Load<TextAsset> ("XML/Fractions");
				xDoc.LoadXml (fractionXML.text);
			} else {
				xDoc.Load (path);
			}

		XmlNamespaceManager xnm = new XmlNamespaceManager(xDoc.NameTable);
		xnm.AddNamespace("WB", "urn:schemas-microsoft-com:office:spreadsheet");
		XmlElement root = xDoc.DocumentElement;
		XmlNodeList rows = root.SelectNodes("/WB:Workbook/WB:Worksheet/WB:Table/WB:Row", xnm);

		for (int i = 3; i < rows.Count; i++)
		{
			XmlElement rowNode = rows[i] as XmlElement;
			if (rowNode != null)
			{
				Fraction newFraction = new Fraction();
				//评论ID
				newFraction.FractionID = int.Parse(GetInnerData(rowNode.ChildNodes[0]));

				newFraction.FractionsMaxHP = int.Parse(GetInnerData(rowNode.ChildNodes[1]));

				newFraction.FractionsInitHP= int.Parse(GetInnerData(rowNode.ChildNodes[2]));
				int WinConditionHPInt=int.Parse( GetInnerData(rowNode.ChildNodes[3]));
				newFraction.WinConditionHP = WinConditionHPInt == 1 ? true : false;
				fractionsList.Add(newFraction.FractionID,newFraction);
			}
		}
		return fractionsList;
	}



	private static string GetInnerData(XmlNode node) {
		if (node.ChildNodes[0] != null)
		{
			return node.ChildNodes[0].InnerText;
		}
		else {
			return string.Empty;
		}
	}


}
	
}

