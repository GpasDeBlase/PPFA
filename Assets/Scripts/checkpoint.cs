using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("systemInGame").GetComponent<InGameSystem>().addCheckpoint();
            Destroy(gameObject);
        }
    }
}
