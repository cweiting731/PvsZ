using UnityEngine;

public class CenterController : MonoBehaviour
{
    public int level;
    public int energy;
    public int health;
    void Start()
    {
        
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
        }
        Debug.Log("Health" + health);
    }
    public void NextLeveling()
    {
        if (level == 5)
        {
            // 贏了
            // 需要 UI 暫停遊戲並有重新開始的按紐
        }
        else
        {
            // 贏了 1 個 level，需要 UI 暫停遊戲並有下一level的按鈕、重新開始的按鈕
            level++;
        }
    }
}
