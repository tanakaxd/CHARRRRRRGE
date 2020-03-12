using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctrineManager : MonoBehaviour
{
    [SerializeField]
    private DoctrineDataBase doctrineDataBase;

    public static DoctrineManager instance;

    private List<Doctrine> doctrines;
    private Dictionary<Doctrine, bool> learnedDoctrine = new Dictionary<Doctrine, bool>();
    private Dictionary<Doctrine, bool> learnableDoctrine = new Dictionary<Doctrine, bool>();

    //UIを格納する変数

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        doctrines = doctrineDataBase.GetDoctrineList();
    }

    public void ResearchDonctrine(Doctrine doctrine)
    {




        CheckLearnableDoctrine();
    }

    private void CheckLearnableDoctrine()
    {

    }
}
