using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace UdemySelenium
{
    public partial class Form1 : Form
    {
        public Form1()
        {       
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        ChromeDriver drv; Thread th;string url = "https://www.shutterstock.com/tr/search/3d+black+background";
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(basla); th.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            th = new Thread(siteac);th.Start();
            //programgizle();
        }
        private void siteac()
        {
            drv = new ChromeDriver();
            drv.Navigate().GoToUrl(url);
        }
        private void basla()
        {
            ChromeDriverService servis = ChromeDriverService.CreateDefaultService();
            servis.HideCommandPromptWindow = true;
            drv = new ChromeDriver(servis);
            drv.Navigate().GoToUrl("https://"+textBox1.Text);
        }
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        int hWnd; static int hwndgr = 0; int nrd = 0;
        void programgizle()
        {
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                if (pr.ProcessName == "chrome")
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, 0);

                    if (hWnd != 0)
                    {
                        hwndgr = hWnd;
                    }
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            drv.Quit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            th = new Thread(proxyconneciton);th.Start();
        }
        private void proxyconneciton()
        {
            ChromeOptions co = new ChromeOptions();
            co.AddArguments("--proxy-server=197.231.186.148:53229");
            drv = new ChromeDriver(co);
            drv.Navigate().GoToUrl("https://" + textBox1.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            th = new Thread(proxyeklentiliconnect);th.Start();
        }
        private void proxyeklentiliconnect()
        {
            ChromeOptions co = new ChromeOptions();
            co.AddExtensions(Application.StartupPath+"\\proxy.crx");
            co.AddArguments("--proxy-server=197.231.186.148:53229");
            drv = new ChromeDriver(co);
            drv.Navigate().GoToUrl("chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            th = new Thread(instgrmcreate);th.Start(); 
        }
        private void instgrmcreate()
        {
            drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"))[0].SendKeys(textBox2.Text); Thread.Sleep(1000);
            drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"))[1].SendKeys(textBox3.Text); Thread.Sleep(1000);
            drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"))[2].SendKeys(textBox4.Text); Thread.Sleep(1000);
            drv.FindElements(By.XPath("//input[@class='_2hvTZ pexuQ zyHYP']"))[3].SendKeys(textBox5.Text); Thread.Sleep(1000);
            drv.FindElement(By.XPath("//button[contains(text(),'Kaydol')]")).Click();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            drv.FindElement(By.XPath("//div[@class='clearfix field']//input[@placeholder='Telefon, e-posta veya kullanıcı adı']")).SendKeys(textBox7.Text);
            drv.FindElement(By.XPath("//div[@class='clearfix field']//input[@placeholder='Şifre']")).SendKeys(textBox6.Text);
            drv.FindElement(By.XPath("//button[@class='submit EdgeButton EdgeButton--primary EdgeButtom--medium']")).Click();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            th = new Thread(vericek);th.Start();
        }
        private void vericek()
        {
            int count = drv.FindElements(By.ClassName("text")).Count;int s1 = 1;
            for (int i = 0; i < count; i++)
            {
                listBox1.Items.Add(drv.FindElements(By.TagName("h1"))[s1].Text+": " + drv.FindElements(By.ClassName("text"))[i].Text);
                s1++;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            th = new Thread(googlesearch);th.Start();
        }
        private void googlesearch()
        {
            drv.ExecuteScript("document.getElementsByClassName('gLFyf gsfi')[0].value='"+textBox8.Text+"';");
            drv.ExecuteScript("document.getElementsByClassName('gLFyf gsfi')[0].value='"+textBox8.Text+"';");
            drv.ExecuteScript("document.getElementsByClassName('gLFyf gsfi')[0].value='"+textBox8.Text+"';");
            drv.ExecuteScript("document.getElementsByClassName('gNO89b')[0].click();");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            th = new Thread(sec);th.Start();
        }
        private void sec()
        {
            drv.FindElement(By.XPath("//option[contains(text(),'" + textBox9.Text + "')]")).Click();
        }
        OpenFileDialog op;
        private void button9_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (DialogResult.OK == op.ShowDialog())
            {
                th = new Thread(videosec); th.Start();
            }
           
        }
        private void videosec()
        {
            drv.FindElement(By.XPath("//input[@type='file']")).SendKeys(op.FileName.ToString());
        }
        private void button10_Click(object sender, EventArgs e)
        {
            th = new Thread(imagedown);th.Start();
        }

        private void imagedown()
        {
            for (int i = 0; i < drv.FindElements(By.XPath("//img[@class='z_e_h']")).Count; i++)
            {
                listBox1.Items.Add(drv.FindElements(By.ClassName("z_e_h"))[i].GetAttribute("src"));
            }
            for (int j = 0; j < listBox1.Items.Count; j++)
            {
                imagedownload.DownloadImage ImageDownload = new imagedownload.DownloadImage(listBox1.Items[j].ToString());
                ImageDownload.Download();
                ImageDownload.SaveImage(Application.StartupPath + @"\resimler\" + j+"dsfg125", ImageFormat.Png);
            }
            MessageBox.Show("Bütün resimler İndirildi");
        }
    }
}
