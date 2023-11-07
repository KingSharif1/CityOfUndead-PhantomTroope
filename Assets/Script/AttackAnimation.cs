using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePoint;
    Animator animator;

    public bool fireForward = true;
    public float bulletForce = 1500.0f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if(horizontalInput > 0)
        {
            fireForward = true;
        }
        else if(horizontalInput < 0)
        {
            fireForward = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("doAttack");
            FireBullet();
        }
    }

    private void FireBullet()
    {
        //Creating New Bullet
        GameObject newBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        //get RigidBody off the bullet
        Rigidbody2D tempRigibody = newBullet.GetComponent<Rigidbody2D>();

        // if "push" the bullet forward
        if (fireForward)
        {
            //fire to the right
            tempRigibody.AddForce(transform.right * bulletForce);
        }
        else
        {
            //fire to the negative right aka left
            tempRigibody.AddForce(-transform.right * bulletForce);
        }

        // basic clean, disappear in 2 sec
        Destroy(newBullet, 2.0f);
    }
}
