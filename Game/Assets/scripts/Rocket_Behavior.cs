using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_Behavior : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float thrustSpeed = 370f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] ParticleSystem boost;
    [SerializeField] ParticleSystem dead;
    [SerializeField] ParticleSystem success;

    bool gameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            thrust();
            rotation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Launch":
                break;
            case "Finish":
                gameActive = false;
                success.Play();
                Invoke("loadNextLevel",2f);
                break;
            default:
                gameActive = false;
                dead.Play();
                Invoke("reloadLevel",2f);
                break;
        }
    }

    private void reloadLevel()
    {
        gameActive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void loadNextLevel()
    {
        gameActive = true;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel;
        if(currentLevel!=SceneManager.sceneCountInBuildSettings-1)
        {
            nextLevel = currentLevel+1;
        }
        else
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
    }

    private void rotation()
    {
        rigidBody.angularVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationSpeed*Time.deltaTime);
        }
    }

    private void thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed*Time.deltaTime);
            boost.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            boost.Stop();
            audioSource.Stop();
        }
    }
}
