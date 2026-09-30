using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdamAsmaca
{
    class Program
    {
        static void Main(string[] args)
        {
            string dosyaYolu = "kelime.txt";
            string[] kelimeler;

            if (File.Exists(dosyaYolu))
            {
                kelimeler = File.ReadAllLines(dosyaYolu);
                if (kelimeler.Length == 0)
                {
                    Console.WriteLine("Hata: kelime.txt dosyasının içi boş!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Hata: kelime.txt dosyası bulunamadı!");
                Console.WriteLine("Lütfen programın çalıştığı klasörde dosyayı oluşturun.");
                return;
            }

            Random rnd = new Random();
            string secilenKelime = kelimeler[rnd.Next(0, kelimeler.Length)].Trim().ToLower();

            char[] gosterilenKelime = new char[secilenKelime.Length];
            for (int i = 0; i < gosterilenKelime.Length; i++) gosterilenKelime[i] = '_';

            List<char> hataliHarfler = new List<char>();
            int hak = 6;

            while (hak > 0 && new string(gosterilenKelime) != secilenKelime)
            {
                Console.Clear();
                agac(hak);
                Console.WriteLine("\nKelime: " + string.Join(" ", gosterilenKelime));
                Console.WriteLine("Hatalı Harfler: " + string.Join(", ", hataliHarfler));
                Console.WriteLine($"Kalan Hak: {hak}");
                Console.Write("Bir harf tahmin edin: ");

                string input = Console.ReadLine()?.ToLower();
                if (string.IsNullOrEmpty(input)) continue;

                char tahmin = input[0];

                if (secilenKelime.Contains(tahmin))
                {
                    for (int i = 0; i < secilenKelime.Length; i++)
                    {
                        if (secilenKelime[i] == tahmin)
                            gosterilenKelime[i] = tahmin;
                    }
                }
                else if (!hataliHarfler.Contains(tahmin))
                {
                    hataliHarfler.Add(tahmin);
                    hak--;
                }
            }

            Console.Clear();
            if (new string(gosterilenKelime) == secilenKelime)
            {
                Console.Clear();
                Console.WriteLine("Tebrikler! Kelimeyi bildiniz: " + secilenKelime);

                Console.Write("Adınızı Girin: ");
                string ad = Console.ReadLine();

                int puan = hak * 100;

                DosyayiDoldur(puan, ad);

                Console.WriteLine($"\nSayın {ad}, {DateTime.Now} tarihinde kelimeyi bildiniz!");
                Console.WriteLine("Skorunuz 'puan.txt' dosyasına kaydedildi.");
            }
            else
            {
                agac(0);
                Console.WriteLine("Maalesef kaybettiniz. Kelime şuydu: " + secilenKelime);
            }

            Console.ReadLine();
        }

        static void agac(int hak)
        {
            Console.WriteLine("  +---+");
            Console.WriteLine("  |   |");
            Console.WriteLine("  " + (hak < 6 ? "O" : " ") + "   |");
            Console.WriteLine(" " + (hak < 4 ? "/" : " ") + (hak < 5 ? "|" : " ") + (hak < 3 ? "\\" : "") + "  |");
            Console.WriteLine(" " + (hak < 2 ? "/" : " ") + " " + (hak < 1 ? "\\" : "") + "  |");
            Console.WriteLine("      |");
            Console.WriteLine("=========");
        }

        static void DosyayiDoldur(int skor, string oyuncuAdi)
        {
            string dosyaYolu = "puan.txt";
            string icerik = $"Oyuncu: {oyuncuAdi}\nSkor: {skor}\nTarih: {DateTime.Now}";

            File.WriteAllText(dosyaYolu, icerik);
        }
    }
}