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
    private CenterController gamecontrol;
    private bool cantShow = false;
    void Start()
    {
        system = EventSystem.current;
        Canvas.SetActive(false);
        gamecontrol = GameObject.Find("GameControl").GetComponent<CenterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamecontrol != null && gamecontrol.CantPause())
            {
                return;
            }
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cantShow = false;
        Canvas.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Debug.Log("continue");
    }

    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cantShow = false;
        Canvas.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Debug.Log("restart");
    }

    public void PauseGame()
    {
        Canvas.SetActive(true); // ��ܼȰ����
        Time.timeScale = 0; // �Ȱ��C���ɶ�
        isPaused = true;
        cantShow = true;
        Debug.Log("pause");
    }
    public bool CantShow()
    {
        return cantShow;
    }
}
