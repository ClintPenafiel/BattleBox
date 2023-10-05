using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererController : MonoBehaviour
{
    [SerializeField] private int carryCapacity = 10;
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

    public IEnumerator GatherGold(Transform targetGoldResource)
    {
        while (goldCarried != carryCapacity)
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

    public IEnumerator DepositGold()
    {
        while (true)
        {
            if (!isDepositing && goldCarried > 0)
            {
                isDepositing = true;
                yield return new WaitForSeconds(depositSpeed);
                mainBase.gameObject.GetComponent<BaseController>().AddGold(goldCarried);
                goldCarried = 0;
                isDepositing = false;
            }
            yield return null;
        }
    }

}