using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public GameColor bulletColor;
    private Vector2 direction;

    public void Initialize(Vector2 dir, GameColor color)
    {
        direction = dir.normalized;
        bulletColor = color;
        
        GetComponent<SpriteRenderer>().color = GetColorValue(color);
        
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(bulletColor, this);
        }
    }

    public static Color GetColorValue(GameColor color)
    {
        switch (color)
        {
            case GameColor.Red: return Color.red;
            case GameColor.Blue: return Color.blue;
            case GameColor.Green: return Color.green;
            default: return Color.white;
        }
    }
}