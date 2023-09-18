namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] int Numbers;
        private RectTransform rectTransform;
        private bool IsHighlight = false;
        private bool IsClear = false;
        private CanvasGroup canvasGroup;
        private Image image;
        private static Card selectedCard;
        public void SetIsHighlight(bool aktif) => this.IsHighlight = aktif;
        public void SetIsClear(bool clear) => this.IsClear = clear;        
        public bool GetIsHighlight() => IsHighlight;
        // Start is called before the first frame update
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsClear)
            {                
                if (IsHighlight)
                {
                    rectTransform.localScale = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 150);
                }
                else
                {
                    rectTransform.localScale = new Vector2(0.25f, 0.25f);
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 100);
                }
            }
            else
            {
                rectTransform.localScale = new Vector2(0.25f, 0.25f);
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 100);
                image.raycastTarget = false;
                SetCanvasGroupAlpha(.2f);
            }            
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            IsHighlight = !IsHighlight ? true : false;            

            // Mengatur alpha pada objek yang sedang ditekan
            if (IsHighlight)
            {
                SetCanvasGroupAlpha(1f);
                selectedCard = this;
                // Mengecek semua objek Card dalam scene
                Card[] cards = FindObjectsOfType<Card>();
                foreach (Card card in cards)
                {
                    // Mengatur alpha hanya jika bukan objek yang sedang ditekan
                    if (card != selectedCard)
                    {
                        card.SetCanvasGroupAlpha(0.5f);
                        card.GetComponent<Image>().raycastTarget = false;
                    }
                }

                CardSlot[] cardSlot = FindObjectsOfType<CardSlot>();
                foreach (CardSlot slot in cardSlot)
                {
                    slot.SetNumbers(Numbers);
                    slot.SetActive(true);
                }
            }
            else
            {
                selectedCard = null;
                // Mengecek semua objek Card dalam scene
                Card[] cards = FindObjectsOfType<Card>();
                foreach (Card card in cards)
                {
                    // Mengatur alpha hanya jika bukan objek yang sedang ditekan
                    if (card != selectedCard)
                    {
                        card.SetCanvasGroupAlpha(1f);
                        card.GetComponent<Image>().raycastTarget = true;
                    }
                }

                CardSlot[] cardSlot = FindObjectsOfType<CardSlot>();
                foreach (CardSlot slot in cardSlot)
                {
                    slot.SetNumbers(0);
                    slot.SetActive(false);
                }
            }
        }
        private void SetCanvasGroupAlpha(float alpha)
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = alpha;
            }
        }
    }
}