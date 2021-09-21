using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int mana;
    public int potion;
    public bool wing;
    public float[] position;

    public PlayerData(PaladinLifeManager player)
    {
        potion = PaladinLifeManager.potion;
        level = PaladinLifeManager.level;
        health = PaladinLifeManager.currHP;
        mana = PaladinLifeManager.currMana;
        wing = PaladinScript.gotWing;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
