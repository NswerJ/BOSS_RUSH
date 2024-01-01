using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using MiniMap = UnityEditor.Experimental.GraphView.MiniMap;
using UnityEditor;

namespace FD.Core.Editors
{

    public class FAED_BaseGraphView : GraphView
    {
        public T AddNode<T>()where T : FAED_BaseNode, new()
        {

            return AddNode<T>("New Node");

        }
        public T AddNode<T>(string titleText)where T : FAED_BaseNode, new()
        {

            return AddNode<T>(titleText, new Vector2(300, 300));

        }
        public T AddNode<T>(string titleText, Vector2 size)where T : FAED_BaseNode, new()
        {

            return AddNode<T>(titleText, size, Vector2.zero);

        }
        public T AddNode<T>(string titleText, Vector2 size, Vector2 position)where T : FAED_BaseNode, new()
        {

            return AddNode<T>(titleText, size, position, true, true);

        }
        public T AddNode<T>(string titleText, Vector2 size, Vector2 position, bool movable, bool deletable)where T : FAED_BaseNode, new()
        {

            var node = new T();

            if (!movable) node.capabilities &= ~Capabilities.Movable;
            if (!deletable) node.capabilities &= ~Capabilities.Deletable;

            node.title = titleText;
            node.SetPosition(new Rect(position, size));
            node.RefreshAll();

            AddElement(node);

            return node;

        }
        public void SetMiniMap(Rect rect)
        {

            MiniMap miniMap = new MiniMap { anchored = true };
            miniMap.SetPosition(rect);
            Add(miniMap);

        }
        public void SetZoom()
        {

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        }
        public void SetDrag()
        {

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

        }
        public void SetGrid()
        {

            SetGrid("DefaultGridBG");

        }
        public void SetGrid(string ussPath)
        {
            
            var style = Resources.Load<StyleSheet>(ussPath);
            styleSheets.Add(style);

            var grid = new GridBackground();
            grid.StretchToParentSize();
            Insert(0, grid);

        }

    }

}