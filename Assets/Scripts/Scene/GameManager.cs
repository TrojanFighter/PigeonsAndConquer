using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	public static Dictionary<int,SoldierType> SoldierTypes;
	public static Dictionary<int,Fraction> Fractions;

	public Unit Messenger,Knight,Archer,Pikeman;
	//public GameObject[,] SpawnPointArray;
	public BasePoint[] BasePointArray;
	public GameObject GodRayPurple,GodRayGreen,WallOfFire;
	public static Dictionary<GlobalDefine.Fraction,BasePoint> BasePoints;
	public Material[] materials;

	public InAudioEvent MessengerDeliverEvent,KnightAttackEvent,ArcherAttackEvent,PickmanAttackEvent,VictoryBlast; 
	public Animator LeftPanel,RightPanel;

	void Awake(){
		Instance = this;

		/*if (SpawnPointArray==null) {
			SpawnPointArray = new GameObject[RoadArray.Length, 2];
			for (int i = 0; i < RoadArray.Length; i++) {
				SpawnPointArray [i, 0] = RoadArray [i].transform.GetChild (0).gameObject;
				SpawnPointArray [i, 1] = RoadArray [i].transform.GetChild (1).gameObject;
			}
		}*/
		SoldierTypes = XMLReader.ReadSoldierTypeFile (Application.dataPath+GlobalDefine.PathDefines.XML_Path+GlobalDefine.FileName.SoldierType);
		Fractions = XMLReader.ReadFractionsFile (Application.dataPath+GlobalDefine.PathDefines.XML_Path + GlobalDefine.FileName.Fraction);
		BasePoints = new Dictionary<GlobalDefine.Fraction, BasePoint> ();
		BasePoints.Add(GlobalDefine.Fraction.One,BasePointArray[0]);
		BasePoints.Add(GlobalDefine.Fraction.Two,BasePointArray[1]);


	}


}
