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

        if(Vector2.Distance(Player.transform.position, transform.position) < 2.0f)
        {
            GetComponent<Animator>().SetBool("Attack", true);
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Attack", false);
            GetComponent<Animator>().SetBool("Walk", true);
        }
    }
}
