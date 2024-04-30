using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    [Header("ON/OFF")]
    public bool ON = true;

    [Header("références")]
    public GameObject UIQTE;                                // Ui du qte
    public GameObject texteReussite;                        // ui du texte de reussite
    public TextMeshProUGUI txt;                             // tmp de reussite

    // variables privées
    private float timeBeforeEvent;                          // temps entre deux qte
    private bool isQTE = false;                             // indique si un qte est en cours
    private bool canSuccess = false;                        // indique si dans la zone a viser
    private float rota;                                     // position pour la zone a viser
    private float perRota;
    private float lenObjectif;
    


    [Header("Paramètres cercle")]
    public GameObject cercleQTE;                            // cercle qui se rempli
    public GameObject objectif;                             // zone a viser pour réussir le qte
    [SerializeField] public float tempsRemplissage = 1;     // temps pour remplir le cercle





    void Start()
    {
        UIQTE.SetActive(false);                             // Cache l'ui du qte
        texteReussite.SetActive(false);                     // Cache l'ui de la réussite
        if(ON==true) StartCoroutine(LaunchQTE());           // lance la coroutine de qte
    }

    void Update()
    {
        // calcul de temps pour hit la zone
        perRota = Mathf.Abs(rota) / 360;
        lenObjectif = objectif.GetComponent<Image>().fillAmount;
        //Debug.Log("perRota : "+perRota+"lenObjectif : "+lenObjectif);


        if (isQTE == true && Input.GetKeyDown("space") && canSuccess==true)                        // si on est en qte et que le joueur presse espace
        {
            Debug.Log("oui");
            txt.text = "Réussite";                                              // Change le texte du TMP
            texteReussite.GetComponent<Image>().color = Color.green;            // change la couleur du TMP
            texteReussite.SetActive(true);                                     // active l'ui du TMP
            UIQTE.SetActive(false);                                            // cache l'ui de qte
            StartCoroutine(waitAff());                                          // lance la coroutine de l'aff
            isQTE = false;
        }
        if (isQTE == true && Input.GetKeyDown("space") && canSuccess == false)
        {
            txt.text = "Echec";
            texteReussite.GetComponent<Image>().color = Color.red;
            texteReussite.SetActive(true);
            UIQTE.SetActive(false);
            StartCoroutine(waitAff());
            isQTE = false;
        }

    }


    IEnumerator LaunchQTE()
    {
        // choisi un float random et attends ce float avant de lancer un qte
        timeBeforeEvent = Random.Range(1f, 3f);                      
        yield return new WaitForSeconds(timeBeforeEvent);           // Attends timeBeforeEvent

        // maj de variables
        UIQTE.SetActive(true);                                      // Active l'ui du qte
        isQTE = true;                                               // Change le booléen pour dire on est en qte

        // setup zone a viser
        rota = Random.Range(-270f, -90f);                           // random position pour la zone a viser
        objectif.transform.rotation = Quaternion.Euler(0, 0, rota); // Set la rotation au gameObject
        objectif.GetComponent<Image>().fillAmount = Random.Range(0.05f, 0.15f);



        StartCoroutine(fillCircle());                               // Lance la coroutine de remplissage du cercle


    }
    IEnumerator fillCircle()
    {
        /*yield return new WaitForSeconds(1f/vitesseRemplissage);     // Attends 1 sec divisé par la vitesse de remplissage
        remplissageCercle += 0.1f;                                  // Ajoute un dixième au cercle

        if (remplissageCercle >=1 && isQTE==true)                                  // Si le cercle est rempli
        {
            txt.text = "Echec";
            texteReussite.GetComponent<Image>().color = Color.red;
            texteReussite.SetActive(true);
            UIQTE.SetActive(false);
            StartCoroutine(waitAff());
            isQTE=false;
            yield return null;                                      // Fin de la coroutine
        }
        else if (isQTE==true)
        {
            yield return fillCircle();                              // Si le cercle n'est pas rempli relance la coroutine
        }*/


        float elapsedTime = 0f;
        while (elapsedTime < tempsRemplissage && isQTE == true)
        {
            float per = elapsedTime / tempsRemplissage;
            if (per >= perRota-0.01f && per <= perRota+lenObjectif+0.01f)
            { 
                canSuccess = true;
            }
            else canSuccess = false;
            Debug.Log(canSuccess);
            cercleQTE.GetComponent<Image>().fillAmount = per;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (isQTE == true)
        {
            txt.text = "Echec";
            texteReussite.GetComponent<Image>().color = Color.red;
            texteReussite.SetActive(true);
            UIQTE.SetActive(false);
            StartCoroutine(waitAff());
            isQTE = false;
        }


    }

    IEnumerator waitAff()
    {
        yield return new WaitForSeconds(0.5f);
        texteReussite.SetActive(false);                     // Cache l'ui de la réussite
        StartCoroutine(LaunchQTE());
        yield return null;
    }

}