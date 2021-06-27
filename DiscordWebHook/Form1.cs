using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DiscordWebHook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
		private int le = 0;
		public int savemin = 0;
		int sc = 0;
		bool bb = false;
		private void button4_Click(object sender, EventArgs e)
        {
			bool flag = MessageBox.Show("선택하신 정보가 로드됩니다", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes;
			if (flag)
			{
				FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\save.txt");
				FileStream fileStream = fileInfo.Open(FileMode.Open);
				StreamReader streamReader = new StreamReader(fileStream);
				listBox2.Items.Clear();
				while (!streamReader.EndOfStream)
				{
					le++;
					string text = streamReader.ReadLine();
					bool flag2 = text == null;
					if (flag2)
					{
						break;
					}
					listBox2.Items.Add(text.Substring(0, text.Length));
				}
				fileStream.Close();
				streamReader.Close();
				MessageBox.Show(string.Concat(this.le) + "개");
			}
		}
		public static byte[] Post(string url, NameValueCollection p)
		{
			byte[] result;
			using (WebClient webClient = new WebClient())
			{
				result = webClient.UploadValues(url, p);
			}
			return result;
		}

        private void button2_Click(object sender, EventArgs e)
        {
			MessageBox.Show(string.Concat(this.le));
			홍보();
		}
		public static void aaa(string Url, string msg, string username)
		{
			Post(Url, new NameValueCollection
			{
				{ "username", username }, { "content",msg }
			});
		}

		private void 홍보()
        {
			bb = false;
			int i = 0;
			while (i < le)
			{
				try
				{
					this.listBox2.SelectedIndex = i;
					aaa(this.listBox2.Text, this.textBox3.Text, this.textBox2.Text);
				}
				catch
				{
					try
					{
						this.listBox2.Items.RemoveAt(i);
					}
					catch
					{
					}
				}
				i++;
			}
			sc = savemin;
			bb = true;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch(comboBox1.SelectedIndex)
            {
				case 0:
					this.savemin = 60;
					sc = savemin;
					label1.Text = "설정 시간 : 60초 선택";

					break;
				case 1:
					this.savemin = 300;
					sc = savemin;
					label1.Text = "설정 시간 : 5분 선택";
					break;
				case 2:
					this.savemin = 600;
					sc = savemin;
					label1.Text = "설정 시간 : 10분 선택";
					break;
				case 3:
					this.savemin = 1800;
					sc = savemin;
					label1.Text = "설정 시간 : 30분 선택";
					break;
				case 4:
					this.savemin = 3600;
					sc = savemin;
					label1.Text = "설정 시간 : 60분 선택";
					break;
				case 5:
					this.savemin = 7200;
					sc = savemin;
					label1.Text = "설정 시간 : 120분 선택";
					break;
			}
		}
		private void timer1_Tick(object sender, EventArgs e)
        {
			if (bb)
            {
				sc -= 1;
				label1.Text = "설정 시간 : " + sc.ToString() + " 초";
				if (sc == 0)
				{
					홍보();
				}
			}
		}

        private void button5_Click(object sender, EventArgs e)
        {
			if(this.button5.Text.Equals("자동 홍보"))
            {
				comboBox1.Enabled = false;
				button5.Text = "홍보 중";
				timer1.Enabled = true;
				bb = true;
			}
			else if(this.button5.Text.Equals("홍보 중"))
            {
				button5.Text = "자동 홍보";
				comboBox1.Enabled = true;
				timer1.Enabled = false;
				bb = false;
			}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
			string textfile = Application.StartupPath + "\\save.txt";
			FileInfo fileinfo = new FileInfo(textfile);

			if (fileinfo.Exists == true)
            {
				StreamReader SR = new StreamReader(Application.StartupPath + "\\text.txt");

				string result;
				result = SR.ReadToEnd();
				textBox3.Text = result;

				
			}
			else
            {

            }
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
			if(MessageBox.Show("추가를 저장하겠습니까?", "URL", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
				string savePath = Application.StartupPath + "\\save.txt";
				string textUrl = textBox1.Text;
				System.IO.File.AppendAllText(savePath, "\n", Encoding.Default);
				System.IO.File.AppendAllText(savePath, textUrl, Encoding.Default);
				textBox1.Clear();

				FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\save.txt");
				FileStream fileStream = fileInfo.Open(FileMode.Open);
				StreamReader streamReader = new StreamReader(fileStream);
				listBox2.Items.Clear();
				le = 0;
				while (!streamReader.EndOfStream)
				{
					le++;
					string text = streamReader.ReadLine();
					bool flag2 = text == null;
					if (flag2)
					{
						break;
					}
					listBox2.Items.Add(text.Substring(0, text.Length));
				}
				fileStream.Close();
				streamReader.Close();
			}
			else
            {
				listBox2.Items.Add(textBox1.Text);
				textBox1.Clear();
			}
        }

        private void button3_Click(object sender, EventArgs e)
        {
			Form2 urlEqulq = new Form2();
			urlEqulq.ShowDialog();
        }
    }
}
