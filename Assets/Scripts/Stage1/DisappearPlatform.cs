using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    [SerializeField]
    float StartTime;

    [SerializeField]
    float EndTime;

    float checkTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        GetComponent<Collider2D>().enabled = false;
        checkTime = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.MainCamera.IsObjectInCameraView(gameObject))
        {
            checkTime += Time.deltaTime;
        }

        if(checkTime > StartTime && EndTime > checkTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            GetComponent<Collider2D>().enabled = true;
        }
        else if(checkTime > EndTime)
        {
            gameObject.SetActive(false);
        }
    }
}
