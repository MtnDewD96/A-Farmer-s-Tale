using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    idle,
    walk,
    interact,
    attack,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
         
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCoroutine());
        }
        else if(Input.GetButtonDown("secondaryAttack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(SecondaryAttackCoroutine());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            updateAnimationAndMove();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        FindObjectOfType<AudioManager>().Play("SwordSwing");
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    private IEnumerator SecondaryAttackCoroutine()
    {
        FindObjectOfType<AudioManager>().Play("BowFire");
        //animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    private void MakeArrow()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        Vector2 temp = new Vector2(aimDirection.x, aimDirection.y);
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, GetArrowAngle());
    }

    void updateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Knockback(float knockbackTime)
    {
        StartCoroutine(KnockbackCoroutine(knockbackTime));
    }

    private Vector3 GetArrowAngle()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        //Debug.Log(mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        Debug.Log(aimDirection);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Debug.Log(angle);
        return new Vector3(0, 0, angle);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }
    private Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private IEnumerator KnockbackCoroutine(float knockbackTime)
    {
        if (myRigidBody != null)
        {
            Debug.Log("allo");
            yield return new WaitForSeconds(knockbackTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
