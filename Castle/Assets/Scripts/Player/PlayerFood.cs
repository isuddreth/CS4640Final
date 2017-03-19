using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerFood : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float startingFood = 20;
    public float currentFood;
    public Slider playerFoodSlider;
    public Text playerFoodText;


    //Animator anim;
    //AudioSource playerAudio;
    //PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake()
    {
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentFood = startingFood;
    }


    void Update()
    {

    }


    public void AddFood(float amount)
    {
        currentFood += amount;

        playerFoodSlider.value = currentFood;

        //{0:#.00} to display deciamls
        playerFoodText.text = string.Format("{0:#} / 100", currentFood);

        //playerAudio.Play ();

        
    }
    
}
