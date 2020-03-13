using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ApInfantrySpawnRate", menuName = "ApInfantrySpawnRate")]
public class ApInfantrySpawnRate : IEffectApplier
{
    public override void Run()
    {
        GameObject.FindGameObjectWithTag("Barrack").GetComponent<barrack>().SpawnInterval *= 0.5f;
    }
}
