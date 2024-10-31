using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerstg1 : MonoBehaviour
{
    private static GameManagerstg1 instance;
    public static GameManagerstg1 Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject Canvas;
    public ObjectPools BulletPool;
    public ObjectPools BloodPool;
    public ResetObjects SaveResetItems;
    public PlayerController Player;
    public CameraController MainCamera;

    private void Awake()
    {
        instance = this;

        // For Tutorial
        if(!PlayerPrefs.HasKey("PositionX"))
        {
            PlayerPrefs.SetFloat("PositionX", -6);
            PlayerPrefs.SetFloat("PositionY", -3);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Replay"))
        {
            ReplayInit();
        }
    }
    
    public void Save()
    {
        // Player Save Position
        PlayerPrefs.SetFloat("PositionX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PositionY", Player.transform.position.y + 0.01f);
        Player.SavePosition = new Vector2(Player.transform.position.x, Player.transform.position.y + 0.01f);

        // Camera Save Position
        PlayerPrefs.SetInt("CameraIndex", MainCamera.CamIndex);
    }

    public void ReplayInit()
    {
        BloodPool.SetFalseAllObject();
        BulletPool.SetFalseAllObject();
        Player.ReplayInit();
        MainCamera.ReplayInit();
        SaveResetItems.ReplayInit();
    }
}

