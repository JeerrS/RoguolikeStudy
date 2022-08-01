using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour {
    public GameObject[] wallArray;
    public GameObject[] floorArray;
    public GameObject[] barrierArray;
    public GameObject[] enemyArray;
    public GameObject[] foodArray;
    public GameObject player;
    public GameObject exit;

    public GameManager gm;

    public int rows = 10;
    public int cols = 10;

    public int[] minSet =new int[3]{2, 1, 2};
    public int[] maxSet =new int[3]{8, 2, 2};

    private Transform _mapHolder;
    private List<Vector2> _positionList = new List<Vector2>();
    // Start is called before the first frame update
    void Start() {
        gm = GetComponent<GameManager>();
        InitMap();
        InitPlayer();
    }

    // Update is called once per frame
    void Update() {
    }

    private void InitMap() {
        _mapHolder = new GameObject("Map").transform;
        // Wall and Floor
        for (int x = 0; x < rows; x++) {
            for (int y = 0; y < cols; y++) {
                if (x == 0 || y == 0 || x == rows - 1 || y == cols - 1) {
                    int wallIndex = Random.Range(0, wallArray.Length);
                    GameObject w = GameObject.Instantiate(wallArray[wallIndex], new Vector3(x, y, 0), Quaternion.identity);
                    w.transform.SetParent(_mapHolder);
                }
                else {
                    int floorIndex = Random.Range(0, floorArray.Length);
                    GameObject f = GameObject.Instantiate(floorArray[floorIndex], new Vector3(x, y, 0), Quaternion.identity);
                    f.transform.SetParent(_mapHolder);
                }
            }
        }
        // Barrier Enemy and Food
        for (int x = 2; x < rows - 2; x++) {
            for (int y = 2; y < cols - 2; y++) {
                _positionList.Add(new Vector2(x, y));
            }
        }
        // Barrier
        ItemGenerate(0);
        // Enemy
        ItemGenerate(1);
        // Food
        ItemGenerate(2);
    }

    private void InitPlayer() {
        GameObject.Instantiate(player, new Vector3(1, 1, 0), Quaternion.identity);
        GameObject.Instantiate(exit, new Vector3(rows - 2, cols - 2, 0), Quaternion.identity);
    }

    private void ItemGenerate(int itemType) {
        GameObject[] itemArray;
        int randomChoose = 0;
        int count = 0;
        switch (itemType) {
            case 0:
                randomChoose = 4;
                count = Random.Range(minSet[itemType], maxSet[itemType] + 1);
                itemArray = barrierArray;
                break;
            case 1:
                randomChoose = 2;
                count =  gm.level / 2;
                itemArray = enemyArray;
                break;
            case 2:
                randomChoose = 2;
                count = Random.Range(minSet[itemType], maxSet[itemType] * gm.level + 1);
                itemArray = foodArray;
                break;
            default:
                return;
        }
        for (int i = 0; i < count; i++) {
            int positionIndex = Random.Range(0, _positionList.Count);
            Vector2 pos = _positionList[positionIndex];
            _positionList.RemoveAt(positionIndex);
            int index = Random.Range(0, randomChoose);
            GameObject go = GameObject.Instantiate(itemArray[index], pos, Quaternion.identity);
            go.transform.SetParent(_mapHolder);
        }
    }
    
}