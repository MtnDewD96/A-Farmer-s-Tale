using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float knockbackDistance;
    public float knockbackTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PotBehaviour>().Smash();
        }
        if (other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("PlayerTag"))
        {
            Rigidbody2D hit  = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * knockbackDistance;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemies") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knockback(hit, knockbackTime, damage);
                }

                //if (other.gameObject.CompareTag("Player"))
                //{
                //    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                //    {
                //        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                //        other.GetComponent<PlayerMovement>().Knockback(knockbackTime);
                //    }
                //}
            }
        }
    }
}
