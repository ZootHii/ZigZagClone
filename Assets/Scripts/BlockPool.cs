using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BlockPool : MonoBehaviour
{
    public static BlockPool Instance;
    private readonly Stack<GameObject> blockPool = new Stack<GameObject>();
    
    //[SerializeField] private GameObject blockNextPrefab;
    [SerializeField] private GameObject blockCurrent;
    private GameObject blockNext;
    private const int MaxBlock = 20;
    private readonly Vector3 offsetTop = new Vector3(0,0,0.5f);
    private readonly Vector3 offsetLeft = new Vector3(-0.5f,0,0f);
    private Vector3 nextPosition;
    private int randomValue;
    [HideInInspector] public int scoreCount;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        InitializePath();
    }

    private void InitializePool()
    {
        for (int i = 0; i < MaxBlock; i++)
        {
            blockPool.Push(Instantiate(GamePrefabs.Instance.blockNextPrefab));
            blockPool.Peek().SetActive(false);
        }
    }

    private void InitializePath()
    {
        InitializePool();
        for (int i = 0; i < MaxBlock; i++)
        {
            PopBlock();
        }
    } 
    public void PopBlock() // spawn
    {
        if (blockPool.Count == 0)
        {
            InitializePool();
        }
        
        blockNext = blockPool.Pop();
        
        randomValue = Random.Range(0, 2);
        if (randomValue == 1)
        {
            //Debug.Log(blockCurrent.transform.GetChild(randomValue).transform.position);
            nextPosition = blockCurrent.transform.GetChild(randomValue).transform.position + offsetTop;
        }
        else if (randomValue == 0)
        {
            //Debug.Log(blockCurrent.transform.GetChild(randomValue).transform.position);
            nextPosition = blockCurrent.transform.GetChild(randomValue).transform.position + offsetLeft;
        }
        
        blockNext.transform.position = nextPosition;
        blockNext.SetActive(true);
        randomValue = Random.Range(0, 5);
        if (randomValue == 0)
        {
            blockNext.transform.GetChild(3).gameObject.SetActive(true); // start points
        }
        
        blockCurrent = blockNext;
    }
    
    public void PushBlock(GameObject block, Rigidbody rb) //reuse
    {
        scoreCount++; // pushed back block count is our score
        block.transform.GetChild(3).gameObject.SetActive(false); // stop points
        rb.isKinematic = true; //stop falling
        block.SetActive(false);
        blockPool.Push(block);
    }
}
