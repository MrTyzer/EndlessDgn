using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Spider : Monster
{
    public void Awake()
    {
        StatBuilder();
        SpellBook = new List<Ability>();
        SpellBook.Add(new NormalAttack(this));
        Name = "Spider";
        _MType = MonstersType.spider;
        expValue = 5 + (3 * DgnInfo.DgnLvl);
        Alive = true;
    }

    protected override void StatBuilder()
    {
        ResultStats = new Dictionary<Stats, Stat>();
        ResultStats[Stats.TurnLine] = new Stat(Stats.TurnLine, 0);
        ResultStats[Stats.Speed] = new Stat(Stats.Speed, 100);
        ResultStats[Stats.MaxHp] = new Stat(Stats.MaxHp, 30 + (HpLvlMod * DgnInfo.DgnLvl));
        ResultStats[Stats.Hp] = new Hp(ResultStats[Stats.MaxHp]);
        ResultStats[Stats.Accuracy] = new Stat(Stats.Accuracy, 40 + (ADLvlMod * DgnInfo.DgnLvl));
        ResultStats[Stats.Dodge] = new Stat(Stats.Dodge, 40 + (ADLvlMod * DgnInfo.DgnLvl));
        ResultStats[Stats.MinDmg] = new Stat(Stats.MinDmg, 7 + DmgLvlMod * DgnInfo.DgnLvl);
        int delta = Random.Range(0, 2 + DgnInfo.DgnLvl);
        ResultStats[Stats.MaxDmg] = new Stat(Stats.MaxDmg, 10 + delta + DmgLvlMod * DgnInfo.DgnLvl);
    }

    public override void Turn(RoomType room)
    {
        Ability selectedAbility = new NormalAttack(this);//дописать логику выбора способности монстром
        List<Creatures> availableTargets = selectedAbility.GetAvailableTargets(room, this);
        Creatures target = availableTargets[0];
        foreach (Creatures c in availableTargets)
        {
            if (c.GetResultStat(Stats.Hp) < target.GetResultStat(Stats.Hp))
                target = c;
        }

        selectedAbility.UseAbility(target, this, room);
        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_HIT, target.gameObject);
        this.gameObject.GetComponent<Animator>().SetTrigger("StartAttack");
    }
}
