using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordWebHook
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
		}

		int ListIndex = 0, ErrorList = 0, NormalList = 0;

		private string WebHookUrl;

		private void button2_Click(object sender, EventArgs e)
        {
			this.ErrorList = 0;
			checked
			{
				int num = this.ListIndex - 1;
				for (int i = 0; i <= num; i++)
				{
					this.listBox1.SelectedIndex = i;
					this.WebHookUrl = Conversions.ToString(this.listBox1.SelectedItem);
					this.Tester(this.WebHookUrl);
				}
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\save.txt");
			FileStream fileStream = fileInfo.Open(FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			this.listBox1.Items.Clear();
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				bool flag = Operators.CompareString(text, null, false) == 0;
				bool flag2 = flag;
				if (flag2)
				{
					break;
				}
				this.listBox1.Items.Add(text.Substring(0, text.Length));
				ref int ptr = ref this.ListIndex;
				this.ListIndex = checked(ptr + 1);
			}
			fileStream.Close();
			streamReader.Close();
		}
		public async void Tester(string WebHookUrl)
		{
			checked
			{
				try
				{
					WebClient DownloadString = new WebClient();
					string WebHookText = DownloadString.DownloadString(WebHookUrl);
					bool flag = WebHookText.Contains("Unknown Webhook") | WebHookText.Contains("10015");
					if (!flag)
					{
						this.NormalList++;
						this.label1.Text = "오류 : " + string.Concat(this.ErrorList) + " / 정상 : " + string.Concat(this.NormalList);
						RichTextBox richTextBox;
						this.richTextBox1.Text += WebHookUrl + "\n";
					}
				}
				catch (Exception ex)
				{
					this.ErrorList++;
					this.label1.Text = "오류 : " + string.Concat(this.ErrorList) + " / 정상 : " + string.Concat(this.NormalList);
				}
			}
		}
	}
}
