using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoctrineDataBase", menuName = "DoctrineDataBase")]
public class DoctrineDataBase : ScriptableObject
{

    [SerializeField]
    private List<Doctrine> doctrineList = new List<Doctrine>();


    //　アイテムリストを返す
    public List<Doctrine> GetDoctrineList()
    {
        return doctrineList;
    }
}
