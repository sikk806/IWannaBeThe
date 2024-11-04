using Unity.VisualScripting;
using UnityEngine;

public class StartBossTrigger : MonoBehaviour
{
    public AlexKid Boss;

    bool bStart;
    Vector2 StartPosition = new Vector2(79f, 15.5f);

    void Start()
    {
        bStart = false;
        Boss.transform.position = StartPosition;
    }

    void Update()
    {
        if(bStart)
        {
            if(Boss.transform.position.y > 10)
            {
                Boss.GetComponent<Rigidbody2D>().linearVelocityY = -1.5f;
            }
            else if(Boss.transform.position.y < 10)
            {
                Boss.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                Invoke("StartTheGame", 1.0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            bStart = true;
        }
    }

    void StartTheGame()
    {
        Boss.GetComponent<AlexKid>().bStartBossTrigger = true;
        gameObject.SetActive(false);
        
    }
}
