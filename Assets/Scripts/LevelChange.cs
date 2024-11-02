using UnityEngine;

public class LevelChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            LevelManager.Instance.StartLevel();
        }
    }
}
