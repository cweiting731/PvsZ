using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sunflower : MonoBehaviour, Damageable
{
    public int EnergyCost = 10;
    public int energyGain = 5;        // 每次恢復的能量值
    public float energyInterval = 5f; // 恢復能量的時間間隔（秒）
    public float maxHealth = 100f;   // 最大血量
    public Slider healthBar;         // 血量條（UI Slider）
    public Vector3 healthBarOffset = new Vector3(0, 1.5f, 0); // 血量條偏移
    //public GameManager gameManager;  // 遊戲管理器，用於全局能量管理
    public Canvas healthBarCanvas;

    private float currentHealth;     // 當前血量
    private bool isActive = true;    // 控制向日葵是否正常工作
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        // 初始化血量
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            healthBar.gameObject.SetActive(false); // 血量條初始為隱藏
        }
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + healthBarOffset; // 初始化位置
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward); // 朝向攝影機
        }
        foreach (Transform child in transform)
        {
            if (child.name.Equals("Canvas"))
            {
                Canvas canvas = child.GetComponent<Canvas>();
                RectTransform rt = child.GetComponent<RectTransform>();
                if (canvas != null && canvas.renderMode == RenderMode.WorldSpace)
                {
                    canvas.worldCamera = Camera.main;
                    rt.localPosition = new Vector3(0f, 0.25f, 0f);
                }
                break;
            }
        }
        // 確保遊戲管理器存在
        /*if (gameManager == null)
        {
            Debug.LogError("GameManager 尚未分配給向日葵！");
            return;
        }*/

        // 啟動定時恢復能量
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        StartCoroutine(RestoreEnergyRoutine());
    }

    void Update()
    {
        // 確保血量條位置始終在向日葵上方
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }

    IEnumerator RestoreEnergyRoutine()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(energyInterval); // 每隔一段時間恢復能量
            /*if (gameManager != null && isActive)
            {
                gameManager.AddEnergy(energyGain); // 增加能量
                Debug.Log($"向日葵恢復了 {energyGain} 點能量！當前能量：{gameManager.GetEnergy()}");
            }*/
        }
    }

    // 接受傷害
    public void TakeDamage(float damage)
    {
        if (!isActive) return;

        currentHealth -= damage;
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }

        if (healthBar != null)
        {
            healthBar.value = currentHealth; // 更新血量條
        }

        Debug.Log($"向日葵受到了 {damage} 點傷害，剩餘血量：{currentHealth}");

        // 如果血量歸零，摧毀向日葵
        if (currentHealth <= 0)
        {
            DestroySunflower();
        }
    }

    // 摧毀向日葵
    private void DestroySunflower()
    {
        Debug.Log("向日葵被摧毀！");
        isActive = false; // 停止恢復能量

        // 播放摧毀效果（如果有粒子或音效，可以在這裡添加）
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject); // 刪除血量條
        }
        Destroy(gameObject); // 刪除向日葵
    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}
