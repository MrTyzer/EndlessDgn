using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventHandler : MonoBehaviour
{
    private Animator _enemy;
    private void Awake()
    {
        Messenger<GameObject>.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    public void OnEnemyHit(GameObject enemy)
    {
        _enemy = enemy.GetComponent<Animator>();
    }

    public void EndOfTurn()
    {
        Messenger.Broadcast(GameEvent.END_OF_TURN);
    }

    public void AttackEvent()
    {
        if (_enemy.gameObject.GetComponent<Creatures>().Alive)
            _enemy.SetTrigger("TakeDamage");
        else
            _enemy.SetTrigger("Death");
        Messenger.Broadcast(GameEvent.ATTACK_MOMENT);
    }

    public void OnDamageWindowAnimEnds()
    {
        //криво получилось. Можно попробовать кэшировать родителя
        AnimController.QueueDmgPool.Enqueue(gameObject.transform.parent.gameObject);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
