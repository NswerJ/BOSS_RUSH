using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FD.Core
{

    public class FAED_DelayInvoke : MonoBehaviour
    {

        public void InvokeDelay(Action action, float delayTime) 
        {

            StartCoroutine(InvokeDelayCo(action, delayTime));

        }

        public void InvokeDelayRealTime(Action action, float delayTime)
        {

            StartCoroutine(InvokeDelayRealTimeCo(action, delayTime));

        }

        private IEnumerator InvokeDelayRealTimeCo(Action action, float delayTime)
        {

            yield return new WaitForSecondsRealtime(delayTime);
            action();

        }

        private IEnumerator InvokeDelayCo(Action action, float time)
        {

            yield return new WaitForSeconds(time);
            action();

        }

    }

}