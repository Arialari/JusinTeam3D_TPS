using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Dictionary_Script : MonoBehaviour
{
    public GameObject m_Panel_Item;
    public GameObject m_Panel_Enemy;

    public Button m_Btn_ItemInfo;
    public Button m_Btn_EnemyInfo;
    public GameObject m_InfoviewSpace;
    public Button m_Btn_Runner;
    public Button m_Btn_Breacher;
    public Button m_Btn_Rooter;

    public Text m_Txt_Runner;
    public Text m_Txt_Breacher;
    public Text m_Txt_Rooter;

    private Image m_Img_InfoView;

    public Animator m_Anim_Btn_ItemInfo { get; set; }
    public Animator m_Anim_Btn_EnemyInfo { get; set; }
    public Animator m_Anim_Btn_Runner { get; set; }
    public Animator m_Anim_Btn_Breacher { get; set; }
    public Animator m_Anim_Btn_Rooter { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        m_Anim_Btn_ItemInfo = m_Btn_ItemInfo.GetComponent<Animator>();
        m_Anim_Btn_EnemyInfo = m_Btn_EnemyInfo.GetComponent<Animator>();
        m_Anim_Btn_Runner = m_Btn_Runner.GetComponent<Animator>();
        m_Anim_Btn_Breacher = m_Btn_Breacher.GetComponent<Animator>();
        m_Anim_Btn_Rooter = m_Btn_Rooter.GetComponent<Animator>();

        m_Img_InfoView = m_InfoviewSpace.transform.GetTransformInAllChildren("Monster_Pic").GetComponent<Image>();

        m_Img_InfoView.color = new Color(1, 1, 1, 0);
        Txt_ZombieInfo_Active();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void This_Panel_Active(Animator _Anim)
    {
        if (_Anim == m_Anim_Btn_EnemyInfo)
            m_Anim_Btn_EnemyInfo.SetBool("bSelected", true);

        if (_Anim != m_Anim_Btn_Runner)
            m_Anim_Btn_Runner.SetBool("bSelected", false);
        else
            m_Anim_Btn_Runner.SetBool("bSelected", true);

        if (_Anim != m_Anim_Btn_Breacher)
            m_Anim_Btn_Breacher.SetBool("bSelected", false);
        else
            m_Anim_Btn_Breacher.SetBool("bSelected", true);

        if (_Anim != m_Anim_Btn_Rooter)
            m_Anim_Btn_Rooter.SetBool("bSelected", false);
        else
            m_Anim_Btn_Rooter.SetBool("bSelected", true);
    }

    private void Txt_ZombieInfo_Active()
    {
        if (!m_Anim_Btn_Runner.GetBool("bSelected"))
            m_Txt_Runner.enabled = false;
        else
            m_Txt_Runner.enabled = true;

        if (!m_Anim_Btn_Breacher.GetBool("bSelected"))
            m_Txt_Breacher.enabled = false;
        else
            m_Txt_Breacher.enabled = true;

        if (!m_Anim_Btn_Rooter.GetBool("bSelected"))
            m_Txt_Rooter.enabled = false;
        else
            m_Txt_Rooter.enabled = true;
    }

    public void ClickTo_BtnItemInfo()
    {
        This_Panel_Active(m_Anim_Btn_ItemInfo);
    }

    public void ClickTo_BtnEnemyInfo()
    {
        This_Panel_Active(m_Anim_Btn_EnemyInfo);
    }

    public void ClickTo_Btn_Runner()
    {
        This_Panel_Active(m_Anim_Btn_Runner);

        if (m_Img_InfoView.color != new Color(1, 1, 1, 1))
            m_Img_InfoView.color = new Color(1, 1, 1, 1);
        m_Img_InfoView.sprite = Resources.Load<Sprite>("ZOMBIE/Zombie01");
        Txt_ZombieInfo_Active();
    }

    public void ClickTo_Btn_Breacher()
    {
        This_Panel_Active(m_Anim_Btn_Breacher);

        if (m_Img_InfoView.color != new Color(1, 1, 1, 1))
            m_Img_InfoView.color = new Color(1, 1, 1, 1);
        m_Img_InfoView.sprite = Resources.Load<Sprite>("ZOMBIE/Zombie03");
        Txt_ZombieInfo_Active();
    }

    public void ClickTo_Btn_Rooter()
    {
        This_Panel_Active(m_Anim_Btn_Rooter);

        if (m_Img_InfoView.color != new Color(1, 1, 1, 1))
            m_Img_InfoView.color = new Color(1, 1, 1, 1);
        m_Img_InfoView.sprite = Resources.Load<Sprite>("ZOMBIE/Zombie02");
        Txt_ZombieInfo_Active();
    }

    private void OnDisable()
    {
        if (m_Img_InfoView.color == new Color(1, 1, 1, 1))
            m_Img_InfoView.color = new Color(1, 1, 1, 0);

        m_Txt_Runner.enabled = false;
        m_Txt_Breacher.enabled = false;
        m_Txt_Rooter.enabled = false;
    }
}
