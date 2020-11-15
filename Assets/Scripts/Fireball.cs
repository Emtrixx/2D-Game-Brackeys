using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float speed = 40f;
    public int dmg = 20;
    public GameObject impact;


    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.takeDamage(dmg);
            Destroy(gameObject);
        }
        Instantiate(impact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
