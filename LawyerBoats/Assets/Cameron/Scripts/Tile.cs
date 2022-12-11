using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject attachedTower;
    [SerializeField] public bool highlighted = false;
    Material startingMat;
    MeshRenderer meshRenderer;
    TileManager tm;
    GameManager gm;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startingMat = meshRenderer.material;
        
    }

    private void Start()
    {
        tm = TileManager.Instance;
        gm = GameManager.Instance;
    }

    void OnMouseOver()
    {
        if (attachedTower == null && Time.timeScale == 1)
        {
            if(!highlighted)
            {
                meshRenderer.material = TileManager.Instance.highlightedMat;
                highlighted = true;
            }
        }
    }

    void OnMouseExit()
    {
        if (highlighted)
        {
            meshRenderer.material = startingMat;
            highlighted = false;
        }
        
    }

    void OnMouseDown()
    {
        if(gm.getMoney() - tm.towerTypes[tm.selectedTower].GetComponentsInChildren<BaseTurret>()[0].GetCost() > 0)
        {
            if (attachedTower == null && Time.timeScale == 1)
            {
                gm.loseMoney(tm.towerTypes[tm.selectedTower].GetComponentsInChildren<BaseTurret>()[0].GetCost());
                attachedTower = Instantiate(tm.towerTypes[tm.selectedTower].gameObject, transform);
            }
        }
        
    }
}
