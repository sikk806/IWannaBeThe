using UnityEngine;

public class CameraIndexTrigger : MonoBehaviour
{
    [SerializeField]
    int CamIndex = 0;
    bool bCheck = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!bCheck)
            {
                bCheck = true;
                CamIndex++;
            }
            else
            {
                bCheck = false;
                CamIndex--;
            }
            GameManager.Instance.MainCamera.CamChange(CamIndex);
        }
    }
}
