using UnityEngine;
using System.Collections;
using Enums;

public class Necklace : ItemType
{
    public Necklace()
    {
        ItClass = ItClasses.Neck;
        Name = typeIt + " " + "neck";
        RollStat(RandChar());
    }
    public Necklace(typeI type) : base(type)
    {
        ItClass = ItClasses.Neck;
        Name = typeIt + " " + "neck";
        RollStat(RandChar());
    }
    public string Show()
    {
        string Ret = "";
        Ret += Name + "\n";
        Ret += "ItemLvl: [" + ItemLvl.ToString() + "]" + "\n";
        Ret += ReturnStats();
        return Ret;
    }
}
