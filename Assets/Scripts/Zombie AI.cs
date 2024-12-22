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
        if (collision.gameObject.name != "Plane")
        {
            Debug.Log("碰撞開始: " + collision.gameObject.name);
            speed = 0;
            SetState(State.Attack);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name != "Plane")
        {
            Debug.Log("碰撞結束: " + collision.gameObject.name);
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
                rigidbody.isKinematic = false;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 僅鎖定旋轉
                break;
            case State.Attack:
                animator.SetInteger("state", 1);
                rigidbody.isKinematic = false;
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                break;
        }
        Debug.Log("Zombie State: " + state.ToString());
    }
}
