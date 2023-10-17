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

    public IEnumerator DepositGold()
    {
        //while (goldCarried > 0)
        //{
            if (!isDepositing && goldCarried > 0)
            {
                isDepositing = true;
                yield return new WaitForSeconds(depositSpeed);
                BaseController baseScript = mainBase.gameObject.GetComponent<BaseController>();
                baseScript.AddGold(goldCarried);
                Debug.Log($"base has {baseScript.GetGold()} gold");
                goldCarried = 0;
                isDepositing = false;
            }
            //yield return null;
        }
    
    
    public Transform baseTransform()
    {
        return mainBase;
    }
    
}