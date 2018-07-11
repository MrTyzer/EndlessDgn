using UnityEngine;
using System.Collections;
using Enums;

public class Armour : ItemType
{
    public int bonusHp;
    public void rollHp()
    {
        int delta = Random.Range(0, (5 + Dgnlvl));
        bonusHp = 10 + delta + 3 * Dgnlvl;
    }
    public Armour()
    {
        ItClass = ItClasses.Armour;
        Name = typeIt + " " + "armour";
        rollHp();
    }
    public Armour(typeI type) : base(type)
    {
        ItClass = ItClasses.Armour;
        Name = typeIt + " " + "armour";
        rollHp();
    }
    public string Show()
    {
        string Ret = "";
        Ret += Name + "\n";
        Ret += "ItemLvl: [" + ItemLvl.ToString() + "]" + "\n";
        Ret += "Bonus Hp = " + bonusHp.ToString() + "\n";
        Ret += ReturnStats();
        return Ret;
    }
}
