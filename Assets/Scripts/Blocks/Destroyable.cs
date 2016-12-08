using UnityEngine;

public class Destroyable : MonoBehaviour, IResettable
{
    public int score;

    public string buildingName;

    [SerializeField]
    BuildingTracker buildingTracker;
     
    [HideInInspector]
    public Building BuildingType;

    [HideInInspector]
    public Shop ShopType;

    [HideInInspector]
    public Car CarType;

    [HideInInspector]
    private static GameObject explosionPrefab;

    public ObjectSort SortObject;
    private bool isDestroyed = false;

    public enum ObjectSort
    {
        Shop,
        Residence,
        Vehicle
    }

    public enum Shop
    {
        CoffeeShop,
        BookShop,
        ChickenShop,
        ClothingShop,
        DrugsStore,
        FastFoodShop,
        FruitShop,
        GiftShop,
        PizzaShop,
        MusicStore,
        Bakery,
        GasStation,
        CarService,
        Bar,
        SuperMarket,
        Restaurant
    }
    public enum Building
    {
        None,
        House,
        Flat,
        SkyTower,
        Stadium,
        Factory       
    }
    public enum Car
    {
        None,
        Ambulance,
        Bus,
        Car,
        Container,
        PickupTruck,
        PoliceCar,
        Suv,
        Taxi,
        Truck
    }

    void Start()
    {
        if (SortObject == ObjectSort.Residence)
        {
            this.buildingName = BuildingType.ToString ();
        } else if (SortObject == ObjectSort.Shop)
        {
            this.buildingName = ShopType.ToString ();
        } else if (SortObject == ObjectSort.Vehicle)
        {
            this.buildingName = CarType.ToString ();
        }
    }


    private void OnCollisionEnter(Collision coll)
    {
            if (coll.collider.CompareTag ("towerBlock"))
            {
                HighScore.CurrentScore += score;
                TrackBuilding ();
                CreateExplosion();
                CameraShake.Instance().ScreenShake(.5f);
            }
    }

    void TrackBuilding()
    {
        if (gameObject.activeSelf)
        {
            buildingTracker.AddBuilding(this);
            gameObject.SetActive(false);
        }
    }

    private void CreateExplosion()
    {
        if (!explosionPrefab)
        {
            explosionPrefab = Resources.Load("Prefabs/explosion") as GameObject;
        }
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.transform.SetParent(transform.parent);
    }

    public void Reset()
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}