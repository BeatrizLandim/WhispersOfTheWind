using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrulha : MonoBehaviour
{
    [Header("Patrulha")]
    //public Transform leftPoint;
    //public Transform rightPoint;
    public float patrolSpeed = 2f;
    //[SerializeField] bool movingRight = true;
    //[SerializeField] bool patrulhando = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    //private BossState currentState = BossState.Parado;





    //public BossState GetState()
    //{
        //return currentState;
    //}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
