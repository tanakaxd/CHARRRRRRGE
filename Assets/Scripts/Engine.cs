using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    public static Engine instance;

    private Text xpText;
    private GameObject gameClearContainer;


    private List<GameObject> infantries = new List<GameObject>();
    public List<GameObject> Infantiries { get { return infantries; } }
    private int xp;
    private float timeLapse;
    //public int Xp { get { return xp; } set { } }

    private bool isGameover;
    private bool pause = false;

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

    }

    void Init()
    {
        xp = 0;
        isGameover = false;
        timeLapse = 0;
        xpText = GameObject.Find("XPText").GetComponent<Text>();
        gameClearContainer = GameObject.Find("GameClearContainer");
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
        xp += amount;
        xpText.text = "XP: " + xp;
    }
}
