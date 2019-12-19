using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    stagger,
    attack 
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttackDamage;
    public float baseSpeed;
    public GameObject deathAnimation;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("DeathSound");
            DeathAnimation();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathAnimation()
    {
        if(deathAnimation != null)
        {
            GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

    }

    public void Knockback(Rigidbody2D myRigidbody, float knockbackTime, float damage)
    {
        StartCoroutine(KnockbackCoroutine(myRigidbody, knockbackTime));
        ApplyDamage(damage);
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D myRigidbody, float knockbackTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
