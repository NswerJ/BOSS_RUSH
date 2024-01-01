using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Dev.AI
{

    public enum FAED_NodeState
    {

        Success,
        Failure,
        Running

    } 

    public abstract class FAED_Node : ScriptableObject
    {

        [HideInInspector] public string guid;
        [HideInInspector] public Rect editorPos;

        protected FAED_NodeState state;
        protected bool started;

        public FAED_NodeState Execute()
        {

            if (!started)
            {

                Enable();
                started = true;

            }

            state = OnExecute();

            if (state == FAED_NodeState.Failure || state == FAED_NodeState.Success)
            {

                Disable();
                started = false;

            }

            return state;

        }

        public virtual FAED_Node Copy()
        {

            return Instantiate(this);

        }
        public virtual void Init(Transform trm) { }

        public void Breaking()
        {

            Disable();
            started = false;

        }

        protected virtual void Enable() { }
        protected virtual void Disable() { }
        protected abstract FAED_NodeState OnExecute();


    }

    public abstract class FAED_ActionNode : FAED_Node { }

    public abstract class FAED_CompositeNode : FAED_Node
    {

        [HideInInspector] public List<FAED_Node> childrens = new List<FAED_Node>();

        public override FAED_Node Copy()
        {

            var node = Instantiate(this);




            node.childrens = new List<FAED_Node>();

            foreach(var ch in childrens)
            {

                node.childrens.Add(ch.Copy());

            }

            return node;

        }

        public override void Init(Transform trm)
        {

            childrens.ForEach(x => x.Init(trm));

        }

    }

    public abstract class FAED_DecoratorNode : FAED_Node 
    {

        [HideInInspector] public FAED_Node children;

        public override void Init(Transform trm)
        {

            children.Init(trm);

        }

        public override FAED_Node Copy()
        {

            var node = Instantiate(this);
            node.children = children.Copy();
            
            return node;

        }

    }

}