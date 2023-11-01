using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject gathererPrefab;
    public GameObject meleePrefab;
    public GameObject rangePrefab;
    public GameObject tankPrefab;

    public int gathererCost = 2;
    public int meleeCost = 10;
    public int rangeCost = 8;
    public int tankCost = 15;

    public BaseController baseController;

    public void SpawnGatherer()
    {
        if (baseController.GetGold() >= gathererCost)
        {
            baseController.AddGold(-gathererCost);
            Instantiate(gathererPrefab);
        }
        else
        {
            Debug.Log("Not enough gold to spawn Gatherer");
        }
    }

    public void SpawnMelee()
{
    if (baseController.GetGold() >= meleeCost)
    {
        baseController.AddGold(-meleeCost);
        Instantiate(meleePrefab);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Melee");
    }
}

public void SpawnRange()
{
    if (baseController.GetGold() >= rangeCost)
    {
        baseController.AddGold(-rangeCost);
        Instantiate(rangePrefab);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Range");
    }
}

public void SpawnTank()
{
    if (baseController.GetGold() >= tankCost)
    {
        baseController.AddGold(-tankCost);
        Instantiate(tankPrefab);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Tank");
    }
}
}