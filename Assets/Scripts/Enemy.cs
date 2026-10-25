using UnityEngine;

public class Enemy : EnemyBase
{
    public override void TakeDamage(GameColor bulletColor, Bullet bullet)
    {
        if (bulletColor == this.enemyColor)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
        else
        {
            Destroy(bullet.gameObject);
        }
    }
}