using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SatrançProje
{
    public partial class Form1 : Form
    {

        List<PictureBox> pictureBoxList = new List<PictureBox>();
        List<int[]> TableStates = new List<int[]>();
        private static Random random = new Random();
        int sayac = 1;
        int[] test_list = new int[] {0, 1, 2, 3, 4, 5, 6, 7};
        Stopwatch watch = new Stopwatch();


        //Paremetre olarak görderilen satranç tahtasının durumunu formda gösterir.





        public void Cakisma_sayisi_hesapla_ve_yaz(int[] State)
        {
            int cakisma_sayisi = 0;
            for(int i = 0; i < 8; i++)
            {
                for(int j = i+1; j < 8; j++)
                {
                    if(State[i] == State[j])
                    {
                        cakisma_sayisi++;
                    }
                    if(State[i] == State[j]+ Math.Abs(i-j))
                    {
                        cakisma_sayisi++;
                    }
                    if (State[i] == State[j]- Math.Abs(i-j))
                    {
                        cakisma_sayisi++;
                    }
                }
            }
            DataGridViewCell cell = (DataGridViewCell)dataGridView1.Rows[TableStates.IndexOf(State)].Cells[1];
            cell.Value = cakisma_sayisi.ToString();
            sayac++;
        }

        public int Cakisma_sayisi_hesapla(int[] State)
        {
            int cakisma_sayisi = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = i + 1; j < 8; j++)
                {
                    if (State[i] == State[j])
                    {
                        cakisma_sayisi++;
                    }
                    if (State[i] == State[j] + Math.Abs(i - j))
                    {
                        cakisma_sayisi++;
                    }
                    if (State[i] == State[j] - Math.Abs(i - j))
                    {
                        cakisma_sayisi++;
                    }
                }
            }
            return cakisma_sayisi;
        }


        public void konumu_coz(int[] Konum)
        {
            watch.Start();
            int[] Thread_count_list = new int[8];
            int cakisma_sayisi = 0;
            int index = 0;
            int baslangic_cakisma_sayisi = 99;
            int sonuc_cakisma_sayisi = -2;
            int deneme = 0;
            int reset_sayisi=0;
            int yer_degistirme_sayisi = 0;

            while (sonuc_cakisma_sayisi != 0)
            {
                    baslangic_cakisma_sayisi = Cakisma_sayisi_hesapla(Konum);
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                if (i == k)
                                {
                                    continue;
                                }
                                if (test_list[j] == Konum[k])
                                {
                                    cakisma_sayisi++;
                                }
                                if (test_list[j] == Konum[k] + Math.Abs(k+i))
                                {
                                    cakisma_sayisi++;
                                }
                                if (test_list[j] == Konum[k] - Math.Abs(k - i))
                                {
                                    cakisma_sayisi++;
                                }

                            }
                            Thread_count_list[j] = cakisma_sayisi;
                            cakisma_sayisi = 0;
                        }
                        int[] v = Thread_count_list.Select((b, k) => b == Thread_count_list.Min() ? k : -1).Where(k => k != -1).ToArray();
                        index = v[(random.Next(0, v.Length))];
                    if (index != Konum[i])
                    {
                        yer_degistirme_sayisi++;
                        Konum[i] = index;
                    }
                        
                    }
                    sonuc_cakisma_sayisi = Cakisma_sayisi_hesapla(Konum);
                    if(sonuc_cakisma_sayisi == baslangic_cakisma_sayisi)
                    {
                        deneme++;
                    }
                    if (sonuc_cakisma_sayisi != 0 & deneme==3)
                    {
                        random_restart(Konum);
                        baslangic_cakisma_sayisi = 99;
                        sonuc_cakisma_sayisi = -2;
                        deneme = 0;
                        reset_sayisi++;
                    }
                }
                watch.Stop();
                DataGridViewCell Reset_Cell = (DataGridViewCell)dataGridView1.Rows[TableStates.IndexOf(Konum)].Cells[2];
                DataGridViewCell Displacament_Cell = (DataGridViewCell)dataGridView1.Rows[TableStates.IndexOf(Konum)].Cells[3];
                DataGridViewCell Run_Time = (DataGridViewCell)dataGridView1.Rows[TableStates.IndexOf(Konum)].Cells[4];
                Reset_Cell.Value = reset_sayisi.ToString();
                Displacament_Cell.Value = yer_degistirme_sayisi.ToString();
                Run_Time.Value = (1000*(double)watch.ElapsedTicks/Stopwatch.Frequency).ToString("F2");
                watch.Reset();
            
        }



        //Verilen state i restart eder.
        public void random_restart(int[] State)
        {
            int index = TableStates.IndexOf(State);
            for (int a = 0; a < 8; a++)
            {
                State[a] = random.Next(0, 8);
            }
            DataGridViewCell Reset_Cell = (DataGridViewCell)dataGridView1.Rows[index].Cells[2];
            DataGridViewCell Displacament_Cell = (DataGridViewCell)dataGridView1.Rows[index].Cells[3];
            DataGridViewCell Run_Time = (DataGridViewCell)dataGridView1.Rows[index].Cells[4];
            Reset_Cell.Value = "";
            Displacament_Cell.Value = "";
            Run_Time.Value = "";

        }



        public Form1()
        {

            InitializeComponent();

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //Vezirler listeye picture box olarak ekleniyor
            pictureBoxList.Add(Vezir1);
            pictureBoxList.Add(Vezir2);
            pictureBoxList.Add(Vezir3);
            pictureBoxList.Add(Vezir4);
            pictureBoxList.Add(Vezir5);
            pictureBoxList.Add(Vezir6);
            pictureBoxList.Add(Vezir7);
            pictureBoxList.Add(Vezir8);
            //
            //20 tablo oluşturulup listeye eklenir.
            for(int i = 0; i < 20; i++)
            {
                TableStates.Add(new int[8]);
            }

            //Datagridview e 20 satır eklenir.
            for (int m = 0; m < 20; m++)
            {
                dataGridView1.Rows.Add("Table " + (m + 1).ToString(), "");
            }

            //20 tablonun vezir konumları oluşturulur.
            for (int b = 0; b < 20; b++)
            {
                random_restart(TableStates[b]);
            }

            
            


            //Ilk durumların cakisma sayilari hesaplanır.
            for (int n = 0; n < 20; n++)
            {
                Cakisma_sayisi_hesapla_ve_yaz(TableStates[n]);

            }

            //Table 1 gösterilir.
            Show_Table(TableStates[0]);
            textBox1.Text = "1";


        }
        //Belirli bir tabloyu gösteren Fonksiyon.
        public void Show_Table(int[] State)
        {
            int tno = 0;
            for (int b = 0; b < 8; b++)
            {
               tableLayoutPanel1.SetCellPosition(pictureBoxList[b], new TableLayoutPanelCellPosition(State[b], b));
            }
            tno = TableStates.IndexOf(State);
            label2.Text = "Table no : " + (tno + 1).ToString();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Bütün konumları restart eden buton.
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 20; i++)
            {
                random_restart(TableStates[i]);
            }
            Show_Table(TableStates[0]);
            for (int d = 0; d < 20; d++)
            {
                Cakisma_sayisi_hesapla_ve_yaz(TableStates[d]);
            }
            sayac = 0;
            textBox1.Text = "";
            button2.Enabled = true;
            label3.Visible = false;
        }

        //Bütün konumları çözen buton.
        private void button2_Click(object sender, EventArgs e)
        {
            for(int m = 0; m < 20; m++)
            {
                konumu_coz(TableStates[m]);
            }
            for(int n = 0; n < 20; n++)
            {
                Cakisma_sayisi_hesapla_ve_yaz(TableStates[n]);               
            }
            sayac = 0;
            Show_Table(TableStates[0]);
            button2.Enabled = false;
            label3.Visible = true;
        }

////////////////////////////////Tek bir tabloyla ilgilenen butonlar////////////////////////////////////////
        //Belirli bir tabloyu çözmeye yarayan buton.
        private void button4_Click(object sender, EventArgs e)
        {
            konumu_coz(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
            Cakisma_sayisi_hesapla_ve_yaz(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
            Show_Table(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
            
        }
        //Belirli Bir tabloyu restart eden buton.
        private void button5_Click(object sender, EventArgs e)
        {
            random_restart(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
            Cakisma_sayisi_hesapla_ve_yaz(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
            Show_Table(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
        }
        //Belirli Bir tabloyu gösteren buton.
        private void button3_Click(object sender, EventArgs e)
        {
            Show_Table(TableStates[Convert.ToInt32(textBox1.Text) - 1]);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
