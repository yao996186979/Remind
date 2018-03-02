using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
       
        static Input f2;
        //实例化Timer类  
        System.Timers.Timer aTimer = new System.Timers.Timer();
        //默认10秒
        static float actionTime;
        public Form3()
        {
            InitializeComponent();

            StreamReader sr = File.OpenText("..\\..\\Image\\list.txt");
            String nowTime = sr.ReadLine();//从当前位置读取到文本结束 
            sr.Close();                     //释放资源 
            if (nowTime != null)
            {
                actionTime = int.Parse(nowTime) * 60 * 1000;
            }
            else
            {
                actionTime = 1000 * 10;
            }
            // 初始化f1
            this.WindowState = FormWindowState.Minimized;
            // 初始化f2
            f2 = new Input();
            f2.TimeGo += new ActionHandler(TimeChanged);//将事件和处理方法绑在一起，这句话必须放在f2.ShowDialog();
            SetTimerParam();
        }
        // 时间改变后作出的处理
        public void TimeChanged(int time)
        {
            // MessageBox.Show("打打打");\
            f2 = new Input();
            f2.TimeGo += new ActionHandler(TimeChanged);//将事件和处理方法绑在一起，这句话必须放在f2.ShowDialog();
            aTimer.Stop();
            aTimer.Close();
            actionTime = time * 1000 * 60;   //传过来的是分钟 需要转换成秒
            this.SetTimerParam();
        }
 
        public void SetTimerParam()
        {
            //到时间的时候执行事件  
            aTimer.Elapsed += new ElapsedEventHandler(showNotic);
            aTimer.Interval = actionTime;
            aTimer.AutoReset = true;//执行一次 false，一直执行true  
            //是否执行System.Timers.Timer.Elapsed事件  
            aTimer.Enabled = true;
        }

        public void showNotic(Object source, System.Timers.ElapsedEventArgs e)
        {
            // 初始化f1
            Form1 f1 = new Form1();//Form1为要弹出的窗体（提示框），
            f1.Show();
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            f2.Show();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

   
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();

            }
            else
            {
                this.Show();
            }
            notifyIcon1.ShowBalloonTip(3000, "hellow", "老芳儿...", ToolTipIcon.Info);
        }
    }
}
