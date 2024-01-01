using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace FD.Core
{

    public class FAED_SettingSO : ScriptableObject
    {

        [HideInInspector] public bool usePooling;

        public FAED_PoolingSO poolingSO;
    }


}