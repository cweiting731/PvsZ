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
            // ��F
            // �ݭn UI �Ȱ��C���æ����s�}�l������
        }
        Debug.Log("Health" + health);
    }
    public void NextLeveling()
    {
        if (level == 5)
        {
            // Ĺ�F
            // �ݭn UI �Ȱ��C���æ����s�}�l������
        }
        else
        {
            // Ĺ�F 1 �� level�A�ݭn UI �Ȱ��C���æ��U�@level�����s�B���s�}�l�����s
            level++;
        }
    }
}
