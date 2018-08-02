using UnityEngine;
using System.Collections;
using Enums;

public abstract class Monster : Creatures
{
    public enum MonstersType
    {
        skeleton,
        spider
    }
    public MonstersType _MType { get; protected set; }

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
    /// <summary>
    /// опыт, получаемый с монстра
    /// </summary>
    public int expValue { get; protected set; }
    /// <summary>
    /// модификатор acc/dodge per DgnLvl
    /// </summary>
    public static int ADLvlMod = 4;
    /// <summary>
    /// модификатор Dmg per DgnLvl
    /// </summary>
    public static int DmgLvlMod = 3;
    /// <summary>
    /// модификатор Hp per DgnLvl
    /// </summary>
    public static int HpLvlMod = 9;
}
