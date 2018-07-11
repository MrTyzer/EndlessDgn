using UnityEngine;
using System.Collections;
using Enums;

public class Weapon : ItemType
{
    public int mindmg, maxdmg;
    public wTypes wType;
    public void rolldmg()
    {
        int delta = Random.Range(0, (2 + Dgnlvl / 3));
        mindmg = 3 + delta + (2 * Dgnlvl);
        delta = Random.Range(0, (2 + Dgnlvl / 2));
        maxdmg = 8 + delta + (2 * Dgnlvl);
        if (maxdmg < mindmg)
            mindmg = maxdmg - 1;
    }
    public wTypes RandItClass()
    {
        int r_class = Random.Range(0, 3);
        switch (r_class)
        {
            case 0:
                return wTypes.axe;
            case 1:
                return wTypes.mace;
            case 2:
                return wTypes.sword;
            default:
                break;
        }
        return wTypes.sword;
    }
    public Weapon()
    {
        ItClass = ItClasses.Weapon;
        wType = RandItClass();
	    Name = typeIt + " " + wType.ToString();
        rolldmg();
    }
    public Weapon(typeI type) : base(type)
    {
        ItClass = ItClasses.Weapon;
        wType = RandItClass();
        Name = typeIt + " " + wType.ToString();
        rolldmg();
    }
    public string Show()
    {
        string Ret = "";
        Ret += Name + "\n";
        Ret += "ItemLvl: [" + ItemLvl.ToString() + "]" + "\n";
        Ret += mindmg.ToString() + " - " + maxdmg.ToString() + "\n";
        Ret += ReturnStats();
        return Ret;
    }
}
