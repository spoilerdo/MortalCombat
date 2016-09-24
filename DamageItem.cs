﻿using UnityEngine;
using System.Collections;

public class DamageItem : MonoBehaviour {

    public int AmountOfDamage = 1;
    private Player player;

    private float WaitBeforeDestroy = 0.14f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            player = col.gameObject.GetComponent<Player>();
            player.CurrentHealth = player.CurrentHealth - AmountOfDamage;
            StartCoroutine(DestroyObject());
        }
    }
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(WaitBeforeDestroy);
        Destroy(transform.parent.gameObject);
    }
}
