using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    public static Block Instance;
    public Rigidbody blockRb;
    public int count;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            BlockPool.Instance.PopBlock();
            StartCoroutine(FallAndReuseBlock());
        }
    }

    private IEnumerator FallAndReuseBlock()
    {
        yield return new WaitForSeconds(0.25f);
        //Debug.Log("fall");
        count++;
        blockRb.isKinematic = false;
        yield return new WaitForSeconds(1.25f);
        BlockPool.Instance.PushBlock(gameObject.transform.parent.gameObject, blockRb);
        //Debug.Log("added");
    }
}
