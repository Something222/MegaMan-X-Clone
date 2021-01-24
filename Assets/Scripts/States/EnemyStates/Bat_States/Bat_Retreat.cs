using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Retreat :Bat_States
{

    private Vector2 destination;
    private Transform currLocation;
    private float distanceToSwitchState = 2f;
    private static Bat_Retreat instance = null;

    public static Bat_Retreat GetInstance(Bat_Enemy self, GameObject player)
    {
        if (instance == null)
            instance = new Bat_Retreat(self, player);
        else
            instance.phase = Phase.ENTER;

        return instance;
    }
   public Bat_Retreat(Bat_Enemy self,GameObject player):base(self,player)
    {

    }

    public override void Enter()
    {
        self.moveSpeed = self.retreatMoveSpeed;
        currLocation = self.transform;
        float xDestination = Random.Range(currLocation.position.x - self.retreatRange, currLocation.position.x + self.retreatRange);
        float yDestination = Random.Range(currLocation.position.y - self.retreatRange, currLocation.position.y + self.retreatRange);

        destination = new Vector2(xDestination, yDestination);

        base.Enter();
    }
    private void CheckToRest()
    {
        self.transform.position = Vector2.MoveTowards(currPos, destination, self.moveSpeed * Time.deltaTime);

        if (Vector2.Distance(currPos, destination) < distanceToSwitchState)
        {
            //nextState = Bat_Asleep.GetInstance(self, player);
            nextState = new Bat_Asleep(self, player);
            phase = Phase.EXIT;
        }
    }
    public override void Update()
    {
        base.Update();
        CheckToRest();
    }

  
}
