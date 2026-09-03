using UnityEngine;
using UnityEngine.UI;

public class DeliveryManager : MonoBehaviour
{
    [Header("Pickup and Dropoff Parents")]
    public GameObject pickupPointsParent;
    public GameObject dropoffPointsParent;

    [Header("UI")]
    public Text deliveryStatusText;
    public Text moneyText;
    public Text timerText;

    [Header("Timer Settings")]
    public float deliveryTimeLimit = 180f;

    private GameObject[] pickupPoints;
    private GameObject[] dropoffPoints;

    private GameObject currentPickupPoint;
    private GameObject currentDropoffPoint;

    private bool hasPackage = false;
    private bool timerRunning = false;

    private float currentTimer;
    private int money = 0;

    void Start()
    {
        pickupPoints = GetChildren(pickupPointsParent);
        dropoffPoints = GetChildren(dropoffPointsParent);

        hasPackage = false;
        timerRunning = false;
        money = 0;

        HideAllPoints();
        ChooseRandomPickup();

        UpdateUI();
        HideTimer();
    }

    void Update()
    {
        if (timerRunning == true)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerUI();

            if (currentTimer <= 0)
            {
                DeliveryFailed();
            }
        }
    }

    GameObject[] GetChildren(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }

        return children;
    }

    void HideAllPoints()
    {
        foreach (GameObject pickup in pickupPoints)
        {
            pickup.SetActive(false);
        }

        foreach (GameObject dropoff in dropoffPoints)
        {
            dropoff.SetActive(false);
        }
    }

    void ChooseRandomPickup()
    {
        HideAllPoints();

        int randomIndex = Random.Range(0, pickupPoints.Length);
        currentPickupPoint = pickupPoints[randomIndex];

        currentPickupPoint.SetActive(true);

        deliveryStatusText.text = "Pick package from " + GetLocationName(currentPickupPoint);
        HideTimer();
    }

    void ChooseRandomDropoff()
    {
        HideAllPoints();

        int randomIndex = Random.Range(0, dropoffPoints.Length);
        currentDropoffPoint = dropoffPoints[randomIndex];

        currentDropoffPoint.SetActive(true);

        deliveryStatusText.text = "Drop package at " + GetLocationName(currentDropoffPoint);

        StartDeliveryTimer();
    }

    public void PickUpPackage(GameObject pickupPoint)
    {
        if (hasPackage == false && pickupPoint == currentPickupPoint)
        {
            hasPackage = true;
            ChooseRandomDropoff();
        }
    }

    public void CompleteDelivery(GameObject dropoffPoint)
    {
        if (hasPackage == true && dropoffPoint == currentDropoffPoint)
        {
            hasPackage = false;
            timerRunning = false;

            money += 100;
            UpdateUI();

            ChooseRandomPickup();
        }
    }

    void StartDeliveryTimer()
    {
        currentTimer = deliveryTimeLimit;
        timerRunning = true;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }

        UpdateTimerUI();
    }

    void DeliveryFailed()
    {
        timerRunning = false;
        hasPackage = false;

        deliveryStatusText.text = "Delivery Unsuccessful!";

        Invoke("ChooseRandomPickup", 2f);
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        if (currentTimer < 0)
        {
            currentTimer = 0;
        }

        int minutes = Mathf.FloorToInt(currentTimer / 60);
        int seconds = Mathf.FloorToInt(currentTimer % 60);

        timerText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void HideTimer()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    string GetLocationName(GameObject point)
    {
        DeliveryLocation location = point.GetComponent<DeliveryLocation>();

        if (location != null && location.locationName != "")
        {
            return location.locationName;
        }

        return point.name;
    }

    void UpdateUI()
    {
        moneyText.text = "$" + money;
    }
}