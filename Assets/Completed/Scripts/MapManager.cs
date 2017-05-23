using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject[] exitArray;

    private int rows = 20;
    private int cols = 20;
    private int minWallCount = 20;
    private int maxWallCount = 50;

    private List<Vector2> positionList = new List<Vector2>();
    private GameManager gameManager;

    private Transform mapHolder;

    private void Awake()
    {
        mapHolder = new GameObject("Map").transform;
    }

    // Use this for initialization
    void Start()
    {
        
        //InitMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //初始化地图
    public void InitMap()
    {
        gameManager = this.GetComponent<GameManager>();
        
        //创建OutWall和Floor
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                //初始化OutWall
                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1)
                {
                    int index = Random.Range(0, outWallArray.Length);
                    //GameObject go = GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    GameObject go = GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                //初始化Floor
                else
                {
                    int index = Random.Range(0, floorArray.Length);
                    //GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
            }
        }

        //创建障碍物
        positionList.Clear();
        for (int y = 2; y < cols-2; y++)
        {
            for (int x = 2; x < rows-2; x++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }
        int wallCount = Random.Range(minWallCount, maxWallCount + 1);
        InstantiateItems(wallCount, wallArray);

        //创建食物
        int foodCount = Random.Range(2, gameManager.level + 1);
        InstantiateItems(foodCount, foodArray);

        //创建敌人
        int enemyCount = gameManager.level + 1;
        InstantiateItems(enemyCount, enemyArray);

        //创建Exit
        Vector2 exitPosition = RandomPosition();
        InstantiateItems(1,exitArray);
        //GameObject exitGo = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
        //exitGo.transform.SetParent(mapHolder);
    }

    private Vector2 RandomPosition()
    {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 position = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return position;
    }

    private GameObject RandomPrefab(GameObject[] prefabs)
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    private void InstantiateItems(int count, GameObject[] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 position = RandomPosition();
            GameObject prefab = RandomPrefab(prefabs);
            GameObject go = Instantiate(prefab, position, Quaternion.identity);
            go.transform.SetParent(mapHolder);
        }
    }

    public void clear()
    {
        GameObject[] wall = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] outWall = GameObject.FindGameObjectsWithTag("OutWall");
        GameObject[] floor = GameObject.FindGameObjectsWithTag("Floor");
        GameObject[] soda = GameObject.FindGameObjectsWithTag("Soda");
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        for(int i = 0; i < wall.Length; i++)
        {
            Destroy(wall[i]);
        }
        for (int i = 0; i < outWall.Length; i++)
        {
            Destroy(outWall[i]);
        }
        for (int i = 0; i < floor.Length; i++)
        {
            Destroy(floor[i]);
        }
        for (int i = 0; i < soda.Length; i++)
        {
            Destroy(soda[i]);
        }
        for(int i = 0; i < food.Length; i++)
        {
            Destroy(food[i]);
        }
        Destroy(exit);

        GameObject.FindGameObjectWithTag("Masked").GetComponent<FOVMesh>().ResetWalls();
    }
}
