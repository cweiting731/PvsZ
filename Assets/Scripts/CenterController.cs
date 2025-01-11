using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CenterController : MonoBehaviour
{
    public int level;
    public int energy;
    public int health;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;
    public GameObject LpCanvas;
    public Button restart_F;
    public Button restart_Lp;
    public Button restart_W;
    public Button levelup;
    public Image starPrefab;
    public Transform starsContainer;
    private bool cantPause = false;
    private Vector3 offset = Vector3.one;
    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
        if (LpCanvas != null)
        {
            LpCanvas.SetActive(false);
        }
        //GenerateStars(level);
    }

    void Update()
    {
        
    }

    public bool HaveEnoughEnergy(int energyCost)
    {
        return energy >= energyCost;
    }
    public void DecreaseEnergy(int energyCost)
    {
        energy -= energyCost;
    }
    public void DecreaseHealth()
    {
        health--;
        if (health <= 0)
        {
            // ��F
            // �ݭn UI �Ȱ��C���æ����s�}�l������
            GameOver();
        }
        Debug.Log("Health" + health);
    }
    public void NextLeveling()
    {
        if (level == 5)
        {
            // Ĺ�F
            // �ݭn UI �Ȱ��C���æ����s�}�l������
            Win();
        }
        else
        {
            // Ĺ�F 1 �� level�A�ݭn UI �Ȱ��C���æ��U�@level�����s�B���s�}�l�����s
            LevelUp();
            level++;
        }
    }
    public void GameOver()
    {
        // ��ܹC�����Ѥ���
        if (gameOverCanvas != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            gameOverCanvas.SetActive(true);
        }
        cantPause = true;
        // �Ȱ��C��
        Time.timeScale = 0f;
    }
    public void Win()
    {
        if (winCanvas != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            winCanvas.SetActive(true);
        }
        cantPause = true;
        // �Ȱ��C��
        Time.timeScale = 0f;
    }
    public void LevelUp()
    {
        if (LpCanvas != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            LpCanvas.SetActive(true);
        }
        cantPause = true;
        // �Ȱ��C��
        Time.timeScale = 0f;
    }
    public void RestartGame_Failed()
    {
        gameOverCanvas.SetActive(false );
        // ��_�ɶ��y�u
        Time.timeScale = 1f;
        cantPause = false;
        // ���s�[�����e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RestartGame_Win()
    {
        winCanvas.SetActive(false);
        // ��_�ɶ��y�u
        Time.timeScale = 1f;
        cantPause = false;
        // ���s�[�����e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RestartGame_Lp()
    {
        LpCanvas.SetActive(false);
        // ��_�ɶ��y�u
        Time.timeScale = 1f;
        cantPause = false;
        // ���s�[�����e����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Lp()
    {
        LpCanvas.SetActive(false);
        // ��_�ɶ��y�u
        Time.timeScale = 1f;
        cantPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public bool CantPause()
    {
        return cantPause;
    }
    /*public void GenerateStars(int starCount)
    {
        offset = new Vector3(0,1,1);
        // �M�Ť��e���P�P
        foreach (Transform child in starsContainer)
        {
            Destroy(child.gameObject);
        }

        // �ھڼƶq�ͦ��P�P
        for (int i = 0; i < starCount; i++)
        {
            Image star = Instantiate(starPrefab, starsContainer);
            star.transform.localScale = offset; // �T�O�Y�񥿱`
            offset.x += 1;
        }
    }*/
}
