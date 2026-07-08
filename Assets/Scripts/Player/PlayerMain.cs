using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [field: SerializeField] public float MaxEnergy { get; private set; }
    [field: SerializeField] public float MaxExperience { get; set; }
    public Rigidbody2D Rb { get; set; }
    public MyInputActions InputActions;
    public float CurrentEnergy { get; set; }
    public float CurrentExperience { get; set; }
    public int currentLevel=1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        InputActions = new MyInputActions();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
