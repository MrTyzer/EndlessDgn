using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public abstract class Ability
{
    public Creature Owner { get; protected set; }
    public Abilities Name { get; protected set; }
    public int ALevel { set; get; }
    public int Cooldown { set; get; }

    /// <summary>
    /// Результат применения способности на конкретном существе
    /// </summary>
    public struct ResultOfAbility
    {
        /// <summary>
        /// величина нанесенного урона или лечения
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// результат атаки (крит, хит...)
        /// </summary>
        public Creature.AttackResult Res { get; private set; }

        public ResultOfAbility(int value, Creature.AttackResult res)
        {
            Value = value;
            Res = res;
        }
    }

    /// <summary>
    /// метод возвращает возможные цели для способности
    /// </summary>
    /// <returns></returns>
    public abstract List<Creature> GetAvailableTargets(RoomType room, Creature user);

    /// <summary>
    /// возвращает true, если способность можно использовать на данный таргет
    /// </summary>
    public abstract bool IsAvailable(Creature user, Creature target);

    public Ability(Creature owner)
    {
        Owner = owner;
    }

    public abstract void UseAbility(Creature target, Creature user, RoomType room);

    /// <summary>
    /// задает начальные значения словарю ResultsForAnim
    /// </summary>
    /// <param name="room">
    /// текущая комната, в которой применяется способность
    /// </param>
    /// <returns>
    /// словарь с нанесенным уроном или лечением по всем существам в комнате.
    /// Нужен для передачи в скрипт, управляющий анимацией.
    /// </returns>
    public Dictionary<Creature, ResultOfAbility> AwakeResForAnim(RoomType room)
    {
        Dictionary<Creature, ResultOfAbility> res = new Dictionary<Creature, ResultOfAbility>();

        foreach (Creature c in room.HeroesAndMobs)
        {
            res[c] = new ResultOfAbility(0, Creature.AttackResult.Hit);
        }

        return res;
    }

    public Creature.AttackResult Attack(Creature attacker, Creature target, int BonusAcc = 0)
    {
        int delta = attacker.GetResultStat(Stats.Accuracy) - target.GetResultStat(Stats.Dodge) + BonusAcc;
        int ranNum = Random.Range(0, 101);
        double deltad = delta / (1 + 0.1 * DgnInfo.DgnLvl);
        if (deltad > 40)
            deltad = 40;
        ranNum += (int)deltad;
        if ((ranNum >= 25) && (ranNum <= 50))
        {
            return Creature.AttackResult.Glance;
        }
        else
        {
            if ((ranNum > 50) && (ranNum <= 100))
            {
                return Creature.AttackResult.Hit;
            }
            else
            {
                if (ranNum > 100)
                {
                    return Creature.AttackResult.Crit;
                }
                else
                {
                    return Creature.AttackResult.Miss;
                }
            }
        }
    }

    public abstract string AbilityInfoShow();
}
