using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
           // OpenFileDialog file = new OpenFileDialog();
          //  file.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader oku = new StreamReader(openFileDialog1.FileName);
                string satir = oku.ReadLine();
                while (satir != null)
                {
                    listBox1.Items.Add(satir.ToUpper());
                    satir = oku.ReadLine();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string[] sembolik_adres = new string[listBox1.Items.Count];
            int LC=0,LC_2=0;
            int q = 0, z = 0;
            string[] secili = new string[listBox1.Items.Count];
            for(int i = 0; i < listBox1.Items.Count;i++)
            {
                secili[i]= listBox1.Items[i].ToString().Trim();
               // listBox2.Items.Add(secili[i]);
            }
            
            for(int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i == 0)
                {
                    if (secili[i].StartsWith("ORG") == true)
                    {
                        string a = secili[0].Substring(4, 3);
                        LC = Convert.ToInt16(a, 16);
                    }
                    else if (secili[i].StartsWith("ORG") == false)
                    {
                        LC = 0;
                    }
                }
                else if (i > 0)
                {
                   int length2 = secili[i].IndexOf(',');
                    if (secili[i].IndexOf(',') != -1)
                    {
                        string myHex = LC.ToString("X");
                        sembolik_adres[q] = secili[i].Substring(0, length2) + myHex;
                        LC++;
                        q++;
                    }
                    else if (secili[i].IndexOf(',') == -1)
                    {
                        LC++;
                    }
                }
            }
            for(int i = 0; i < listBox1.Items.Count; i++)
            {
                if (secili[i].IndexOf(',') != -1)
                {
                    int length3 = secili[i].IndexOf(',');
                    secili[i] = secili[i].Remove(0, length3+1).TrimStart();
                }
            }
            listBox2.Items.Add("Onaltılık Kod");
            listBox2.Items.Add("Adres  İçerik        Sembolik Program");
            listBox2.Items.Add("                "+listBox1.Items[0]);
            for(int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i == 0)
                {
                    //int length = secili[i].Length - 4;
                    if (secili[i].StartsWith("ORG") == true)
                    {
                         string a = secili[0].Substring(4, 3);
                         LC = Convert.ToInt32(a, 16);
                         LC_2 = Convert.ToInt32(a, 16);
                        //listBox2.Items.Add(LC);

                    }
                    else if (secili[i].StartsWith("ORG") == false)
                    {
                        LC = 0;
                        LC_2 = 0;
                        //listBox2.Items.Add(LC);
                    }
                }
                else if (i > 0)
                {
                    int length = secili[i].Length - 4;
                    if (secili[i].StartsWith("AND"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    8" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                        }
                        else if (secili[i].EndsWith("AND") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    0" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    0" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                        }
                    }
                    else if (secili[i].StartsWith("ADD"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    9" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("ADD") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    1" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, length);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    1" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("LDA"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    A" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("LDA") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    2" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, length);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    2" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("STA"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    B" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("STA") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    3" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, length);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    3" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("BUN"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    C" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("BUN") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    4" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    4" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("BSA"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    D" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("BSA") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    5" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    5" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("ISZ"))
                    {
                        if (secili[i].EndsWith("I") == true)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length-2), secili[i].Substring(4, length-2)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    E" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                        else if (secili[i].EndsWith("ISZ") == true)
                        {
                            string içerik = sembolik_adres[z].Substring(3, 3);
                            LC = Convert.ToInt32(içerik, 16);
                            string myHex = LC_2.ToString("X") + "    6" + LC_2.ToString("X") + "   " + listBox1.Items[i];
                            listBox2.Items.Add(myHex);
                            break;
                        }
                        else if (secili[i].EndsWith("I") == false)
                        {
                            for (z = 0; z < sembolik_adres.Length; z++)
                            {
                                if (Equals(sembolik_adres[z].Substring(0, length), secili[i].Substring(4, length)) == true)
                                {
                                    string içerik = sembolik_adres[z].Substring(3, 3);
                                    LC = Convert.ToInt32(içerik, 16);
                                    string myHex = LC_2.ToString("X") + "    6" + LC.ToString("X") + "   " + listBox1.Items[i];
                                    listBox2.Items.Add(myHex);
                                    break;
                                }
                            }
                            LC++;
                            LC_2++;
                        }
                    }
                    else if (secili[i].StartsWith("CLA"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7800" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("CLE"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7400" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("CMA"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7200" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("CME"))
                    {  
                        string HexKod = LC_2.ToString("X") + "    7100" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("CIR"))
                    {  
                        string HexKod = LC_2.ToString("X") + "    7080" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("CIL"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7040" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("INC"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7020" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SPA"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7010" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SNA"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7008" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SZA"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7004" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SZE"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7002" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("HLT"))
                    {
                        string HexKod = LC_2.ToString("X") + "    7001" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("INP"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F800" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("OUT"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F400" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SKI"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F200" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("SKO"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F100" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("ION"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F080" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("IOF"))
                    {
                        string HexKod = LC_2.ToString("X") + "    F040" + "   " + listBox1.Items[i];
                        listBox2.Items.Add(HexKod);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("HEX")) 
                    {
                        string myHex = LC_2.ToString("X") + "    " +secili[i].Substring(4, secili[i].Length - 4).PadLeft(4,'0') + "   " + listBox1.Items[i];
                        listBox2.Items.Add(myHex);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("DEC"))
                    {
                        string dec1 = secili[i].Substring(4, secili[i].Length - 4);
                        string myHex = LC_2.ToString("X") + "    " + Convert.ToInt16(dec1).ToString("X").PadLeft(4,'0') + "   " + listBox1.Items[i];
                        listBox2.Items.Add(myHex);
                        LC_2++;
                    }
                    else if (secili[i].StartsWith("AGN"))
                    {
                        listBox2.Items.Add("AGN");
                    }
                    else if (secili[i].StartsWith("END"))
                    {
                        listBox2.Items.Add("                               END");
                    }
                } 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Metin Dosyası|*.txt";
            //save.OverwritePrompt = true;
            //save.CreatePrompt = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                 StreamWriter Kayit = new StreamWriter(save.FileName);
                foreach (string z in listBox2.Items)
                {
                    Kayit.WriteLine(z);
                }
                 Kayit.Close();
            }
        }
    }
}
