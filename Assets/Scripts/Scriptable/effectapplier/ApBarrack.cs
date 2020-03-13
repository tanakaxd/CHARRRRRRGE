using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ApBarrack", menuName = "ApBarrack")]
public class ApBarrack : IEffectApplier
{
    public override void Run()
    {
        GameObject.Instantiate(Resources.Load("NewBarrack"));
    }
}
