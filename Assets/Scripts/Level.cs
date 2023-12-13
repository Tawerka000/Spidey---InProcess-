using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level : MonoBehaviour
{
    public int XP = 0;

    private int newLevel = 10;
    public float playerDamage = 1.5f;
    public float shotSpeed = 1f;
    public int bullets = 1;
    private int firstUp, secondUp;
    public Text XPbar;
    private GameObject spider;
    public GameObject shield;
    public bool slowBullets = false;
    public GameObject Web;
    public GameObject Bee;


    public GameObject AttackButton;
    public GameObject SpeedButton;
    public GameObject WebButton;
    public GameObject SlowShotButton;
    public GameObject BeeButton;
    public GameObject BulletX2Button;
    public GameObject BulletX3Button;
    public GameObject BulletX4Button;
    public GameObject BulletX5Button;
    public GameObject ShieldButton;
    public GameObject Place1;
    public GameObject Place2;
    private List<GameObject> upgrades = new List<GameObject>();
    public GameObject UpgradeMenu;
    public GameObject SkipButton;
    void Start()
    {
        upgrades.Add(AttackButton);
        upgrades.Add(ShieldButton);
        upgrades.Add(SpeedButton);
        upgrades.Add(WebButton);
        upgrades.Add(SlowShotButton);
        upgrades.Add(BeeButton);
        upgrades.Add(BulletX2Button);

        XPbar.text = XP + "/" + newLevel;
    }
    public void addXP()
    {
        XP = XP + 1;
        XPbar.text = XP + "/" + newLevel;
        if (XP == newLevel)
        {
            XP = 0;
            UpgradeMenu.SetActive(true);
            SkipButton.SetActive(true);
            RandomUpgrade();
            newLevel = newLevel + 2;
            XPbar.text = XP + "/" + newLevel;
        }
    }

    void RandomUpgrade()
    {
        int num = upgrades.Count;
        firstUp = Random.Range(0, num);
        secondUp = Random.Range(0, num);
        while (!Check(secondUp, firstUp))
        {
            secondUp = Random.Range(0, num);
        }
        upgrades[firstUp].SetActive(true);
        upgrades[firstUp].transform.position = Place1.transform.position;
        upgrades[secondUp].SetActive(true);
        upgrades[secondUp].transform.position = Place2.transform.position;
        Pause();
    }
    bool Check(int second, int first)
    {
        if (second == first)
            return false;
        else
            return true;
    }
    void RemoveUpgrade(GameObject upgrade)
    {
        upgrades.Remove(upgrade);
    }
    void AfterPressButton()
    {
        SkipButton.SetActive(false);
        UpgradeMenu.SetActive(false);
        upgrades[firstUp].SetActive(false);
        upgrades[secondUp].SetActive(false);
        Unpause();
    }
    public void SkipUpgrade()
    {
        AfterPressButton();
    }
    private void Pause()
    {
        Time.timeScale = 0;
    }
    private void Unpause()
    {
        Time.timeScale = 1;
    }
    public void UpgradeDamage()
    {
        playerDamage += 0.5f;
        AfterPressButton();
    }
    public void UpgradeShotSpeed()
    {
        shotSpeed -= 0.1f;
        AfterPressButton();
    }
    public void Shield()
    {
        spider = GameObject.FindGameObjectWithTag("Spider");
        spider.GetComponent<PlayerController>().ShieldIsActive = true;
        shield.SetActive(true);
        AfterPressButton();
        RemoveUpgrade(ShieldButton);
    }
    public void NumberOfBullets()
    {
        if (bullets == 1)
        {
            bullets = 2;
            upgrades.Add(BulletX3Button);
            AfterPressButton();
            RemoveUpgrade(BulletX2Button); 
        }
        else if (bullets == 2)
        {
            bullets = 3;
            upgrades.Add(BulletX4Button);
            AfterPressButton();
            RemoveUpgrade(BulletX3Button);
        }
        else if (bullets == 3)
        {
            bullets = 4;
            upgrades.Add(BulletX5Button);
            AfterPressButton();   
            RemoveUpgrade(BulletX4Button);
        }
        else if (bullets == 4)
        {
            bullets = 5;
            AfterPressButton();
            RemoveUpgrade(BulletX5Button);
        }
    }
    public void SlowingShot()
    {
        slowBullets = true;
        AfterPressButton();
        RemoveUpgrade(SlowShotButton);
    }
    public void SlowingWeb()
    {
        Web.SetActive(true);
        AfterPressButton();
        RemoveUpgrade(WebButton);
    }
    public void BeeTurret()
    {
        Bee.SetActive(true);
        AfterPressButton();
        RemoveUpgrade(BeeButton);
    }
}
