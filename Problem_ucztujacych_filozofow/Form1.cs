using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Problem_ucztujacych_filozofow
{

    public partial class Form1 : Form
    {
        static bool czykoniec = false;                  //zmienna informujaca o koncu posilku
        static Paleczka[] paleczki = new Paleczka[5];   //obiekty-paleczki w liczbie 5, czyli tyle ile filozofow

        public Form1()
        {
            InitializeComponent();


            for (int i = 0; i < 5; i++)         //stworzenie paleczek
            {
                paleczki[i] = new Paleczka(i);
            }

            Thread F1 = new Thread(() => Mysl(1, ref paleczki[0], ref paleczki[1], 0));  //tworzenie watkow, odpowiadajacych za cykl zycia kazdego z 5 filozofow
            F1.Start();

            Thread F2 = new Thread(() => Mysl(2, ref paleczki[1], ref paleczki[2], 0));
            F2.Start();

            Thread F3 = new Thread(() => Mysl(3, ref paleczki[2], ref paleczki[3], 0));
            F3.Start();

            Thread F4 = new Thread(() => Mysl(4, ref paleczki[3], ref paleczki[4], 0));
            F4.Start();

            Thread F5 = new Thread(() => Mysl(5, ref paleczki[4], ref paleczki[0], 0));
            F5.Start();

        }

        private void button1_Click(object sender, EventArgs e) //po kliknieciu przycisku "STOP"
        {
            button1.Enabled = false;   //zablokowanie przycisku
            czykoniec = true;  //zmiana zawartosci zmiennej, konczenie watkow w jak najkrotszym czasie
        }

        Random rand = new Random();

        private void Mysl(int ID, ref Paleczka Prawa, ref Paleczka Lewa, int ilerazyjadl)  //Watek filozfa
        {

            while (!czykoniec)  //jezeli nie kliknieto przycisku "STOP"
            {
                Console.WriteLine("Filozof numer" + ID + " mysli");
                AktualizacjaTekstu(ID, "Filozof numer" + ID + " mysli");  //wypisanie komunikatu w oknie programu i konsoli
                AktualizacjaLicznika(ID, ilerazyjadl.ToString());  //wypisanie stanu licznika zjedzonych ziarenek ryzu
                Thread.Sleep(rand.Next(200, 400));  //uspienie watku na mniej niz 0,5 sekundy

                if (ID <= 4)  //jezeli to filozof o ID od 1 do 4 to
                {
                    if (Prawareka(ref Prawa, ID))  //proba podniesienia prawej paleczki
                    {
                        Console.WriteLine("Podniesiono prawa paleczke");
                        AktualizacjaTekstu(ID, "Podniesiono prawa paleczke");  //podniesiono paleczke, wypisanie komunikatu

                        if (Lewareka(ref Lewa, ID)) //proba podniesienia lewej paleczki
                        {
                            Console.WriteLine("Filozof numer" + ID + " je");
                            AktualizacjaTekstu(ID, "Filozof numer" + ID + " je");  //spozycie ziarenka ryzu
                            ilerazyjadl++;
                            AktualizacjaLicznika(ID, ilerazyjadl.ToString()); //inkrementacja licznika i wypisanie aktualnego stanu
                            Thread.Sleep(rand.Next(200, 400)); //uspienie watku na mniej niz 0,5 sekundy
                            Lewa.Odloz();  //odlozenie najpierw lewej paleczki, potem lewej, aby uniknac zakleszczenia
                            Prawa.Odloz();
                            Console.WriteLine("Odłożono pałeczki");
                            AktualizacjaTekstu(ID, "Odłożono pałeczki");  //wypisanie stosownego komunikatu
                        }
                        else //jezeli nie podniesiono lewej paleczki
                        {
                            Thread.Sleep(rand.Next(200, 400)); //uspienie watku i proba druga
                            if (Lewareka(ref Lewa, ID))
                            {
                                Console.WriteLine("Filozof numer" + ID + " je");
                                AktualizacjaTekstu(ID, "Filozof numer" + ID + " je"); //spozycie ziarenka ryzu
                                ilerazyjadl++;
                                AktualizacjaLicznika(ID, ilerazyjadl.ToString()); //inkrementacja licznika i wypisanie aktualnego stanu
                                Thread.Sleep(rand.Next(200, 400)); //uspienie watku na mniej niz 0,5 sekundy
                                Lewa.Odloz(); //odlozenie najpierw lewej paleczki, potem prawej, aby uniknac zakleszczenia
                                Prawa.Odloz();
                                Console.WriteLine("Odłożono pałeczki");
                                AktualizacjaTekstu(ID, "Odłożono pałeczki"); //wypisanie stosownego komunikatu
                            }
                            else //jezeli nie podniesiono lewej paleczki w drugiej probie
                            {
                                Console.WriteLine("Odłożono prawa pałeczke");
                                AktualizacjaTekstu(ID, "Odłożono prawa pałeczke"); //wypisanie stosownego komunikatu
                                Prawa.Odloz(); //odlozenie prawej paleczki
                            }
                        }
                    }
                }
                else //jezeli to filozof o ID równym 5 to 
                {
                    if (Lewareka(ref Lewa, ID)) //proba podniesienia lewej paleczki
                    {
                        Console.WriteLine("Podniesiono lewa paleczke");
                        AktualizacjaTekstu(ID, "Podniesiono lewa paleczke"); //wypisanie stosownego komunikatu

                        if (Prawareka(ref Prawa, ID)) //proba podniesienia prawej paleczki
                        {
                            Console.WriteLine("Filozof numer" + ID + " je");
                            AktualizacjaTekstu(ID, "Filozof numer" + ID + " je"); //spozycie ziarenka ryzu
                            ilerazyjadl++;
                            AktualizacjaLicznika(ID, ilerazyjadl.ToString()); //inkrementacja licznika i wypisanie aktualnego stanu
                            Thread.Sleep(rand.Next(200, 400)); //uspienie watku na mniej niz 0,5 sekundy
                            Prawa.Odloz(); //odlozenie najpierw prawej paleczki, potem lewej, aby uniknac zakleszczenia
                            Lewa.Odloz();
                            Console.WriteLine("Odłożono pałeczki");
                            AktualizacjaTekstu(ID, "Odłożono pałeczki");  //wypisanie stosownego komunikatu
                        }
                        else
                        {
                            Thread.Sleep(rand.Next(200, 400)); //uspienie watku i druga proba podniesienia prawej paleczki
                            if (Prawareka(ref Prawa, ID))
                            {
                                Console.WriteLine("Filozof numer" + ID + " je");
                                AktualizacjaTekstu(ID, "Filozof numer" + ID + " je"); //spozycie ziarenka ryzu
                                ilerazyjadl++;
                                AktualizacjaLicznika(ID, ilerazyjadl.ToString()); //inkrementacja licznika i wypisanie aktualnego stanu
                                Thread.Sleep(rand.Next(200, 400)); //uspienie watku na mniej niz 0,5 sekundy
                                Prawa.Odloz(); //odlozenie najpierw prawej paleczki, potem lewej, aby uniknac zakleszczenia
                                Lewa.Odloz();
                                Console.WriteLine("Odłożono pałeczki");
                                AktualizacjaTekstu(ID, "Odłożono pałeczki");  //wypisanie stosownego komunikatu
                            }
                            else //nie podniesieno prawej paleczki w drugiej probie
                            {
                                Console.WriteLine("Odłożono prawa pałeczke");
                                AktualizacjaTekstu(ID, "Odłożono prawa pałeczke"); //stosowny komunikat
                                Lewa.Odloz(); //odlozenie lewej paleczki
                            }
                        }
                    }
                }
                
            }

            AktualizacjaTekstu(ID, " Odszedl od stolu"); //jezeli kliknieto "STOP", a wątek zakończył pracę to nastepuje wypisanie stosownego komunikatu

        }

        private bool Prawareka(ref Paleczka Prawa, int ID) //funkcja podniesienia prawej paleczki
        {
            return Prawa.Podnies(ID);
        }

        private bool Lewareka(ref Paleczka Lewa, int ID)  //funkcja podniesienia lewej paleczki
        {
            return Lewa.Podnies(ID);
        }
    
        private void AktualizacjaTekstu(int ID, String tekst)  //funkcja odpowiadajaca za wypisanie komunikatu do Text Boxa w zaleznosci od ID filozofa
        {
            if (ID==1)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int,string>(AktualizacjaTekstu), new object[] {ID,tekst});
                    return;
                }
                Ph1TextState.Text = tekst;
            }
            else if (ID==2)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaTekstu), new object[] { ID, tekst });
                    return;
                }
                Ph2TextState.Text = tekst;
            }
            else if (ID == 3)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaTekstu), new object[] { ID, tekst });
                    return;
                }
                Ph3TextState.Text = tekst;
            }
            else if (ID == 4)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaTekstu), new object[] { ID, tekst });
                    return;
                }
                Ph4TextState.Text = tekst;
            }
            else if (ID == 5)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaTekstu), new object[] { ID, tekst });
                    return;
                }
                Ph5TextState.Text = tekst;
            }
        }

        private void AktualizacjaLicznika(int ID, string tekst) //funkcja odpowiadajaca za wypisanie komunikatu do Text Boxa w zaleznosci od ID filozofa
            {
            if (ID == 1)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaLicznika), new object[] { ID, tekst });
                    return;
                }
                licznik1.Text = tekst;
            }
            else if (ID == 2)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaLicznika), new object[] { ID, tekst });
                    return;
                }
                licznik2.Text = tekst;
            }
            else if (ID == 3)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaLicznika), new object[] { ID, tekst });
                    return;
                }
                licznik3.Text = tekst;
            }
            else if (ID == 4)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaLicznika), new object[] { ID, tekst });
                    return;
                }
                licznik4.Text = tekst;
            }
            else if (ID == 5)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<int, string>(AktualizacjaLicznika), new object[] { ID, tekst });
                    return;
                }
                licznik5.Text = tekst;
            }
        }
    }
}
