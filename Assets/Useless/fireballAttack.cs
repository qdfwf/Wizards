using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, firePosition.position, firePosition.rotation);
        }
    }
}
