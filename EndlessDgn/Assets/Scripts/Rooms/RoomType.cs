using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Enums;


public abstract class RoomType
{
    /// <summary>
    /// размер комнаты
    /// </summary>
    public static int RoomSize { get; protected set; }
    public int RoomExp { get; private set; }
    public string Name { get; protected set; }
    public List<Hero> Heroes { get; protected set; }
    public List<Monster> Mobs { get; protected set; }
    public List<Creature> HeroesAndMobs { get; protected set; }
    public List<ItemType> Loot { get; protected set; }

    public RoomType()
    {
        Heroes = new List<Hero>();
        Mobs = new List<Monster>();
        HeroesAndMobs = new List<Creature>();
        Loot = new List<ItemType>();
    }

    protected void GetRoomExp()
    {
        foreach (Monster m in Mobs)
            RoomExp += m.expValue;
    }

    public Result EndOfRoom()//проверка на состояние комнаты
    {
        bool fl = true;
        foreach (Hero h in Heroes)
            fl &= !h.Alive;
        if (fl)
            return Result.Lose;
        fl = true;
        foreach (Monster m in Mobs)
            fl &= !m.Alive;
        if (fl)
            return Result.Win;
        else
            return Result.Cont;
    }

    public void RollLoot(int AmountOfItems)
    {
        int roll;
        for (int i = 0; i < AmountOfItems; i++)
        {
            roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    Loot.Add(new Weapon());
                    break;
                case 1:
                    Loot.Add(new Armour());
                    break;
                default:
                    Loot.Add(new Necklace());
                    break;
            }
        } 
    }
    public void RollLoot(int AmountOfIt, typeI ItType)
    {
        int roll;
        for (int i = 0; i < AmountOfIt; i++)
        {
            roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    Loot.Add(new Weapon(ItType));
                    break;
                case 1:
                    Loot.Add(new Armour(ItType));
                    break;
                default:
                    Loot.Add(new Necklace(ItType));
                    break;
            }
        }
    }
    /// <summary>
    /// функция создает комнату с монстрами 
    /// </summary>
    public static RoomType RollRT(List<Hero> party)
    {
        RoomSize = 3;
        if (DgnInfo.RoomNum < 5)
        {
            int Rand = Random.Range(0, 100) + 1;
            if (Rand <= 80)
            {
                OrdRoom Room = new OrdRoom(party);
                return Room;
                //Room.Fight(Room);
                DgnInfo.RoomNum++;
            }
            else
            {
                Treasury Treasury = new Treasury(party);
                return Treasury;
                //Treasury.Fight(Treasury);
                DgnInfo.RoomNum++;
            }
        }
        else
        {
            BossRoom Boss = new BossRoom(party);
            return Boss;
            //Boss.Fight(Boss);
            DgnInfo.RoomNum = 1;
            DgnInfo.DgnLvl++;
        }
    }

}
