using UnityEngine;

public class HedgeHog : MonoBehaviour
{
    public float Speed = 5f;
    PlayerController Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x < transform.position.x)
        {
            if (GetComponent<Rigidbody2D>().linearVelocityX < Speed)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Rigidbody2D>().linearVelocityX -= Speed * Time.deltaTime;
            }
        }
        else if(Player.transform.position.x > transform.position.x)
        {
            if (GetComponent<Rigidbody2D>().linearVelocityX < Speed)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Rigidbody2D>().linearVelocityX += Speed * Time.deltaTime;
            }
        }

        if(Vector2.Distance(Player.transform.position, transform.position) < 3.0f)
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Walk");
        }
    }
}
