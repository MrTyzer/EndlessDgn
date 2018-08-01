using UnityEngine;
using System.Collections.Generic;
using StateMachine;

public class GUIController : MonoBehaviour
{
    public GameObject SelectFrame;
    public GameObject AbilityBar;
    public GameObject SelectionCirclePrefab;
    public Camera GUICamera;
    public GameObject StatsWindow;
    public AbilityButton[] AbilityButtons;
    
    /// <summary>
    /// машина состояний для интерфейса в бою
    /// </summary>
    private IStateChart _guiStateChart;

    public static RoomType CurrentRoom { get; private set; }
    public static Hero CurrentHero { get; private set; }
    public static Ability SelectedAbility { get; private set; }
    public static Creatures SelectedTarget { get; private set; }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ABILITY_BUTTON_CLICK, OnAbilityButtonClick);
        Messenger<Hero, RoomType>.AddListener(GameEvent.HERO_TURN, OnHeroTurn);
        _guiStateChart = StateChartFactory.GetInterfaceSC(this, GUICamera);
        //AbilityBar.SetActive(false);
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
        CurrentHero = hero;
        CurrentRoom = room;
        _guiStateChart.SwitchState(States.Idle, false);
    }

    private void OnAbilityButtonClick()
    {
        _guiStateChart.SwitchState(States.SelectTarget, false);
    }

    /// <summary>
    /// установка способности(берется из класса AbilityButton)
    /// </summary>
    public void SetAbility(Ability ability)
    {
        TurnOffAllSelectCircles();
        SelectedAbility = ability;
        TurnOnSelectCircles();
    }

    /// <summary>
    /// включает круги выбора таргета под существами, соответствующие условиям используемой способности
    /// </summary>
    public void TurnOnSelectCircles()
    {
        List<Creatures> availableTargets = SelectedAbility.GetAvailableTargets(CurrentRoom, CurrentHero);
        foreach (Creatures c in availableTargets)
        {
            c.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
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
        TurnOffAllSelectCircles();
        SelectedAbility = null;
        SelectedTarget = null;
        SelectFrame.SetActive(false);
    }

    /// <summary>
    /// метод установки таргета(ввести ограничения, которые будут храниться в способностях)
    /// </summary>
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
        _guiStateChart.BackToParent(true, false);
        _guiStateChart.BackToParent();
        SelectFrame.SetActive(false);
        SelectedAbility.UseAbility(SelectedTarget, CurrentHero, CurrentRoom);
        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_HIT, SelectedTarget.gameObject);
        CurrentHero.gameObject.GetComponent<Animator>().SetTrigger("StartAttack");
        DisactivateAbility();
    }
}
