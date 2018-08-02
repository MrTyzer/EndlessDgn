using UnityEngine;
using System.Collections.Generic;
using Enums;

/// <summary>
/// отвечает за ход игрового процесса
/// </summary>
public class Game : MonoBehaviour
{
    public GameObject[] Positions;

    /// <summary>
    /// результат конкретного боя
    /// </summary>
    private Result _result;
    private RoomType _curRoom;
    private List<Creatures> _roomCreatures;

    /// <summary>
    /// существо, ходящее в данный момент
    /// </summary>
    private Creatures _crTurn;

    /// <summary>
    /// максимальное значение шкалы хода (при котором существо получает ход)
    /// </summary>
    public static int TurnLineMaxValue { get; private set; }

    private void Awake()
    {
        TurnLineMaxValue = 1000;
    }

    private void Start()
    {
        Messenger.AddListener(GameEvent.END_OF_TURN, OnEndOfTurn);
        EnterRoom();
        TurnCalc(_curRoom);
    }

    /// <summary>
    /// Основной метод.
    /// </summary>
    public void EnterRoom()
    {
        _curRoom = RoomType.RollRT(Creator.PartyList);
        foreach (Hero h in Creator.PartyList)
        {
            h.RefreshStats();
        }
        RoomShow(_curRoom.HeroesAndMobs);
        Messenger<RoomType>.Broadcast(GameEvent.ROOM_INTERFACE_INIT, _curRoom);
    }

    /// <summary>
    /// метод подсчета очередности ходов
    /// </summary>
    public void TurnCalc(RoomType Room)
    {
        while (_crTurn == null)
        {
            _crTurn = FillingTurnLine(_curRoom.HeroesAndMobs);
        }
        _crTurn.Turn();
        _crTurn = null;
    }

    /// <summary>
    /// метод заполнения шкалы хода у существ. Возвращает существо, у которого подошла очередность хода (null, если таких нет)
    /// </summary>
    private Creatures FillingTurnLine(List<Creatures> heroesAndMobs)
    {
        //все существа с полной шкалой хода на данной итерации заполнения шкалы
        List<Creatures> creaturesToTurn = new List<Creatures>();
        
        foreach (Creatures c in heroesAndMobs)
        {
            if (c.GetResultStat(Stats.TurnLine) >= TurnLineMaxValue && c.Alive)
            {
                creaturesToTurn.Add(c);
            }
        }

        if (creaturesToTurn.Count != 0)
        {
            Creatures crWithHighSpeed = creaturesToTurn[0];
            foreach (Creatures c in creaturesToTurn)
            {
                if (c.GetResultStat(Stats.Speed) > crWithHighSpeed.GetResultStat(Stats.Speed))
                    crWithHighSpeed = c;
            }
            Messenger.Broadcast(GameEvent.UPDATE_ENERGY_BARS);
            crWithHighSpeed.ChangeResultStat(Stats.TurnLine, -TurnLineMaxValue);
            return crWithHighSpeed;
        }

        foreach (Creatures c in heroesAndMobs)
        {
            if (c.Alive)
                c.ChangeResultStat(Stats.TurnLine, c.GetResultStat(Stats.Speed));
        }
        return null;
    }

    private void OnEndOfTurn()
    {
        if (_curRoom.EndOfRoom() == Result.Cont)
            TurnCalc(_curRoom);
        else
        {
            foreach (ItemType i in _curRoom.Loot)
            {
                Hero.Inventory.AddNewItem(i);//переработать всю структуру инвентаря
            }
            DestroyRoom();
            EnterRoom();
            TurnCalc(_curRoom);
        }
    }

    private void DestroyRoom()
    {
        foreach (Monster m in _curRoom.Mobs)
        {
            Destroy(m.gameObject);
        }
    }

    public void RoomShow(List<Creatures> heroesAndMobs)
    {
        _roomCreatures = heroesAndMobs;
        for (int i = 0; i < heroesAndMobs.Count; i++)
        {
            heroesAndMobs[i].transform.position = Positions[i].transform.position;
        }
    }
}
