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
        m_tWeapInfo[(int)WeaponName.AK_74]._strKind = "돌격소총";
        m_tWeapInfo[(int)WeaponName.AK_74]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.AK_74]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.AK_74]._strExplane = "아.. 반란 마렵다..";
        m_tWeapInfo[(int)WeaponName.AK_74]._iPrice = 700;
        m_tWeapInfo[(int)WeaponName.AK_74]._WeapImage = Resources.Load<Sprite>("WEAPON/AK");
        m_tWeapInfo[(int)WeaponName.AK_74]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tWeapInfo[(int)WeaponName.AK_74]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.AK_74]._strResourceName = "Weapon AK-74";
        #endregion
        #region ## M4-A1 ##
        m_tWeapInfo[(int)WeaponName.M4]._Prefab = Resources.Load("Weapon M4-A1") as GameObject;
        m_tWeapInfo[(int)WeaponName.M4]._strName = "M4-A1";
        m_tWeapInfo[(int)WeaponName.M4]._strKind = "돌격소총";
        m_tWeapInfo[(int)WeaponName.M4]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.M4]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.M4]._strExplane = "떆띠깔! 뤼로딍!";
        m_tWeapInfo[(int)WeaponName.M4]._iPrice = 750;
        m_tWeapInfo[(int)WeaponName.M4]._WeapImage = Resources.Load<Sprite>("WEAPON/M4");
        m_tWeapInfo[(int)WeaponName.M4]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tWeapInfo[(int)WeaponName.M4]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.M4]._strResourceName = "Weapon M4-A1";
        #endregion

        #region ## UZI ##
        m_tWeapInfo[(int)WeaponName.UZI]._Prefab = Resources.Load("Weapon Uzi") as GameObject;
        m_tWeapInfo[(int)WeaponName.UZI]._strName = "Uzi";
        m_tWeapInfo[(int)WeaponName.UZI]._strKind = "기관단총";
        m_tWeapInfo[(int)WeaponName.UZI]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.UZI]._strChar = "10";
        m_tWeapInfo[(int)WeaponName.UZI]._strExplane = "초고속 총알 지급기.";
        m_tWeapInfo[(int)WeaponName.UZI]._iPrice = 450;
        m_tWeapInfo[(int)WeaponName.UZI]._WeapImage = Resources.Load<Sprite>("WEAPON/UZI");
        m_tWeapInfo[(int)WeaponName.UZI]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_HG");
        m_tWeapInfo[(int)WeaponName.UZI]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.UZI]._strResourceName = "Weapon Uzi";
        #endregion

        #region ## 권총 ##
        m_tWeapInfo[(int)WeaponName.M1911]._Prefab = Resources.Load("Weapon M1911") as GameObject;
        m_tWeapInfo[(int)WeaponName.M1911]._strName = "M1911";
        m_tWeapInfo[(int)WeaponName.M1911]._strKind = "권총";
        m_tWeapInfo[(int)WeaponName.M1911]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.M1911]._strChar = "15";
        m_tWeapInfo[(int)WeaponName.M1911]._strExplane = "구너총";
        m_tWeapInfo[(int)WeaponName.M1911]._iPrice = 400;
        m_tWeapInfo[(int)WeaponName.M1911]._WeapImage = Resources.Load<Sprite>("WEAPON/PISTOL");
        m_tWeapInfo[(int)WeaponName.M1911]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_HG");
        m_tWeapInfo[(int)WeaponName.M1911]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.M1911]._strResourceName = "Weapon M1911";
        #endregion

        #region ## 샷건 ##
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._Prefab = Resources.Load("Weapon Benelli M4") as GameObject;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strName = "Benelli M4";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strKind = "산탄총";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strChar = "50";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strExplane = "샤-깐!";
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._iPrice = 900;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._WeapImage = Resources.Load<Sprite>("WEAPON/SHOT");
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_SG");
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.Benelli_M4]._strResourceName = "Weapon Benelli M4";
        #endregion

        #region ## 스나 ##
        m_tWeapInfo[(int)WeaponName.M107]._Prefab = Resources.Load("Weapon M107") as GameObject;
        m_tWeapInfo[(int)WeaponName.M107]._strName = "Barret M107";
        m_tWeapInfo[(int)WeaponName.M107]._strKind = "대물저격소총";
        m_tWeapInfo[(int)WeaponName.M107]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.M107]._strChar = "80";
        m_tWeapInfo[(int)WeaponName.M107]._strExplane = "땅끄사냥꾼";
        m_tWeapInfo[(int)WeaponName.M107]._iPrice = 1100;
        m_tWeapInfo[(int)WeaponName.M107]._WeapImage = Resources.Load<Sprite>("WEAPON/SNIPER");
        m_tWeapInfo[(int)WeaponName.M107]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_SR");
        m_tWeapInfo[(int)WeaponName.M107]._tWeapType = WeapType.Primary;
        m_tWeapInfo[(int)WeaponName.M107]._strResourceName = "Weapon M107";
        #endregion

        #region ## 빠따 ##
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._Prefab = Resources.Load("Weapon Baseball Bat") as GameObject;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strName = "야구방망이";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strKind = "근접무기";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strExplane = "투수로 유명한 류OO의 싸인이 새겨져있다..";
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._iPrice = 300;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._WeapImage = Resources.Load<Sprite>("MELEE/BaseballBat");
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Baseball_Bat]._strResourceName = "Weapon Baseball Bat";
        #endregion

        #region ## 함마 ##
        m_tWeapInfo[(int)WeaponName.Hammer]._Prefab = Resources.Load("Weapon Hammer") as GameObject;
        m_tWeapInfo[(int)WeaponName.Hammer]._strName = "망치";
        m_tWeapInfo[(int)WeaponName.Hammer]._strKind = "근접무기";
        m_tWeapInfo[(int)WeaponName.Hammer]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.Hammer]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Hammer]._strExplane = "여러분의 머리를 철거해드립니다!";
        m_tWeapInfo[(int)WeaponName.Hammer]._iPrice = 350;
        m_tWeapInfo[(int)WeaponName.Hammer]._WeapImage = Resources.Load<Sprite>("MELEE/Hammer");
        m_tWeapInfo[(int)WeaponName.Hammer]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Hammer]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Hammer]._strResourceName = "Weapon Hammer";
        #endregion

        #region ## 까따나 ##
        m_tWeapInfo[(int)WeaponName.Katana]._Prefab = Resources.Load("Weapon Katana") as GameObject;
        m_tWeapInfo[(int)WeaponName.Katana]._strName = "일본도";
        m_tWeapInfo[(int)WeaponName.Katana]._strKind = "근접무기";
        m_tWeapInfo[(int)WeaponName.Katana]._strCharTitle = "공격력:";
        m_tWeapInfo[(int)WeaponName.Katana]._strChar = "20";
        m_tWeapInfo[(int)WeaponName.Katana]._strExplane = "프스으으으으...";
        m_tWeapInfo[(int)WeaponName.Katana]._iPrice = 800;
        m_tWeapInfo[(int)WeaponName.Katana]._WeapImage = Resources.Load<Sprite>("MELEE/Katana");
        m_tWeapInfo[(int)WeaponName.Katana]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_MELEE");
        m_tWeapInfo[(int)WeaponName.Katana]._tWeapType = WeapType.Secondary;
        m_tWeapInfo[(int)WeaponName.Katana]._strResourceName = "Weapon Katana";
        #endregion

        #region ## 폭발수류탄 ##
        m_tItemInfo[(int)ItemName.Grenade]._Prefab = Resources.Load("Throwable Grenade") as GameObject;
        m_tItemInfo[(int)ItemName.Grenade]._strName = "폭발 수류탄";
        m_tItemInfo[(int)ItemName.Grenade]._strKind = "투척무기";
        m_tItemInfo[(int)ItemName.Grenade]._strCharTitle = "공격력:";
        m_tItemInfo[(int)ItemName.Grenade]._strChar = "300";
        m_tItemInfo[(int)ItemName.Grenade]._strExplane = "빠이어 인 어 홀!";
        m_tItemInfo[(int)ItemName.Grenade]._iPrice = 150;
        m_tItemInfo[(int)ItemName.Grenade]._WeapImage = Resources.Load<Sprite>("THROW/Granade");
        m_tItemInfo[(int)ItemName.Grenade]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Grenade]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.Grenade]._strResourceName = "Throwable Grenade";
        #endregion

        #region ## 화염병 ##
        m_tItemInfo[(int)ItemName.Molotov]._Prefab = Resources.Load("Throwable Molotov") as GameObject;
        m_tItemInfo[(int)ItemName.Molotov]._strName = "화염병";
        m_tItemInfo[(int)ItemName.Molotov]._strKind = "투척무기";
        m_tItemInfo[(int)ItemName.Molotov]._strCharTitle = "공격력:";
        m_tItemInfo[(int)ItemName.Molotov]._strChar = "50/s";
        m_tItemInfo[(int)ItemName.Molotov]._strExplane = "불타오르네.";
        m_tItemInfo[(int)ItemName.Molotov]._iPrice = 100;
        m_tItemInfo[(int)ItemName.Molotov]._WeapImage = Resources.Load<Sprite>("THROW/Molotov");
        m_tItemInfo[(int)ItemName.Molotov]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Molotov]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.Molotov]._strResourceName = "Throwable Molotov";
        #endregion

        #region ## 파이프폭탄 ##
        m_tItemInfo[(int)ItemName.PipeBomb]._Prefab = Resources.Load("Throwable Pipebomb") as GameObject;
        m_tItemInfo[(int)ItemName.PipeBomb]._strName = "파이프 폭탄";
        m_tItemInfo[(int)ItemName.PipeBomb]._strKind = "투척무기";
        m_tItemInfo[(int)ItemName.PipeBomb]._strCharTitle = "공격력:";
        m_tItemInfo[(int)ItemName.PipeBomb]._strChar = "200";
        m_tItemInfo[(int)ItemName.PipeBomb]._strExplane = "모두모두 모여라! 하나 둘 셋! 펑!";
        m_tItemInfo[(int)ItemName.PipeBomb]._iPrice = 200;
        m_tItemInfo[(int)ItemName.PipeBomb]._WeapImage = Resources.Load<Sprite>("THROW/Pipe");
        m_tItemInfo[(int)ItemName.PipeBomb]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.PipeBomb]._tWeapType = WeapType.Throwable;
        m_tItemInfo[(int)ItemName.PipeBomb]._strResourceName = "Throwable Pipebomb";
        #endregion

        #region ## 응급치료 키트 ##
        m_tItemInfo[(int)ItemName.FirstAid]._Prefab = Resources.Load("Medicine FirstAid") as GameObject;
        m_tItemInfo[(int)ItemName.FirstAid]._strName = "응급치료 키트";
        m_tItemInfo[(int)ItemName.FirstAid]._strKind = "의료품";
        m_tItemInfo[(int)ItemName.FirstAid]._strCharTitle = "회복량:";
        m_tItemInfo[(int)ItemName.FirstAid]._strChar = "50";
        m_tItemInfo[(int)ItemName.FirstAid]._strExplane = "빨간약은 만능이지.";
        m_tItemInfo[(int)ItemName.FirstAid]._iPrice = 300;
        m_tItemInfo[(int)ItemName.FirstAid]._WeapImage = Resources.Load<Sprite>("MEDICINE/FirstAid");
        m_tItemInfo[(int)ItemName.FirstAid]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.FirstAid]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.FirstAid]._strResourceName = "Medicine FirstAid";
        #endregion

        #region ## 진통제 ##
        m_tItemInfo[(int)ItemName.PainPill]._Prefab = Resources.Load("Medicine PainPills") as GameObject;
        m_tItemInfo[(int)ItemName.PainPill]._strName = "진통제";
        m_tItemInfo[(int)ItemName.PainPill]._strKind = "의료품";
        m_tItemInfo[(int)ItemName.PainPill]._strCharTitle = "회복량:";
        m_tItemInfo[(int)ItemName.PainPill]._strChar = "임시체력 30";
        m_tItemInfo[(int)ItemName.PainPill]._strExplane = "한 통 다 먹어도 괜찮은 건가..?";
        m_tItemInfo[(int)ItemName.PainPill]._iPrice = 150;
        m_tItemInfo[(int)ItemName.PainPill]._WeapImage = Resources.Load<Sprite>("MEDICINE/PainPill");
        m_tItemInfo[(int)ItemName.PainPill]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.PainPill]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.PainPill]._strResourceName = "Medicine PainPills";
        #endregion

        #region ## 아드레날린 주사기 ##
        m_tItemInfo[(int)ItemName.Adrenalin]._Prefab = Resources.Load("Medicine AdrenalinShot") as GameObject;
        m_tItemInfo[(int)ItemName.Adrenalin]._strName = "아드레날린 주사기";
        m_tItemInfo[(int)ItemName.Adrenalin]._strKind = "의료품";
        m_tItemInfo[(int)ItemName.Adrenalin]._strCharTitle = "회복량:";
        m_tItemInfo[(int)ItemName.Adrenalin]._strChar = "임시체력 80";
        m_tItemInfo[(int)ItemName.Adrenalin]._strExplane = "스팀팩 상위호환.";
        m_tItemInfo[(int)ItemName.Adrenalin]._iPrice = 400;
        m_tItemInfo[(int)ItemName.Adrenalin]._WeapImage = Resources.Load<Sprite>("MEDICINE/Adrenalin");
        m_tItemInfo[(int)ItemName.Adrenalin]._AmmoImage = Resources.Load<Sprite>("AMMO/AMMO_AR");
        m_tItemInfo[(int)ItemName.Adrenalin]._tWeapType = WeapType.Medicine;
        m_tItemInfo[(int)ItemName.Adrenalin]._strResourceName = "Medicine AdrenalinShot";
        #endregion
    }
}
