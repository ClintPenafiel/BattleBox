using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererController : MonoBehaviour
{
    [SerializeField] private ResourceNodeController resourceNode;
    [SerializeField] private int carryCapacity = 10;
    [SerializeField] private int gatherSpeed = 1;
    [SerializeField] private int depositSpeed = 1;
    [SerializeField] private Transform mainBase;

    private int goldCarried = 0;
    private bool isGathering = false;
    private bool isDepositing = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GatherGold");
        StartCoroutine("DepositGold");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GatherGold()
    {
        while (true)
        {
            if (!isGathering && goldCarried < carryCapacity)
            {
                isGathering = true;
                yield return new WaitForSeconds(gatherSpeed);
                int goldGathered = resourceNode.SubtractGold(carryCapacity - goldCarried);
                goldCarried += goldGathered;
                isGathering = false;
            }
            yield return null;
        }
    }

    private IEnumerator DepositGold()
    {
        while (true)
        {
            if (!isDepositing && goldCarried > 0)
            {
                isDepositing = true;
                yield return new WaitForSeconds(depositSpeed);
                mainBase.GetComponent<BaseController>().AddGold(goldCarried);
                goldCarried = 0;
                isDepositing = false;
            }
            yield return null;
        }
    }
}