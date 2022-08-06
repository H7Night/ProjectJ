using UnityEngine;
public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject springPregab;
    public GameObject coinPrefab;

    GameObject platform;
    GameObject randomObject;
    public Transform platformHolder;

    
    public float currentY;
    float Offset;
    Vector3 topLeft;
    void Start()
    {
        //platformHolder = transform.Find("PlatformHolder").transform;
        topLeft = Camera.main.ScreenToViewportPoint(new Vector3(0, 0, 0));
        Offset = 1.2f;

        GeneratePlatform(10);
    }
    public void GeneratePlatform(int Num)
    {
        for (int i = 0; i < Num; i++)
        {
            // Calculate platform x, y
            float Dist_X = Random.Range(topLeft.x + Offset, -topLeft.x - Offset);
            float Dist_Y = Random.Range(2f, 5f);

            // Create platform
            currentY += Dist_Y;
            Vector3 Platform_Pos = new Vector3(Dist_X, currentY, 0);
            int Rand_Platform = Random.Range(1, 10);

            if (Rand_Platform <= 7) // Create platform
                platform = Instantiate(platformPrefab, Platform_Pos, Quaternion.identity, platformHolder);

            else
                platform = Instantiate(springPregab, Platform_Pos, Quaternion.identity, platformHolder);
            
            // Create random objects; like spring, trampoline and etc...
            if (Rand_Platform != 2)
            {
                int Rand_Object = Random.Range(1, 40);

                if (Rand_Object >= 30) // Create Coin
                {
                    Vector3 Coin_Pos = new Vector3(Platform_Pos.x + 0.5f, Platform_Pos.y + 0.27f, 0);
                    randomObject = Instantiate(coinPrefab, Coin_Pos, Quaternion.identity);
                    
                    // Set parent to object
                    randomObject.transform.parent = platform.transform;
                }
                // else if (Rand_Object == 7) // Create trampoline
                // {
                //     Vector3 Trampoline_Pos = new Vector3(Platform_Pos.x + 0.13f, Platform_Pos.y + 0.25f, 0);
                //     randomObject = Instantiate(Trampoline, Trampoline_Pos, Quaternion.identity);

                //     // Set parent to object
                //     randomObject.transform.parent = Platform.transform;
                // }
                // else if (Rand_Object == 15) // Create propeller
                // {
                //     Vector3 Propeller_Pos = new Vector3(Platform_Pos.x + 0.13f, Platform_Pos.y + 0.15f, 0);
                //     randomObject = Instantiate(Propeller, Propeller_Pos, Quaternion.identity);

                //     // Set parent to object
                //     randomObject.transform.parent = Platform.transform;
                // }
            }
        }
    }
}
