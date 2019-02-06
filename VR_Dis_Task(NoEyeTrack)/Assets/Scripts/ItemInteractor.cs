using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    [SerializeField] private Material NormalMaterial;

    [SerializeField] private Material GazeMaterial;

    [SerializeField] private InteractiveItem Item;

    [SerializeField] private Renderer renderer;

    [SerializeField] public GameObject StartButton;

    public void Awake()
    {
        renderer.material = NormalMaterial;
    }

    private void OnEnable()
    {
        Item.gazeOver += HandleOver;
        Item.gazeNotOver +=HandleOut;
    }

    private void OnDisable()
    {
        Item.gazeOver -= HandleOver;
        Item.gazeNotOver -= HandleOut;
    }

    private void HandleOver()
    {
        renderer.material = GazeMaterial;
        //Destroy(StartButton, 1,0f);
    }

    private void HandleOut()
    {
        renderer.material = NormalMaterial;
    }

}