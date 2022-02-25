using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponList : MonoBehaviour
{
    public enum WeapType { Primary, Secondary, Throwable, Medicine, WeapTypeEnd }
    public struct tagWeapInfo
    {
        public Sprite _WeapImage;
        public Sprite _AmmoImage;

        public string _strName;
        public string _strKind;
        public string _strCharTitle;
        public string _strChar;
        public string _strExplane;
        public string _strResourceName;
        
        public uint _iBaseAmmo;
        public int _iPrice;

        public WeapType _tWeapType;
        public GameObject _Prefab;
    }

    public enum WeaponName { AK_74, M4, UZI, M1911, Benelli_M4, M107, Baseball_Bat, Hammer, Katana, End }
    public enum ItemName { Grenade, Molotov, PipeBomb, FirstAid, PainPill, Adrenalin, End }

    public static tagWeapInfo[] m_tWeapInfo { get; set; } = new tagWeapInfo[(int)WeaponName.End];
    public static tagWeapInfo[] m_tItemInfo { get; set; } = new tagWeapInfo[(int)ItemName.End];

    // Start is called before the first frame update
    void Awake()
    {
        WeaponInfoList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void WeaponInfoList()
    {
        #region ## AK-74 ##
        m_tWeapInfo[(int)WeaponName.AK_74]._Prefab = Resources.Load("Weapon AK-74") as GameObject;
        m_tWeapInfo[(int)WeaponName.AK_74]._strName = "AK-74";
        m_tWeapInfo[(int)WeaponName.AK_74]._strKind = "���ݼ���";
        m_tWeapInfo[(int)WeaponName.AK_74]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.AK_74]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.AK_74]._strExplane = "��.. �ݶ� ���ƴ�..";
        m_tWeapInfo[(int)WeaponName.AK_74]._iPrice = 700;
        m_tWeapInfo[(int)WeaponName.AK_74]._WeapImage = Resources.Load<Sprite>("WEAPON/AK");
        m_tWeapInfo[(int)WeaponName.AK_74]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tWeapInfo[(int)WeaponName.AK_74]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.AK_74]._strResourceName = "Weapon AK-74";
        #endregion
        #region ## M4-A1 ##
        m_tWeapInfo[(int)WeaponName.M4]._Prefab = Resources.Load("Weapon M4-A1") as GameObject;
        m_tWeapInfo[(int)WeaponName.M4]._strName = "M4-A1";
        m_tWeapInfo[(int)WeaponName.M4]._strKind = "���ݼ���";
        m_tWeapInfo[(int)WeaponName.M4]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.M4]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.M4]._strExplane = "�����! ��Ί�!";
        m_tWeapInfo[(int)WeaponName.M4]._iPrice = 750;
        m_tWeapInfo[(int)WeaponName.M4]._WeapImage = Resources.Load<Sprite>("WEAPON/M4");
        m_tWeapInfo[(int)WeaponName.M4]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tWeapInfo[(int)WeaponName.M4]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.M4]._strResourceName = "Weapon M4-A1";
        #endregion

        #region ## UZI ##
        m_tWeapInfo[(int)WeaponName.UZI]._Prefab = Resources.Load("Weapon Uzi") as GameObject;
        m_tWeapInfo[(int)WeaponName.UZI]._strName = "Uzi";
        m_tWeapInfo[(int)WeaponName.UZI]._strKind = "�������";
        m_tWeapInfo[(int)WeaponName.UZI]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.UZI]._strChar = "10";
        m_tWeapInfo[(int)WeaponName.UZI]._strExplane = "�ʰ�� �Ѿ� ���ޱ�.";
        m_tWeapInfo[(int)WeaponName.UZI]._iPrice = 450;
        m_tWeapInfo[(int)WeaponName.UZI]._WeapImage = Resources.Load<Sprite>("WEAPON/UZI");
        m_tWeapInfo[(int)WeaponName.UZI]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_HG");
        m_tWeapInfo[(int)WeaponName.UZI]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.UZI]._strResourceName = "Weapon Uzi";
        #endregion

        #region ## ���� ##
        m_tWeapInfo[(int)WeaponName.M1911]._Prefab = Resources.Load("Weapon M1911") as GameObject;
        m_tWeapInfo[(int)WeaponName.M1911]._strName = "M1911";
        m_tWeapInfo[(int)WeaponName.M1911]._strKind = "����";
        m_tWeapInfo[(int)WeaponName.M1911]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.M1911]._strChar = "15";
        m_tWeapInfo[(int)WeaponName.M1911]._strExplane = "������";
        m_tWeapInfo[(int)WeaponName.M1911]._iPrice = 400;
        m_tWeapInfo[(int)WeaponName.M1911]._WeapImage = Resources.Load<Sprite>("WEAPON/PISTOL");
        m_tWeapInfo[(int)WeaponName.M1911]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_HG");
        m_tWeapInfo[(int)WeaponName.M1911]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.M1911]._strResourceName = "Weapon M1911";
        #endregion

        #region ## ���� ##
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._Prefab = Resources.Load("Weapon Benelli M4") as GameObject;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strName = "Benelli M4";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strKind = "��ź��";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strChar = "50";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strExplane = "��-��!";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._iPrice = 900;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._WeapImage = Resources.Load<Sprite>("WEAPON/SHOT");
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_SG");
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strResourceName = "Weapon Benelli M4";
        #endregion

        #region ## ���� ##
        m_tWeapInfo[(int)WeaponName.M107]._Prefab = Resources.Load("Weapon M107") as GameObject;
        m_tWeapInfo[(int)WeaponName.M107]._strName = "Barret M107";
        m_tWeapInfo[(int)WeaponName.M107]._strKind = "�빰���ݼ���";
        m_tWeapInfo[(int)WeaponName.M107]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.M107]._strChar = "80";
        m_tWeapInfo[(int)WeaponName.M107]._strExplane = "������ɲ�";
        m_tWeapInfo[(int)WeaponName.M107]._iPrice = 1100;
        m_tWeapInfo[(int)WeaponName.M107]._WeapImage = Resources.Load<Sprite>("WEAPON/SNIPER");
        m_tWeapInfo[(int)WeaponName.M107]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_SR");
        m_tWeapInfo[(int)WeaponName.M107]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.M107]._strResourceName = "Weapon M107";
        #endregion

        #region ## ���� ##
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._Prefab = Resources.Load("Weapon Baseball Bat") as GameObject;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strName = "�߱������";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strKind = "��������";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strExplane = "������ ������ ��OO�� ������ �������ִ�..";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._iPrice = 300;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._WeapImage = Resources.Load<Sprite>("MELEE/BaseballBat");
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strResourceName = "Weapon Baseball Bat";
        #endregion

        #region ## �Ը� ##
        m_tWeapInfo[(int)WeaponName.Hammer]._Prefab = Resources.Load("Weapon Hammer") as GameObject;
        m_tWeapInfo[(int)WeaponName.Hammer]._strName = "��ġ";
        m_tWeapInfo[(int)WeaponName.Hammer]._strKind = "��������";
        m_tWeapInfo[(int)WeaponName.Hammer]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.Hammer]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Hammer]._strExplane = "�������� �Ӹ��� ö���ص帳�ϴ�!";
        m_tWeapInfo[(int)WeaponName.Hammer]._iPrice = 350;
        m_tWeapInfo[(int)WeaponName.Hammer]._WeapImage = Resources.Load<Sprite>("MELEE/Hammer");
        m_tWeapInfo[(int)WeaponName.Hammer]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Hammer]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Hammer]._strResourceName = "Weapon Hammer";
        #endregion

        #region ## ����� ##
        m_tWeapInfo[(int)WeaponName.Katana]._Prefab = Resources.Load("Weapon Katana") as GameObject;
        m_tWeapInfo[(int)WeaponName.Katana]._strName = "�Ϻ���";
        m_tWeapInfo[(int)WeaponName.Katana]._strKind = "��������";
        m_tWeapInfo[(int)WeaponName.Katana]._strCharTitle = "���ݷ�:";
        m_tWeapInfo[(int)WeaponName.Katana]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Katana]._strExplane = "������������...";
        m_tWeapInfo[(int)WeaponName.Katana]._iPrice = 800;
        m_tWeapInfo[(int)WeaponName.Katana]._WeapImage = Resources.Load<Sprite>("MELEE/Katana");
        m_tWeapInfo[(int)WeaponName.Katana]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Katana]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Katana]._strResourceName = "Weapon Katana";
        #endregion

        #region ## ���߼���ź ##
        m_tItemInfo[(int)ItemName.Grenade]._Prefab = Resources.Load("Throwable Grenade") as GameObject;
        m_tItemInfo[(int)ItemName.Grenade]._strName = "���� ����ź";
        m_tItemInfo[(int)ItemName.Grenade]._strKind = "��ô����";
        m_tItemInfo[(int)ItemName.Grenade]._strCharTitle = "���ݷ�:";
        m_tItemInfo[(int)ItemName.Grenade]._strChar = "300";
        m_tItemInfo[(int)ItemName.Grenade]._strExplane = "���̾� �� �� Ȧ!";
        m_tItemInfo[(int)ItemName.Grenade]._iPrice = 150;
        m_tItemInfo[(int)ItemName.Grenade]._WeapImage = Resources.Load<Sprite>("THROW/Granade");
        m_tItemInfo[(int)ItemName.Grenade]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Grenade]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.Grenade]._strResourceName = "Throwable Grenade";
        #endregion

        #region ## ȭ���� ##
        m_tItemInfo[(int)ItemName.Molotov]._Prefab = Resources.Load("Throwable Molotov") as GameObject;
        m_tItemInfo[(int)ItemName.Molotov]._strName = "ȭ����";
        m_tItemInfo[(int)ItemName.Molotov]._strKind = "��ô����";
        m_tItemInfo[(int)ItemName.Molotov]._strCharTitle = "���ݷ�:";
        m_tItemInfo[(int)ItemName.Molotov]._strChar = "50/s";
        m_tItemInfo[(int)ItemName.Molotov]._strExplane = "��Ÿ������.";
        m_tItemInfo[(int)ItemName.Molotov]._iPrice = 100;
        m_tItemInfo[(int)ItemName.Molotov]._WeapImage = Resources.Load<Sprite>("THROW/Molotov");
        m_tItemInfo[(int)ItemName.Molotov]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Molotov]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.Molotov]._strResourceName = "Throwable Molotov";
        #endregion

        #region ## ��������ź ##
        m_tItemInfo[(int)ItemName.PipeBomb]._Prefab = Resources.Load("Throwable Pipebomb") as GameObject;
        m_tItemInfo[(int)ItemName.PipeBomb]._strName = "������ ��ź";
        m_tItemInfo[(int)ItemName.PipeBomb]._strKind = "��ô����";
        m_tItemInfo[(int)ItemName.PipeBomb]._strCharTitle = "���ݷ�:";
        m_tItemInfo[(int)ItemName.PipeBomb]._strChar = "200";
        m_tItemInfo[(int)ItemName.PipeBomb]._strExplane = "��θ�� �𿩶�! �ϳ� �� ��! ��!";
        m_tItemInfo[(int)ItemName.PipeBomb]._iPrice = 200;
        m_tItemInfo[(int)ItemName.PipeBomb]._WeapImage = Resources.Load<Sprite>("THROW/Pipe");
        m_tItemInfo[(int)ItemName.PipeBomb]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.PipeBomb]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.PipeBomb]._strResourceName = "Throwable Pipebomb";
        #endregion

        #region ## ����ġ�� ŰƮ ##
        m_tItemInfo[(int)ItemName.FirstAid]._Prefab = Resources.Load("Medicine FirstAid") as GameObject;
        m_tItemInfo[(int)ItemName.FirstAid]._strName = "����ġ�� ŰƮ";
        m_tItemInfo[(int)ItemName.FirstAid]._strKind = "�Ƿ�ǰ";
        m_tItemInfo[(int)ItemName.FirstAid]._strCharTitle = "ȸ����:";
        m_tItemInfo[(int)ItemName.FirstAid]._strChar = "50";
        m_tItemInfo[(int)ItemName.FirstAid]._strExplane = "�������� ��������.";
        m_tItemInfo[(int)ItemName.FirstAid]._iPrice = 300;
        m_tItemInfo[(int)ItemName.FirstAid]._WeapImage = Resources.Load<Sprite>("MEDICINE/FirstAid");
        m_tItemInfo[(int)ItemName.FirstAid]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.FirstAid]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.FirstAid]._strResourceName = "Medicine FirstAid";
        #endregion

        #region ## ������ ##
        m_tItemInfo[(int)ItemName.PainPill]._Prefab = Resources.Load("Medicine PainPills") as GameObject;
        m_tItemInfo[(int)ItemName.PainPill]._strName = "������";
        m_tItemInfo[(int)ItemName.PainPill]._strKind = "�Ƿ�ǰ";
        m_tItemInfo[(int)ItemName.PainPill]._strCharTitle = "ȸ����:";
        m_tItemInfo[(int)ItemName.PainPill]._strChar = "�ӽ�ü�� 30";
        m_tItemInfo[(int)ItemName.PainPill]._strExplane = "�� �� �� �Ծ ������ �ǰ�..?";
        m_tItemInfo[(int)ItemName.PainPill]._iPrice = 150;
        m_tItemInfo[(int)ItemName.PainPill]._WeapImage = Resources.Load<Sprite>("MEDICINE/PainPill");
        m_tItemInfo[(int)ItemName.PainPill]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.PainPill]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.PainPill]._strResourceName = "Medicine PainPills";
        #endregion

        #region ## �Ƶ巹���� �ֻ�� ##
        m_tItemInfo[(int)ItemName.Adrenalin]._Prefab = Resources.Load("Medicine AdrenalinShot") as GameObject;
        m_tItemInfo[(int)ItemName.Adrenalin]._strName = "�Ƶ巹���� �ֻ��";
        m_tItemInfo[(int)ItemName.Adrenalin]._strKind = "�Ƿ�ǰ";
        m_tItemInfo[(int)ItemName.Adrenalin]._strCharTitle = "ȸ����:";
        m_tItemInfo[(int)ItemName.Adrenalin]._strChar = "�ӽ�ü�� 80";
        m_tItemInfo[(int)ItemName.Adrenalin]._strExplane = "������ ����ȣȯ.";
        m_tItemInfo[(int)ItemName.Adrenalin]._iPrice = 400;
        m_tItemInfo[(int)ItemName.Adrenalin]._WeapImage = Resources.Load<Sprite>("MEDICINE/Adrenalin");
        m_tItemInfo[(int)ItemName.Adrenalin]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Adrenalin]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.Adrenalin]._strResourceName = "Medicine AdrenalinShot";
        #endregion
    }
}
