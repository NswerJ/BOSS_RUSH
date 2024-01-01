using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public class FAED_RootNode : FAED_Node
    {

        public FAED_Node children;

        public override void Init(Transform trm)
        {

            children.Init(trm);

        }

        protected override FAED_NodeState OnExecute()
        {

            return children.Execute();

        }

        public override FAED_Node Copy()
        {

            var node = Instantiate(this);
            node.children = children.Copy();

            return node;

        }

    }

}