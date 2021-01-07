using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Shot : BasicPlayerBulletBehaviour
{
   
    [SerializeField] private ParticleSystem beginShot;
    [SerializeField] private ParticleSystem endShot;
    [SerializeField] private ParticleSystemRenderer beginShotRenderer;
    [SerializeField] private GameObject X;



    private void Start()
    {
        X = FindObjectOfType<PlayerCharacter>().gameObject;
            beginShot.transform.parent = X.transform;
        if (endShot.transform.parent != null)
            endShot.transform.parent = null;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (gameObject.transform.localScale.x > 0)
            beginShotRenderer.flip = new Vector3(0, 0, 0);
        else
            beginShotRenderer.flip = new Vector3(1, 0, 0);

        beginShot.transform.position = gameObject.transform.position;
        beginShot.Play();

    }
    private void OnDisable()
    {
     
        endShot.transform.position = gameObject.transform.position;
        endShot.Play();
        Level1ShotPool.Instance.ReturnToPool(this);
    }
}