using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class HeroicStrike : Ability
{
    public HeroicStrike(Creatures owner) : base(owner)
    {
        Name = Abilities.HeroicStrike;
        Cooldown = 2;
        ALevel = 0;
    }

    public override List<Creatures> GetAvailableTargets(RoomType room, Creatures user)
    {
        List<Creatures> availableTargets = new List<Creatures>();
        if (user is Monster)
        {
            foreach (Hero m in room.Heroes)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        else
        {
            foreach (Monster m in room.Mobs)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        return availableTargets;
    }

    public override bool IsAvailable(Creatures user, Creatures target)
    {
        if (user is Monster && target is Hero)
            return true;
        else if (user is Hero && target is Monster)
            return true;
        else
            return false;
    }

    public override string AbilityInfoShow()
    {
        int minDmg = Owner.GetResultStat(Stats.Strength) * 2 + ALevel * 2 + Owner.GetResultStat(Stats.MinDmg);
        int maxDmg = Owner.GetResultStat(Stats.Strength) * 2 + ALevel * 2 + Owner.GetResultStat(Stats.MaxDmg);

        string ret = "Hit the target for \n";
        ret += minDmg + " - " + maxDmg;
        return ret;
    }

    public override void UseAbility(Creatures target, Creatures user, RoomType room)
    {
        Dictionary<Creatures, ResultOfAbility> ResultsForAnim = AwakeResForAnim(room);
        Creatures.AttackResult res = Attack(user, target);

        int damage = user.GetResultStat(Stats.Strength) * 2 + ALevel * 2 + Random.Range(user.GetResultStat(Stats.MinDmg), 
            target.GetResultStat(Stats.MaxDmg) + 1);
        switch (res)
        {
            case Creatures.AttackResult.Miss:
                {
                    //miss
                    ResultsForAnim[target] = new ResultOfAbility(0, Creatures.AttackResult.Miss);
                }
                break;
            case Creatures.AttackResult.Glance:
                {
                    //glance
                    target.ChangeResultStat(Stats.Hp, -damage / 2);
                    ResultsForAnim[target] = new ResultOfAbility(-damage / 2, Creatures.AttackResult.Glance);
                }
                break;
            case Creatures.AttackResult.Hit:
                {
                    //hit
                    target.ChangeResultStat(Stats.Hp, -damage);
                    ResultsForAnim[target] = new ResultOfAbility(-damage, Creatures.AttackResult.Hit);
                }
                break;
            case Creatures.AttackResult.Crit:
                {
                    //crit
                    target.ChangeResultStat(Stats.Hp, -damage * 2);
                    ResultsForAnim[target] = new ResultOfAbility(-damage * 2, Creatures.AttackResult.Crit);
                }
                break;
            default:
                break;
        }

        target.TestLive();
        Messenger<Dictionary<Creatures, ResultOfAbility>>.Broadcast(GameEvent.ABILITY_INFO, ResultsForAnim);
    }
}


