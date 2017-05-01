using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {
    public float speed {get;set;}
    public float range;
    public string nickname;
    protected string keySession = DataManager.keySession;

}
