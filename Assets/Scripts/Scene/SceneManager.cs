using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lords
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        public GameObject Messenger1, Messenger2;//General1,General2//, Cavalary, Archer, Pikeman;
        private int currentUnitIDNum = 1;
        private static Dictionary<int, Unit> PresentUnits;

        public GeneralUnit GeneralFraction1, GeneralFraction2;
        
        public GameObject arrowPrefab;

        public InAudioEvent MessengerDeliverEvent, CavalryAttackEvent, ArcherAttackEvent, PickmanAttackEvent, VictoryBlast;

        public GameObject RestartScreen;

        public DataManager _dataManager;

        public CommandManager _commandManager;
        
        public void Restart()
        {
            Destroy(gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        //public Animator LeftPanel, RightPanel;
        protected override void Init()
        {
            base.Init();
            _dataManager=new DataManager();
            _commandManager=new CommandManager();
            _dataManager.Init();
            _commandManager.Init();
            PresentUnits=new Dictionary<int, Unit>();
        }

        private void Start()
        {
            /*if (GeneralFraction1 != null)
            {
                RegisterUnit(GeneralFraction1);
            }
            if (GeneralFraction2 != null)
            {
                RegisterUnit(GeneralFraction2);
            }*/
        }

        void Update()
        {
            if (GeneralFraction1 == null || GeneralFraction2 == null)
            {
                RestartScreen.SetActive(true);
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

        public Vector3 QueryUnitPosition(int unitID)
        {
            Unit queryUnit;
            PresentUnits.TryGetValue(unitID,out queryUnit);
            if(queryUnit!=null)
            return queryUnit.transform.position;
            else
            {
                return Vector3.one*1000;
            }
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