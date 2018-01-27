using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lords
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        public Unit Messenger, Knight, Archer, Pikeman;
        
        public GameObject arrowPrefab;

        public InAudioEvent MessengerDeliverEvent, KnightAttackEvent, ArcherAttackEvent, PickmanAttackEvent, VictoryBlast;
        //public Animator LeftPanel, RightPanel;
        private void Awake()
        {
            Instance = this;
        }

    }
}