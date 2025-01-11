using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sunflower : MonoBehaviour, Damageable
{
    public int EnergyCost = 10;
    public int energyGain = 5;        // �C����_����q��
    public float energyInterval = 5f; // ��_��q���ɶ����j�]���^
    public float maxHealth = 100f;   // �̤j��q
    public Slider healthBar;         // ��q���]UI Slider�^
    public Vector3 healthBarOffset = new Vector3(0, 1.5f, 0); // ��q������
    //public GameManager gameManager;  // �C���޲z���A�Ω������q�޲z
    public Canvas healthBarCanvas;
    private CenterController gameControl;
    private float currentHealth;     // ���e��q
    private bool isActive = true;    // ����V�鸪�O�_���`�u�@
    private Animator animator;
    private float timer = 0f;
    void Start()
    {
        gameControl = GameObject.Find("GameControl").GetComponent<CenterController>();

        animator = GetComponent<Animator>();
        // ��l�Ʀ�q
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            healthBar.gameObject.SetActive(false); // ��q����l������
        }
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + healthBarOffset; // ��l�Ʀ�m
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
        // �T�O�C���޲z���s�b
        /*if (gameManager == null)
        {
            Debug.LogError("GameManager �|�����t���V�鸪�I");
            return;
        }*/

        // �Ұʩw�ɫ�_��q
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        StartCoroutine(RestoreEnergyRoutine());
    }

    void Update()
    {
        healthBarCanvas.transform.rotation = Quaternion.identity;

        timer += Time.deltaTime;
        if(timer >= energyInterval){
            gameControl.energy += energyGain;
            timer = 0;
        }
        // �T�O��q����m�l�צb�V�鸪�W��
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        
    }

    IEnumerator RestoreEnergyRoutine()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(energyInterval); // �C�j�@�q�ɶ���_��q
            /*if (gameManager != null && isActive)
            {
                gameManager.AddEnergy(energyGain); // �W�[��q
                Debug.Log($"�V�鸪��_�F {energyGain} �I��q�I���e��q�G{gameManager.GetEnergy()}");
            }*/
        }
    }

    // �����ˮ`
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
            healthBar.value = currentHealth; // ��s��q��
        }

        Debug.Log($"�V�鸪����F {damage} �I�ˮ`�A�Ѿl��q�G{currentHealth}");

        // �p�G��q�k�s�A�R���V�鸪
        if (currentHealth <= 0)
        {
            DestroySunflower();
        }
    }

    // �R���V�鸪
    private void DestroySunflower()
    {
        Debug.Log("�V�鸪�Q�R���I");
        isActive = false; // �����_��q

        // ����R���ĪG�]�p�G���ɤl�έ��ġA�i�H�b�o�̲K�[�^
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject); // �R����q��
        }
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject); // �R���V�鸪
    }

    public int TakeEnergyCost()
    {
        return EnergyCost;
    }
}
