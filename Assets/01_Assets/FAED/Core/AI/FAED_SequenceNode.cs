using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_SequenceNode : FAED_CompositeNode
    {

        protected int count;

        protected override void Enable()
        {

            count = 0;

        }

        protected override FAED_NodeState OnExecute()
        {
            
            var state = childrens[count].Execute();

            if(state == FAED_NodeState.Success)
            {

                count++;

                if (count == childrens.Count) return FAED_NodeState.Success;
                else return FAED_NodeState.Running;

            }

            return state;

        }

    }

}


