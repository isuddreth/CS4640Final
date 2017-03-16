using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    Ray newRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    LineRenderer gunLinePrefab;
    LineRenderer shotgunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;


        for (int i = 0; i < 15; i++)
        {
            RaycastHit rayHit;
            GameObject shotgunLineObject = (GameObject)Instantiate(Resources.Load("ShotgunLineRenderer"));
            LineRenderer shotgunLine = shotgunLineObject.GetComponent<LineRenderer>();
            //line render position
            newRay = shootRay;
            newRay.direction = new Vector3(transform.forward.x + Random.Range(0, 10), transform.forward.y + Random.Range(0, 10), transform.forward.z);
            
            if (Physics.Raycast(newRay, out rayHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = rayHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, rayHit.point);
                }
                shotgunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                shotgunLine.SetPosition(1, newRay.origin + newRay.direction * range);
            }
            
        }
    }
}
