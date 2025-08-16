using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject winDescriptionObject;
    public GameObject pauseScreenObject;
    public GameObject explosionFX;
    public GameObject pickupFX;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private AudioSource backgroundMusic;
    public AudioClip pickupSound;
    public AudioClip destroySound;

    public AudioClip stageWinSound;
    public AudioClip stageLoseSound;

    public float pickupVolume = 0.8f;
    public float destroyVolume = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        
        Transform backgroundMusicTransform = Camera.main.transform.Find("BackgroundMusic");
            if (backgroundMusicTransform != null)
            {
               backgroundMusic = backgroundMusicTransform.GetComponent<AudioSource>();
            }                   
    }

    void OnMove (InputValue movementValue)
   {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
   }

   void SetCountText()
   {
        countText.text = "Count: " + count.ToString();
        if (count >= 17)
        {
          pauseScreenObject.SetActive(true);
          winTextObject.SetActive(true);
          winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
          
          if (stageWinSound != null)
          {
               AudioSource.PlayClipAtPoint(stageWinSound, transform.position, 1.0f);
          }
          
          StopBGMusic();
          Destroy(GameObject.Find("Character_MrRaccoon"));
        }
   }

   void FixedUpdate()
   {
        Vector3 movementVector = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movementVector * speed);
   }

     private void OnCollisionEnter(Collision collision)
     {
           if (collision.gameObject.CompareTag("Wall"))
        {
            AudioSource wallAudio = collision.gameObject.GetComponent<AudioSource>();
            if (wallAudio != null)
            {
                wallAudio.Play();
            }
            else
            {
                Debug.LogWarning("Wall object does not have an AudioSource component.");
            }
        }

          if (collision.gameObject.CompareTag("Enemy")) 
          {    
               
               var currentPickupFX = Instantiate(explosionFX, transform.position, Quaternion.identity);
               Destroy(gameObject);
               
               winTextObject.SetActive(true);
               winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
               winDescriptionObject.SetActive(true);
               winDescriptionObject.GetComponent<TextMeshProUGUI>().text = "Better luck next time!";
               pauseScreenObject.SetActive(true);

               StopBGMusic();
               
               if (destroySound != null)
               {
                    AudioSource.PlayClipAtPoint(destroySound, transform.position, destroyVolume);
               }

               if (stageLoseSound != null)
               {
                    AudioSource.PlayClipAtPoint(stageLoseSound, transform.position, 1.0f);
               }

               // if (collision.gameObject.GetComponentInChildren<Animator>() != null)
               // {
               //      collision.gameObject.GetComponentInChildren<Animator>().SetFloat("speed_f", 0);
               // }
               

               Destroy(currentPickupFX, 3);              
          }
     }

   void OnTriggerEnter(Collider other)
   {
     
    if (other.gameObject.CompareTag("PickUp")) 
       {
            var currentPickupFX = Instantiate(pickupFX, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            if (pickupSound != null)
               {
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);
               }

            Destroy(currentPickupFX, 3);
       }
   }

     void StopBGMusic()
     {
          
          if (backgroundMusic != null)
          {
               backgroundMusic.Stop();
          }
     }
    void OnDestroy()
    {     
            
                                   
    }
}
