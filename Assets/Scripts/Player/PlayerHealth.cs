using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public Slider healthSlider;
    public Text healthText;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public GameObject gameOver;
    public float deathTimer;
    public Text gameOverTimer;
    public AudioClip deathClip;

    public GameObject respawn;


    Animator anim;
    AudioSource playerAudio;
    TPCharController playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <TPCharController> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (isDead)
        {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0)
            {
                isDead = false;
                gameOver.SetActive(false);
                AddHealth(10);
            }
            gameOverTimer.text = string.Format("{0:#0}", deathTimer);
        }
    }


    public void TakeDamage (float amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        //{0:#.00} to display deciamls
        healthText.text = string.Format("{0:#0} / 100", currentHealth);

        //playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }

    public void AddHealth(float amount)
    {
        if (currentHealth + amount > 100)
            currentHealth = 100;
        else
            currentHealth += amount;

        healthSlider.value = currentHealth;

        //{0:#.00} to display deciamls
        healthText.text = string.Format("{0:#0} / 100", currentHealth);

        
    }


    void Death ()
    {
        isDead = true;
        gameOver.SetActive(true);

        this.transform.position = respawn.transform.position;
        deathTimer = 10;
        //playerShooting.DisableEffects ();

        //anim.SetTrigger ("Die");

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
