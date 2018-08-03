namespace Enums
{

    public enum MonstersType
    {
        skeleton,
        spider
    }

    public enum Abilities
    {
        NormalAttack,
        HeroicStrike,
        Heal
    };

    /// <summary>
    /// перечисление возможных статов персонажей
    /// </summary>
    public enum Stats
    {
        Hp,
        MaxHp,
        /// <summary>
        /// текущая величина полоски хода
        /// </summary>
        TurnLine,
        MinDmg,
        MaxDmg,
        Accuracy,
        Dodge,
        Speed,
        Strength,
        Stamina,
        Agility
    };

    /// <summary>
    /// перечисление типов шмоток (ord, rare, unick, leg)
    /// </summary>
    public enum typeI
    {
        ord = 2,
        rare,
        unick,
        leg = 5
    };

    /// <summary>
	/// перечисление статов
	/// </summary>
    public enum e_stat
    {
        e_accuracy,
        e_dodge,
        e_speed,
        e_str,
        e_stam,
        e_agi
    };

    /// <summary>
    /// Возможные типы оружия
    /// </summary>
    public enum wTypes
    {
        axe,
        sword,
        mace
    };

    public enum ItClasses
    {
        Weapon,
        Armour,
        Neck
    };

    /// <summary>
    /// результат боя
    /// </summary>
    public enum Result {
        Cont,
        Win,
        Lose
    };
}