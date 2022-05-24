using UnityEngine;

public class subTargetControl : MonoBehaviour
{
    public targetControl myTC;
    public float offsetTimer;
    public float xTimer;
    public float yTimer;
    // Start is called before the first frame update
    void Awake()
    {
        if (myTC.difficulty != 4f) this.gameObject.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.fixedTime * xTimer + offsetTimer) * 3f, 1f - Mathf.Cos(Time.fixedTime * yTimer + offsetTimer) * 1f, transform.position.z);
      
    }
}
