using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject thisGameObject;
    private Controller controller;
    private Player player;
    private KeyCode keyBind;
    // Start is called before the first frame update
    void Start()
    {
        thisGameObject = GameObject.Find("Player");
        controller = thisGameObject.GetComponent<Controller>();
        player = thisGameObject.GetComponent<Player>();

    }

    public void Tele(KeyCode keyBind)
    {
        if (Input.GetKeyDown(keyBind))
        {
            Vector3 value = controller.getInput() * 10;
            Vector3 value2 = new Vector3 (value[2], value[1], -value[0]);
            Debug.Log(value2);
            transform.localPosition += value2;
        }
    }

}
