using UnityEngine;
using System.Collections;

public class MinerZombie : MonoBehaviour
{
    public float originalSpeed;
    public float originalHealth;
    public float originalAttack;
    public float attackInterval = 2f;
    public int energyGive;
    public Animator animator;
    public float destroyDelay;
    public BoxCollider boxCollider;
    // setting up
    private CenterController centerController;
    private float speed;
    private float health;
    private float attack;
    private State state;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    private Rigidbody rb;
    private bool isDead = false;
    private float attackTimer = 0f;
    private bool isAttacking = false;  // �O�_���b����
    private bool isStand = false;
    private bool firstTouchGround = false;
    private float backToGroundY;
    private float endOfMinerZ;
    private Coroutine attackCoroutine;

    private enum State
    {
        Walk,
        Attack
    }
    public void Init(float originalSpeed, float originalHealth, float originalAttack)
    {
        this.originalSpeed = originalSpeed;
        this.originalHealth = originalHealth;
        this.originalAttack = originalAttack;
        speed = originalSpeed;
        health = originalHealth;
        attack = originalAttack;
    }
    public void ZombieRescale(float scale)
    {
        if (scale <= 0)
        {
            Debug.Log("Zombie's scale must be greater than 0");
            return;
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) Debug.Log("Don't find the zombie Animator");
        if (rb == null) Debug.Log("Don't find the zombie Rigidbody");
        if (boxCollider == null) Debug.Log("Don't find the zombie boxCollider");
        speed = originalSpeed;
        health = originalHealth;
        attack = originalAttack;
        SetState(State.Walk);
        centerController = GameObject.Find("GameControl").GetComponent<CenterController>();
        if (centerController == null) Debug.Log("Don't find the centerController");
        endOfMinerZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Walk)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        if (isStand && transform.position.y < backToGroundY)
        {
            transform.position += new Vector3(0, 1, 0) * 0.8f * Time.deltaTime;
        }
        if (isStand && transform.position.z < endOfMinerZ)
        {
            DeleteThis(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!firstTouchGround && (collision.gameObject.tag == "Ground"))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            backToGroundY = transform.position.y;
            transform.position += new Vector3(0, -1, 0) * 1.5f;
            firstTouchGround = true;
        }
        if (!firstTouchGround &&collision.gameObject.tag == "AntiGround")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            backToGroundY = transform.position.y;
            transform.position += new Vector3(0, -1, 0) * 1.8f;
            firstTouchGround = true;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Damaged Zombie");
            return;
        }
        if (!isStand && collision.gameObject.tag == "EndLine")
        {
            Debug.Log("Miner zombie walk to EndLine");
            isStand = true;
            transform.Rotate(0, 180, 0);
            moveDirection = new Vector3(0, 0 , -1);
            return;
        }
        if (isStand && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "AntiGround" && collision.gameObject.tag != "EndLine")
        {
            Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
            speed = 0;
            SetState(State.Attack);
        }
        if (isStand && collision.gameObject.tag == "item")
        {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null && !isAttacking)
            {
                isAttacking = true;
                attackCoroutine = StartCoroutine(Attack(damageable));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "AntiGround" && collision.gameObject.tag != "EndLine")
        {
            Debug.Log("OnCollisionExit: " + collision.gameObject.name);
            speed = originalSpeed;
            SetState(State.Walk);
        }
        if (collision.gameObject.GetComponent<Nut>() != null)
        {
            //attackTimer = 0f;    // ���m�����p�ɾ�
            //isAttacking = false;
            StopAttack();
        }
    }

    private void SetState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Walk:
                animator.SetInteger("state", 0);
                break;
            case State.Attack:
                animator.SetInteger("state", 1);
                break;
        }
        // Debug.Log("Zombie State: " + state.ToString());
    }

    public void TakeDamage(float damage)
    {
        if (!isStand) return;
        if (isDead) return;
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Frozen(float percent)
    {
        speed *= percent;
    }

    public void AntiFrozen()
    {
        speed = originalSpeed;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        speed = 0;

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        boxCollider.enabled = false;

        centerController.energy += energyGive;
        Debug.Log("energy: " + centerController.energy);
        DeleteThis(true);
    }

    private void DeleteThis(bool needDelay)
    {
        Transform parent = transform.parent;
        if (needDelay)
        {
            Destroy(gameObject, destroyDelay);
            if (parent != null)
            {
                Destroy(parent.gameObject, destroyDelay);
            }
        }
        else
        {
            Destroy(gameObject);
            if (parent != null)
            {
                Destroy(parent.gameObject);
            }
        }
    }
    IEnumerator Attack(Damageable target)
    {
        while (target != null)
        {
            //Debug.Log("Zombie attacking");
            target.TakeDamage(attack); // ��������

            for (float timer = 0; timer < attackInterval; timer += Time.deltaTime)
            {
                if (target == null || (target as MonoBehaviour) == null)  // �ˬd nut �O�_�Q�P��
                {
                    StopAttack();
                    yield break; // �ߧY�h�X��{
                }
                yield return null; // �C�V�ˬd
            }
        }

        Debug.Log("destroy item");
        StopAttack(); // reset
    }


    public void StopAttack()
    {
        Debug.Log("zombie stop attacking");
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine); // �����{
            attackCoroutine = null;
        }
        isAttacking = false;
        speed = originalSpeed;
        SetState(State.Walk);
    }

    public bool isHalfHealth()
    {
        return health < (originalHealth / 2);
    }

    public void RunnerChangeSpeed()
    {
        originalSpeed = 2.75f;
        animator.SetBool("lessHealth", true);
        if (state == State.Walk)
        {
            speed = originalSpeed;
        }
    }
}
