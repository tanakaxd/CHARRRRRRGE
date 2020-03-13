using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public static Engine instance;
    public Doctrine researchCenter;

    private Text xpText;
    private GameObject gameClearContainer;


    private List<GameObject> infantries = new List<GameObject>();
    public List<GameObject> Infantiries { get { return infantries; } }
    private int xp;
    public int Xp
    {
        get { return xp; }
    }
    private float timeLapse;
    //public int Xp { get { return xp; } set { } }

    private bool isGameover;
    private bool pause = false;
    private bool builtResearchCenter;
    private float timeToBuildResearchCenter = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause && !isGameover)
        {
            timeLapse += Time.deltaTime;
        }

        if (!builtResearchCenter)
        {
            if (timeLapse >= timeToBuildResearchCenter)
            {
                DoctrineManager.instance.ResearchDoctrine(researchCenter);
                builtResearchCenter = true;
            }
        }

    }

    void Init()
    {
        xp = 1000;
        isGameover = false;
        timeLapse = 0;
        builtResearchCenter = false;
        xpText = GameObject.Find("XPText").GetComponent<Text>();
        gameClearContainer = GameObject.Find("Canvas").transform.Find("GameClearContainer").gameObject;
        gameClearContainer.SetActive(false);

    }

    public void GameOver()
    {
        Debug.Log("game cleared");
        //GameObject gameClearContainer = GameObject.Find("Canvas");
        gameClearContainer.SetActive(true);
        gameClearContainer.transform.Find("FinalTime").GetComponent<Text>().text = "TIME TO FINISH: " + timeLapse;
        isGameover = true;

    }

    public void UpdateXp(int amount)
    {
        if (amount >= 0 && DoctrineManager.instance.AskLearned("XPUP"))
        {
            amount = (int)(amount * 1.3f);
        }
        xp += amount;
        xpText.text = "XP: " + xp;
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    public void ModifyGravity()
    {
        if (DoctrineManager.instance.AskLearned("GravityModification"))
        {
            Physics.gravity = new Vector3(0, -9.81f * 0.5f, 0);
        }
    }
}
