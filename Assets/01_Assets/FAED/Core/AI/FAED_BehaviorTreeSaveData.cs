using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{
    public class FAED_BehaviorTreeSaveData : ScriptableObject
    {

        [HideInInspector] public FAED_BehaviorTree behaviorTree;
        [HideInInspector] public List<FAED_ConnectData> connectData = new List<FAED_ConnectData>();

    }

}

