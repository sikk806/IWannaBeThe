using UnityEngine;

public class AlexBomb : MonoBehaviour
{
    [SerializeField]
    float MaxVec = 5f;
    float time;

    Vector2 BombVector;
    bool addForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addForce = false;
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Rigidbody2D>().linearVelocityY -= Time.deltaTime * 10;

        if (!GameManager.Instance.MainCamera.IsObjectInCameraView(gameObject))
        {
            gameObject.SetActive(false);
            addForce = false;
        }
    }

    public void SetForce()
    {
        float gravity = GetComponent<Rigidbody2D>().gravityScale;

        // Calculate The Vector
        Vector2 playerPosition = GameManager.Instance.Player.transform.position;
        Vector2 thisPosition = transform.position;
        Vector2 thisToPlayer = playerPosition - thisPosition;

        float Vx = Random.Range(-MaxVec, MaxVec);
        float f = Random.Range(2f, 3f);
        float Vy = MaxVec * f;
        time = Random.Range(0.5f, 2.0f);

        BombVector = new Vector2(Vx, Vy);
        GetComponent<Rigidbody2D>().linearVelocity = BombVector;
        addForce = true;
    }
}

