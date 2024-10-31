using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int camIndex;
    public int CamIndex
    {
        get
        {
            return camIndex;
        }
        set
        {
            camIndex = value;
        }
    }


    List<Vector3> CamPositionTutorial = new List<Vector3>();
    List<Vector3> CamPositionStage1 = new List<Vector3>();

    void Start()
    {
        CamPositionTutorial.Add(new Vector3(0.0f, 0.0f, -10.0f));
        CamPositionTutorial.Add(new Vector3(17.9f, 0.0f, -10.0f));
        CamPositionTutorial.Add(new Vector3(17.9f * 2, 0.0f, -10.0f));
        CamPositionTutorial.Add(new Vector3(17.9f * 3, 0.0f, -10.0f));
        CamPositionTutorial.Add(new Vector3(17.9f * 4, 0.0f, -10.0f));

        if(!PlayerPrefs.HasKey("CameraIndex"))
        {
            PlayerPrefs.SetInt("CameraIndex", 0);
        }
        else
        {
            camIndex = PlayerPrefs.GetInt("CameraIndex");
        }
        transform.position = CamPositionTutorial[camIndex];
        GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
    }

    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 otherPosition = other.transform.position;
            if (otherPosition.x > transform.position.x + 9.0f)
            {
                transform.position = CamPositionTutorial[++camIndex];
                GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
            }
        }
    }

    public bool IsObjectInCameraView(GameObject obj)
    {
        Vector2 cameraPosition = GetComponent<Camera>().WorldToViewportPoint(obj.transform.position);

        return cameraPosition.x > 0 && cameraPosition.x < 1 && cameraPosition.y > 0 && cameraPosition.y < 1;
    }

    public void OnSave()
    {
        PlayerPrefs.SetInt("CameraIndex", camIndex);
    }

    public void ReplayInit()
    {
        camIndex = PlayerPrefs.GetInt("CameraIndex");
        transform.position = CamPositionTutorial[camIndex];
        GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
    }

}
