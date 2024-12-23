using System.Threading;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float originalSpeed;
    public float originalHealth;
    public Animator animator;
    public Rigidbody rigidbody;
    public float destroyDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed;
    private float health;
    private State state;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    private bool isDead = false;

    //private float TEST_TIMER;
    //private float TEST_INTERVAL = 10f;

    private enum State
    {
        Walk,
        Attack
    }
    void Start()
    {
        if (animator == null) Debug.Log("Don't find the zombie Animator");
        if (rigidbody == null) Debug.Log("Don't find the zombie Rigidbody");
        speed = originalSpeed;
        health = originalHealth;
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
        Destroy(gameObject, destroyDelay);
    }
}
