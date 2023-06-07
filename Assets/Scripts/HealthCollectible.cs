using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : Collectible
{
    Player player;
    // Start is called before the first frame update
    protected override void Start() {
        player = FindObjectOfType<Player>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        if (player.hitpoints == player.maxhitpoints) return;
        base.Update();
    }
}
