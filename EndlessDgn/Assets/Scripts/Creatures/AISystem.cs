using System.Collections.Generic;
using UnityEngine;
using Enums;

public class AISystem
{
    private RoomType _currentRoom;

    public AISystem()
    {
        Messenger<RoomType>.AddListener(GameEvent.AI_INIT, AIInit);
        Messenger<Monster>.AddListener(GameEvent.AI_STRATEGY_SELECT, StrategySelector);
    }

    private void AIInit(RoomType room)
    {
        _currentRoom = room;
    }

    private void StrategySelector(Monster monster)
    {
        switch (monster.MonsterType)
        {
            case MonstersType.skeleton:
                LowHpFocusStrategy(monster);
                break;
            case MonstersType.spider:
                LowHpFocusStrategy(monster);
                break;
            default:
                LowHpFocusStrategy(monster);
                break;
        }
    }

    private void LowHpFocusStrategy(Monster monster)
    {
        Ability curAbility = monster.SelectAbility(_currentRoom);
        List<Creature> availableTargets = curAbility.GetAvailableTargets(_currentRoom, monster);
        Creature target = SelectLowHp(monster, availableTargets);
        curAbility.UseAbility(target, monster, _currentRoom);

        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_HIT, target.gameObject);
        monster.gameObject.GetComponent<Animator>().SetTrigger("StartAttack");
    }

    private Creature SelectLowHp(Monster monster, List<Creature> availableTargets)
    {
        Creature target = availableTargets[0];
        foreach (Creature c in availableTargets)
        {
            if (c.GetResultStat(Stats.Hp) < target.GetResultStat(Stats.Hp))
                target = c;
        }

        return target;
    }

    

}