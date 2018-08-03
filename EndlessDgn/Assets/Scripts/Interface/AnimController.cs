using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Enums;

/// <summary>
/// отвечает за анимированный интерфейс
/// </summary>
public class AnimController : MonoBehaviour
{
    public GameObject[] HpBars;
    public GameObject[] DamageWindowPool;
    public SimpleHealthBar[] EnergyBars;

    /// <summary>
    /// очередь для хранения окон отображения урона
    /// </summary>
    public static Queue<GameObject> QueueDmgPool { get; private set; }

    /// <summary>
    /// список скриптов для управления хп барами
    /// </summary>
    private List<SimpleHealthBar> _hpBarScript = new List<SimpleHealthBar>();

    /// <summary>
    /// словарь с существами в комнате и соответствующие им хп бары
    /// </summary>
    private Dictionary<Creature, SimpleHealthBar> _creaturesHp;

    /// <summary>
    /// словарь с существами в комнате и соответствующие им energy bars
    /// </summary>
    private Dictionary<Creature, SimpleHealthBar> _creaturesEnergy = new Dictionary<Creature, SimpleHealthBar>();

    /// <summary>
    /// информация о результатах приминения способности
    /// </summary>
    private Dictionary<Creature, Ability.ResultOfAbility> _abilityInfo = new Dictionary<Creature, Ability.ResultOfAbility>();

    private void Awake()
    {
        Messenger<Dictionary<Creature, Ability.ResultOfAbility>>.AddListener(GameEvent.ABILITY_INFO, OnAbilityInfo);
        Messenger<RoomType>.AddListener(GameEvent.ROOM_INTERFACE_INIT, OnRoomShow);
        Messenger.AddListener(GameEvent.ATTACK_MOMENT, OnAttackMoment);
        Messenger.AddListener(GameEvent.UPDATE_ENERGY_BARS, OnUpgradeEnergyBars);
        QueueDmgPool = new Queue<GameObject>();
        foreach (GameObject g in HpBars)
        {
            _hpBarScript.Add(g.GetComponentInChildren<SimpleHealthBar>());
        }
        
        foreach (GameObject g in DamageWindowPool)
        {
            QueueDmgPool.Enqueue(g);
            g.SetActive(false);
        }
    }

    private void OnUpgradeEnergyBars()
    {
        foreach (KeyValuePair<Creature, SimpleHealthBar> c in _creaturesEnergy)
        {
            c.Value.UpdateBar(c.Key.GetComponent<Creature>().GetResultStat(Stats.TurnLine),
                         Game.TurnLineMaxValue);
        }
    }

    /// <summary>
    /// момент отображения результатов использования способности персонажами
    /// </summary>
    private void OnAttackMoment()
    {
        foreach (KeyValuePair<Creature, Ability.ResultOfAbility> c in _abilityInfo)
        {
            if (c.Value.Value != 0)
            {
                GameObject movWindow = QueueDmgPool.Dequeue();
                movWindow.SetActive(true);
                movWindow.transform.position = Camera.main.WorldToScreenPoint(c.Key.GetComponentInChildren<HpBarPosition>().transform.position);
                if (c.Value.Value > 0)
                    movWindow.GetComponentInChildren<Text>().color = new Color(0, 100, 0);//делаем цвет текста зеленым, если персонаж получает хилл
                else
                    movWindow.GetComponentInChildren<Text>().color = new Color(100, 0, 0);
                int mod = Mathf.Abs(c.Value.Value);
                movWindow.GetComponentInChildren<Text>().text = mod.ToString();
                movWindow.GetComponentInChildren<Animator>().SetTrigger("StartAnim");
            }

            //отобразить промахи
            if (c.Value.Res == Creature.AttackResult.Miss)
            {
                GameObject movWindow = QueueDmgPool.Dequeue();
                movWindow.GetComponentInChildren<Text>().color = new Color(100, 0, 0);
                movWindow.SetActive(true);
                movWindow.transform.position = Camera.main.WorldToScreenPoint(c.Key.GetComponentInChildren<HpBarPosition>().transform.position);
                movWindow.GetComponentInChildren<Text>().text = "Miss";
                movWindow.GetComponentInChildren<Animator>().SetTrigger("StartAnim");
            }

        }

        foreach (KeyValuePair<Creature, SimpleHealthBar> c in _creaturesHp)
        {
            c.Value.UpdateBar(c.Key.GetComponent<Creature>().GetResultStat(Stats.Hp),
                        c.Key.GetComponent<Creature>().GetResultStat(Stats.MaxHp));
        }
        
    }

    private void OnRoomShow(RoomType room)
    {
        _creaturesHp = new Dictionary<Creature, SimpleHealthBar>();
        _creaturesEnergy = new Dictionary<Creature, SimpleHealthBar>();
        for (int i = 0; i < room.HeroesAndMobs.Count; i++)
        {
            _creaturesHp[room.HeroesAndMobs[i]] = _hpBarScript[i];
            _hpBarScript[i].UpdateBar(room.HeroesAndMobs[i].GetResultStat(Stats.Hp),
                room.HeroesAndMobs[i].GetResultStat(Stats.MaxHp));
            HpBars[i].transform.position = Camera.main.WorldToScreenPoint(room.HeroesAndMobs[i].gameObject.GetComponentInChildren<HpBarPosition>().transform.position);

            _creaturesEnergy[room.HeroesAndMobs[i]] = EnergyBars[i];
            EnergyBars[i].UpdateBar(room.HeroesAndMobs[i].GetResultStat(Stats.TurnLine),
                Game.TurnLineMaxValue);
        }
        foreach (KeyValuePair<Creature, SimpleHealthBar> c in _creaturesHp)
        {
            c.Value.UpdateBar(c.Key.GetComponent<Creature>().GetResultStat(Stats.Hp),
                        c.Key.GetComponent<Creature>().GetResultStat(Stats.MaxHp));
        }
    }

    private void OnAbilityInfo(Dictionary<Creature, Ability.ResultOfAbility> abilityInfo)
    {
        _abilityInfo = abilityInfo;
    }
}
