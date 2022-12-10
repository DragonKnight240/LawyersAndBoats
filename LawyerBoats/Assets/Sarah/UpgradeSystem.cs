using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public enum BaseTowerNames
    {
        CrowNest,
        FrostTower,
        MagicTower,
        MortorTower
    }

    internal GameObject SelectedTower;

    public List<UpgradeTree> TowerUpgradeTrees;

    public static UpgradeSystem Instance;
    internal Tile SelectedTile;
    GameObject CurrentPanel;

    public GameObject CrowNestUpgradePanel;
    public GameObject FrostTowerUpgradePanel;
    public GameObject MagicTowerUpgradePanel;
    public GameObject MortorTowerUpgradePanel;

    Button button;
    internal GameObject Tower1;
    internal GameObject Tower2;

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


    public void UpgradeTower(GameObject newTower)
    {
        print(newTower.name);
        int CostOfUpgrade = newTower.GetComponent<BaseTurret>().GetCost();

        if (GameManager.Instance.getMoney() - CostOfUpgrade > 0)
        {
            Destroy(SelectedTile.attachedTower);
            SelectedTile.attachedTower = Instantiate(newTower.gameObject, SelectedTile.transform);
            GameManager.Instance.loseMoney(CostOfUpgrade);
        }

        CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(false);
    }

    TreeAndUpgrade GetUpgradeTree(GameObject Tower)
    {
        TreeAndUpgrade FoundTree = new TreeAndUpgrade();
        BaseTurret TowerBase = Tower.GetComponent<BaseTurret>();

        FoundTree.AvailableTowers = new List<GameObject>();

        foreach (UpgradeTree Tree in TowerUpgradeTrees)
        {
            if(Tree.BaseTowers.GetComponent<BaseTurret>().BaseTower == TowerBase.BaseTower)
            {
                if (Tree.BaseTowers.GetComponent<BaseTurret>().TowerName == TowerBase.TowerName)
                {
                    FoundTree.Tree = Tree;
                    FoundTree.AvailableTowers.Add(Tree.TowersBranch1[0]);
                    FoundTree.AvailableTowers.Add(Tree.TowersBranch2[0]);
                    return FoundTree;
                }
            }
            else
            {
                continue;
            }

            for (int i = 0; i < Tree.TowersBranch1.Count - 1; i++)
            {
                if (Tree.TowersBranch1[i].GetComponent<BaseTurret>().TowerName == TowerBase.TowerName)
                {
                    FoundTree.Tree = Tree;
                    FoundTree.AvailableTowers.Add(Tree.TowersBranch1[i+1]);
                    return FoundTree;
                }
            }

            for (int i = 0; i < Tree.TowersBranch2.Count -1; i++)
            {
                if (Tree.TowersBranch2[i].GetComponent<BaseTurret>().TowerName == TowerBase.TowerName)
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
        CrowNestUpgradePanel.transform.GetChild(0).gameObject.SetActive(false);
        FrostTowerUpgradePanel.transform.GetChild(0).gameObject.SetActive(false);
        MagicTowerUpgradePanel.transform.GetChild(0).gameObject.SetActive(false);
        MortorTowerUpgradePanel.transform.GetChild(0).gameObject.SetActive(false);

        GameObject CurrentTower = SelectedTile.attachedTower;
        TreeAndUpgrade Tree = GetUpgradeTree(CurrentTower);

        switch (CurrentTower.GetComponent<BaseTurret>().BaseTower)
        {
            case BaseTowerNames.CrowNest:
                {
                    CurrentPanel = CrowNestUpgradePanel;
                    break;
                }
            case BaseTowerNames.FrostTower:
                {
                    CurrentPanel = FrostTowerUpgradePanel;
                    break;
                }
            case BaseTowerNames.MagicTower:
                {
                    CurrentPanel = MagicTowerUpgradePanel;
                    break;
                }
            case BaseTowerNames.MortorTower:
                {
                    CurrentPanel = MortorTowerUpgradePanel;
                    break;
                }
            default:
                {
                    CurrentPanel = CrowNestUpgradePanel;
                    break;
                }
        }

        if (Tree.AvailableTowers.Count == 1)
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.GetComponent<TMP_Text>().text = Tree.AvailableTowers[0].GetComponent<BaseTurret>().TowerName.ToString();
            Tower1 = Tree.AvailableTowers[0];
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.transform.parent.GetComponent<Button>().onClick.AddListener(delegate { UpgradeTower(Tower1); });

            CurrentPanel.GetComponent<UpgradeMenuChange>().Element1.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element2.SetActive(false);
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(true);
        }
        else if(Tree.AvailableTowers.Count == 2)
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.GetComponent<TMP_Text>().text = Tree.AvailableTowers[0].GetComponent<BaseTurret>().TowerName.ToString();
            Tower1 = Tree.AvailableTowers[0];
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement1.transform.parent.GetComponent<Button>().onClick.AddListener(delegate { UpgradeTower(Tower1); });

            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement2.GetComponent<TMP_Text>().text = Tree.AvailableTowers[1].GetComponent<BaseTurret>().TowerName.ToString();
            Tower2 = Tree.AvailableTowers[1];
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement2.transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
            CurrentPanel.GetComponent<UpgradeMenuChange>().TextElement2.transform.parent.GetComponent<Button>().onClick.AddListener(delegate { UpgradeTower(Tower2); });

            CurrentPanel.GetComponent<UpgradeMenuChange>().Element1.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().Element2.SetActive(true);
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(true);
        }
        else
        {
            CurrentPanel.GetComponent<UpgradeMenuChange>().MainPanel.SetActive(false);
        }
        
    }
}
