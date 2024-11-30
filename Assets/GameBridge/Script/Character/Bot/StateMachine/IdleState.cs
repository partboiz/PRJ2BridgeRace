using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;
    public void OnEnter(Bot bot)
    {
        bot.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 3f);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > randomTime) bot.ChangeSate(new PatrolState());
    }

    public void OnExit(Bot bot)
    {
        
    }
}
