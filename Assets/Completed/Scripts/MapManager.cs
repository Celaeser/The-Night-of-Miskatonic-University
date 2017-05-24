using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class positionInfo
{
    public bool mark;
    public Vector2 position;
}


//public struct positionInfo
//{
//    public bool mark;
//    public Vector2 position;

//}

public class MapManager : MonoBehaviour
{

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] bookShelfArray;

    public GameObject[] chairArray;
    public GameObject[] tableArray;

    public GameObject[] propArray;
    
    public GameObject[] enemyArray;
    public GameObject[] exitArray;

    private int rows = 20;
    private int cols = 20;
    private int minBookShelfCount = 15;
    private int maxBooksfelfCount = 20;
    private int minChairCount = 10;
    private int maxChairCount = 20;
    private int minTableCount = 10;
    private int maxTableCount = 15;

    private List<positionInfo> positionInfoList = new List<positionInfo>();
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
        if(gameManager.level == 1)
        {
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
        }
        

        
        positionInfoList.Clear();
        for (int y = 2; y < cols-2; y++)
        {
            for (int x = 2; x < rows-2; x++)
            {
                positionInfo newPositionInfo = new positionInfo();
                newPositionInfo.mark = false;
                newPositionInfo.position = new Vector2(x, y);
                positionInfoList.Add(newPositionInfo);
            }
        }

        //创建书架
        int bookShelfCount = Random.Range(minBookShelfCount, maxBooksfelfCount + 1);
        InstantiateBigItems(bookShelfCount, bookShelfArray);        

        //创建椅子
        int chairCount = Random.Range(minChairCount, maxChairCount + 1);
        InstantiateSmallItems(chairCount, chairArray);

        //创建桌子
        int tableCount = Random.Range(minTableCount, maxTableCount + 1);
        InstantiateSmallItems(tableCount, tableArray);

        //创建道具
        int propCount = Random.Range(1, gameManager.level + 1);
        InstantiateSmallItems(propCount, propArray);

        //创建敌人
        int enemyCount = gameManager.level + 1;
        InstantiateSmallItems(enemyCount, enemyArray);

        //创建Exit
        Vector2 exitPosition = RandomPosition();
        InstantiateSmallItems(1,exitArray);
        //GameObject exitGo = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
        //exitGo.transform.SetParent(mapHolder);
    }

    private Vector2 RandomPosition()
    {
        int positionIndex = Random.Range(0, positionInfoList.Count);
        while (positionInfoList[positionIndex].mark)
        {
            positionIndex = Random.Range(0, positionInfoList.Count);
        }
        Vector2 position = positionInfoList[positionIndex].position;
        positionInfoList[positionIndex].mark = true;
        
        return position;
    }

    private GameObject RandomPrefab(GameObject[] prefabs)
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    private void InstantiateBigItems(int count,GameObject[] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
            int positionIndex = Random.Range(0, positionInfoList.Count-rows);
            while(positionInfoList[positionIndex].mark || positionInfoList[positionIndex+1].mark ||
                  positionInfoList[positionIndex+rows-4].mark || positionInfoList[positionIndex+rows-3].mark)
            {
                positionIndex = Random.Range(0, positionInfoList.Count - rows);
            }
            Vector2 position = positionInfoList[positionIndex].position;
            positionInfoList[positionIndex].mark = true;
            positionInfoList[positionIndex+1].mark = true;
            positionInfoList[positionIndex+rows-4].mark = true;
            positionInfoList[positionIndex+rows-3].mark = true;
            GameObject prefab = RandomPrefab(prefabs);
            position.x = position.x + 0.5f;
            position.y = position.y + 0.5f;
            GameObject go = Instantiate(prefab, position, Quaternion.identity);
            go.transform.SetParent(mapHolder);
        }
    }

    private void InstantiateSmallItems(int count, GameObject[] prefabs)
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
        
        GameObject[] bookShelf = GameObject.FindGameObjectsWithTag("BookShelf");
        GameObject[] chair = GameObject.FindGameObjectsWithTag("Chair");
        GameObject[] table = GameObject.FindGameObjectsWithTag("Table");
        GameObject[] prop = GameObject.FindGameObjectsWithTag("Prop");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject exit = GameObject.FindGameObjectWithTag("Exit");
        for(int i = 0; i < bookShelf.Length; i++)
        {
            Destroy(bookShelf[i]);
        }
        for (int i = 0; i < chair.Length; i++)
        {
            Destroy(chair[i]);
        }
        for (int i = 0; i < table.Length; i++)
        {
            Destroy(table[i]);
        }
        for (int i = 0; i < prop.Length; i++)
        {
            Destroy(prop[i]);
        }
        for(int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
        Destroy(exit);

        GameObject.FindGameObjectWithTag("Masked").GetComponent<FOVMesh>().ResetWalls();
    }
}
