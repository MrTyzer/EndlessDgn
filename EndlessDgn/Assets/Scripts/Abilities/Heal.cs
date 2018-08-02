using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Heal : Ability
{

    public Heal(Creatures owner) : base(owner)
    {
        Name = Abilities.Heal;
        Cooldown = 2;
        ALevel = 0;
    }

    public override List<Creatures> GetAvailableTargets(RoomType room, Creatures user)
    {
        List<Creatures> availableTargets = new List<Creatures>();
        if (user is Monster)
        {
            foreach (Monster m in room.Mobs)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        else
        {
            foreach (Hero m in room.Heroes)
            {
                if (m.Alive)
                    availableTargets.Add(m);
            }
        }
        return availableTargets;
    }

    public override bool IsAvailable(Creatures user, Creatures target)
    {
        if (user is Monster && target is Monster)
            return true;
        else if (user is Hero && target is Hero)
            return true;
        else
            return false;
    }

    public override string AbilityInfoShow()
    {
        int healHpMin = Owner.GetResultStat(Stats.Stamina) * 2 + ALevel * 2 + 3;
        int healHpMax = Owner.GetResultStat(Stats.Stamina) * 2 + ALevel * 2 + 9;

        string ret = "Heal a friend for \n";
        ret += healHpMin + " - " + healHpMax;
        return ret;
    }

    public override void UseAbility(Creatures target, Creatures healer, RoomType room)
    {
        Dictionary<Creatures, ResultOfAbility> ResultsForAnim = AwakeResForAnim(room);

        //сделать проверку на макс хп

        int healHp = healer.GetResultStat(Stats.Stamina) * 2 + ALevel * 2 + Random.Range(3,9);
        target.ChangeResultStat(Stats.Hp, healHp);
        //if (target.DynamicStats[Creatures.Stats.Hp] > target)
        ResultsForAnim[target] = new ResultOfAbility(healHp, Creatures.AttackResult.Hit);

        //сделать отдельный результат атаки - хилл

        target.TestLive();
        Messenger<Dictionary<Creatures, ResultOfAbility>>.Broadcast(GameEvent.ABILITY_INFO, ResultsForAnim);
    }

    
}
