using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_BehaviorTree : ScriptableObject
    {
        
        [HideInInspector] public List<FAED_Node> nodes = new List<FAED_Node>();
        [HideInInspector] public FAED_Node rootNode;

        public void SettingRootNode()
        {

            rootNode = nodes.Find(x => x as FAED_RootNode != null);

        }

    }


}