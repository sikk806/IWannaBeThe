using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField]
    float MaxVec = 10f;
    float time;

    Vector2 bloodVector;
    bool addForce = false;
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
            float angle = Vector2.Angle(MoveVector, bloodVector);
            if (MoveVector.x < 0)
            {
                angle = angle + 180;
            }
            else
            {
                angle = -angle;
            }

            GetComponent<Rigidbody2D>().SetRotation(angle);
            if (!GameManager.Instance.MainCamera.IsObjectInCameraView(gameObject))
            {
                gameObject.SetActive(false);
                addForce = false;
            }
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
        float Vy = Random.Range(0f, MaxVec);
        time = Random.Range(0.5f, 2.0f);
        float Vy0 = (thisToPlayer.y + 0.5f * gravity * time * time) / time;

        bloodVector = new Vector2(Vx, Vy);

        GetComponent<Rigidbody2D>().linearVelocity = bloodVector;
        addForce = true;
    }
}
