using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FD.Core
{

    public class FAED_Core : MonoBehaviour
    {

        private static FAED_PoolManager poolManager;
        private static FAED_Core instance;
        private static FAED_DelayInvoke delayInvoke;
        private static FAED_EasingFunc easingFunc;

        public static FAED_PoolManager PoolManager 
        {

            get 
            {

                Init();
                return poolManager; 

            } 

        }
        public static FAED_Core Instance 
        { 
            get 
            {

                Init();
                return instance; 

            }
        }
        public static FAED_DelayInvoke DelayInvoke 
        {

            get
            {
                Init();
                return delayInvoke;
            }

        }
        public static FAED_EasingFunc EasingFunc
        {

            get 
            {

                Init();
                return easingFunc;

            }

        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {          

            if(instance == null) 
            {

                GameObject go = new GameObject("_@*FAED_CORE*@_");
                DontDestroyOnLoad(go);

                instance = go.AddComponent<FAED_Core>();
                delayInvoke = go.AddComponent<FAED_DelayInvoke>();
                easingFunc = new FAED_EasingFunc();

                var res = Resources.Load<FAED_SettingSO>("FAED/SettingSO");

                if (poolManager == null && res.usePooling)
                {

                    poolManager = new FAED_PoolManager(res.poolingSO, go.transform);
                    SceneManager.sceneLoaded += CreateScenePool;

                }

                SceneManager.sceneLoaded += CreateSceneObj;

            }

        }

        private static void CreateScenePool(Scene scene, LoadSceneMode mode) 
        { 
           
            poolManager.CreateScenePool(scene.name);

        }

        private static void CreateSceneObj(Scene scene, LoadSceneMode mode)
        {

            GameObject go = new GameObject("_@*FAED_SCENE*@_");

            if(poolManager != null)
            {

                poolManager.SetSceneParent(go.transform);

            }

        }

    }

}