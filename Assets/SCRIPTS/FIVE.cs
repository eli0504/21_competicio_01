using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FIVE : MonoBehaviour
{
    //ranom Pos
    public float miny = 6;
    public float minx = 9;
    public float minz = 10;

    //gameOver
    public bool isGameOver;

    //materials
    private Material _material;

    //punts
    public int points;

    //bloquear es clik
    public bool hasBeenClicked;

    //audio
    public AudioSource _audiosource;
    public AudioClip soundClick;
    public AudioClip soundLoose;

    //lives
    public int lives = 3;

    //text
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pointsText;

    //panel
    public GameObject restartGamePanel;


    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _audiosource = GetComponent<AudioSource>();

    }
    void Start()
    {
        points = 0; //reiniciar puntuació
        hasBeenClicked = false; //reset clik
        lives = 3;
        SetText();
        //posar aquí que començi sa musica

        StartCoroutine(GenerateNextRandomPos());

        restartGamePanel.SetActive(true); //panel de pantalla restart

    }
    
     private Vector3 GenerateRandomPos()
    {
        //random position (no posam sa z pq no se veu sa profunditat)
      
        Vector3 pos = new Vector3(Random.Range(-minx, minx), Random.Range(-miny, miny), Random.Range(0, minz));

        return pos;
    }

    private IEnumerator GenerateNextRandomPos()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));

            _material.color = Color.blue; //reseteamos el color a nes principi

            if(hasBeenClicked == false)
            {
                _audiosource.PlayOneShot(soundLoose,1); //si no cliques fa renou de perdre
                
                if (--lives == 0) //si tras restar una vida, no men queden, gameover
                {
                    SetText();
                    
                    
                    //aturar es temps
                    isGameOver = true;

                    restartGamePanel.SetActive(true); //quan mors surt es botó amb es panel

                    //posam es brake perquè no se segueixi executant ses línees des materix ambit de visibilitat
                    break;
                }
            }

            transform.position = GenerateRandomPos();

            hasBeenClicked = false; //reset que hagui pitjat o no 

            restartGamePanel.SetActive(false); //quan no morim no surt es panel

        }
      
    }


    private void OnMouseDown()
    {
        if(!hasBeenClicked) //si no s'ha pitjat, anteriorment, s'executa 
        {
            _material.color = Color.green;
            points++;
            //update points

            hasBeenClicked = true; //ja no se pot pitjar +
            _audiosource.PlayOneShot(soundClick,1); //sona "sound" un pic a volum 1
        } 
        //fer un renou(AudioClip.play) {declarar audioclip i audiosource a s'awake}
    }


    public void SetText()
    {
        livesText.text = $"LIVES: {lives}";
        pointsText.text = $"POINTS: {points}"; //update text points
    }

    
}
