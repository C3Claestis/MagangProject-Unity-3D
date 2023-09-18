namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    public class CardSlot : MonoBehaviour, IPointerClickHandler
    {        
        private bool IsActive = false; //Cek kondisi ketika ada kartu yang di interect
        private bool IsIn = false; //Cek kondisi ketika ada isi kartunya
        private bool Iswait = false; //Delay animasi dari kartu
        private int Numbers;
        [SerializeField] GameObject[] cards;

        public void SetNumbers(int no) => this.Numbers = no;
        public void SetActive(bool aktif) => this.IsActive = aktif;     
        // Update is called once per frame
        void Update()
        {
            if (IsIn)
            {
                if (!Iswait)
                {
                    CekIsi();
                }                
            }
            
            RemoveFirstChildIfMultiple();
        }
        public void OnPointerClick(PointerEventData eventData)
        {           
            if (IsActive)
            {
                CardSlot[] cardSlot = FindObjectsOfType<CardSlot>();
                Card[] card = FindObjectsOfType<Card>();
                switch (Numbers) //Untuk menambahkan kartu didalam
                {
                    case 1:
                        GameObject newCard = Instantiate(cards[0], transform);                                                
                        foreach (CardSlot slot in cardSlot)
                        {
                            slot.SetNumbers(0);
                            slot.SetActive(false);
                        }                        
                        foreach (Card cards in card)
                        {
                            cards.GetComponent<Image>().raycastTarget = true;
                        }
                        IsActive = false;
                        IsIn = true;
                        break;
                    case 2:
                        GameObject newCard1 = Instantiate(cards[1], transform);
                        foreach (CardSlot slot in cardSlot)
                        {
                            slot.SetNumbers(0);
                            slot.SetActive(false);
                        }
                        foreach (Card cards in card)
                        {
                            cards.GetComponent<Image>().raycastTarget = true;
                        }
                        IsActive = false;
                        IsIn = true;
                        break;
                    case 3:
                        GameObject newCard2 = Instantiate(cards[2], transform);
                        foreach (CardSlot slot in cardSlot)
                        {
                            slot.SetNumbers(0);
                            slot.SetActive(false);
                        }
                        foreach (Card cards in card)
                        {
                            cards.GetComponent<Image>().raycastTarget = true;
                        }
                        IsActive = false;
                        IsIn = true;
                        break;
                    case 4:
                        GameObject newCard3 = Instantiate(cards[3], transform);
                        foreach (CardSlot slot in cardSlot)
                        {
                            slot.SetNumbers(0);
                            slot.SetActive(false);
                        }
                        foreach (Card cards in card)
                        {
                            cards.GetComponent<Image>().raycastTarget = true;
                        }
                        IsActive = false;
                        IsIn = true;
                        break;
                }
            }
            else
            {
                if (IsIn) //Untuk menghapus kartu
                {
                    Iswait = true;
                    Kembalikan();
                    IsIn = false;
                    Iswait = false;
                }
            }            
        }
        private void Kembalikan() //Fungsi hapus kartu
        {
            Transform parentTransform = transform;            
            string clone = "(Clone)";

            if (parentTransform.GetChild(0).name == "Sacra" + clone)
            {
                Card card = GameObject.Find("Canvas/Panel-Party/Sacra").GetComponent<Card>();
                card.SetIsClear(false);
                card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                card.GetComponent<Image>().raycastTarget = true;
                card.GetComponent<CanvasGroup>().alpha = 1;
                Destroy(parentTransform.GetChild(0).gameObject);
            }
            else if (parentTransform.GetChild(0).name == "Vana" + clone)
            {
                Card card = GameObject.Find("Canvas/Panel-Party/Vana").GetComponent<Card>();
                card.SetIsClear(false);
                card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                card.GetComponent<Image>().raycastTarget = true;
                card.GetComponent<CanvasGroup>().alpha = 1;
                Destroy(parentTransform.GetChild(0).gameObject);
            }
            else if (parentTransform.GetChild(0).name == "Lin" + clone)
            {
                Card card = GameObject.Find("Canvas/Panel-Party/Lin").GetComponent<Card>();
                card.SetIsClear(false);
                card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                card.GetComponent<Image>().raycastTarget = true;
                card.GetComponent<CanvasGroup>().alpha = 1;
                Destroy(parentTransform.GetChild(0).gameObject);
            }
            else if (parentTransform.GetChild(0).name == "Guard" + clone)
            {
                Card card = GameObject.Find("Canvas/Panel-Party/Guard").GetComponent<Card>();
                card.SetIsClear(false);
                card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                card.GetComponent<Image>().raycastTarget = true;
                card.GetComponent<CanvasGroup>().alpha = 1;
                Destroy(parentTransform.GetChild(0).gameObject);
            }
          
        }
        private void CekIsi()  //Fungsi cek isi kartu
        {
            Transform parentTransform = transform;
            int childCount = parentTransform.childCount;
            string clone = "(Clone)";

            if(childCount > 0)
            {
                if(parentTransform.GetChild(0).name == "Sacra"+clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Sacra").GetComponent<Card>();
                    card.SetIsHighlight(false);
                    card.SetIsClear(true);                    
                }
                else if (parentTransform.GetChild(0).name == "Vana"+clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Vana").GetComponent<Card>();
                    card.SetIsHighlight(false);
                    card.SetIsClear(true);                    
                }
                else if (parentTransform.GetChild(0).name == "Lin"+clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Lin").GetComponent<Card>();
                    card.SetIsHighlight(false);
                    card.SetIsClear(true);                    
                }
                else if (parentTransform.GetChild(0).name == "Guard"+clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Guard").GetComponent<Card>();
                    card.SetIsHighlight(false);
                    card.SetIsClear(true);                    
                }
            }
        }
        private void RemoveFirstChildIfMultiple() //Fungsi hapus otomatis ketika ada lebih dari 1 kartu
        {
            Transform parentTransform = transform;
            int childCount = parentTransform.childCount;
            string clone = "(Clone)";

            if (childCount > 1)
            {
                if (parentTransform.GetChild(0).name == "Sacra" + clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Sacra").GetComponent<Card>();
                    card.SetIsClear(false);
                    card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                    card.GetComponent<Image>().raycastTarget = true;
                    card.GetComponent<CanvasGroup>().alpha = 1;
                    Destroy(parentTransform.GetChild(0).gameObject);
                }
                if (parentTransform.GetChild(0).name == "Vana" + clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Vana").GetComponent<Card>();
                    card.SetIsClear(false);
                    card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                    card.GetComponent<Image>().raycastTarget = true;
                    card.GetComponent<CanvasGroup>().alpha = 1;
                    Destroy(parentTransform.GetChild(0).gameObject);
                }
                if (parentTransform.GetChild(0).name == "Lin" + clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Lin").GetComponent<Card>();
                    card.SetIsClear(false);
                    card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                    card.GetComponent<Image>().raycastTarget = true;
                    card.GetComponent<CanvasGroup>().alpha = 1;
                    Destroy(parentTransform.GetChild(0).gameObject);
                }
                if (parentTransform.GetChild(0).name == "Guard" + clone)
                {
                    Card card = GameObject.Find("Canvas/Panel-Party/Guard").GetComponent<Card>();
                    card.SetIsClear(false);
                    card.GetComponent<RectTransform>().localScale = new Vector2(0.25f, 0.25f);
                    card.GetComponent<Image>().raycastTarget = true;
                    card.GetComponent<CanvasGroup>().alpha = 1;
                    Destroy(parentTransform.GetChild(0).gameObject);
                }
            }                             
        }        
    }
}