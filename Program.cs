using System;

public class Weapon
{
    private const int BulletPerShot = 1;

    public int Damage { get; }
    public int Bullets { get; private set; }

    public Weapon(int damage, int bullets)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Урон должен быть > 0");
        if (bullets < 0)
            throw new ArgumentOutOfRangeException(nameof(bullets), "Пуль не может быть < 0");

        Damage = damage;
        Bullets = bullets;
    }

    public void Fire(Player player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player), "Нет цели");

        if (Bullets < BulletPerShot)
            throw new InvalidOperationException("Недостаточно пуль");

        player.TakeDamage(Damage);
        Bullets -= BulletPerShot;
    }
}

public class Player
{
    private int _health;

    public Player(int health)
    {
        if (health <= 0)
            throw new ArgumentOutOfRangeException(nameof(health), "Здоровье должно быть > 0");
        _health = health;
    }

    public int Health => _health;
    public bool IsDead => _health <= 0;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Урон должен быть > 0");
        if (IsDead)
        {
            throw new InvalidOperationException("Цель мертва");
        }
        _health = (int)MathF.Max(0, _health - damage);
    }
}


public class Bot
{
    private readonly Weapon _weapon;

    public Bot(Weapon weapon)
    {
        _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}
