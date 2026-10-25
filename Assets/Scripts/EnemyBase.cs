using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    public float moveSpeed = 3f;
    public GameColor enemyColor;
    protected Transform player;

    protected Transform enemyTransform;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        enemyTransform = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().color = Bullet.GetColorValue(enemyColor);
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
        CheckGameOver();
    }

    protected void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    protected void CheckGameOver()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= 0.5f)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0f;
            }
        }
    }

    public abstract void TakeDamage(GameColor bulletColor, Bullet bullet);
}