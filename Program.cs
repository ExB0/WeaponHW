using System;

public class Weapon
{
     private int _damage;
     private int _bullets;

    public Weapon(int damage, int bullets)
    {
        _damage = damage;
        _bullets = bullets;
    }

    public int Damage => _damage;
    public int Bullets => _bullets;


    public void Fire(Player player)
    {
        if (player == null)
        {
            throw new ArgumentOutOfRangeException(nameof(player),"нет Цели");
        }
        if (_bullets <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_bullets),"Нет пули");
        }
        player.TakeDamage(_damage);
        _bullets -= 1;

    }
}

public class Player
{
    private int _health;

    public Player(int health)
    {
        _health = health;
    }

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        if (damage <=0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage),"урон не может быть меньше или равно 0");
        }
        _health = (int)MathF.Max(0, _health - damage);
        if (IsDead(_health))
        {
            return;
        }
    }

    private bool IsDead(int health)
    {
        return health <= 0;
    }
}

public class Bot
{
    private Weapon _weapon = new Weapon(2,30);

    public Bot(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void OnSeePlayer(Player player)
    {
        _weapon.Fire(player);
    }
}

