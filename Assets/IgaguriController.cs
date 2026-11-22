using NUnit.Framework.Constraints;
using UnityEngine;

public class IgaguriController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audio;
    public AudioClip throwSE;
    public AudioClip catchSE;

    [SerializeField] private float TimeDestroy = 5f;

    float dTime = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
        //Shoot(new Vector3(0, 200, 2000));
        dTime = 0;
    }

    // Update is called once per frame
    public void Shoot(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir);
        audio.PlayOneShot(throwSE);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, GetComponent<Rigidbody>().linearVelocity.y, 0f);
        //GetComponent<ParticleSystem>().Play();
        if (collision.gameObject.CompareTag("Target"))
        {
            GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, GetComponent<Rigidbody>().linearVelocity.y, 0f);
            GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject);
            audio.PlayOneShot(catchSE);
        }
    }
    void Update()
    {
        if (dTime > TimeDestroy)
        {
            Destroy(gameObject);
        }
        dTime += Time.deltaTime;
    }
}
