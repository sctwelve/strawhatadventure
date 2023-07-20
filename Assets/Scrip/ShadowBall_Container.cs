using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBall_Container : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {  
        // Check if the 'enemy' reference is not null before accessing its 'localScale'
        if (enemy != null)
        {
            transform.localScale = enemy.localScale;
        }
    }
}
