using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject attachedTower;
    [SerializeField] public bool highlighted = false;
    Material startingMat;
    MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startingMat = meshRenderer.material;
    }

    void OnMouseOver()
    {
        if (attachedTower == null)
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
        //if has enough money
        if(attachedTower == null)
        {
            attachedTower = Instantiate(TileManager.Instance.towerTypes[TileManager.Instance.selectedTower], transform);
        }
    }
}
