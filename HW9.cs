

 /* 2.1*/



using System;

public interface ICombatant
{
    public string Name { get; }
    public int HP { get; set; }
    public bool IsDead { get; }
    public int Damage { get; }
    public void Attack(ICombatant target);
    public void TakeDamage(int amount);
}

public class Knight : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 150;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 25;

    public Knight(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} мёртв и не может атаковать!");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Рыцарь {Name} не может атаковать мёртвую цель!");
            return;
        }

        Console.WriteLine($"Рыцарь {Name} атакует!");
        target.TakeDamage(Damage);
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Рыцарь {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

public class Mage : ICombatant
{
    public string Name { get; }
    public int HP { get; set; } = 80;
    public bool IsDead => HP <= 0;
    public int Damage { get; } = 40;

    public Mage(string name)
    {
        Name = name;
    }

    public void Attack(ICombatant target)
    {
        if (IsDead)
        {
            Console.WriteLine($"Маг {Name} мёртв и не может атаковать!");
            return;
        }

        if (target.IsDead)
        {
            Console.WriteLine($"Маг {Name} не может атаковать мёртвую цель");
            return;
        }

        Console.WriteLine($"Маг {Name} применяет двойное заклинание!");
        target.TakeDamage(Damage);

        if (!target.IsDead)
        {
            target.TakeDamage(Damage);
        }
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
        Console.WriteLine($"Маг {Name} получил {amount} урона. Осталось HP: {HP}");
    }
}

class Program
{
    static void Main()
    {
        ICombatant knight = new Knight("Артур");
        ICombatant mage = new Mage("Мерлин");

        while (!knight.IsDead && !mage.IsDead)
        {
            knight.Attack(mage);
            if (mage.IsDead) break;
            mage.Attack(knight);
            Console.WriteLine();
        }

        Console.WriteLine();
        if (!knight.IsDead)
            Console.WriteLine($"Победил Рыцарь {knight.Name}!");
        else
            Console.WriteLine($"Победил Маг {mage.Name}!");
    }
}



/*
 * 2.2

using System;




using System.Collections.Generic;

public enum Rarity
{
    Common,
    Rare,
    Legendary
}

public interface IUsableItem
{
    string Name { get; }
    string Description { get; }
    Rarity Rarity { get; }
    void Use(Player player);
}

public class Player
{
    public string Name { get; }
    public int HP { get; private set; }
    public int Damage { get; private set; }
    public bool IsDead => HP <= 0;

    public Player(string name, int hp = 100, int damage = 15)
    {
        Name = name;
        HP = hp;
        Damage = damage;
    }

    public void Heal(int amount)
    {
        HP += amount;
    }

    public void TakeDamage(int amount)
    {
        HP = Math.Max(0, HP - amount);
    }

    public void IncreaseDamage(int amount)
    {
        Damage += amount;
    }
}

public class HealthPotion : IUsableItem
{
    public string Name => "Малое зелье здоровья";
    public string Description => "Восстанавливает 50 единиц здоровья.";
    public Rarity Rarity => Rarity.Common;

    public void Use(Player player)
    {
        player.Heal(50);
        Console.WriteLine($"[{Rarity}] {Name}: Вы выпили зелье и восстановили 50 HP.");
    }
}

public class Sword : IUsableItem
{
    public string Name => "Стальной клинок";
    public string Description => "Увеличивает силу атаки на 10.";
    public Rarity Rarity => Rarity.Rare;
    public void Use(Player player)
    {
        player.IncreaseDamage(10);
        Console.WriteLine($"[{Rarity}] {Name}: Вы экипировали меч, ваш урон вырос на 10.");
    }
}

public class AncientScroll : IUsableItem
{
    public string Name => "Свиток тёмной силы";
    public string Description => "Наносит 20 урона владельцу, но повышает урон на 30.";
    public Rarity Rarity => Rarity.Legendary;

    public void Use(Player player)
    {
        player.TakeDamage(20);
        player.IncreaseDamage(30);
        Console.WriteLine($"[{Rarity}] {Name}: Проклятие свитка отняло 20 HP, но ваш урон увеличился на 30!");
    }
}

class Program
{
    static void Main()
    {
        Player player = new Player("Геральт", hp: 60, damage: 20);
        Console.WriteLine($"Старт — Игрок: {player.Name}, HP: {player.HP}, Урон: {player.Damage}");

        List<IUsableItem> inventory = new List<IUsableItem>
        {
            new HealthPotion(),
            new Sword(),
            new AncientScroll() };
        foreach (IUsableItem item in inventory)
        {
            item.Use(player);
        }

        Console.WriteLine("--- Итоговые характеристики ---");
        Console.WriteLine($"Игрок:{player.Name}");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Урон: {player.Damage}");
        Console.WriteLine($"Состояние: {(player.IsDead ? "Мёртв" : "Жив")}");
    }
}

 */
/*
using System;

public interface IDamageCalculator
{
    int CalculateDamage(int baseDamage, int targetArmor);
}

public class PhysicalDamageCalculator: IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor/ 2;
        return Math.Max(1, damage);
    }
}

public class MagicDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 4;
        damage = Math.Max(1, damage);

        if (targetArmor == 0)
        {
            damage *= 2;
        }

        return damage;
    }
}

public class FireDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int baseDamage, int targetArmor)
    {
        int damage = baseDamage - targetArmor / 10;
        damage = Math.Max(1, damage);

        if (baseDamage > 50)
        {
            damage += 10;
        }

        return damage;
    }
}
public class Unit
{
    public string Name { get; }
    public int HP {get;set; }
    public int Armor { get;}
    public Unit(string name, int hp, int armor)
    {
        Name = name;
        HP = hp;
        Armor = armor;
    }
}

public class DamageSystem
{
    private readonly IDamageCalculator _calculator;

    public DamageSystem(IDamageCalculator calculator)
    {
        _calculator = calculator;
    }
    public void ApplyDamage(Unit target, int baseDamage)
    {
        int damage = _calculator.CalculateDamage(baseDamage, target.Armor);
        target.HP = Math.Max(0, target.HP - damage);
        Console.WriteLine($"Юнит {target.Name} получил {damage} урона. Осталось HP: {target.HP}");
    }
}

class Program
{
    static void Main()
    {
        Unit armoredGolem = new Unit("Каменный голем", hp: 300, armor: 40);
        Unit unarmoredSlime = new Unit("Лесной слизень", hp: 150, armor: 0);

        DamageSystem physicalSystem = new DamageSystem(new PhysicalDamageCalculator());
        DamageSystem magicSystem = new DamageSystem(new MagicDamageCalculator());
        DamageSystem fireSystem = new DamageSystem(new FireDamageCalculator());

        Console.WriteLine("--- Атаки по бронированному голему (Armor = 40, baseDamage = 60) ---");
        physicalSystem.ApplyDamage(armoredGolem, 60); // 60- 20 = 40
        magicSystem.ApplyDamage(armoredGolem, 60);    // 60 - 10 = 50
        fireSystem.ApplyDamage(armoredGolem, 60);     // (60 -4) + 10 = 66 

        Console.WriteLine("--- Атака магией по цели без брони (Armor = 0, baseDamage = 30) ---");
        magicSystem.ApplyDamage(unarmoredSlime, 30);  // 30 *2=60
    }
}
*/

/*
 * Ответ:
 * Чтобы добавить новый тип урона, класс DamageSystem менять вообще не нужно.
 * Достаточно создать новый класс IceDamageCalculator, который реализует интерфейс IDamageCalculator
 * и описывает свою формулу расчёта в методе CalculateDamage.
 * После этого экземпляр new IceDamageCalculator()(если например замораживаем) просто передаётся в конструктор new DamageSystem(...).
 * Это работает благодаря полиморфизму и принципу инверсии зависимостей (Dependency Injection):
 * DamageSystem зависит от абстракции (интерфейса IDamageCalculator), а не от конкретных классов расчёта,
 * что полностью соответствует принципу открытости/закрытости.
 */


