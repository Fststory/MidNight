using UnityEngine;

public class ObjRotate : MonoBehaviour
{

    public Vector3 objdir;
    public float objspeed;

    private void Awake()
    {
        objspeed = Random.Range(200f, 300f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * objspeed * Time.deltaTime);
    }
}
