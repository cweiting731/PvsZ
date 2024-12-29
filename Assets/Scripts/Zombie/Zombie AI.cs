using System.Threading;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float originalSpeed;
    public float originalHealth;
    public float originalAttack;
    public Animator animator;
    public float destroyDelay;
    public BoxCollider boxCollider;
    // setting up
    private float speed;
    private float health;
    private float attack;
    private State state;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    private Rigidbody rb;
    private bool isDead = false;

    //private float TEST_TIMER;
    //private float TEST_INTERVAL = 10f;

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
        //TEST_TIMER = TEST_INTERVAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Walk)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        //TEST_TIMER -= Time.deltaTime;
        //if (TEST_TIMER <= 0f)
        //{
        //    Die();
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Damaged Zombie");
            // 我覺得要把Damage和子彈效果寫到bullet上欸->Bullet.cs
            // 那殭屍得要有一個Damage函式能夠讓我呼叫
            // 啊所以這個區塊先留空應該沒關係
            return;
        }
        if (collision.gameObject.tag != "Ground")
        {
            Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
            speed = 0;
            SetState(State.Attack);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // 有些物件消失時不會進到Exit，所以殭屍會卡在OnCollisionEnter，
        // 像是子彈使用Destroy()之後並不會觸發OnCollisionExit
        // 這是我Debug子彈試出來的，註解先留著
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            Debug.Log("OnCollisionExit: " + collision.gameObject.name);
            speed = originalSpeed;
            SetState(State.Walk);
        }
    }

    private void SetState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Walk:
                animator.SetInteger("state", 0);
                // rigidbody.isKinematic = false;
                // rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // ����w����
                break;
            case State.Attack:
                animator.SetInteger("state", 1);
                // rigidbody.isKinematic = false;
                // rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                break;
        }
        Debug.Log("Zombie State: " + state.ToString());
    }

    public void TakeDamage(float damage)
    {
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

        Transform parent = transform.parent;
        Transform grandparent = (parent != null) ? parent.parent : null;
        if (grandparent != null && grandparent.CompareTag("ZombieSpawner"))
        {
            ZombieSpawner zombieSpawner = grandparent.GetComponent<ZombieSpawner>();
            if (zombieSpawner != null)
            {
                zombieSpawner.RemoveZombieFromLinkedList(gameObject);
            }
        }
        Destroy(gameObject, destroyDelay);
        if (parent != null)
        {
            Destroy(parent.gameObject, destroyDelay);
        }
    }
}
