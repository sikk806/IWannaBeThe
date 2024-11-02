using UnityEngine;

public class LinearMotion : MonoBehaviour
{
    [SerializeField]
    float Vy = 0;
    [SerializeField]
    bool isTrigger = false;

    bool bEnter = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bEnter)
        {
            GetComponent<Rigidbody2D>().linearVelocityY = Vy;
            bEnter = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isTrigger)
        {
            bEnter = true;
        }
    }
}
