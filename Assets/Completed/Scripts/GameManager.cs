using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //当前关卡
    public int level = 1;
    public GameObject player;
    public GameObject masked;
    //public GameObject shadow;

    private Text HPText;
    private Text LevelText;
    private Text FailText;
    private Text SanText;
    private Image LevelBeginImage;
    private Text LevelBeginText;
    private MapManager mapManager;
    //private GameObject player;
    private int tempHP;
    private int tempSan;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        InitGame();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        tempHP =  player.GetComponent<Player>().getHP();
        tempSan = player.GetComponent<Player>().getSan();
        UpdateHPText(tempHP);
        UpdateSanText(tempSan);
        if(tempHP <= 0)
        {
            player.GetComponent<Player>().enabled = false;
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0;i<enemyList.Length;i++)
            {
                enemyList[i].GetComponent<Enemy>().setView(-1f);
            }
            FailText.enabled = true;
        }
        UpdateLevelText(level);

    }

    private void InitGame()
    {
        player = GameObject.Instantiate(player, new Vector3(1, 1, 0), Quaternion.identity);
        masked = GameObject.Instantiate(masked, new Vector3(0, 0, 0), Quaternion.identity);
        //GameObject.Instantiate(shadow, new Vector3(1, 1, 0), Quaternion.identity);
        HPText = GameObject.Find("HPText").GetComponent<Text>();
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();
        FailText = GameObject.Find("FailText").GetComponent<Text>();
        SanText = GameObject.Find("SanText").GetComponent<Text>();
        LevelBeginImage = GameObject.Find("LevelBeginImage").GetComponent<Image>();
        LevelBeginText = GameObject.Find("LevelBeginText").GetComponent<Text>();

        UpdateLevelBeginText(level);
        Invoke("HideLevelBeginImage", 3);

        FailText.enabled = false;
        mapManager = GetComponent<MapManager>();
        mapManager.InitMap();
        UpdateHPText(100);
        UpdateLevelText(level);
        UpdateSanText(5);
        
        UpdateLevelBeginText(level);
        Invoke("HideLevelBeginImage", 2);
    }

    private void UpdateHPText(int hp)
    {
        HPText.text = "HP : " + hp;
    }

    private void UpdateLevelText(int level)
    {
        LevelText.text = "LEVEL : " + level;
    }

    private void UpdateSanText(int san)
    {
        SanText.text = "SAN : " + san;
    }

    private void UpdateLevelBeginText(int level)
    {
        LevelBeginText.text = "LEVEL : " + level;
    }

    private void HideLevelBeginImage()
    {
        LevelBeginImage.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        this.level++;
        //Destroy(mapManager);
        UpdateLevelBeginText(level);
        LevelBeginImage.gameObject.SetActive(true);
        Invoke("HideLevelBeginImage", 2);

        mapManager = GetComponent<MapManager>();
        mapManager.clear();
        mapManager.InitMap();
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(1, 1, 0);
    }
}
