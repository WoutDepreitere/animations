using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    public float CurrentHealth { get; private set; }
    private Animator anim;
    private bool death = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, StartingHealth);
        if (CurrentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!death)
            {
                anim.SetTrigger("death");
                //GetComponent<PlayerMovement>().enabled = false;
                death = true;
            }
        }
    }
}