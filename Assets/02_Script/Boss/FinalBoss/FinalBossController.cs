using System.Collections;
using UnityEngine;

public enum EnumfinalAwakeState
{
    Pattern_1,
    Pattern_2,
    Pattern_3,
    Pattern_4,
    Pattern_5,
    Pattern_6,
    Pattern_7,
    Pattern_8,
    Pattern_9,
    Pattern_10,
    Pattern_11,
    Pattern_12,
    Pattern_13,
    Pattern_14,
    Pattern_15,
}

public class FinalBossController : MonoBehaviour
{
    int pattern;
    public EnumfinalAwakeState[] availablePatterns; 

    private void Start()
    {
        pattern = Random.Range(0, availablePatterns.Length);
        StartCoroutine(ExecutePattern());
    }

    private IEnumerator ExecutePattern()
    {
        while (true)
        {
            yield return StartCoroutine(availablePatterns[pattern].ToString());
            yield return new WaitForSeconds(1f);

            pattern = Random.Range(0, availablePatterns.Length);
        }
    }

    private IEnumerator Pattern_1()
    {
        Debug.Log("Pattern 1");
        yield return null; 
    }

    private IEnumerator Pattern_2()
    {
        Debug.Log("Pattern 2");
        yield return null;
    }
}
