using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSetting : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Stage0");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("SaveLevel"));
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
