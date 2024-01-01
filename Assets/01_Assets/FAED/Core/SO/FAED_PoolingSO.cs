using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Core
{

    public class FAED_PoolingSO : ScriptableObject
    {

        [Header("____AlwaysPool____")]
        public List<FAED_PoolingObject> alwaysPoolingObjects;
        [Header("____ScenePool____")]
        public List<FAED_ScenePoolingObject> scenePoolingObjects;

    }

}