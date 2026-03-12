using UnityEngine;

public class Barricade : MonoBehaviour
{
    public int health = 5;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Barricade hit by: " + collision.gameObject.name);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet") ||
            collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            Destroy(collision.gameObject); 
            health--;

            transform.localScale *= 0.8f; 

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}