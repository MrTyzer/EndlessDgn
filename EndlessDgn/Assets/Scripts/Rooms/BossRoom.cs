﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;


public class BossRoom : RoomType
{
    private void RollM()//дописать!
    {
        for (int i = 0; i < RoomSize; i++)
        {
            int randmob = Random.Range(0, 2);
            Monsters mon;
            switch (randmob)
            {
                case 0:
                    mon = Creator.CreateSkeleton();
                    break;
                case 1:
                    mon = Creator.CreateSpider();
                    break;
                default:
                    mon = Creator.CreateSkeleton();
                    break;
            }
            Mobs.Add(mon);
            HeroesAndMobs.Add(mon);
        }
    }

    public BossRoom(List<Hero> party)
    {
        Heroes = party;
        foreach (Hero h in Heroes)
        {
            HeroesAndMobs.Add(h);
        }
        int AmountOfDrop = 2;
        Name = "Boss room";
        RollM();
        RollLoot(AmountOfDrop);
        RollLoot(1, typeI.leg);
        GetRoomExp();
    }
}
