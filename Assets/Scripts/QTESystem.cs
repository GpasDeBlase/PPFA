using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QTESystem : MonoBehaviour
{
    [Header("ON/OFF")]
    public bool ON = true;



    [Header("Parametres QTE")]
    [Min(1f), Tooltip("Temps minimal avant le prochain QTE")]
    public int tempsMin;
    [Range(1f, 10f), Tooltip("Temps maximal avant le prochain QTE")]
    public float tempsMax;
    [Min(0.5f), Tooltip("Temps minimal pour le le cercle se remplisse")]
    public float tempsRemplissageMin;
    [Range(1f,5f), Tooltip("Temps maximal pour le le cercle se remplisse")]
    public float tempsRemplissageMax;      
    [Range(0.5f,2f), Tooltip("Temps durant lequel le moteur est coupe apres un echec")]
    public float tempsStop;

    [Header("references")]
    public GameObject UIQTE;                                // Ui du qte
    public GameObject texteReussite;                        // ui du texte de reussite
    public TextMeshProUGUI txt;                             // tmp de reussite
    public GameObject cercleQTE;                            // cercle qui se rempli
    public GameObject objectif;                             // zone a viser pour reussir le qte
    public AudioSource boom;

    // variables privees
    private float timeBeforeEvent;                          // temps entre deux qte
    private bool isQTE = false;                             // indique si un qte est en cours
    private bool canSuccess = false;                        // indique si dans la zone a viser
    private float rota;                                     // position pour la zone a viser
    private float perRota;
    private float lenObjectif;
    public GameObject player;


    void Start()
    {
        UIQTE.SetActive(false);                             // Cache l'ui du qte
        texteReussite.SetActive(false);                     // Cache l'ui de la reussite
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
            //Debug.Log("oui");
            txt.text = "Reussite";                                              // Change le texte du TMP
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
            Echec();
        }

    }


    IEnumerator LaunchQTE()
    {
        // choisi un float random et attends ce float avant de lancer un qte
        timeBeforeEvent = Random.Range(tempsMin, tempsMax);                      
        yield return new WaitForSeconds(timeBeforeEvent);           // Attends timeBeforeEvent

        // maj de variables
        UIQTE.SetActive(true);                                      // Active l'ui du qte
        isQTE = true;                                               // Change le booleen pour dire on est en qte

        // setup zone a viser
        rota = Random.Range(-270f, -90f);                           // random position pour la zone a viser
        objectif.transform.rotation = Quaternion.Euler(0, 0, rota); // Set la rotation au gameObject
        objectif.GetComponent<Image>().fillAmount = Random.Range(0.06f, 0.12f);



        StartCoroutine(fillCircle());                               // Lance la coroutine de remplissage du cercle


    }

    IEnumerator fillCircle()
    {
        float elapsedTime = 0f;
        float tempsRemplissage = Random.Range(tempsRemplissageMin, tempsRemplissageMax);
        while (elapsedTime < tempsRemplissage && isQTE == true)
        {
            float per = elapsedTime / tempsRemplissage;
            if (per >= perRota-0.01f && per <= perRota+lenObjectif+0.01f)
            { 
                canSuccess = true;
            }
            else canSuccess = false;
            //Debug.Log(canSuccess);
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
            Echec();
        }


    }

    IEnumerator waitAff()
    {
        yield return new WaitForSeconds(0.5f);
        texteReussite.SetActive(false);                     // Cache l'ui de la reussite
        StartCoroutine(LaunchQTE());
        yield return null;
    }

    void Echec ()
    {
        
        boom.Play();
        GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Ennemi");                             // Quand un QTE n'est pas reussi cherche tout les go avec le tag ennemi
        foreach (GameObject go in ennemis)                                                              // Pour chaque Ennemi dans la liste fait la boucle
        {
            go.GetComponent<EnnemiCTRL>().ActiveEnnemi(GameObject.Find("NewKartClassic_Player"));       // Trouve le script de l'ennemi et lance la methode ActiveEnnemi
        }

        Debug.Log(player);
        player.GetComponent<ArcadeKart>().StopMove(tempsStop);

        //GameObject.FindGameObjectsWithTag("Player").GetComponent<ArcadeKart>().StopMove(tempsMax);

    }

}