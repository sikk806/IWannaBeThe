using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject Canvas;
    public GameObject GameOver;
    public GameObject Clear;
    public ObjectPools BulletPool;
    public ObjectPools BloodPool;
    public ResetObjects SaveResetItems;
    public PlayerController Player;
    public CameraController MainCamera;
    public AudioManager AudioManager;

    private void Awake()
    {
        instance = this;
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
        SaveResetItems.CanReplayInit();
    }

    public void GameClear()
    {
        AudioManager.GetComponent<AudioManager>().StopSound();
        Time.timeScale = 0;
        Clear.SetActive(true);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Main");
    }
}
