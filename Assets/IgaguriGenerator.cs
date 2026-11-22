using UnityEngine;
using System.Collections;

public class IgaguriGenerator : MonoBehaviour
{
    public GameObject igaguriPrefab;
    bool isThrown = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isThrown)
        {
            StartCoroutine(throwRoutine());
            isThrown = true;
        }
    }
    IEnumerator throwRoutine()
    {
        GameObject igaguri = Instantiate(igaguriPrefab);
        igaguri.GetComponent<IgaguriController>().Shoot(new Vector3(100, 200, 400));
        yield return new WaitForSeconds(3f);
        igaguri = Instantiate(igaguriPrefab);
        igaguri.GetComponent<IgaguriController>().Shoot(new Vector3(400, 200, 400));
        yield return new WaitForSeconds(3f);
        igaguri = Instantiate(igaguriPrefab);
        igaguri.GetComponent<IgaguriController>().Shoot(new Vector3(650, 200, 400));
        
    }
}
