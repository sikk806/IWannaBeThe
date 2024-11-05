using UnityEngine;

public class GameOver : MonoBehaviour
{
    RectTransform rectTransform;
    float checkTime;
    bool m;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        checkTime = 0;
        m = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkTime < -10f)
        {
            m = true;
        }
        else if(checkTime > 10f)
        {
            m = false;
        }

        if(m == true)
        {
            checkTime += Time.deltaTime * 10f;
        }
        else
        {
            checkTime -= Time.deltaTime * 10f;
        }
        rectTransform.transform.rotation = Quaternion.Euler(0, 0, checkTime);
    }
}
