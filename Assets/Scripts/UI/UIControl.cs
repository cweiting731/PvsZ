using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    EventSystem system;
    private bool isPaused = false;
    public Button continued;
    public Button restart;
    public GameObject Canvas;
    public Button pause;

    void Start()
    {
        system = EventSystem.current;
        Canvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Canvas.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("continue");
    }

    public void RestartGame()
    {
        Canvas.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Debug.Log("restart");
    }

    public void PauseGame()
    {
        Canvas.SetActive(true); // 顯示暫停菜單
        Time.timeScale = 0; // 暫停遊戲時間
        isPaused = true;
        Debug.Log("pause");
    }
}
