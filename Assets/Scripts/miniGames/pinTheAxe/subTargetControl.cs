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
    public bool random;
    public float randomReset = 2f;
    

    private MeshRenderer myMR;
    private MeshCollider myMC;
    private float randomTimer;

    // Start is called before the first frame update
    void Start()
    {
        myMR = GetComponent<MeshRenderer>();
        myMC = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myTC.stage >= stageMin)
        {
            myMR.enabled = true;
            myMC.enabled = true;
        }


        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Sin(Time.fixedTime * xTimer + offsetTimer) * xWidth, 1 + Mathf.Cos(Time.fixedTime * yTimer + offsetTimer) * yWidth, transform.position.z), Time.deltaTime);
      
        if(random == true)
        {
            randomTimer -= Time.deltaTime;
            if(randomTimer <= 0)
            {
                xTimer = Random.Range(-15, 15);
                yTimer = Random.Range(-15, 15);
                randomTimer = randomReset;
            }
        }
    }
}
