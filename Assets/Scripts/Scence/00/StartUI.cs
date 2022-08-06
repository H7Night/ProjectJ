using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{
    Button playButton;
    Button quitButton;
    void Start()
    {
        playButton = transform.Find("PlayButton").GetComponent<Button>();
        quitButton = transform.Find("QuitButton").GetComponent<Button>();
        playButton.onClick.AddListener(ClickPlayButton);
        Time.timeScale = 1;
    }

    void ClickPlayButton()
    {
        SceneManager.LoadScene("01");
    }
    void ClickQuitButton()
    {
        Application.Quit();
    }
}
