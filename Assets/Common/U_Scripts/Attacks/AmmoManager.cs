using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : AttackManager
{
    [Header("Ammo params: ")]
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Image cooldownUI;
    [HideInInspector] public bool reloadOverwritten;

    [SerializeField] private bool fillConsecutively;
    [SerializeField] private int maxBullets;
    private int bulletCount;
    [SerializeField] private float reloadTime;
    private float reloadTimer;

    [Header("Ammo displays: ")] 
    [SerializeField] private Transform objParent;
    [SerializeField] private GameObject objRepresentation;
    [SerializeField] private Transform UIParent;
    [SerializeField] private GameObject UIRepresentation;
    
    
    private void Start()
    {
        bulletCount = maxBullets;
        for (int i = maxBullets; i != 0; i--)
        {
            CreateDisplayObject();
        }
        
        cooldownUI.fillAmount = 0f;
    }

    private void Update()
    {
        cooldownUI.fillAmount = reloadTimer / reloadTime;
        
        if (!fillConsecutively)
        {
            if (bulletCount <= 0)
            {
                DisableAttacks();
                bulletCount = 0;
            
                reloadTimer += Time.deltaTime;
            
                if (reloadTimer >= reloadTime)
                {
                    if (!reloadOverwritten)
                        EnableAttacks();
                
                    bulletCount = fillConsecutively && bulletCount < maxBullets ? +1 : maxBullets;
                    reloadTimer = 0f;
                    
                    CreateDisplayObject();
                }
            }
        }
        else if (bulletCount < maxBullets)
        {
            if (bulletCount <= 0f)
                DisableAttacks();
            
            reloadTimer += Time.deltaTime;
        
            if (reloadTimer >= reloadTime)
            {
                if (!reloadOverwritten)
                    EnableAttacks();
            
                bulletCount++;
                reloadTimer = 0f;
                
                CreateDisplayObject();
            }
        }

        ammoText.text = AmmoString();
    }

    public void SpendBullet(int count)
    {
        bulletCount -= count;
        DestroyDisplayObject();
    }

    public void ResetTimer()
    {
        reloadTimer = 0f;
    }

    public string AmmoString()
    {
        return bulletCount.ToString("D2") + "/" + maxBullets.ToString("D2");
    }

    private void CreateDisplayObject()
    {
        if (objParent != null)
            Instantiate(objRepresentation, Vector3.zero, Quaternion.identity, objParent);
        
        if (UIParent != null)
            Instantiate(UIRepresentation, Vector3.zero, Quaternion.identity, UIParent);
    }

    private void DestroyDisplayObject()
    {
        if (objParent != null)
            Destroy(objParent.GetChild(0).gameObject);
        
        if (UIParent != null)
            Destroy(UIParent.GetChild(0).gameObject);
    }
}
