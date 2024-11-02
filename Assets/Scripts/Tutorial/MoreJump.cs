using UnityEngine;

public class MoreJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().JumpCnt != 0)
            {
                other.GetComponent<PlayerController>().JumpCnt = 1;
            }
            GameManager.Instance.SaveResetItems.GetComponent<ResetObjects>().SaveResetItems(gameObject);
            gameObject.SetActive(false);

            Invoke("Respawn", 2.0f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

    
}
