using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private GameObject button;
    [SerializeField] private Text textDeadScore;
    [SerializeField] private Text textScore;
    [SerializeField] private GameObject objectTextDeadScore;
    [SerializeField] private Rigidbody baseRb;

    public Vector3 direction;
    public float speed;
    private Transform transformPlayer;
    private CapsuleCollider playerCollider;
    private bool isDead;
    private Ray ray;
    private Vector3 offsetScorePopUp;
    //private Rigidbody baseRb;
    

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Instance = this;
    }

    private void Start()
    {
        transformPlayer = transform;
        playerCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Controller();
        SpeedUp();
    }

    private void Controller()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            direction = direction == Vector3.forward ? Vector3.left : Vector3.forward;
        }
        
        if (!isDead)
        {
            textScore.text = BlockPool.Instance.scoreCount.ToString();
            transformPlayer.Translate(direction * (speed * Time.deltaTime));
        }

        if (isDead && transformPlayer.position.y < -15)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void SpeedUp()
    {
        if (BlockPool.Instance.scoreCount >= 75)
        {
            speed = 4.15f;
        }
        if (BlockPool.Instance.scoreCount >= 150)
        {
            speed = 4.55f;
        }
        if (BlockPool.Instance.scoreCount >= 275)
        {
            speed = 4.95f;
        }
        if (BlockPool.Instance.scoreCount >= 550)
        {
            speed = 5.50f;
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("Point"))
        {
            // score collect effect
            other.gameObject.transform.parent.GetChild(3).gameObject.SetActive(false); // deactivate point
            Instantiate(GamePrefabs.Instance.particlePrefab, other.transform.position, Quaternion.identity);

            // popup score in nice position and add +2 to score
            offsetScorePopUp = new Vector3(0, 0, 0.3f);
            Instantiate(GamePrefabs.Instance.scorePopUpPrefab, transformPlayer.position + offsetScorePopUp, 
                Quaternion.Euler(0,-45,0));
            BlockPool.Instance.scoreCount += 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("BlockTrigger") || other.tag.Equals("BaseTrigger"))
        {
            //private RaycastHit hit; no need that use _ :D
            
            ray = new Ray(transformPlayer.position, Vector3.down); // create ray to down direction
            if (!Physics.Raycast(ray, out _)) // if we hit no block
            {
                textDeadScore.text = BlockPool.Instance.scoreCount.ToString(); // score text when we die
                isDead = true;
                button.SetActive(true);
                objectTextDeadScore.SetActive(true);
                transformPlayer.GetChild(0).transform.parent = null;
                playerCollider.enabled = false; // fall nice
            }
        }
        if (other.tag.Equals("BaseTrigger") && !isDead)
        {
            baseRb.isKinematic = false;
            StartCoroutine(StopBase());
        }
    }
    
    // dont fall base forever
    private IEnumerator StopBase()
    {
        yield return new WaitForSeconds(1.5f);
        baseRb.isKinematic = true;
    }
}
