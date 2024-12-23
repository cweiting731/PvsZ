using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float originalSpeed;
    public float originalHealth;
    public Animator animator;
    public Rigidbody rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float speed;
    private float health;
    private State state;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Walk)
        {
            Vector3 direction = new Vector3(0, 0, 1);
            transform.position += direction * speed * Time.deltaTime;
        }
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
            Debug.Log("�I���}�l: " + collision.gameObject.name);
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
            Debug.Log("�I������: " + collision.gameObject.name);
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
}
