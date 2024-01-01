using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_RepeatNode : FAED_DecoratorNode
    {
        protected override FAED_NodeState OnExecute()
        {

            children.Execute();

            return FAED_NodeState.Running;
        }

    }

}