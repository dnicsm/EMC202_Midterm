using UnityEngine;

public enum GameColor
{
    Red,
    Blue,
    Green
}

public interface ISwitchable
{
    void SwitchState();
}

public interface IDamageable
{
    void TakeDamage(GameColor bulletColor, Bullet bullet);
}