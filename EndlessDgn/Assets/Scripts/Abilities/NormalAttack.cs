using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class NormalAttack : Ability
{
    public NormalAttack(Creatures owner) : base(owner)
    {
        Name = Abilities.NormalAttack;
        Cooldown = 0;
        ALevel = 0;
    }

    public override List<Creatures> GetAvailableTargets(RoomType room, Creatures user)
    {
        List<Creatures> availableTargets = new List<Creatures>();
        if (user is Monsters)
        {
            foreach (Hero m in room.Heroes)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        else
        {
            foreach (Monsters m in room.Mobs)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        return availableTargets;
    }

    public override bool IsAvailable(Creatures user, Creatures target)
    {
        if (user is Monsters && target is Hero)
            return true;
        else if (user is Hero && target is Monsters)
            return true;
        else
            return false;
    }

    public override string AbilityInfoShow()
    {
        int minDmg = Owner.GetResultStat(Stats.MinDmg);
        int maxDmg = Owner.GetResultStat(Stats.MaxDmg);

        string ret = "Hit the target for \n";
        ret += minDmg + " - " + maxDmg;
        return ret;
    }


    public override void UseAbility(Creatures target, Creatures user, RoomType room)
    {
        Dictionary<Creatures, ResultOfAbility> ResultsForAnim = AwakeResForAnim(room);
        Creatures.AttackResult res = Attack(user, target);
        
        int damage = Random.Range(user.GetResultStat(Stats.MinDmg), target.GetResultStat(Stats.MaxDmg) + 1);
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
                    target.ChangeResultStat(Stats.Hp, -damage / 2);
                    ResultsForAnim[target] = new ResultOfAbility(-damage / 2, Creatures.AttackResult.Glance);
                   //glance
                }
                break;
            case Creatures.AttackResult.Hit:
                {
                    target.ChangeResultStat(Stats.Hp, -damage);
                    ResultsForAnim[target] = new ResultOfAbility(-damage, Creatures.AttackResult.Hit);

                    //hit
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
        Messenger<Dictionary<Creatures, ResultOfAbility> >.Broadcast(GameEvent.ABILITY_INFO, ResultsForAnim);
    }
}
