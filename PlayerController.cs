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
    private GameObject [] spellArr;
    static int spellIndex = 0;
    public GameObject indicator;

    [Header("Player Health")]
    public int health;
    private GameController _gameController;

    //Canvas object that controls the main HUD of the game
    private HUDController _hudCntrl;

    private UnityEngine.AI.NavMeshAgent agent;

    // private Rigidbody rb;

    // Start is called before the first frame update
    void Start(){
        spellArr = new GameObject[3];
        _hudCntrl = HUDController.Instance;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        // //PLAYER MOVEMENT
        // float horizontalMovement = Input.GetAxis("Horizontal") * translationSpeed;
        // float verticalMovement = Input.GetAxis("Vertical") * translationSpeed;


        // // if(Input.GetAxis("Vertical")<0){
        // //     transform.Rotate(0, -180, 0);
        // // }

        // transform.Translate(horizontalMovement * Time.deltaTime, 0 , verticalMovement * Time.deltaTime);

        PlayerInput();
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

        firedProjectile.transform.rotation = Quaternion.Euler(originalRotation.x, projectileSpawn.rotation.eulerAngles.y, originalRotation.z);

        //firedProjectile.GetComponent<Rigidbody>().velocity =  .forward * projectileSpeed * Time.deltaTime;
        firedProjectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.VelocityChange);
        damageToGive = 1;
    }

    public void Fire(GameObject spell){
        
        GameObject firedSpell = Instantiate(projectile);

        //Ignore collisions between the player and its projectile
        //Physics.IgnoreCollision(spell.GetComponent<Collider>(), 
        //projectileSpawn.parent.GetComponent<Collider>());

        firedSpell.transform.position = projectileSpawn.position;
        Vector3 originalRotation = projectile.transform.rotation.eulerAngles;

        firedSpell.transform.rotation = Quaternion.Euler(originalRotation.x, projectileSpawn.rotation.eulerAngles.y, originalRotation.z);
        firedSpell.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * projectileSpeed, ForceMode.VelocityChange);
        Debug.Log("Spell I'm using: " + spell.GetComponent<Spell>().getName());
        damageToGive = spell.GetComponent<Spell>().getDamage();
        // projectileSpeed = spell.GetComponent<Spell>().getName();
        
    }

    public void Equip(GameObject spell){
        // if it goes to last index in spell array make it go to 0 index for next spell
        Debug.Log(spell.GetComponent<Spell>().getName());
        spellArr[spellIndex] = spell;
        _hudCntrl.changeSpell(spellIndex, spell.GetComponent<Spell>().getImage(),spell.GetComponent<Spell>().getName());
        spellIndex++;
        if (spellIndex == 3){
            spellIndex = 0;
        }
    }

    private void PlayerInput() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;    //This controls the fire rate
            Fire();
        }

        if (Input.GetButtonDown("Fire2")) {
            //GameObject indicator = spellArr[0].GetComponent<Spell>().indicator; to be uncommented when the spell prefab is created
            if (!indicator.activeSelf) {
                indicator.SetActive(!indicator.activeSelf);
                Cursor.visible = !Cursor.visible;
            } else {
                Debug.Log("ACTIVATING SPELL: ");
                //Fire(spellArr[0]);
                indicator.SetActive(!indicator.activeSelf);
                Cursor.visible = !Cursor.visible;
            }
        }

        if (Input.GetButton("Fire3")) {

        }

        // if (Input.GetButton("Fire4")) {
        //     Fire(spellArr[2]);
        // }
    }

    public void TakeDamage(int ammount) {
        health -= ammount;
        _hudCntrl.UpdateHealth("Player", health);
        if (health <= 0) {
            Destroy(gameObject);
        } 
    }

    public void MovePlayer(){
        // float horizontalMovement = Input.GetAxis("Horizontal");
        // float verticalMovement = Input.GetAxis("Vertical");

        // Vector3 direction = new Vector3 (horizontalMovement, 0.0f, verticalMovement);
        // GetComponent<Rigidbody>().velocity = direction * translationSpeed;

        if (Input.GetMouseButton(1)){
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo)) {
                if (hitInfo.collider != null) {
                    agent.SetDestination(hitInfo.point);
                }
            }
        }
    }
}
