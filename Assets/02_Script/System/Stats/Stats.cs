using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{

    [SerializeField] private float value;

    private List<float> modify;

    public void AddMod(float value)
    {

        modify.Add(value);

    }

    public void RemoveMod(float value)
    {

        modify.Remove(value);

    }

    public float GetValue()
    {

        float v = value;

        foreach (var mod in modify)
        {

            v += mod;

        }

        return v;

    }

}
