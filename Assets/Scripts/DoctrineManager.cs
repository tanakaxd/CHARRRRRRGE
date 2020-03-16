using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctrineManager : MonoBehaviour
{
    [SerializeField]
    private DoctrineDataBase doctrineDataBase;

    public static DoctrineManager instance;

    private List<Doctrine> doctrines;
    private Dictionary<Doctrine, bool> learnedDoctrine = new Dictionary<Doctrine, bool>();
    private Dictionary<Doctrine, bool> learnableDoctrine = new Dictionary<Doctrine, bool>();

    //UIを格納する変数
    //private Transform canvasTransform;
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField]
    private GameObject popupButton;
    [SerializeField]
    private GameObject doctrineTree;
    [SerializeField]
    private GameObject doctrineClose;

    private void Awake()
    {
        Debug.Log("Awake called");

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
        Debug.Log("Start called");

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        Debug.Log("Init called");
        doctrines = new List<Doctrine>(doctrineDataBase.GetDoctrineList());

        //UI
        //canvasTransform = GameObject.Find("Canvas").transform;
        //buttons = GameObject.FindGameObjectsWithTag("DoctrineButton");
        //popupButton = GameObject.Find("DoctrinePopup");
        //doctrineTree = canvasTransform.Find("DoctrineTree").gameObject;
        //doctrineClose = canvasTransform.Find("CloseButton").gameObject;

        UnlearnAll();
        RegisterEvent();
        CheckLearnableDoctrine();
        RegisterPopup();
    }

    public void ResearchDoctrine(Doctrine doctrine)
    {
        Debug.Log("ResearchDoctrine called");

        learnedDoctrine[doctrine] = true;
        CheckLearnableDoctrine();
        Engine.instance.UpdateXp(-doctrine.GetDoctrineCost());
    }

    private void InitLearnableDoctrine()
    {
        Debug.Log("InitLearnableDoctrine called");

        foreach (Doctrine doctrine in doctrines)
        {
            learnableDoctrine.Add(doctrine, false);
        }
    }

    private void CheckLearnableDoctrine()
    {
        Debug.Log("CheckLearnableDoctrine called");

        learnableDoctrine.Clear();

        foreach(Doctrine doctrine in doctrines)
        {
            //reset
            learnableDoctrine.Add(doctrine, false);

            //int式
            //int[] requiredId = doctrine.GetRequirements();
            //bool result = true;
            //for (int i = 0; i < requiredId.Length; i++)
            //{
            //    foreach(KeyValuePair<Doctrine,bool> keyValuePair in learnedDoctrine)
            //    {
            //        if(keyValuePair.Key.GetDoctrineID() == requiredId[i])
            //        {
            //            result = result && keyValuePair.Value;
            //        }
            //    }
            //}
            //learnableDoctrine[doctrine] = result;

            //Doctrine式
            Doctrine[] required = doctrine.GetRequirements();
            //doctrine一つ一つに対して、前提条件のdoctrineを学習済みか確認
            bool result = true;
            for (int i = 0; i < required.Length; i++)
            {
                foreach (KeyValuePair<Doctrine, bool> keyValuePair in learnedDoctrine)
                {
                    if (keyValuePair.Key == required[i])
                    {
                        result = result && keyValuePair.Value;
                    }
                }
            }
            learnableDoctrine[doctrine] = result;
        }
    }

    private void UnlearnAll()
    {
        Debug.Log("UnlearnAll called");

        learnedDoctrine.Clear();
        foreach(Doctrine doctrine in doctrines)
        {
            learnedDoctrine.Add(doctrine, false);
        }
    }

    private void RegisterEvent()
    {
        Debug.Log("RegisterEvent called");

        foreach (Doctrine doctrine in doctrines)
        {
            foreach(GameObject buttonObject in buttons)
            {
                if(buttonObject.name == doctrine.GetDoctrineName())
                {
                    Button button = buttonObject.GetComponent<Button>();

                    //情報登録
                    Text text = buttonObject.transform.Find("DoctrineInfo").GetComponent<Text>();
                    text.text = doctrine.GetDoctrineName() + "\n" + "XP cost: "+ doctrine.GetDoctrineCost() + "\n" + doctrine.GetInformation();

                    //処理登録
                    button.onClick.AddListener(() =>
                    {
                        if (learnableDoctrine[doctrine])
                        {
                            if (Engine.instance.Xp >= doctrine.GetDoctrineCost())
                            {
                                button.interactable = false; 
                                ResearchDoctrine(doctrine);
                                doctrine.ApplyEffect();
                            }
                        }
                    });
                }
            }
        }
    }

    void RegisterPopup()
    {
        Debug.Log("RegisterPopup called");

        popupButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("popupButton pushed");
            Engine.instance.ResearchText.gameObject.SetActive(false);
            doctrineTree.SetActive(true);
        });
        doctrineClose.GetComponent<Button>().onClick.AddListener(() =>
        {
            doctrineTree.SetActive(false);
        });
    }

    public bool AskLearned(string doctrineName)
    {
        foreach(Doctrine doctrine in doctrines)
        {
            if(doctrineName == doctrine.GetDoctrineName())
            {
                return learnedDoctrine[doctrine];
            }
        }
        Debug.LogError("invalid doctrine name");
        return false;
    }
}
