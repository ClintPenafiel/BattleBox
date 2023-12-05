using UnityEngine;

public enum UnitType
{
    None,
    Gatherer,
    Melee,
    Range,
    Tank
}
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
    public Transform spawnPoint;
    private UnitType placingUnit = UnitType.None;
    private GameObject unitPreview;
    public void SpawnGatherer()
    {
        if (baseController.GetGold() >= gathererCost)
        {
            baseController.AddGold(-gathererCost);
            placingUnit = UnitType.Gatherer;
            unitPreview = Instantiate(gathererPrefab);
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
        placingUnit = UnitType.Melee;
        unitPreview = Instantiate(meleePrefab);
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
        placingUnit = UnitType.Range;
        unitPreview = Instantiate(rangePrefab);
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
        placingUnit = UnitType.Tank;
        unitPreview = Instantiate(tankPrefab);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Tank");
    }
}

void Update() 
{
    if (placingUnit != UnitType.None)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        unitPreview.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(unitPreview, unitPreview.transform.position, Quaternion.identity);
            placingUnit = UnitType.None;
            Destroy(unitPreview);
        }
    }
}
}