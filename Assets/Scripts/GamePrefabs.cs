using UnityEngine;

public class GamePrefabs : MonoBehaviour
{
    private static GamePrefabs _Instance;

    public static GamePrefabs Instance
    {
        get
        {
            if (_Instance == null) _Instance = Instantiate(Resources.Load<GamePrefabs>("GamePrefabs"));
            return _Instance;
        }
    }
    public Transform scorePopUpPrefab;
    public ParticleSystem particlePrefab;
    public GameObject blockNextPrefab;
}
