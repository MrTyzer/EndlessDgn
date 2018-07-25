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
    private Stat _maxHp;

    public Hp(Stat maxHp) : base(Stats.Hp, maxHp.Value)
    {
        _maxHp = maxHp;
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        else if (Value > _maxHp.Value)
            Value = _maxHp.Value;
    }

}

/// <summary>
/// структура, содержащая силу определенного персонажа
/// </summary>
public class Strength : Stat
{
    private Stat _minDmg;
    private Stat _maxDmg;
    /// <summary>
    /// множитель dmg для силы
    /// </summary>
    private static int StrMod = 2;

    public Strength(int value, Stat mindmg, Stat maxdmg) : base(Stats.Strength, value)
    {
        _minDmg = mindmg;
        _maxDmg = maxdmg;
        _minDmg.ChangeStat(value * StrMod);
        _maxDmg.ChangeStat(value * StrMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        _minDmg.ChangeStat(value * StrMod);
        _maxDmg.ChangeStat(value * StrMod);
    }
}

/// <summary>
/// структура, содержащая ловкость определенного персонажа
/// </summary>
public class Agility : Stat
{
    private Stat _accuracy;
    private Stat _dodge;
    /// <summary>
	/// множитель acc/dodge для ловкости
	/// </summary>
    private static int AgiMod = 2;

    public Agility(int value, Stat accuracy, Stat dodge) : base(Stats.Agility, value)
    {
        _accuracy = accuracy;
        _dodge = dodge;
        _accuracy.ChangeStat(value * AgiMod);
        _dodge.ChangeStat(value * AgiMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        _accuracy.ChangeStat(value * AgiMod);
        _dodge.ChangeStat(value * AgiMod);
    }
}

/// <summary>
/// структура, содержащая стамину определенного персонажа
/// </summary>
public class Stamina : Stat
{
    private Stat _maxHp;
    private Stat _hp;
    /// <summary>
	/// множитель хп для стамины
	/// </summary>
    private static int StamMod = 6;

    public Stamina(int value, Stat maxHp, Stat hp) : base(Stats.Stamina, value)
    {
        _hp = hp;
        _maxHp = maxHp;
        _maxHp.ChangeStat(value * StamMod);
        _hp.ChangeStat(value * StamMod);
    }

    public override void ChangeStat(int value)
    {
        Value += value;
        if (Value < 0)
            Value = 0;
        _maxHp.ChangeStat(value * StamMod);
        if (_maxHp.Value < _hp.Value)
        _hp.ChangeStat(_maxHp.Value - _hp.Value);
    }
}
