using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData
{
    public int Level;
    public List<Vector3> CameraInfo;
}

public class LevelManager : MonoBehaviour
{
    public List<Vector2> PlayerStartLocation = new List<Vector2>();
    public List<LevelData> levels = new List<LevelData>();
    int nowLevel;

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("SaveLevel"))
        {
            nowLevel = PlayerPrefs.GetInt("SaveLevel");
        }
        else
        {
            nowLevel = 0;
            PlayerPrefs.SetInt("SaveLevel", 0);
        }
        Init();
    }

    void Start()
    {
        SceneManager.LoadScene("Stage" + nowLevel);
    }

    void Init()
    {
        // Tutorial
        List<Vector3> CameraPositionTutorial = new List<Vector3>();
        CameraPositionTutorial.Add(new Vector3(0.0f, 0.0f, -10.0f));
        CameraPositionTutorial.Add(new Vector3(17.9f, 0.0f, -10.0f));
        CameraPositionTutorial.Add(new Vector3(17.9f * 2, 0.0f, -10.0f));
        CameraPositionTutorial.Add(new Vector3(17.9f * 3, 0.0f, -10.0f));
        CameraPositionTutorial.Add(new Vector3(17.9f * 4, 0.0f, -10.0f));
        levels.Add(new LevelData { Level = 0, CameraInfo = CameraPositionTutorial });

        PlayerStartLocation.Add(new Vector2(-6f, -3f));

        // Stage1
        List<Vector3> CameraPositionStage1 = new List<Vector3>();
        CameraPositionStage1.Add(new Vector3(0.0f, 0.0f, -10.0f));
        CameraPositionStage1.Add(new Vector3(17.9f, 0.0f, -10.0f));
        CameraPositionStage1.Add(new Vector3(17.9f * 2, 0.0f, -10.0f));
        CameraPositionStage1.Add(new Vector3(17.9f, 0.0f, -10.0f));
        CameraPositionStage1.Add(new Vector3(0.0f, 0.0f, -10.0f));


        levels.Add(new LevelData { Level = 1, CameraInfo = CameraPositionStage1 });

        PlayerStartLocation.Add(new Vector2(-2f, -3f));
    }

    public void StartLevel()
    {
        nowLevel++;
        PlayerPrefs.SetInt("SaveLevel", nowLevel);
        SceneManager.LoadScene("Stage" + nowLevel);
    }
}
