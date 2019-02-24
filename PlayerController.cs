using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("Player Movement Controller")]
    public float translationSpeed = 10.0F;
    public float rotationSpeed = 5.0F;

    [Header("Player Projectile Controller")]
    public GameObject projectile;
    public int projectileSpeed;
    public float fireRate;
    private float nextFire;
    public Transform projectileSpawn;
    public static int damageToGive;
    private AudioClip spellAudio;

    private GameObject [] spellArr;
    static int spellIndex = 0;
    public GameObject indicator;
    private GameObject spellProjectile;
    public GameObject spellUsing;
    public float coolDownPercentage = 100.0f;

    public Camera viewCamera;


    [Header("Player Health")]
    public int health;

    //Canvas object that controls the main HUD of the game
    private HUDController hudCntrl;
    
    // string sfire = "Fireball_projectile";

    // private Rigidbody rb;

    // Start is called before the first frame update
    void Start(){
        spellArr = new GameObject[3];
        hudCntrl = HUDController.Instance;
    }

    // Update is called once per frame
    void Update() {

        Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
        Plane groundPlane = new Plane (Vector3.up, Vector3.zero); //creates a clickable plane
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)) { // sets the distance along the ray where it intersects the plane
            Vector3 point = ray.GetPoint(rayDistance); //returns a point at rayDistance units along the ray 
            Debug.DrawLine(ray.origin,point,Color.red); // just a visualization of the ray 
            LookAt(point); // rotate player to point 
        }

        PlayerInput();
         
         // coolDownPercentage = (((nextFire-Time.time)/fireRate)*100);
         // Debug.Log("Cool down percentage: " + coolDownPercentage + "%");
    }
    


    public void LookAt(Vector3 lookPoint) {
        Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt (heightCorrectedPoint);
    }


    private void FixedUpdate() {
       //Fixed update should be used whenether dealing with physics. 
       //As it runs every frame at the exact same time.

        MovePlayer();

    }
    public void Fire() {
        GameObject firedProjectile = Instantiate(projectile);

        //Ignore collisions between the player and its projectile
        //Physics.IgnoreCollision(firedProjectile.GetComponent<Collider>(), 
        //projectileSpawn.parent.GetComponent<Collider>());


        firedProjectile.transform.position = projectileSpawn.position;
        Vector3 originalRotation = firedProjectile.transform.rotation.eulerAngles;

        firedProjectile.transform.rotation = Quaternion.Euler(originalRotation.x, transform.eulerAngles.y, originalRotation.z);

        //firedProjectile.GetComponent<Rigidbody>().velocity =  .forward * projectileSpeed * Time.deltaTime;
        firedProjectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.VelocityChange);
        damageToGive = 1;

    }

    public void Fire(GameObject spell){
        damageToGive = spell.GetComponent<Spell>().getDamage();
        // Debug.Log("Spell firing: "+ spell.GetComponent<Spell>().getName());
        // Debug.Log(damageToGive);
        
        spell.GetComponent<Spell>().Fire();

        
    }

    public void playSpellAudio(GameObject spell){
        spellAudio = spell.GetComponent<Spell>().getAudioFire();
        AudioSource.PlayClipAtPoint(spellAudio, new Vector3(5, 1, 2));
    }

    public void Equip(GameObject spell){
        // if it goes to last index in spell array make it go to 0 index for next spell
        // Debug.Log(spell.GetComponent<Spell>().getName());
        spellArr[spellIndex] = spell;
        hudCntrl.changeSpell(spellIndex, spell.GetComponent<Spell>().getImage(),spell.GetComponent<Spell>().getName());
        spellIndex++;
        if (spellIndex == 3){
            spellIndex = 0;
        }
    }

    private void PlayerInput() {

        // if (Input.GetButton("Fire1") && coolDownPercentage<= 0) {
        if (Input.GetButton("Fire1")) {
            nextFire = Time.time + fireRate;    //This controls the fire rate
            Fire(spellUsing);
        }


        if (Input.GetButtonDown("Fire2")) {
            //GameObject indicator = spellArr[0].GetComponent<Spell>().indicator; to be uncommented when the spell prefab is created
            if (indicator.activeSelf) {
                indicator.SetActive(!indicator.activeSelf);
                Cursor.visible = !Cursor.visible;
            } else {
                // Debug.Log("ACTIVATING SPELL: ");
                // GameObject proj = GameObject.Instantiate((GameObject)Resources.Load("Fireball_projectile"));
                spellUsing = spellArr[0];
                //Fire(spellArr[0]);
                // spellUsing.GetComponent<Spell>().coolDownPercentage
                fireRate = spellArr[0].GetComponent<Spell>().getSpellCoolDown();
                spellUsing.GetComponent<Spell>().setCoolDownPercentage(0f);
                // Debug.Log("FIRERATE" + fireRate);
                indicator.SetActive(!indicator.activeSelf);
                Cursor.visible = !Cursor.visible;
            }
        }

        if (Input.GetButton("Fire3")) {
            spellUsing = spellArr[1];
            fireRate = spellArr[1].GetComponent<Spell>().getSpellCoolDown();
            // Fire(spellArr[1]);
        }

        if (Input.GetButton("Fire4")) {
             spellUsing = spellArr[2];
            fireRate = spellUsing.GetComponent<Spell>().getSpellCoolDown();
        }
    }

    public void TakeDamage(int ammount) {
        health -= ammount;
        hudCntrl.UpdateHealth("Player", health);
    }

    public void MovePlayer(){
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalMovement, 0.0f, verticalMovement);
        // transform.rotation = Quaternion.LookRotation(direction);
        GetComponent<Rigidbody>().velocity = direction * translationSpeed;
    }
}
