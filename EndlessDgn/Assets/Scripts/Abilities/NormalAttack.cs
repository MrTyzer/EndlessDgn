using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class NormalAttack : Ability
{
    public NormalAttack(Creature owner) : base(owner)
    {
        Name = Abilities.NormalAttack;
        Cooldown = 0;
        ALevel = 0;
    }

    public override List<Creature> GetAvailableTargets(RoomType room, Creature user)
    {
        List<Creature> availableTargets = new List<Creature>();
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

    public override bool IsAvailable(Creature user, Creature target)
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
        int minDmg = Owner.GetResultStat(Stats.MinDmg);
        int maxDmg = Owner.GetResultStat(Stats.MaxDmg);

        string ret = "Hit the target for \n";
        ret += minDmg + " - " + maxDmg;
        return ret;
    }


    public override void UseAbility(Creature target, Creature user, RoomType room)
    {
        Dictionary<Creature, ResultOfAbility> ResultsForAnim = AwakeResForAnim(room);
        Creature.AttackResult res = Attack(user, target);
        
        int damage = Random.Range(user.GetResultStat(Stats.MinDmg), target.GetResultStat(Stats.MaxDmg) + 1);
        switch (res)
        {
            case Creature.AttackResult.Miss:
                {
                    //miss
                    ResultsForAnim[target] = new ResultOfAbility(0, Creature.AttackResult.Miss);
                }
                break;
            case Creature.AttackResult.Glance:
                {
                    target.ChangeResultStat(Stats.Hp, -damage / 2);
                    ResultsForAnim[target] = new ResultOfAbility(-damage / 2, Creature.AttackResult.Glance);
                   //glance
                }
                break;
            case Creature.AttackResult.Hit:
                {
                    target.ChangeResultStat(Stats.Hp, -damage);
                    ResultsForAnim[target] = new ResultOfAbility(-damage, Creature.AttackResult.Hit);

                    //hit
                }
                break;
            case Creature.AttackResult.Crit:
                {
                    //crit
                    target.ChangeResultStat(Stats.Hp, -damage * 2);
                    ResultsForAnim[target] = new ResultOfAbility(-damage * 2, Creature.AttackResult.Crit);
                }
                break;
            default:
                break;
        }

        target.TestLive();
        Messenger<Dictionary<Creature, ResultOfAbility> >.Broadcast(GameEvent.ABILITY_INFO, ResultsForAnim);
    }
}
