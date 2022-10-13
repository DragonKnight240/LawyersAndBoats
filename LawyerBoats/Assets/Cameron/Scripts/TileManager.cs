using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager _instance;
    public static TileManager Instance { get { return _instance; } }

    [SerializeField] List<Tile> tiles;
    public Material highlightedMat;

    public int selectedTower = 0;
    public GameObject[] towerTypes;



    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        
    }
}
