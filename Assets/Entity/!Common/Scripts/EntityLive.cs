using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLive : MonoBehaviour
{
    [SerializeField] int lives = 3;

    HurtCollider hurtCollider;

    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();  
    }

    private void OnDisable()
    {
        hurtCollider.onHit.RemoveListener(OnHit);
    }

    private void OnEnable()
    {
        hurtCollider.onHit.AddListener(OnHit);
    }

    private void OnHit(HurtCollider hurtCollider, HitCollider hitCollider)
    {
        LoseLife();
    }


    public void LoseLife()
    {
        lives--;
    }

    public int GetLives() { return lives; }
}
