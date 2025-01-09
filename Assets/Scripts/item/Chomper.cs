using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chomper : MonoBehaviour, Damageable
{
    public float maxHealth = 100f; // 最大血量
    public Slider healthBar;       // 血量條
    public float cooldownTime = 5f; // 技能冷卻時間
    public float eatRadius = 1.5f; // 吃掉殭屍的範圍
    //public ParticleSystem eatEffect; // 吃掉殭屍的特效
    public Canvas healthBarCanvas;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public int EnegyCost = 30;

    private float currentHealth;   // 當前血量
    private bool isOnCooldown = false; // 技能是否在冷卻中
    private bool isActive = true;  // 是否能正常工作
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
        }
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + offset; // 初始化位置
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
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }

    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);

        // 主動檢測敵人並攻擊
        if (!isOnCooldown && isActive)
        {
            DetectAndAttackEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isActive) return;
        if (animator != null)
        {
            animator.SetTrigger("hurtTrigger");
        }
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log($"食人花受到了 {damage} 點傷害，剩餘血量：{currentHealth}");

        // 如果血量歸零，摧毀食人花
        if (currentHealth <= 0)
        {
            DestroyChomper();
        }
    }

    private void DestroyChomper()
    {
        Debug.Log("食人花被摧毀！");
        isActive = false;

        // 刪除血量條
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        // 播放摧毀效果（可選）
        Destroy(gameObject);
    }
    private void DetectAndAttackEnemy()
    {
        // 檢測附近的敵人
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, eatRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EatZombie(hitCollider.gameObject);
                break; // 每次只攻擊一個敵人
            }
        }
    }

    private void EatZombie(GameObject zombie)
    {
        Debug.Log("食人花吃掉了一個殭屍！");

        // 播放特效
        /*if (eatEffect != null)
        {
            Instantiate(eatEffect, zombie.transform.position, Quaternion.identity);
        }*/

        // 摧毀殭屍
        Destroy(zombie);

        // 開始冷卻
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        Debug.Log($"技能冷卻中，需等待 {cooldownTime} 秒");

        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
        Debug.Log("技能冷卻結束，可以再次使用！");
    }
    private void OnDrawGizmosSelected()
    {
        // 在場景中顯示攻擊範圍
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, eatRadius);
    }
}

