using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

/// <summary>
/// базовый класс для всех персонажей игры (и монстров и героев)
/// </summary>
public abstract class Creatures : MonoBehaviour
{
    public bool Alive { get; protected set; }
    public List<Ability> SpellBook { get; protected set; }

    public void AddAbility(Ability _ability)
    {
        SpellBook.Add(_ability);
    }

    /// <summary>
    /// Имя существа
    /// </summary>
    public string Name { get; set; }

    public enum AttackResult { Miss = 1, Glance, Hit, Crit };

    /// <summary>
    /// статы существа
    /// </summary>
    protected Dictionary<Stats, Stat> ResultStats = new Dictionary<Stats, Stat>();


    /// <summary>
    /// изменяет статы(во время боя)
    /// </summary>
    public void ChangeResultStat(Stats stat, int value)
    {
        ResultStats[stat].ChangeStat(value);
    }

    /// <summary>
    /// getter для resultStats
    /// </summary>
    public int GetResultStat(Stats stat)
    {
        return ResultStats[stat].Value;
    }

    public void TestLive()
    {
        if (ResultStats[Stats.Hp].Value <= 0)
        {
            Alive = false;
            ResultStats[Stats.TurnLine].ChangeStat(-ResultStats[Stats.TurnLine].Value);
        }
        else
            Alive = true;
    }

    public abstract void StatBuilder();
    public abstract string ShowStats();
    public abstract void Turn(RoomType Room);
}
