//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using UnityEngine.SceneManagement;


//public class CastleFood : MonoBehaviour
//{
//    public float startingFood = 10;
//    public float currentFood;
//    public Slider castleFoodSlider;
//    public Text castleFoodText;


//    //Animator anim;
//    //AudioSource playerAudio;
//    //PlayerMovement playerMovement;
//    //PlayerShooting playerShooting;
//    bool isDead;
//    bool damaged;


//    void Awake()
//    {
//        //anim = GetComponent<Animator>();
//        //playerAudio = GetComponent<AudioSource>();
//        //playerMovement = GetComponent <PlayerMovement> ();
//        //playerShooting = GetComponentInChildren <PlayerShooting> ();
//        currentFood = startingFood;
//    }


//    void Update()
//    {

//    }


//    public void AddFood(float amount)
//    {
//        currentFood += amount;

//        castleFoodSlider.value = currentFood;

//        //{0:#.00} to display deciamls
//        castleFoodText.text = string.Format("{0:#} / 100", currentFood);

//        //playerAudio.Play ();


//    }

//}
