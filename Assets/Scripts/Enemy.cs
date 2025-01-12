using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 7f;
    public float fireRate = 0.5f;
    private float nextFireTime;

    public LayerMask groundLayer;
    public float speed = 3f;
    public float detectionRange = 12f;
    public float edgeDetectionDistance = 1.5f;

    private bool playerInSight;

    private void Update()
    {
        Vector3 scale = transform.localScale;

        Vector2 directionToPlayer = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, ~groundLayer);
        Debug.DrawLine(transform.position, transform.position + (Vector3)directionToPlayer.normalized * detectionRange, Color.red);

        playerInSight = hit.collider != null && hit.collider.gameObject == player;

        if (playerInSight)
        {

            Vector2 direction = directionToPlayer.normalized;
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);


            if (player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1; 
            }
            else
            {
                scale.x = Mathf.Abs(scale.x); 
            }
            transform.localScale = scale;


            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {

            Patrol();
        }
    }

    private void Patrol()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, edgeDetectionDistance, groundLayer);
        Debug.DrawLine(transform.position + Vector3.down, transform.position + Vector3.down + Vector3.down * edgeDetectionDistance, Color.blue);

        if (groundInfo.collider == false)
        {
            Flip();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
        speed *= -1; 
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
}