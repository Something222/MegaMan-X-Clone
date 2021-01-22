using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Retreat :Bat_States
{

   private Vector2 destination;
    private Transform currLocotion;
    private float distanceToSwitchState = 2f;
   
   public Bat_Retreat(Bat_Enemy self,GameObject player):base(self,player)
    {

    }

    public override void Enter()
    {
        self.moveSpeed = self.retreatMoveSpeed;
        currLocotion = self.transform;
        float xDestination = Random.Range(currLocotion.position.x - self.retreatRange, currLocotion.position.x + self.retreatRange);
        float yDestination = Random.Range(currLocotion.position.y - self.retreatRange, currLocotion.position.y + self.retreatRange);

        destination = new Vector2(xDestination, yDestination);

        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        self.transform.position = Vector2.MoveTowards(currPos, destination, self.moveSpeed * Time.deltaTime);

        if(Vector2.Distance(currPos,destination)<distanceToSwitchState)
        {
            nextState = new Bat_Asleep(self,player);
            phase = Phase.EXIT;
        }
    }

}
