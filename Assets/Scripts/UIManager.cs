using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Text m_Life;
    [SerializeField] private Image m_BulletImage;
    [SerializeField] private Text m_BulletCount;
    [SerializeField] private Image m_HandIcon;
    [SerializeField] private Image m_ItemInHand;
    [SerializeField] private Image m_ScannedItem;
    [SerializeField] private Image m_ScannedIcon;
    [SerializeField] private Sprite m_defaultWeapon;
    [SerializeField] private GameObject m_GameOver;



    private void Awake() {
        Instance = this;
        UIManager.Instance.SetDefaultWeapon();
        UIManager.Instance.SetItemInHand(null);
        UIManager.Instance.SetScannedItem(null);
    }
    public void SetScannedItem(Sprite item)
    {
        m_ScannedIcon.gameObject.SetActive(!item);
        m_ScannedItem.gameObject.SetActive(item);
        m_ScannedItem.sprite = item;
    }
    public void SetLife(int life)
    {
        m_Life.text = life.ToString();
    }
    public void SetAmmo(int amount)
    {
        m_BulletCount.text = amount.ToString();
    }
    public void SetNewWeapon(Sprite bullet, int amount)
    {
        m_BulletImage.sprite = bullet;
        m_BulletCount.text = amount.ToString();
    }
    public void SetDefaultWeapon()
    {
        m_BulletImage.sprite = m_defaultWeapon;
        m_BulletCount.text = "∞";
    }
    public void SetItemInHand(Sprite item)
    {
        m_HandIcon.gameObject.SetActive(!item);
        m_ItemInHand.gameObject.SetActive(item);
        m_ItemInHand .sprite = item;
    }
    public void GameOver()
    {
        m_GameOver.SetActive(true);
    }
}
