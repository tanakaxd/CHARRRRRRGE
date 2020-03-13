using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Doctrine", menuName = "Doctrine")]
public class Doctrine : ScriptableObject
{
    public enum DoctrineType
    {
        prerequisite,
        office,
        effector,
        propaganda,
        artillery,
        ultimate
    }

    [SerializeField]
    private DoctrineType doctrineType;

    [SerializeField]
    private int doctrineID;

    [SerializeField]
    private string doctrineName;

    [SerializeField]
    private int doctrineCost;

    [SerializeField]
    private Doctrine[] requirements;

    [SerializeField]
    private string information;

    [SerializeField]
    private IEffectApplier effectApplier;

    public DoctrineType GetTypeOfDoctrine()
    {
        return doctrineType;
    }

    public int GetDoctrineID()
    {
        return doctrineID;
    }

    public string GetDoctrineName()
    {
        return doctrineName;
    }

    public int GetDoctrineCost()
    {
        return doctrineCost;
    }

    public Doctrine[] GetRequirements()
    {
        return requirements;
    }

    public string GetInformation()
    {
        return information;
    }

    public void ApplyEffect()
    {
        Debug.Log("ApplyEffect called");
        if (effectApplier != null)
        {
            Debug.Log("ApplyEffect inside called");
            effectApplier.Run();
        }
    }
}
