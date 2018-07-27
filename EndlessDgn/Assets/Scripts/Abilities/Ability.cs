using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public abstract class Ability
{
    public Creatures Owner { get; protected set; }
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
        public Creatures.AttackResult Res { get; private set; }

        public ResultOfAbility(int value, Creatures.AttackResult res)
        {
            Value = value;
            Res = res;
        }
    }

    /// <summary>
    /// метод возвращает возможные цели для способности
    /// </summary>
    /// <returns></returns>
    public abstract List<Creatures> GetAvailableTargets(RoomType room, Creatures user);

    /// <summary>
    /// возвращает true, если способность можно использовать на данный таргет
    /// </summary>
    public abstract bool IsAvailable(Creatures user, Creatures target);

    public Ability(Creatures owner)
    {
        Owner = owner;
    }

    public abstract void UseAbility(Creatures target, Creatures user, RoomType room);

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
    public Dictionary<Creatures, ResultOfAbility> AwakeResForAnim(RoomType room)
    {
        Dictionary<Creatures, ResultOfAbility> res = new Dictionary<Creatures, ResultOfAbility>();

        foreach (Creatures c in room.HeroesAndMobs)
        {
            res[c] = new ResultOfAbility(0, Creatures.AttackResult.Hit);
        }

        return res;
    }

    public Creatures.AttackResult Attack(Creatures attacker, Creatures target, int BonusAcc = 0)
    {
        int delta = attacker.GetResultStat(Stats.Accuracy) - target.GetResultStat(Stats.Dodge) + BonusAcc;
        int ranNum = Random.Range(0, 101);
        double deltad = delta / (1 + 0.1 * DgnInfo.DgnLvl);
        if (deltad > 40)
            deltad = 40;
        ranNum += (int)deltad;
        if ((ranNum >= 25) && (ranNum <= 50))
        {
            return Creatures.AttackResult.Glance;
        }
        else
        {
            if ((ranNum > 50) && (ranNum <= 100))
            {
                return Creatures.AttackResult.Hit;
            }
            else
            {
                if (ranNum > 100)
                {
                    return Creatures.AttackResult.Crit;
                }
                else
                {
                    return Creatures.AttackResult.Miss;
                }
            }
        }
    }

    public abstract string AbilityInfoShow();
}
