using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

/// <summary>
/// структура для создания статов, изменения которых не влияют на другие статы (hp, accuracy, dodge и тд)
/// </summary>
public class Stat
{
    public Stats StatName { get; protected set; }
    public int Value { get; protected set; }
    
    public Stat(Stats name, int value)
    {
        StatName = name;
        Value = value;
    }
 
    /// <summary>
    /// добавляет величину value к параметру
    /// </summary>
    public virtual void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
    }

}

public class Hp : Stat
{
    private Stat MaxHp;

    public Hp(Stat maxHp) : base(Stats.Hp, maxHp.Value)
    {
        MaxHp = maxHp;
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        else if (Value > MaxHp.Value)
            Value = MaxHp.Value;
    }

}

/// <summary>
/// структура, содержащая силу определенного персонажа
/// </summary>
public class Strength : Stat
{
    private Stat MinDmg;
    private Stat MaxDmg;
    /// <summary>
    /// множитель dmg для силы
    /// </summary>
    private static int StrMod = 2;

    public Strength(int value, Stat mindmg, Stat maxdmg) : base(Stats.Strength, value)
    {
        MinDmg = mindmg;
        MaxDmg = maxdmg;
        MinDmg.ChangeStat(value * StrMod);
        MaxDmg.ChangeStat(value * StrMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        MinDmg.ChangeStat(value * StrMod);
        MaxDmg.ChangeStat(value * StrMod);
    }
}

/// <summary>
/// структура, содержащая ловкость определенного персонажа
/// </summary>
public class Agility : Stat
{
    private Stat Accuracy;
    private Stat Dodge;
    /// <summary>
	/// множитель acc/dodge для ловкости
	/// </summary>
    private static int AgiMod = 2;

    public Agility(int value, Stat accuracy, Stat dodge) : base(Stats.Agility, value)
    {
        Accuracy = accuracy;
        Dodge = dodge;
        Accuracy.ChangeStat(value * AgiMod);
        Dodge.ChangeStat(value * AgiMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        Accuracy.ChangeStat(value * AgiMod);
        Dodge.ChangeStat(value * AgiMod);
    }
}

/// <summary>
/// структура, содержащая стамину определенного персонажа
/// </summary>
public class Stamina : Stat
{
    private Stat MaxHp;
    private Stat Hp;
    /// <summary>
	/// множитель хп для стамины
	/// </summary>
    private static int StamMod = 6;

    public Stamina(int value, Stat maxHp, Stat hp) : base(Stats.Stamina, value)
    {
        Hp = hp;
        MaxHp = maxHp;
        MaxHp.ChangeStat(value * StamMod);
        Hp.ChangeStat(value * StamMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        MaxHp.ChangeStat(value * StamMod);
        if (MaxHp.Value < Hp.Value)
        Hp.ChangeStat(MaxHp.Value - Hp.Value);
    }
}
