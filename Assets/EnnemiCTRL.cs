using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiCTRL : MonoBehaviour
{

    private GameObject player;
    private bool active = false;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float activeDistance;
    private float dif;


    void Start()
    {
        player = GameObject.Find("NewKartClassic_Player");

    }


    void Update()
    {
        /*if (Input.GetKeyDown("t"))
        {
            active = true;
        }

        if (active == true)
        {
            dif = (player.transform.position - transform.position).magnitude;
            float step = Mathf.Clamp(speed * Time.deltaTime * dif, minSpeed, 1f);
            //Debug.Log(step);
            //Debug.Log("Distance :" +dif);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 0.5f, 0f), step);
        }*/


    }

    public void ActiveEnnemi (GameObject go)
    {
        if ((go.transform.position - transform.position).magnitude <= activeDistance) 
        {
            active = true;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("t"))
        {
            active = true;
        }

        if (active == true)
        {
            dif = (player.transform.position - transform.position).magnitude;
            float step = Mathf.Clamp(Time.fixedDeltaTime * dif, minSpeed, maxSpeed);
            //Debug.Log(step);
            //Debug.Log("Distance :" +dif);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0f, 0.5f, 0f), step*2);
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
