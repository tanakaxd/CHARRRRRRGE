using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CooldownReduction", menuName = "CooldownReduction")]
public class CooldownReduction : IEffectApplier
{
    public override void Run()
    {
        GameObject.FindGameObjectWithTag("Artillery").GetComponent<Artillery>().CoolDownTime *= 0.5f;
    }
}
