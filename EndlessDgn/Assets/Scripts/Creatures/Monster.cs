using UnityEngine;
using System.Collections;
using Enums;

public abstract class Monster : Creature
{
    /// <summary>
    /// Тип монстра
    /// </summary>
    public MonstersType MonsterType { get; protected set; }

    public abstract Ability SelectAbility(RoomType room);

    public override string ShowStats()
    {
        string Ret;
        Ret = Name + "\n";
        Ret += "Damage = " + GetResultStat(Stats.MinDmg) + " - " + GetResultStat(Stats.MaxDmg) + "\n";
        Ret += "Speed = " + GetResultStat(Stats.Speed) + "\n";
        Ret += "Accuracy = " + GetResultStat(Stats.Accuracy) + "\n";
        Ret += "Dodge = " + GetResultStat(Stats.Dodge) + "\n";
        return Ret;
    }

    public override void Turn()
    {
        Messenger<Monster>.Broadcast(GameEvent.AI_STRATEGY_SELECT, this);
    }

    /// <summary>
    /// опыт, получаемый с монстра
    /// </summary>
    public int expValue { get; protected set; }
    /// <summary>
    /// модификатор acc/dodge per DgnLvl
    /// </summary>
    protected static int ADLvlMod = 4;
    /// <summary>
    /// модификатор Dmg per DgnLvl
    /// </summary>
    protected static int DmgLvlMod = 3;
    /// <summary>
    /// модификатор Hp per DgnLvl
    /// </summary>
    protected static int HpLvlMod = 9;
}
