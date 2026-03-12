using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float moveStep = 0.5f;
    public float moveInterval = 1f;
    public float edgeLimit = 8f;

    int direction = 1;
    float timer = 0f;

    public GameObject enemyBulletPrefab;
    public float shootInterval = 2f;
    float shootTimer = 0f;

    void Start()
    {
        Enemy.onEnemyDied += onEnemyDied;
        Enemy2.onEnemyDied += onEnemyDied;
        Enemy3.onEnemyDied += onEnemyDied;
    }

    void OnDestroy()
    {
        Enemy.onEnemyDied -= onEnemyDied;
        Enemy2.onEnemyDied -= onEnemyDied;
        Enemy3.onEnemyDied -= onEnemyDied;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= moveInterval)
        {
            timer = 0;
            transform.position += new Vector3(moveStep * direction, 0, 0);

            foreach (Transform enemy in transform)
            {
                if (Mathf.Abs(enemy.position.x) > edgeLimit)
                {
                    direction *= -1;
                    transform.position += Vector3.down * 0.5f;
                    break;
                }
            }
        }

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval && transform.childCount > 0)
        {
            shootTimer = 0;

            Transform randomEnemy =
                transform.GetChild(Random.Range(0, transform.childCount));

         
            Animator enemyAnim = randomEnemy.GetComponent<Animator>();
            if (enemyAnim != null)
            {
                enemyAnim.SetTrigger("shoot");
            }

            StartCoroutine(SpawnEnemyBullet(randomEnemy.position, 0.1f));
        }
    }

    System.Collections.IEnumerator SpawnEnemyBullet(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(enemyBulletPrefab, position, Quaternion.identity);
    }

    void onEnemyDied(float points)
    {
        moveInterval *= 0.9f;

        if (moveInterval < 0.2f)
            moveInterval = 0.2f;
    }
}