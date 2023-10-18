using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererController : MonoBehaviour
{
    [SerializeField] private int carryCapacity = 9;
    [SerializeField] private int goldCarried;
    [SerializeField] private int gatherSpeed = 1;
    [SerializeField] private int depositSpeed = 1;
    [SerializeField] private Transform mainBase;
    
    private bool isGathering = false;
    private bool isDepositing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool gatherState()
    {
        return isGathering;
    }

    public IEnumerator GatherGold(Transform targetGoldResource)
    {
        while (goldCarried < carryCapacity)
        {
            Debug.Log($"gatherer has {goldCarried} gold");
            if (!isGathering && goldCarried < carryCapacity)
            {
                isGathering = true;
                yield return new WaitForSeconds(gatherSpeed);
                ResourceNodeController resourceScript =
                    targetGoldResource.gameObject.GetComponent<ResourceNodeController>();
                int goldGathered = resourceScript.SubtractGold(carryCapacity - goldCarried);
                goldCarried += goldGathered;
                isGathering = false;
            }
            yield return null;
        }
        
    }
    
    public bool depositState()
    {
        return isDepositing;
    }

    public IEnumerator DepositGold() //this is where the gold is added to the base from the gatherer when it reaches the base with gold it should update the gold text, and add the gold to the base 
    {
        while (goldCarried > 0)
        {
            if (!isDepositing && goldCarried > 0)
            {
                isDepositing = true;
                yield return new WaitForSeconds(depositSpeed);
                BaseController.Instance.AddGold(goldCarried);
                Debug.Log($"base has {BaseController.Instance.GetGold()} gold");
                goldCarried = 0;
                isDepositing = false;
            }
            yield return null;
        }
    }
    
    
    public Transform baseTransform()
    {
        return mainBase;
    }
    
}