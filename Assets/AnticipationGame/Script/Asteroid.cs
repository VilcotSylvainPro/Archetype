using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Asteroid : MonoBehaviour
{
    
    
    [SerializeField] private float Vitesse;
    [SerializeField] private float Impulsion;
    [SerializeField] private ParticleSystem ExplosionParticule;
    [SerializeField] private ParticleSystem ExplosionParticuleVaisseau;
    [SerializeField] private GameObject Rouge;
    private Vector3 targetPosition;
    private int RedImageTimer;
    private int ShakeTimer = 4;

    private bool Toucher = false;





    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); 
        if (player != null)
        {
            Vector3 FuseePostion = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            targetPosition = FuseePostion; 
        }



       


    }

    // Update is called once per frame
    void Update()
    {
       Vector3 Test = Vector3.MoveTowards(transform.position, targetPosition, Vitesse * Time.deltaTime);
        transform.position = new Vector3(Test.x, 0, Test.z);



        transform.Rotate(Random.Range(0f, 70f) * Time.deltaTime, Random.Range(0f, 70f) * Time.deltaTime, Random.Range(0f, 70f) * Time.deltaTime, Space.Self);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
            Instantiate(ExplosionParticule, gameObject.transform.position, Quaternion.identity);
        }
    }



    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            //Debug.Log("Je touche Asteroide");
            /*Destroy(gameObject);
            Destroy(other);

            Instantiate(ExplosionParticule, transform.position, Quaternion.identity);

        }


        else if (other.gameObject.tag == "Player")
        {
            Debug.Log("Je touche Joueur");
        }
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            //Debug.Log("Je touche Asteroide");
            //Destroy(gameObject);
            //Destroy(other);
        }


        else if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Je touche Joueur");
        }
    }*/

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid")) 
        {
            
            ContactPoint contact = collision.contacts[0];

            // Instancier le système de particules au point de contact
            Instantiate(ExplosionParticule, contact.point, Quaternion.identity);

            float reductionFactor = Random.Range(0.5f, 0.9f);

            transform.localScale *= reductionFactor;
            collision.gameObject.transform.localScale *= reductionFactor;


            if (transform.localScale.y < 0.25f)
            {
                Destroy(gameObject);
                Instantiate(ExplosionParticule, gameObject.transform.position, Quaternion.identity);
            }
            else
            {

                // Pousser l'astéroïde dans une direction aléatoire
                Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
                GetComponent<Rigidbody>().AddForce(randomDirection * Impulsion, ForceMode.Impulse);
            }


            if (collision.gameObject.transform.localScale.y < 0.25f)
            {
                Destroy(collision.gameObject);
                Instantiate(ExplosionParticule, collision.gameObject.transform.position, Quaternion.identity);
            }
            else
            {

                // Pousser l'astéroïde dans une direction aléatoire
                Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
                GetComponent<Rigidbody>().AddForce(randomDirection * Impulsion, ForceMode.Impulse);
            }


            // Détruire l'astéroïde
            //Destroy(gameObject);
            //Destroy(collision.gameObject);
        }



        if (collision.gameObject.CompareTag("Player"))
        {

            ContactPoint contact = collision.contacts[0];

            Toucher = true;
            // Instancier le système de particules au point de contact
            Instantiate(ExplosionParticule, contact.point, Quaternion.identity);
            Instantiate(ExplosionParticuleVaisseau, contact.point, Quaternion.identity);

            float reductionFactor = Random.Range(0.5f, 0.9f);
            ShakeGame2.Instance.ShakeCamera(2f, 2f);
            
            transform.localScale *= reductionFactor;

            



            Destroy(gameObject);
            Instantiate(ExplosionParticule, gameObject.transform.position, Quaternion.identity);

            /*if (transform.localScale.y < 0.25f)
            {
                
                toucher = false;
            }
            else
            {

                // Pousser l'astéroïde dans une direction aléatoire
                Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
                GetComponent<Rigidbody>().AddForce(randomDirection * Impulsion, ForceMode.Impulse);
                toucher = false;
            }*/


            


        }



        
    }


    


    


}
