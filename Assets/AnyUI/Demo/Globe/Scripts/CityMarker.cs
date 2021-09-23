
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AnyUI.Demo
{
    public class CityMarker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public enum CITYMARKERSTATE { INACTIVE,ACTIVE }
        public CITYMARKERSTATE CityMarkerState { get { return m_cityMarkerState; } set { m_cityMarkerState = value; ChangeSprite(value); } }
        //-----------------------------------------------------------------------------------------------------
        public string CityName;
        public Popup CityPopup;
        public Sprite ActiveImage;
        public Sprite InactiveImage;
<<<<<<< HEAD
        [SerializeField]
        private int codeIndex;
        [SerializeField]
        private int btnIndex;
=======
>>>>>>> b1c3fc40b605553191da667f6df4b0b5b682d756
        [HideInInspector]
        public RectTransform RectTransform;
        //-----------------------------------------------------------------------------------------------------
        private Button m_button;
        private Animator m_Animator;
       
        private CITYMARKERSTATE m_cityMarkerState;
        //-----------------------------------------------------------------------------------------------------
        private void Start()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(OnClick);
            m_Animator = GetComponent<Animator>();
            RectTransform = GetComponent<RectTransform>();
        }
        //-----------------------------------------------------------------------------------------------------
        public void OnClick()
        {
<<<<<<< HEAD
            CallJSEvent(codeIndex, btnIndex);
           // if (CityMarkerState == CITYMARKERSTATE.INACTIVE&& CityPopup.CanMove) CityPopup.RelocatePopup(this);      
        }

        private void CallJSEvent(int codeIndex, int btnIndex)
        {
            Debug.Log(string.Format("paint_Popup({0}, {1}) is called", codeIndex, btnIndex));

=======

            if (CityMarkerState == CITYMARKERSTATE.INACTIVE&& CityPopup.CanMove) CityPopup.RelocatePopup(this);      
>>>>>>> b1c3fc40b605553191da667f6df4b0b5b682d756
        }
        //-----------------------------------------------------------------------------------------------------
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_cityMarkerState == CITYMARKERSTATE.INACTIVE)
                m_Animator.SetTrigger("Highlighted");
        }
        //-----------------------------------------------------------------------------------------------------
        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_cityMarkerState == CITYMARKERSTATE.INACTIVE)
                m_Animator.SetTrigger("Normal");
        }
        //-----------------------------------------------------------------------------------------------------
        public void ChangeState()
        {
            CityMarkerState = (m_cityMarkerState == (CITYMARKERSTATE.ACTIVE) ? (CITYMARKERSTATE.INACTIVE) : (CITYMARKERSTATE.ACTIVE));
        }
        //-----------------------------------------------------------------------------------------------------
        private void ChangeSprite(CITYMARKERSTATE _state)
        {
          GetComponent<Image>().sprite = _state==CITYMARKERSTATE.ACTIVE ? ActiveImage : InactiveImage;
            m_Animator.SetTrigger(_state == CITYMARKERSTATE.ACTIVE ? "Active" : "Deactive");
        }
    }

}
