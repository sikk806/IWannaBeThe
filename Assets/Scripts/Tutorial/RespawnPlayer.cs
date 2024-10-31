using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    List<Vector2> SpawnPoint = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpawnPoint.Add(new Vector2(-6f, -3f));
        SpawnPoint.Add(new Vector2(11f, -3f));
        SpawnPoint.Add(new Vector2(29f, -1f));
        SpawnPoint.Add(new Vector2(47.5f, -1f));
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            int camIndex = GameManager.Instance.MainCamera.CamIndex;
            other.transform.position = SpawnPoint[camIndex];
        }
    }
}
