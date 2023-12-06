using System;
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
    private Camera mainCamera;
    private int cost;

    public void Start()
    {
        mainCamera = Camera.main;
    }

    public void SpawnGatherer()
    {
        cost = gathererCost;
        if (baseController.GetGold() >= gathererCost)
        {
            baseController.AddGold(-gathererCost);
            placingUnit = UnitType.Gatherer;
            unitPreview = Instantiate(gathererPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not enough gold to spawn Gatherer");
        }
    }

    public void SpawnMelee()
{
    cost = meleeCost;
    if (baseController.GetGold() >= meleeCost)
    {
        baseController.AddGold(-meleeCost);
        placingUnit = UnitType.Melee;
        unitPreview = Instantiate(meleePrefab, spawnPoint.position, Quaternion.identity);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Melee");
    }
}

public void SpawnRange()
{
    cost = rangeCost;
    if (baseController.GetGold() >= rangeCost)
    {
        baseController.AddGold(-rangeCost);
        placingUnit = UnitType.Range;
        unitPreview = Instantiate(rangePrefab, spawnPoint.position, Quaternion.identity);
    }
    else
    {
        Debug.Log("Not enough gold to spawn Range");
    }
}

public void SpawnTank()
{
    cost = tankCost;
    if (baseController.GetGold() >= tankCost)
    {
        baseController.AddGold(-tankCost);
        placingUnit = UnitType.Tank;
        unitPreview = Instantiate(tankPrefab, spawnPoint.position, Quaternion.identity);
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
        if (placingUnit != UnitType.Gatherer)
        {
            unitPreview.GetComponent<LongRangeAttacker>().SetTarget(null); // to make sure unit preview doesn't attack
        }
        unitPreview.GetComponent<Collider2D>().enabled = false; // disable collider so enemies don't attack
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // limit mouse position to stay only on the player's side
        if (mousePosition.x > 0)
        {
            mousePosition.x = 0;
        }
        unitPreview.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        // place down unit
        if (Input.GetMouseButtonDown(0))
        {
            unitPreview.GetComponent<Collider2D>().enabled = true; // enable collider again when unit has been placed
            GameObject placedUnit = Instantiate(unitPreview, unitPreview.transform.position, Quaternion.identity);
            var localScale = transform.localScale;
            // reset the scale to positive values
            placedUnit.transform.localScale = new Vector3(Math.Abs(localScale.x), Math.Abs(localScale.y), 1); 
            if (placingUnit != UnitType.Range) // resize non-ranged units 
            {
                placedUnit.transform.localScale = new Vector3(Math.Abs(localScale.x * 1.5f), Math.Abs(localScale.y * 1.5f), 1); 
            }
            placingUnit = UnitType.None;
            Destroy(unitPreview); // destroy unit preview
        }
        // cancel placing down unit
        if (Input.GetMouseButtonDown(1))
        {
            baseController.AddGold(cost);
            placingUnit = UnitType.None;
            Destroy(unitPreview);
        }
    }
}
}