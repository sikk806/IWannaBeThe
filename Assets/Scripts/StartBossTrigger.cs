//using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartBossTrigger : MonoBehaviour
{
    public AlexKid Boss;
    public Image BossHealth;

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
            GameManager.Instance.AudioManager.GetComponent<AudioManager>().StopSound();
            other.GetComponent<PlayerController>().transform.position = new Vector2(64.0700f, 6.366f);
            bStart = true;
        }
    }

    void StartTheGame()
    {
        GameManager.Instance.AudioManager.GetComponent<AudioManager>().ChangeSound(2);
        BossHealth.gameObject.SetActive(true);
        Boss.GetComponent<AlexKid>().bStartBossTrigger = true;
        gameObject.SetActive(false);
        
    }
}
