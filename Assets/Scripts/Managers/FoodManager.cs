using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class FoodManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float startingPlayerFood = 20;
    public float currentPlayerFood;
    public float startingCastleFood = 10;
    public float currentCastleFood;
    public Slider playerFoodSlider;
    public Slider castleFoodSlider;
    public Text playerFoodText;
    public Text castleFoodText;
    public float playerCapacity = 100;
    public float castleCapacity = 50;
    public float addCastleCount = 0;

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
        currentPlayerFood = startingPlayerFood;
        currentCastleFood = startingCastleFood;

        UpdateFood();

    }


    void Update()
    {

        if (currentPlayerFood == 0)
        {
            playerHealth.TakeDamage(.001f);
        }

    }

    public void UpdateFood()
    {
        castleFoodText.text = string.Format("{0:#0} / {1}", currentCastleFood, castleCapacity);
        playerFoodText.text = string.Format("{0:#0} / {1}", currentPlayerFood, playerCapacity);
        playerFoodSlider.maxValue = playerCapacity;
        castleFoodSlider.maxValue = castleCapacity;
    }


    public void AddPlayerFood(float amount)
    {
        if (currentPlayerFood + amount > playerCapacity)
            return;

        currentPlayerFood += amount;

        playerFoodSlider.value = currentPlayerFood;

        //{0:#.00} to display deciamls
        playerFoodText.text = string.Format("{0:#0} / {1}", currentPlayerFood, playerCapacity);

        //playerAudio.Play ();

    }

    public void AddCastleFood(float amount)
    {
        if ((currentPlayerFood - amount) < 0 || currentCastleFood + amount > castleCapacity)
            return;

        AddPlayerFood(-amount);
        currentCastleFood += amount;
        addCastleCount += amount;
        castleFoodSlider.value = currentCastleFood;

        //{0:#.00} to display deciamls
        castleFoodText.text = string.Format("{0:#0} / {1}", currentCastleFood, castleCapacity);

        //playerAudio.Play ();
        
    }

    public void RemoveCastleFood(float amount)
    {
        if (currentCastleFood - amount < 0)
            return;

        playerHealth.AddHealth(5f);
        currentCastleFood -= amount;

        castleFoodSlider.value = currentCastleFood;

        //{0:#.00} to display deciamls
        castleFoodText.text = string.Format("{0:#0} / {1}", currentCastleFood, castleCapacity);

        //playerAudio.Play ();
    }


}
