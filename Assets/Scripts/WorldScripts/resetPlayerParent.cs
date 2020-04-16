﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayerParent : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}