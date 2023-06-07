using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : Collectible
{
    // Start is called before the first frame update
    protected override void Start() {
        value = Random.Range(1, 4);
        base.Start();
    }
}
