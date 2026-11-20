using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject goodEnding, badEnding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goodEnding.SetActive(GM.Instance.killedAllSlimes);
        badEnding.SetActive(!GM.Instance.killedAllSlimes);
    }

    
}
