using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [System.Serializable]
    public struct UpgradeTree
    {
        public GameObject BaseTowers;
        public List<GameObject> TowersBranch1;
        public List<GameObject> TowersBranch2;
    }

    struct TreeAndUpgrade
    {
        public UpgradeTree Tree;
        public List<GameObject> AvailableTowers;
    }

    internal GameObject SelectedTower;

    public List<UpgradeTree> TowerUpgradeTrees;

    public static UpgradeSystem Instance;
    internal Tile SelectedTile;
    GameObject CurrentPanel;
    public List<GameObject> UpgradeMenuChangesPanels;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpgradeTower(Tile SelectedTile, GameObject newTower)
    {
        int CostOfUpgrade = newTower.GetComponent<BaseTurret>().GetCost();

        if (GameManager.Instance.getMoney() - CostOfUpgrade > 0)
        {
            Destroy(SelectedTile.attachedTower);
            GameManager.Instance.loseMoney(CostOfUpgrade);
            SelectedTile.attachedTower = Instantiate(newTower, transform);
        }
    }

    TreeAndUpgrade GetUpgradeTree(GameObject Tower)
    {
        TreeAndUpgrade FoundTree = new TreeAndUpgrade();

        foreach (UpgradeTree Tree in TowerUpgradeTrees)
        {
            if(Tree.BaseTowers == Tower)
            {
                FoundTree.Tree = Tree;
                FoundTree.AvailableTowers.Add(Tree.TowersBranch1[0]);
                FoundTree.AvailableTowers.Add(Tree.TowersBranch2[0]);
                return FoundTree;
            }

            for (int i = 0; i < Tree.TowersBranch1.Count - 1; i++)
            {
                if (Tree.TowersBranch1[i] == Tower)
                {
                    FoundTree.Tree = Tree;
                    FoundTree.AvailableTowers.Add(Tree.TowersBranch1[i+1]);
                    return FoundTree;
                }
            }

            for (int i = 0; i < Tree.TowersBranch2.Count -1; i++)
            {
                if (Tree.TowersBranch2[i] == Tower)
                {
                    FoundTree.Tree = Tree;
                    FoundTree.AvailableTowers.Add(Tree.TowersBranch1[i+1]);
                    return FoundTree;
                }
            }
        }

        return FoundTree;
    }

    public void ChangeButtons()
    {
        GameObject CurrentTower = SelectedTile.attachedTower;
        TreeAndUpgrade Tree = GetUpgradeTree(CurrentTower);

        for (int i = 0; i < UpgradeMenuChangesPanels.Count; i++)
        {
            if (UpgradeMenuChangesPanels[i].GetComponent<UpgradeMenuChange>().TowerButton.transform.GetChild(0).GetComponent<TextMeshPro>().text == SelectedTile.attachedTower.name)
            {

            }
        }

        //CurrentPanel = 

        if (Tree.AvailableTowers.Count == 1)
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.GetComponent<TextMeshPro>().text = Tree.AvailableTowers[0].name;
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element1.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element2.SetActive(false);
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(true);
        }
        else if(Tree.AvailableTowers.Count == 2)
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.GetComponent<TextMeshPro>().text = Tree.AvailableTowers[0].name;
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement2.GetComponent<TextMeshPro>().text = Tree.AvailableTowers[1].name;
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element1.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element2.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(true);
        }
        else
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(true);
        }
        
    }
}
