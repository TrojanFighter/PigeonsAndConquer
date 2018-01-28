using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        public Unit Messenger,General, Cavalary, Archer, Pikeman;
        private int currentUnitIDNum = 1;
        private static Dictionary<int, Unit> PresentUnits;

        public GeneralUnit GeneralFraction1, GeneralFraction2;
        
        public GameObject arrowPrefab;

        public InAudioEvent MessengerDeliverEvent, KnightAttackEvent, ArcherAttackEvent, PickmanAttackEvent, VictoryBlast;
        //public Animator LeftPanel, RightPanel;
        private void Awake()
        {
            Instance = this;
            PresentUnits=new Dictionary<int, Unit>();
        }

        private void Start()
        {
            if (GeneralFraction1 != null)
            {
                RegisterUnit(GeneralFraction1);
            }
            if (GeneralFraction2 != null)
            {
                RegisterUnit(GeneralFraction2);
            }
        }

        public int RegisterUnit(Unit unit)
        {
            currentUnitIDNum++;
            unit.UnitID = currentUnitIDNum;
            PresentUnits.Add(currentUnitIDNum,unit);
            return currentUnitIDNum;
        }

        public bool UnRegisterUnit(int unitID)
        {
            if (PresentUnits.ContainsKey(unitID))
            {
                PresentUnits.Remove(unitID);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Unit QueryUnitID(int unitID)
        {
            Unit queryUnit;
            PresentUnits.TryGetValue(unitID,out queryUnit);
            return queryUnit;
        }

        public GeneralUnit FindGeneral(GlobalDefine.Fraction fraction)
        {
            switch (fraction)
            {
                    case GlobalDefine.Fraction.One:
                        return GeneralFraction1;
                        break;
                        
                        case GlobalDefine.Fraction.Two :
                            return GeneralFraction2;
                        break;
                            default: return null;
            }
        }

    }
}