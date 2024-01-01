using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{
    public abstract class FAED_IF_Node : FAED_DecoratorNode
    {
        protected override FAED_NodeState OnExecute()
        {

            if (Condition()) return children.Execute();
            else return FAED_NodeState.Failure;

        }

        protected abstract bool Condition();

    }

}