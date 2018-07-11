using UnityEngine;
using System.Collections;
using Enums;

public abstract class ItemType
{
    //public Sprite Icon { get; protected set; }
    
    public string Name;
    public string typeIt;
    /// <summary>
	/// класс шмотки (Weapon, Armour, Neck)
	/// </summary>
    public ItClasses ItClass;
    
    public int Strength, Agility, Stamina, Accuracy, Speed, Dodge, ItemLvl, Dgnlvl;
    public void setName(string newname)
    {
        Name = newname;
    }
    /// <summary>
	/// функция возвращает рандомный стат
	/// </summary>
    public e_stat RandStat()
    {
        int roll;
        roll = Random.Range(0, 3);
        return (e_stat)roll; 
    }

    /// <summary>
	/// функция возвращает рандомную хар-ку (сила, ловкость, стамина)
	/// </summary>
    public e_stat RandChar()
    {
        int roll = Random.Range((int)e_stat.e_str, (int)e_stat.e_agi + 1);
        return (e_stat)roll;
    }

    public string ReturnStats()
    {
        string ReturnValues = "";
        if (Speed > 0)
            ReturnValues += "speed = " + Speed + "\n";
        if (Accuracy > 0)
            ReturnValues += "accuracy = " + Accuracy + "\n";
        if (Dodge > 0)
            ReturnValues += "dodge = " + Dodge + "\n";
        if (Strength > 0)
            ReturnValues += "strength = " + Strength + "\n";
        if (Agility > 0)
            ReturnValues += "agility = " + Agility + "\n";
        if (Stamina > 0)
            ReturnValues += "stamina = " + Stamina + "\n";
        return ReturnValues;
    }

    /// <summary>
	/// функция увеличивает стат на определенную величину (зависит от dgnlvl)
	/// </summary>
    public void RollStat(e_stat e_stats)
    {
        switch (e_stats)
        {
            case e_stat.e_accuracy:
                Accuracy += 2 + Dgnlvl / 2 + Random.Range(0, (3 + Dgnlvl / 4));
                break;
            case e_stat.e_dodge:
                Dodge += 2 + Dgnlvl / 2 + Random.Range(0, (3 + Dgnlvl / 4));
                break; 
            case e_stat.e_str:
                Strength += 1 + Dgnlvl / 2 + Random.Range(0, (2 + Dgnlvl / 5));
                break;
            case e_stat.e_stam:
                Stamina += 1 + Dgnlvl / 2 + Random.Range(0, (2 + Dgnlvl / 5));
                break;
            case e_stat.e_agi:
                Agility += 1 + Dgnlvl / 2 + Random.Range(0, (2 + Dgnlvl / 5));
                break;
            case e_stat.e_speed:
                Speed += 7 + Random.Range(0, 3);
                break;
            default:
                break;
        }
    }
    public int RollType()
    {
        int rollittype = Random.Range(0, 100) + 1;
        if (rollittype < 40)
            return 0;
        else if (rollittype < 70)
            return 1;
        else if (rollittype < 90)
            return 2;
        else
            return 3;
    }
    //конструкторы
    public ItemType()
    {
	    ItemLvl = Dgnlvl;
	    typeIt = "ordinary";
	    Accuracy = 0;
	    Dodge = 0;
	    Strength = 0;
	    Agility = 0;
	    Stamina = 0;
	    int rolltypeit = RollType();
	    switch (rolltypeit)
	    {
	    case 0:
		    typeIt = "Ordinary";
		    for (int i = 0; i< (int)typeI.ord; i++)
                RollStat(RandStat());
		    break;
	    case 1:
		    typeIt = "Rare";
		    for (int i = 0; i< (int)typeI.rare; i++)
                RollStat(RandStat());
	    	break;
	    case 2:
	    	typeIt = "Unick";
	    	for (int i = 0; i< (int)typeI.unick - 1; i++)
                RollStat(RandStat());
            RollStat(RandChar());
		    break;
	    case 3:
	    	typeIt = "Legendary";
	    	for (int i = 0; i< (int)typeI.leg; i++)
                RollStat(RandStat());
            RollStat(RandChar());
	    	break;
    	default:
    		break;
    	}
    }
    public ItemType(typeI type)
    {  
        ItemLvl = Dgnlvl;
	    typeIt = "ordinary";
	    Accuracy = 0;
	    Dodge = 0;
	    Strength = 0;
	    Agility = 0;
	    Stamina = 0;
	    switch (type)
	    {
	    case typeI.ord:
		    typeIt = "Ordinary";
		    for (int i = 0; i < (int)typeI.ord; i++)
             RollStat(RandStat());
	    	break;
	    case typeI.rare:
	    	typeIt = "Rare";
	    	for (int i = 0; i < (int)typeI.rare; i++)
               RollStat(RandStat());
	    	break;
	    case typeI.unick:
	    	typeIt = "Unick";
	    	for (int i = 0; i < (int)typeI.unick - 1; i++)
             RollStat(RandStat());
           RollStat(RandChar());
	    	break;
	    case typeI.leg:
	    	typeIt = "Legendary";
	    	for (int i = 0; i < (int)typeI.leg; i++)
             RollStat(RandStat());
         RollStat(RandChar());
	    	break;
	    default:
		    break;
	    }
    }
}
