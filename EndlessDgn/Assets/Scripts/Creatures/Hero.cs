using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Hero : Creatures
{
    /// <summary>
    /// инвентарь группы
    /// </summary>
    public static class Inventory
    {
        private static List<ItemType> Items = new List<ItemType>();

        public static void AddNewItem(ItemType item)
        {
            Items.Add(item);
        }

        public static List<ItemType> GetItemsList()
        {
            return Items; 
        }
    }

    private int SkillPoints, LVLPoint, currentXP, heroLVL;

    /// <summary>
	/// статы героя с учетом шмота
	/// </summary>
    private Dictionary<Stats, int> BasicStats = new Dictionary<Stats, int>();

    /// <summary>
	/// статы шмота
	/// </summary>
    private Dictionary<Stats, int> EquipmentStats = new Dictionary<Stats, int>();

    public void Awake()
    {
        StatBuilder();
        SpellBook = new List<Ability>();
        SpellBook.Add(new NormalAttack(this));
        SpellBook.Add(new HeroicStrike(this));
        SpellBook.Add(new Heal(this));
        WeaponSlot = new Weapon(typeI.ord);
        ArmourSlot = new Armour(typeI.ord);
        NeckSlot = new Necklace(typeI.ord);
        CalcEqupmentStats();
        ResultStats = CalcResStats();
        Alive = true;
    }

    public void SetName(string Name)
    {
        base.Name = Name;
    }

    /// <summary>
    /// рассчет статов шмота
    /// </summary>
    private void CalcEqupmentStats()
    {
        EquipmentStats[Stats.TurnLine] = 0;
        EquipmentStats[Stats.Speed] = WeaponSlot.Speed + ArmourSlot.Speed + NeckSlot.Speed;
        EquipmentStats[Stats.Accuracy] = WeaponSlot.Accuracy + ArmourSlot.Accuracy + NeckSlot.Accuracy;
        EquipmentStats[Stats.Dodge] = WeaponSlot.Dodge + ArmourSlot.Dodge + NeckSlot.Dodge;
        EquipmentStats[Stats.MinDmg] = WeaponSlot.mindmg;
        EquipmentStats[Stats.MaxDmg] = WeaponSlot.maxdmg;
        EquipmentStats[Stats.MaxHp] = ArmourSlot.bonusHp;
        EquipmentStats[Stats.Hp] = ArmourSlot.bonusHp;
        EquipmentStats[Stats.Stamina] = WeaponSlot.Stamina + ArmourSlot.Stamina + NeckSlot.Stamina;
        EquipmentStats[Stats.Strength] = WeaponSlot.Strength + ArmourSlot.Strength + NeckSlot.Strength;
        EquipmentStats[Stats.Agility] = WeaponSlot.Agility + ArmourSlot.Agility + NeckSlot.Agility;
    }

    /// <summary>
    /// добавляет статы шмота к базовым статам героя
    /// </summary>
    private Dictionary<Stats, Stat> CalcResStats()
    {
        Dictionary<Stats, Stat> res = new Dictionary<Stats, Stat>();
        res[Stats.TurnLine] = new Stat(Stats.TurnLine, 0);
        res[Stats.Speed] = new Stat(Stats.Speed, BasicStats[Stats.Speed] + EquipmentStats[Stats.Speed]);
        res[Stats.MaxHp] = new Stat(Stats.MaxHp, BasicStats[Stats.MaxHp] + EquipmentStats[Stats.MaxHp]);
        res[Stats.Hp] = new Hp(res[Stats.MaxHp]);
        res[Stats.Accuracy] = new Stat(Stats.Accuracy, BasicStats[Stats.Accuracy] + EquipmentStats[Stats.Accuracy]);
        res[Stats.Dodge] = new Stat(Stats.Dodge, BasicStats[Stats.Dodge] + EquipmentStats[Stats.Dodge]);
        res[Stats.MinDmg] = new Stat(Stats.MinDmg, BasicStats[Stats.MinDmg] + EquipmentStats[Stats.MinDmg]);
        res[Stats.MaxDmg] = new Stat(Stats.MaxDmg, BasicStats[Stats.MaxDmg] + EquipmentStats[Stats.MaxDmg]);
        res[Stats.Stamina] = new Stamina(BasicStats[Stats.Stamina] + EquipmentStats[Stats.Stamina], res[Stats.MaxHp], res[Stats.Hp]);
        res[Stats.Strength] = new Strength(BasicStats[Stats.Strength] + EquipmentStats[Stats.Strength], res[Stats.MinDmg], res[Stats.MaxDmg]);
        res[Stats.Agility] = new Agility(BasicStats[Stats.Agility] + EquipmentStats[Stats.Agility], res[Stats.Accuracy], res[Stats.Dodge]);
        return res;
    }

    /// <summary>
	/// изменяет базовые статы(без учета шмота). Нужно для окна распределения характеристик при лвл апе
	/// </summary>
    private void ChangeBasicStats(Stats stat, int value)
    {
        BasicStats[stat] += value;
    }

    /// <summary>
    /// getter для BasicStats
    /// </summary>
    public int GetBasicStat(Stats stat)
    {
        return BasicStats[stat];
    }

    /// <summary>
    /// копирует итоговые статы в динамические
    /// </summary>
    public void RefreshStats()
    {
        ResultStats = CalcResStats();
        if (!Alive)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Ressurection");
            Alive = true;
        }
    }

    /// <summary>
	/// оружие героя
	/// </summary>
    public Weapon WeaponSlot { get; private set; }

    /// <summary>
	/// нагрудник героя
	/// </summary>
    public Armour ArmourSlot { get; private set; }

    /// <summary>
	/// ожерелье героя
	/// </summary>
    public Necklace NeckSlot { get; private set; }

    /// <summary>
    /// метод одевания шмотки
    /// </summary>
    /// <param name="Item"></param>
    public void Equip(ItemType Item)
    {
        if (Item is Weapon)
        {
            WeaponSlot = (Weapon)Item;
            CalcEqupmentStats();
        }
        if (Item is Armour)
        {
            ArmourSlot = (Armour)Item;
            CalcEqupmentStats();
        }
        if (Item is Necklace)
        {
            NeckSlot = (Necklace)Item;
            CalcEqupmentStats();
        }
    }

    /// <summary>
    /// создает и инициализирует статы
    /// </summary>
    public override void StatBuilder()
    {
        //создаем статы с начальными значениями для героев
        BasicStats[Stats.TurnLine] = 0;
        BasicStats[Stats.Speed] = 100;
        BasicStats[Stats.MaxHp] = 0;
        BasicStats[Stats.Hp] = 0;
        BasicStats[Stats.Accuracy] = 25;
        BasicStats[Stats.Dodge] = 25;
        BasicStats[Stats.MinDmg] = 0;
        BasicStats[Stats.MaxDmg] = 0;
        BasicStats[Stats.Stamina] = 5;
        BasicStats[Stats.Strength] = 5;
        BasicStats[Stats.Agility] = 5;
        heroLVL = 1;
        currentXP = 0;
        SkillPoints = 0;
    }

    public override void Turn(RoomType room)
    {
        if (Alive)
            Messenger<Hero, RoomType>.Broadcast(GameEvent.HERO_TURN, this, room);//отсюда даем месседж интерфейсу
        else
            Messenger.Broadcast(GameEvent.END_OF_TURN);
    }

    public override string ShowStats()
    {
        string Ret;
        Ret = Name + "\n";
        Ret += "Damage = " + GetResultStat(Stats.MinDmg) + " - " + GetResultStat(Stats.MaxDmg) + "\n";
        Ret += "Speed = " + GetResultStat(Stats.Speed) + "\n";
        Ret += "Accuracy = " + GetResultStat(Stats.Accuracy) + "\n";
        Ret += "Dodge = " + GetResultStat(Stats.Dodge) + "\n";
        Ret += "Stamina = " + GetResultStat(Stats.Stamina) + "\n";
        Ret += "Strength = " + GetResultStat(Stats.Strength) + "\n";
        Ret += "Agility = " + GetResultStat(Stats.Agility);
        return Ret;
    }
}