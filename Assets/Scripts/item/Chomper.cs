using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chomper : MonoBehaviour, Damageable
{
    public float maxHealth = 100f; // �̤j��q
    public Slider healthBar;       // ��q��
    public float cooldownTime = 5f; // �ޯ�N�o�ɶ�
    public float eatRadius = 1.5f; // �Y���L�ͪ��d��
    //public ParticleSystem eatEffect; // �Y���L�ͪ��S��
    public Canvas healthBarCanvas;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public int EnergyCost = 30;

    private float currentHealth;   // ���e��q
    private bool isOnCooldown = false; // �ޯ�O�_�b�N�o��
    private bool isActive = true;  // �O�_�ॿ�`�u�@
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // ��l�Ʀ�q
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + offset; // ��l�Ʀ�m
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward); // �¦V��v��
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
        healthBarCanvas.transform.rotation = Quaternion.identity;

        healthBar.gameObject.SetActive(currentHealth < maxHealth);

        // �D���˴��ĤH�ç���
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

        Debug.Log($"���H�����F {damage} �I�ˮ`�A�Ѿl��q�G{currentHealth}");

        // �p�G��q�k�s�A�R�����H��
        if (currentHealth <= 0)
        {
            DestroyChomper();
        }
    }

    private void DestroyChomper()
    {
        Debug.Log("���H��Q�R���I");
        isActive = false;

        // �R����q��
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        // ����R���ĪG�]�i��^
        Destroy(gameObject);
    }
    private void DetectAndAttackEnemy()
    {
        // �˴����񪺼ĤH
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, eatRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EatZombie(hitCollider.gameObject);
                break; // �C���u�����@�ӼĤH
            }
        }
    }

    private void EatZombie(GameObject zombie)
    {
        Debug.Log("���H��Y���F�@���L�͡I");
        Transform parent = zombie.transform.parent;
        // ����S��
        /*if (eatEffect != null)
        {
            Instantiate(eatEffect, zombie.transform.position, Quaternion.identity);
        }*/

        // �R���L��
        Destroy(zombie);

        if (parent != null)
        {
            Destroy(parent.gameObject);
        }

        // �}�l�N�o
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        Debug.Log($"�ޯ�N�o���A�ݵ��� {cooldownTime} ��");

        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
        Debug.Log("�ޯ�N�o�����A�i�H�A���ϥΡI");
    }
    private void OnDrawGizmosSelected()
    {
        // �b��������ܧ����d��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, eatRadius);
    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}

