using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] NavMeshAgent agent;
    public Transform finish;
    private IState currentState;
    GameObject targetBrick;
    Vector3 targetPos;



    public override void OnInit()
    {
        base.OnInit();
    }

    private void Start()
    {
        base.OnInit(); 
        ChangeSate(new IdleState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void FindBrick()
    {

        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length > 0)
        {
            targetBrick = null;
            foreach (GameObject brick in bricks)
            {
                Renderer brickRenderer = brick.GetComponent<Renderer>();
                if (brickRenderer != null && brickRenderer.material.color == colorCharacter.material.color)
                {
                    targetBrick = brick;
                    break;
                }
            }
            if (targetBrick != null)
            {
                GotoBrick();
            }
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối tượng với tag 'Brick'.");
        }
    }
    public void GotoBrick()
    {
        targetPos = targetBrick.transform.position;
        agent.SetDestination(targetPos);
        ChangeAnim("Run");
        
    }
    public void GotoWinPos()
    {
        targetPos = finish.transform.position;
        agent.SetDestination(targetPos);
        
    }
    /*public void Moving()
    {
        agent.destination = findBrick.position;
    }*/

    public void StopMoving()
    {
        agent.destination = transform.position;
    }
    public void ChangeSate(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
