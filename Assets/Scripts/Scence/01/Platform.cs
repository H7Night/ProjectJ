using UnityEngine;
using UnityEngine.Tilemaps;
public class Platform : MonoBehaviour
{
    GameManager gameManager;
    float destroyDistance;
    bool createNewPlatform;
    GameObject platformGenerator;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        platformGenerator = GameObject.Find("PlatformGenerator");
        //destroyDistance = GameManager.instance.GetDestroyDistance();
        destroyDistance = gameManager.GetDestroyDistance();
    }
    private void FixedUpdate()
    {
        // Platform out of screen
        if (transform.position.y - Camera.main.transform.position.y < destroyDistance)
        {
            // Create new platform
            if (name != "Platform(Clone)" && name != "Spring(Clone)" && !createNewPlatform)
            {
                platformGenerator.GetComponent<PlatformGenerator>().GeneratePlatform(1);
                createNewPlatform = true;
            }
            
            // Deactive Collider and effector
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<PlatformEffector2D>().enabled = false;
            GetComponent<TilemapRenderer>().enabled = false;

            // Deactive collider and effector if gameobject has child
            if (transform.childCount > 0)
            {
                if(transform.GetChild(0).GetComponent<Platform>()) // if child is platform
                {
                    transform.GetChild(0).GetComponent<EdgeCollider2D>().enabled = false;
                    transform.GetChild(0).GetComponent<PlatformEffector2D>().enabled = false;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }

                // Destroy this platform if sound has finished
                // if (!GetComponent<AudioSource>().isPlaying && !transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
                //     Destroy(gameObject);
            }
            else
            {
                // Destroy this platform if sound has finished
                //if (!GetComponent<AudioSource>().isPlaying)
                    Destroy(gameObject);
            }
        }
    }
}
