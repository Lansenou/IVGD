using UnityEngine;

public class Destroyable : MonoBehaviour
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

    [SerializeField]
    private bool destroyParent = false;

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
        if (!isDestroyed)
        {
            if (coll.collider.CompareTag ("towerBlock"))
            {
                HighScore.instance.CurrentScore += score;
                TrackBuilding ();
                isDestroyed = true;
                CreateExplosion();
                SignIn.Instance.IncreaseDestroyedObjects(1);
                SignIn.Instance.DestroyerAchievement(100f);
                CameraShake.Instance().ScreenShake(.5f);
                FallManager.BuildingHasExploded = true;
            }
        }
    }

    void TrackBuilding()
    {
        buildingTracker.AddBuilding (this);
        Destroy(destroyParent ? transform.parent.gameObject : gameObject);
    }

    private void CreateExplosion()
    {
        if (!explosionPrefab)
        {
            explosionPrefab = Resources.Load("Prefabs/explosion") as GameObject;
        }
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.transform.SetParent(destroyParent ? transform.parent.parent : transform.parent);
    }
}
