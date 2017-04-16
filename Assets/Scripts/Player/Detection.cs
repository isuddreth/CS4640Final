
using System.Collections;
using UnityEngine;
//using UnityEngine.UI;

public class Detection : MonoBehaviour
{
    public PlayerHealth health;
    public FoodManager food;
    public QuestManager quest;


    public float Reach = 4.0F;
    [HideInInspector]
    public bool healthInReach;
    public bool throneInReach;
    public bool playerFoodoodInReach;
    public bool castleFoodInReach;
    public bool BerryBushInReach;
    public float timer;

    public Color DebugRayColor = Color.green;
    [Range(0.0F, 1.0F)]
    public float DebugRayColorAlpha = 1.0F; //Opacity.

    void Start()
    {
        gameObject.name = "Player";
        gameObject.tag = "Player";
    }

    void Update()
    {
        //Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
        ray.origin = new Vector3(this.transform.position.x, this.transform.position.y + 2F, this.transform.position.z + 1F);

        RaycastHit hit; //Variable reading information about the collider hit.

        //Cast ray from center of the screen towards where the player is looking.
        if (Physics.Raycast(ray, out hit, Reach))
        {
            // if on test health cube
            if (hit.collider.tag == "HealthTest")
            {
                healthInReach = true;
                throneInReach = false;
                playerFoodoodInReach = false;
                castleFoodInReach = false;
                BerryBushInReach = false;

                if (Input.GetKey(KeyCode.E))
                {
                    health.TakeDamage(.5f);
                }
            }

            // if on throne cube
            else if (hit.collider.tag == "Throne")
            {
                healthInReach = false;
                throneInReach = true;
                playerFoodoodInReach = false;
                castleFoodInReach = false;
                BerryBushInReach = false;

                if (Input.GetKey(KeyCode.E) && timer <= 0)
                {
                    timer = 5;
                    foreach (Quest myQuest in quest.currentQuests)
                    {
                        if (myQuest.questObjective == "Throne")
                        {
                            quest.endQuest(myQuest);
                        }
                    }

                    //update quest tracker text
                    quest.removeDoneQuest();
                    quest.addActiveQuest();
                }

            }

            // if on test food cube
            else if (hit.collider.tag == "PlayerFoodTest")
            {
                healthInReach = false;
                throneInReach = false;
                playerFoodoodInReach = true;
                castleFoodInReach = false;
                BerryBushInReach = false;

                if (Input.GetKey(KeyCode.E))
                {
                    food.AddPlayerFood(.5f);
                }
            }

            //// if on test health cube
            //else if (hit.collider.tag == "CastleFoodTest")
            //{
            //    healthInReach = false;
            //    throneInReach = false;
            //    playerFoodoodInReach = false;
            //    castleFoodInReach = true;

            //    if (Input.GetKey(KeyCode.E))
            //    {
            //        food.AddCastleFood(.5f);
            //    }
            //}

            // if on table
            else if (hit.collider.tag == "KitchenTable")
            {
                healthInReach = false;
                throneInReach = false;
                playerFoodoodInReach = false;
                castleFoodInReach = true;
                BerryBushInReach = false;

                if (Input.GetKey(KeyCode.E))
                {
                    quest.AddCastleFood(.5f);
                }
            }


            // if on test health cube
            else if (hit.collider.tag == "BerryBushInReach")
            {
                healthInReach = false;
                throneInReach = false;
                playerFoodoodInReach = false;
                castleFoodInReach = false;
                BerryBushInReach = true;

                if (Input.GetKey(KeyCode.E))
                {
                    food.AddPlayerFood(.5f);
                }
            }

            else
            {
                healthInReach = false;
                throneInReach = false;
                playerFoodoodInReach = false;
                castleFoodInReach = false;
                BerryBushInReach = false;
            }
        }

        else
        {
            healthInReach = false;
            throneInReach = false;
            playerFoodoodInReach = false;
            castleFoodInReach = false;
            BerryBushInReach = false;
        }

        //Draw the ray as a colored line for debugging purposes.
        Debug.DrawRay(ray.origin, ray.direction * Reach, DebugRayColor);

        if (timer > 0)
            timer -= Time.deltaTime;
        

    }

}
