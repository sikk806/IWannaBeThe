using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    Vector2 StartPos;

    [SerializeField]
    Vector2 EndPos;

    bool bGo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bGo = false;
        transform.position = StartPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(bGo == true)
        {
            Vector2 MovingVector = EndPos - StartPos;
            GetComponent<Rigidbody2D>().linearVelocity = MovingVector.normalized;
        }
        else
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        if(transform.position == new Vector3(EndPos.x, EndPos.y, transform.position.z))
        {
            bGo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bGo = true;
        }

    }
}
