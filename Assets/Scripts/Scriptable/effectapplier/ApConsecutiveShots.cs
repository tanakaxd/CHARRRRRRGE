using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ApConsecutiveShots", menuName = "ApConsecutiveShots")]
public class ApConsecutiveShots : IEffectApplier
{
    public override void Run()
    {
        GameObject.FindGameObjectWithTag("Artillery").GetComponent<Artillery>().ProjectilePerShot = 16;

    }
}
