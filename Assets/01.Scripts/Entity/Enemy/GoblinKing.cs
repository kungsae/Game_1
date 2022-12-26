using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKing : Enemy
{
    
    public override void Attack()
    {
        hand.transform.rotation = Quaternion.FromToRotation(Vector2.right * transform.localScale.x, dir);
        animator.Play(skillData.data[Random.Range(0, skillData.data.Length)].attackName);
    }
}
