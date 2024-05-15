using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneABruitScript : MonoBehaviour
{
    public AudioSource _audio;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            _audio.Play();
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Ennemi");                             // Quand un QTE n'est pas reussi cherche tout les go avec le tag ennemi
            foreach (GameObject go in ennemis)                                                              // Pour chaque Ennemi dans la liste fait la boucle
            {
                go.GetComponent<EnnemiCTRL>().ActiveEnnemi(GameObject.Find("NewKartClassic_Player"));       // Trouve le script de l'ennemi et lance la methode ActiveEnnemi
            }
        }
    }
}
