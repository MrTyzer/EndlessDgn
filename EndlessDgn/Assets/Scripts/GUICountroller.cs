using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUICountroller : MonoBehaviour
{
    public GameObject SelectFrame;
    public GameObject AbilityBar;
    public GameObject SelectionCirclePrefab;
    public AbilityButton[] AbilityButtons;


    /// <summary>
    /// блок интерфейса при выборе таргета для атаки
    /// </summary>
    public static bool SelectionLock = false;

    /// <summary>
    /// блок интерфейса при исполнении анимации
    /// </summary>
    public static bool AnimationLock = false;

    public static RoomType CurrentRoom { get; private set; }
    public static Hero CurrentHero { get; private set; }
    public static Ability SelectedAbility { get; private set; }
    public static Creatures SelectedTarget { get; private set; }

    void Awake()
    {
        Messenger<Hero, RoomType>.AddListener(GameEvent.HERO_TURN, OnHeroTurn);
        AbilityBar.SetActive(false);
    }

    /// <summary>
    ///     Модель корлупа игры:
    ///     Game через корутину запускает fight,
    ///     затем fight 
    ///     дает Broadcast интерфейсу. Интерфейс загружает ability bar и если ходит игрок запускает
    ///     корутину выбора таргета, а если ходит моб, то выполняет выбор таргета через ИИ и запускает анимацию атаки
    ///     и метод Turn, который изменит статы таргета в момент удара.
    ///     Broadcast лучше давать из метода Turn, чтобы выгодно использовать полиморфизм
    ///     Turn лучше сделать корутиной
    /// </summary>
    private void OnHeroTurn(Hero hero, RoomType room)
    {
        AnimationLock = false;
        AbilityBar.SetActive(true);
        int i = 0;
        CurrentHero = hero;
        CurrentRoom = room;
        foreach (Ability h in hero.SpellBook)
        {
            AbilityButtons[i].gameObject.SetActive(true);
            AbilityButtons[i].AbilityShow(h);
            i++;
        }
    }

    /// <summary>
    /// установка способности(берется из класса AbilityButton)
    /// </summary>
    /// <param name="ability"></param>
    public void SetAbility(Ability ability)
    {
        TurnOffAllSelectCircles();
        SelectedAbility = ability;
        TurnOnSelectCircles(true);
    }

    /// <summary>
    /// включает и выключает круги выбора таргета под существами, соответствующие условиям используемой способности
    /// </summary>
    public void TurnOnSelectCircles(bool fl)
    {
        List<Creatures> availableTargets = SelectedAbility.GetAvailableTargets(CurrentRoom, CurrentHero);
        foreach (Creatures c in availableTargets)
        {
            c.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = fl;
        }
    }

    /// <summary>
    /// выключает все круги выбора таргета под существами
    /// </summary>
    public void TurnOffAllSelectCircles()
    {
        foreach (Creatures c in CurrentRoom.HeroesAndMobs)
        {
            c.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// отменяет активацию способности и сопутствующий ему блок интерфейса SelectionLock
    /// </summary>
    public void DisactivateAbility()
    {
        TurnOnSelectCircles(false);
        SelectedAbility = null;
        SelectedTarget = null;
        SelectFrame.SetActive(false);
        SelectionLock = false;
    }

    /// <summary>
    /// метод установки таргета(ввести ограничения, которые будут храниться в способностях
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Creatures target)
    {
        if (target.Alive)
            SelectedTarget = target;

        if (SelectedAbility != null)
            UseAbility();
    }

    /// <summary>
    /// метод использования способности в части GUI
    /// </summary>
    private void UseAbility()
    {
        AnimationLock = true;
        AbilityBar.SetActive(false);
        SelectFrame.SetActive(false);
        SelectedAbility.UseAbility(SelectedTarget, CurrentHero, CurrentRoom);
        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_HIT, SelectedTarget.gameObject);
        CurrentHero.gameObject.GetComponent<Animator>().SetTrigger("StartAttack");
        DisactivateAbility();
    }
}
