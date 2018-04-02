using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{    
    public int playerNumber = 1;
    [SerializeField]
    private Rigidbody ball;    
    [SerializeField]               
    private Transform fireTransform;     
    [SerializeField]      
    private Slider aimSlider;                  
    [SerializeField]             
    private float minLaunchForce = 15f; 
    [SerializeField]      
    private float maxLaunchForce = 30f; 
    [SerializeField]       
    private float maxChargeTime = 0.75f;
    

    private string fireButton;                
    private float currentLaunchForce;         
    private float chargeSpeed;     
    private bool fired;

    private PlayerPickUp playerPickUp;
    private GameManager game;

    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }
    private void Start ()
    {     
        fireButton = "Fire" + playerNumber;
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
        playerPickUp = this.gameObject.GetComponent<PlayerPickUp>();
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        
    }
	private void Update ()
    {
        aimSlider.value = minLaunchForce;

        if (playerPickUp.IsCarrying == true)
        {
            Debug.Log("true");
            if (currentLaunchForce >= maxLaunchForce && !fired)
            {
                currentLaunchForce = maxLaunchForce;
                
                Fire();
            }
            else if (Input.GetButtonDown(fireButton))
            {
                // ... reset the fired flag and reset the launch force.
                fired = false;
                currentLaunchForce = minLaunchForce;
                // Change the clip to the charging clip and start it playing.
                //   shootingAudio.clip = m_ChargingClip;
                //  m_ShootingAudio.Play();
            }
            else if (Input.GetButton(fireButton) && !fired)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;
                aimSlider.value = currentLaunchForce; 
            }
            else if (Input.GetButtonUp(fireButton) && !fired)
            {
                Fire();
            }
        }
        else
        {
            Debug.Log("False");
            playerPickUp.IsCarrying = false;
            fired = false;
        }
    }
    private void Fire()
    {
        
        fired = true;
        playerPickUp.IsCarrying = false;
        game.Ball.GetComponent<Rigidbody>().useGravity = true;
        game.Ball.GetComponent<Rigidbody>().isKinematic = false;
        game.Ball.transform.parent = null;
        game.Ball.GetComponent<Rigidbody>().velocity = currentLaunchForce * fireTransform.forward;
        // Change the clip to the firing clip and play it.
        //m_ShootingAudio.clip = m_FireClip;
        //m_ShootingAudio.Play();
       
        currentLaunchForce = minLaunchForce;
    }
}
