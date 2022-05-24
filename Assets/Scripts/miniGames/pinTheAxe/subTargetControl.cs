using UnityEngine;

public class subTargetControl : MonoBehaviour
{
    public targetControl myTC;
    public float offsetTimer;
    public float xTimer;
    public float yTimer;
    public float xWidth;
    public float yWidth;
    public int stageMin;
    public bool randomSpeed;
    public bool randomMovement;
    public float randomReset = 2f;
    

    private MeshRenderer myMR;
    private MeshCollider myMC;
    private float randomTimeTimer;
    private float randomMovementTimer;

    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
        myMC = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myTC.stage >= stageMin - myTC.difficulty)
        {
            myMR.enabled = true;
            myMC.enabled = true;
        }


        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Sin(Time.fixedTime * xTimer + offsetTimer) * xWidth, 1 + Mathf.Cos(Time.fixedTime * yTimer + offsetTimer) * yWidth, transform.position.z), Time.deltaTime);
      
        if(randomSpeed == true)
        {
            randomTimeTimer -= Time.deltaTime;
            if(randomTimeTimer <= 0)
            {
                xTimer = Random.Range(-15, 15);
                yTimer = Random.Range(-15, 15);
                randomTimeTimer = randomReset;
            }
        }
        if(randomMovement == true)
        {
            randomMovementTimer -= Time.deltaTime;
            if(randomMovementTimer <= 0)
            {
                xWidth = Random.Range(3, 7);
                yWidth = Random.Range(3, 7);
                randomMovementTimer = randomReset;
            }
        }
    }
}
