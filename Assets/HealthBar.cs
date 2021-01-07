using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    //Health Bar Elements
    [SerializeField] private Image mask;
    [SerializeField] private Image self;
    [SerializeField] private Image topper;


    //How Much to hide with mask per damage tick
   [SerializeField] private float maskPercent;
    public float notchPercent;

    //Reference to Player
    [SerializeField] private PlayerCharacter X;


    //pos 206 height 217
    //pos 221 height 247
    //    15          30
    //Health Bar Growths
    //Topper diff y
    //330
    //358
    //28
    private void Start()
    {
        X = FindObjectOfType<PlayerCharacter>();
    }
    private void Update()
    {
        notchPercent = (100 / X.MaxHealth)/100;
        maskPercent = 1 - (X.health * notchPercent);

        mask.fillAmount = maskPercent;


    }



}
