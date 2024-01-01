using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_BehaviorTreeRunner : MonoBehaviour
    {

        [SerializeField] protected FAED_BehaviorTreeSaveData aiData;

        protected FAED_BehaviorTree behaviorTree;

        protected virtual void Awake()
        {

            behaviorTree = Instantiate(aiData.behaviorTree);

        }

        protected virtual void Start()
        {
            
            behaviorTree.rootNode = behaviorTree.rootNode.Copy();
            behaviorTree.rootNode.Init(transform);

        }

        private void Update()
        {

            behaviorTree.rootNode.Execute();

        }

    }


}