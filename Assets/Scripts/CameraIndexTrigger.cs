using UnityEngine;

public class CameraIndexTrigger : MonoBehaviour
{
    [SerializeField]
    int CamIndex = 0;
    bool bCheck = false;

    void Start()
    {
        int GetNowCamIndex = PlayerPrefs.GetInt("CameraIndex");
        if(GetNowCamIndex > CamIndex)
        {
            bCheck = true;
        }
        else
        {
            bCheck = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!bCheck)
            {
                bCheck = true;
                GameManager.Instance.MainCamera.CamChange(CamIndex + 1);
            }
            else
            {
                bCheck = false;
                GameManager.Instance.MainCamera.CamChange(CamIndex);
            }
        }
    }
}
