using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int health;

    public GameObject damageEffect;
    public GameObject destroyEffect;

    void Start()
    {
        if(health == 0)health = maxHealth;
    }

    public void Damage(int value)
    {
        if(value <= 0) return;

        health -= value;

        if(damageEffect != null) Instantiate( damageEffect, transform.position, transform.rotation );

        if(health <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        if( destroyEffect != null ) Instantiate( destroyEffect, transform.position, transform.rotation );
        Destroy( gameObject );
    }
}