using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    int randomCountBrick;
    public void OnEnter(Bot bot)
    {
        randomCountBrick = Random.Range(5, 6);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.listBrick.Count < randomCountBrick)
        {
            bot.FindBrick();
        }
        else
        {
            bot.GotoWinPos();
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
