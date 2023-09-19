namespace Nivandria.Explore.Puzzle
{    
    using UnityEngine;
    using UnityEngine.UI;
    public class ParameterCheck : MonoBehaviour
    {        
        [SerializeField] Text finish;
        [SerializeField] TeksBlock[] teksBlock;
        [SerializeField] Block[] blok;

        [Range(3f, 12f)]
        [SerializeField] int Jangkauan;
        private bool IsEnter = false;
        void Update()
        {
            ScanAwal();                          
        }

        void ScanAwal()
        {
            bool semuaTerisi = true;

            for (int i = 0; i < teksBlock.Length; i++)
            {
                if (!teksBlock[i].GetIsIsi())
                {
                    semuaTerisi = false;
                    break;
                }
            }

            if (semuaTerisi)
            {             
                Debug.Log("SUDAH TERISI SEMUA");
                IsEnter = true;
            }

            if (IsEnter)
            {
                if(Input.GetKey(KeyCode.Space))
                    Kondisi();
            }
        }

        void Kondisi()
        {            
            switch (Jangkauan)
            {              
                case 3:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 4:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 5:
                    if(teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }                    
                    break;
                case 6:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 7:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 8:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect() && teksBlock[7].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 9:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect() && teksBlock[7].GetIsCorrect() &&
                        teksBlock[8].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 10:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect() && teksBlock[7].GetIsCorrect() &&
                        teksBlock[8].GetIsCorrect() && teksBlock[9].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 11:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect() && teksBlock[7].GetIsCorrect() &&
                        teksBlock[8].GetIsCorrect() && teksBlock[9].GetIsCorrect() &&
                        teksBlock[10].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
                case 12:
                    if (teksBlock[0].GetIsCorrect() && teksBlock[1].GetIsCorrect() &&
                        teksBlock[2].GetIsCorrect() && teksBlock[3].GetIsCorrect() &&
                        teksBlock[4].GetIsCorrect() && teksBlock[5].GetIsCorrect() &&
                        teksBlock[6].GetIsCorrect() && teksBlock[7].GetIsCorrect() &&
                        teksBlock[8].GetIsCorrect() && teksBlock[9].GetIsCorrect() &&
                        teksBlock[10].GetIsCorrect() && teksBlock[11].GetIsCorrect())
                    {
                        finish.text = "Semua benar!";
                        Debug.Log("SELESAI");
                    }
                    else
                    {
                        for (int j = 0; j < blok.Length; j++)
                        {
                            blok[j].SetIsReset(true);
                        }
                    }
                    break;
            }
        }
    }
}