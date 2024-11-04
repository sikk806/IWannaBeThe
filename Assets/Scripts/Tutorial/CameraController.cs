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

    List<Vector3> CamPosition = new List<Vector3>();

    public int testingLevel = 0;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("CameraIndex"));
        // SaveLevel : Save The Current Level - LevelManager Save this first.
        if (!PlayerPrefs.HasKey("SaveLevel"))
        {
            PlayerPrefs.SetInt("SaveLevel", 0);
            CamPosition = LevelManager.Instance.levels[0].CameraInfo;
        }
        else
        {
            //CamPosition = LevelManager.Instance.levels[PlayerPrefs.GetInt("SaveLevel")].CameraInfo;
            CamPosition = LevelManager.Instance.levels[testingLevel].CameraInfo;
        }

        // CameraIndex : CameraPosition;
        if (!PlayerPrefs.HasKey("CameraIndex"))
        {
            PlayerPrefs.SetInt("CameraIndex", 0);
        }
        else
        {
            camIndex = PlayerPrefs.GetInt("CameraIndex");
        }

        //LevelInit(PlayerPrefs.GetInt("SaveLevel"));

        transform.position = CamPosition[camIndex];
        if (GameManager.Instance.Canvas != null)
        {
            GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
        }
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
                //transform.position = CamPosition[++camIndex];
                if (GameManager.Instance.Canvas != null)
                {
                    GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
                }
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
        transform.position = CamPosition[camIndex];
        if (GameManager.Instance.Canvas != null)
        {
            GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
        }
    }

    public void LevelInit(int level)
    {
        camIndex = 0;
        PlayerPrefs.SetInt("CameraIndex", 0);
        CamPosition = LevelManager.Instance.levels[PlayerPrefs.GetInt("SaveLevel")].CameraInfo;
    }

    public void CamChange(int indx)
    {
        camIndex = indx;
        transform.position = CamPosition[camIndex];
        if (GameManager.Instance.Canvas != null)
        {
            GameManager.Instance.Canvas.GetComponent<TutorialText>().SetTheText(camIndex);
        }
    }

}
