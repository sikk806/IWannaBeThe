using UnityEngine;
using UnityEngine.U2D;

public class Spit : MonoBehaviour
{
    [SerializeField]
    float time = 0.5f;

    [SerializeField]
    float Vx0 = 100.0f;
    bool addForce = false;
    private Vector2 spitDeltaVector;
    public Vector2 SpitDeltaVector
    {
        set
        {
            spitDeltaVector = value;
        }
    }

    private Vector2 spitVector;
    public Vector2 SpitVector
    {
        set
        {
            spitVector = value;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (addForce)
        {
            GetComponent<Rigidbody2D>().linearVelocityY -= GetComponent<Rigidbody2D>().gravityScale * Time.deltaTime / time;

            Vector2 MoveVector = GetComponent<Rigidbody2D>().linearVelocity;
            float angle = Vector2.Angle(MoveVector, spitVector);
            if(MoveVector.x < 0)
            {
                angle = angle + 180; 
            }
            else
            {
                angle = -angle;
            }

            GetComponent<Rigidbody2D>().SetRotation(angle);
        }
        GameManager GM = GameManager.Instance;
        if (!GM.MainCamera.IsObjectInCameraView(this.gameObject))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().OnDead();
        }
    }

    public void SetForce()
    {
        float gravity = GetComponent<Rigidbody2D>().gravityScale;

        // Calculate The Vector
        Vector2 playerPosition = GameManager.Instance.Player.transform.position;
        Vector2 thisPosition = transform.position;
        Vector2 thisToPlayer = playerPosition - thisPosition;

        float Vx = Mathf.Clamp((thisToPlayer.x * 2f) / time, -Vx0, Vx0);
        // float Vy0 = (thisToPlayer.y + 0.5f * gravity * time * time) / time;
        // Vector2 deltaV = new Vector2(0f, -gravity * time);

        spitVector = new Vector2(Vx, 0);

        GetComponent<Rigidbody2D>().linearVelocity = spitVector;
        addForce = true;
    }
}
