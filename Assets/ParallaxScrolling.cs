using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] float scrollSpeed=.01f;
    PlayerState playerReference;
    [SerializeField]private bool triggered=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
         triggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            triggered = false;
    }
    // Update is called once per frame
    void Update()
    {
        playerReference = FindObjectOfType<PlayerCharacter>().currState;
        ParralaxScrolling();
    }

    private void ParralaxScrolling()
    {
        if (triggered == true)
        {
            if (playerReference.InputValueX > 0 && playerReference.MoveCheck())
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + scrollSpeed, gameObject.transform.position.y);
            else if (playerReference.InputValueX < 0 && playerReference.MoveCheck())
                gameObject.transform.position = new Vector2(gameObject.transform.position.x - scrollSpeed, gameObject.transform.position.y);
        }
      
        
    }
}
