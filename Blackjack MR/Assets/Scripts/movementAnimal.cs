using UnityEngine;


//------------------------------
//------------------------------
//using navMesh instead - NEVERMIND SCRAP IT ALL
//------------------------------
//------------------------------
public class AnimalMovement : MonoBehaviour
{

    public float forceMagnitude = 10f;
    public bool moveOnStart = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("ERROR: Movement script failed to load Rigidbody.");
        }
    }

    private void Start()
    {
        if (moveOnStart)
        {
            MoveInRandomDirection();
        }
    }

    public void MoveInRandomDirection()
    {
        Vector3 randomDirection;
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        if (rb != null)
        {
            rb.AddForce(randomDirection.normalized * forceMagnitude, ForceMode.Impulse);
        }
    }

    public void FixedConstantDirection()
    {
        if (rb == null) return;
        Vector3 randomDirection;
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.velocity = randomDirection * forceMagnitude + new Vector3(0f, rb.velocity.y, 0f);
    }

    public void Update()
    {
        if (rb != null && moveOnStart)
        {
            MoveInRandomDirection();
        }
        else { Debug.LogError("ERROR: Failed to move object, Update method not functional."); }
    }
}
