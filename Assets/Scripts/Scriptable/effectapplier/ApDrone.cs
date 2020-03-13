using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ApDrone", menuName = "ApDrone")]
public class ApDrone : IEffectApplier
{
    public override void Run()
    {
        GameObject.Find("Canvas").transform.Find("DroneImage").gameObject.SetActive(true);
    }
}
