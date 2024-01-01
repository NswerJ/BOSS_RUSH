using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace FD.Core.Editors
{

    public class FAED_BaseNode : Node
    {
        
        public string guid { get; set; }

        public FAED_BaseNode()
        {

            guid = GUID.Generate().ToString();

        }

        public FAED_BaseNode(string path) : base(path)
        {

            guid = GUID.Generate().ToString();

        }

        public void RefreshAll()
        {

            RefreshExpandedState();
            RefreshPorts();

        }
        public Port AddPort(Orientation orientation, Direction direction)
        {

            return AddPort(orientation, direction, Port.Capacity.Multi);

        }
        public Port AddPort(Orientation orientation, Direction direction, Port.Capacity capacity)
        {

            return AddPort(orientation, direction, capacity, typeof(float));

        }
        public Port AddPort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type)
        {

            var port = InstantiatePort(orientation, direction, capacity, type);

            
            if(direction == Direction.Input)
            {

                inputContainer.Add(port);
                

            }
            else
            {

                outputContainer.Add(port);

            }

            port.portName = "";

            RefreshAll();

            return port;

        }

    }

}