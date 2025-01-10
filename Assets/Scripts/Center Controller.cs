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
        GenerateStars(level);
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
            // 輸了
            // 需要 UI 暫停遊戲並有重新開始的按紐
            GameOver();
        }
        Debug.Log("Health" + health);
    }
    public void NextLeveling()
    {
        if (level == 5)
        {
            // 贏了
            // 需要 UI 暫停遊戲並有重新開始的按紐
            Win();
        }
        else
        {
            // 贏了 1 個 level，需要 UI 暫停遊戲並有下一level的按鈕、重新開始的按鈕
            LevelUp();
            level++;
        }
    }
    public void GameOver()
    {
        // 顯示遊戲失敗介面
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        cantPause = true;
        // 暫停遊戲
        Time.timeScale = 0f;
    }
    public void Win()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
        cantPause = true;
        // 暫停遊戲
        Time.timeScale = 0f;
    }
    public void LevelUp()
    {
        if (LpCanvas != null)
        {
            LpCanvas.SetActive(true);
        }
        cantPause = true;
        // 暫停遊戲
        Time.timeScale = 0f;
    }
    public void RestartGame_Failed()
    {
        gameOverCanvas.SetActive(false );
        // 恢復時間流逝
        Time.timeScale = 1f;
        cantPause = false;
        // 重新加載當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartGame_Win()
    {
        winCanvas.SetActive(false);
        // 恢復時間流逝
        Time.timeScale = 1f;
        cantPause = false;
        // 重新加載當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartGame_Lp()
    {
        LpCanvas.SetActive(false);
        // 恢復時間流逝
        Time.timeScale = 1f;
        cantPause = false;
        // 重新加載當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Lp()
    {
        LpCanvas.SetActive(false);
        // 恢復時間流逝
        Time.timeScale = 1f;
        cantPause = false;
    }
    public bool CantPause()
    {
        return cantPause;
    }
    public void GenerateStars(int starCount)
    {
        // 清空之前的星星
        foreach (Transform child in starsContainer)
        {
            Destroy(child.gameObject);
        }

        // 根據數量生成星星
        for (int i = 0; i < starCount; i++)
        {
            Image star = Instantiate(starPrefab, starsContainer);
            star.transform.localScale = Vector3.one; // 確保縮放正常
        }
    }
}
