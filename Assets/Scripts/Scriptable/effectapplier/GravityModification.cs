using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "GravityModification", menuName = "GravityModification")]
public class GravityModification : IEffectApplier
{
    public override void Run()
    {
        Physics.gravity /= 6;
        Debug.Log("GravityModification.Run called");
    }
}
