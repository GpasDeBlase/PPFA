using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private GameObject player;
    private bool active = false;
    [SerializeField] private float speed;
    private float dif;


    void Start()
    {
        player = GameObject.Find("NewKartClassic_Player");

    }


    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            active = true;
        }

        if (active == true)
        {
            dif = (player.transform.position - transform.position).magnitude;
            float step = speed * Time.deltaTime * dif;
            //Debug.Log("Distance :" +dif);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 0.5f, 0f), step);
        }


    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("destroy");
            //Destroy(gameObject);
        }
    } 
}
