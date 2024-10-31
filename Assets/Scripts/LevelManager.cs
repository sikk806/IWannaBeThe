using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public int Level;
    public List<Vector3> CameraInfo;
}

public class LevelManager : MonoBehaviour
{
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
        
        if(!PlayerPrefs.HasKey("SaveLevel"))
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

    void Init()
    {
        List<Vector3> CameraPosition = new List<Vector3>();
        // Tutorial
        CameraPosition.Add(new Vector3(0.0f, 0.0f, -10.0f));
        CameraPosition.Add(new Vector3(17.9f, 0.0f, -10.0f));
        CameraPosition.Add(new Vector3(17.9f * 2, 0.0f, -10.0f));
        CameraPosition.Add(new Vector3(17.9f * 3, 0.0f, -10.0f));
        CameraPosition.Add(new Vector3(17.9f * 4, 0.0f, -10.0f));
        levels.Add(new LevelData { Level = 0, CameraInfo = CameraPosition });
        Debug.Log("test");

        // Stage1
        CameraPosition.Add(new Vector3(0.0f, 0.0f, -10.0f));
    }

    void Start()
    {

    }

    void Update()
    {

    }


}
