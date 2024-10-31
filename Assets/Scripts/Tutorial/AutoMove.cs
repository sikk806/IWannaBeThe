using Unity.VisualScripting;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            float xPos = Player.transform.position.x;
            Player.transform.position = new Vector2(xPos + 0.01f, Player.transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            Player = null;
        }
    }

}
