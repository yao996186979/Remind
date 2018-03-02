using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
namespace WindowsFormsApp1
{   
    //定义委托 我需要将form2的值传递出去
    public delegate void ActionHandler(int Time);
   
    public partial class Input : Form
    {
        // RegistryKey key = Registry.LocalMachine;

      
        public Input()
        {
            InitializeComponent();
            //注册时间

            // 在HKEY_LOCAL_MACHINE\SOFTWARE下新建名为test的注册表项。如果已经存在则不影响！
            //RegistryKey software = key.CreateSubKey("software\\Time");
            //RegistryKey a = key.OpenSubKey("software\\Time", true);
            StreamReader sr = File.OpenText("..\\..\\Image\\list.txt");
            String nowTime = sr.ReadLine();//从当前位置读取到文本结束 
            sr.Close();                     //释放资源 
            if (nowTime != null)
            {
                Text = string.Concat("提醒间隔时间", int.Parse(nowTime), "分钟");
            }
            else
            {
                Text = "提醒间隔时间10秒钟";
            }           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public event ActionHandler TimeGo;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (TimeGo != null)//判断事件是否为空
            { 
                if (textBox1.Text.Length == 0)
                {
                    return;
                }
                else
                {
                    FileStream stream = File.Open("..\\..\\Image\\list.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    stream.Close();
                    StreamWriter sw = File.AppendText("..\\..\\Image\\list.txt");
                    sw.WriteLine(textBox1.Text);             //写入一行文本 
                   // sw.Write("www.csdn.net");       //在文本末尾写入文本 
                    sw.Flush();                     //清空 
                    sw.Close();                     //关闭 

                    //nowTime = int.Parse(textBox1.Text);
                    TimeGo(int.Parse(textBox1.Text));
                }
            }
            else
            {
                MessageBox.Show("dad");
            }
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
                
             
         
        }
        public void timeChange(int time)
        {
            
        }
        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        //判断注册表项是否存在
        private bool IsRegeditItemExist()
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE");
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE", true); 
            subkeyNames = software.GetSubKeyNames();
            //取得该项下所有子项的名称的序列，并传递给预定的数组中 
            foreach (string keyName in subkeyNames)
            //遍历整个数组 
            {
                if (keyName == "test")
                //判断子项的名称 
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }
        //判断键值是否存在
        private bool IsRegeditKeyExit()
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test");
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE\\test", true);
            subkeyNames = software.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)
            {
                if (keyName == "nowTime") //判断键值的名称
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }
         
       

    }
}
