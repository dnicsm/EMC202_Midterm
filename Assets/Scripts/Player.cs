using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, ISwitchable
{
    public GameColor currentState = GameColor.Red;
    private SpriteRenderer spriteRenderer;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 1f;

    public float detectionRange = 10f;
    public LayerMask enemyLayer;
    public float rotationSpeed = 500f;

    private Transform nearestEnemy;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColorVisual();

        StartCoroutine(AutoShootRoutine());
    }

    private void Update()
    {
        FindNearestEnemy();
        RotateTowardsEnemy();
    }

    public void OnMouseDown()
    {
        SwitchState();
    }

    public void SwitchState()
    {
        int nextColor = ((int)currentState + 1) % 3;
        currentState = (GameColor)nextColor;
        UpdateColorVisual();
    }

    private void UpdateColorVisual()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Bullet.GetColorValue(currentState);
        }
    }

    private void FindNearestEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
        float shortestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (Collider2D enemyCol in hitColliders)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemyCol.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearest = enemyCol.transform;
            }
        }

        nearestEnemy = nearest;
    }

    private void RotateTowardsEnemy()
    {
        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle - 90f));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private IEnumerator AutoShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Shooting");
        if (bulletPrefab != null)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.Initialize(transform.up, currentState);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}