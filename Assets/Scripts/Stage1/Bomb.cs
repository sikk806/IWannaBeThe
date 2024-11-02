using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    float time = 1.0f;

    float gravity;
    bool addForce = false;
    private Vector2 bombDeltaVector;
    public Vector2 BombDeltaVector
    {
        set
        {
            bombDeltaVector = value;
        }
    }

    private Vector2 bombVector;
    public Vector2 BombVector
    {
        set
        {
            bombVector = value;
        }
    }

    float BombTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BombTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BombTimer += Time.deltaTime;
        if (BombTimer > 0.5f || Vector2.Distance(GameManager.Instance.Player.transform.position, transform.position) < 1f)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }

        if (addForce)
        {
            GetComponent<Rigidbody2D>().linearVelocityY -= gravity * Time.deltaTime;
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void SetForce()
    {
        // Calculate The Vector
        Vector2 playerPosition = GameManager.Instance.Player.transform.position;
        Vector2 thisPosition = transform.position;
        Vector2 thisToPlayer = playerPosition - thisPosition;


        float s = Mathf.Abs(thisToPlayer.x);
        float h = Mathf.Abs(thisToPlayer.y);
        gravity = (2 * h) / (time * time);
        float Vx = s / time;
        if(playerPosition.y > thisPosition.y - 0.5f)
        {
            Vx *= 2f;
        }

        bombVector = new Vector2(-Vx, 0);

        GetComponent<Rigidbody2D>().linearVelocity = bombVector;
        addForce = true;
    }
}
