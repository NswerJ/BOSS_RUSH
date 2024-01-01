using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_SelectorNode : FAED_CompositeNode
    {

        private int count;

        protected override void Enable()
        {

            count = 0;

        }

        protected override FAED_NodeState OnExecute()
        {

            var state = childrens[count].Execute();

            if (state == FAED_NodeState.Failure)
            {

                count++;

                if (count == childrens.Count) return FAED_NodeState.Failure;
                else return FAED_NodeState.Running;

            }

            return state;

        }

    }

}