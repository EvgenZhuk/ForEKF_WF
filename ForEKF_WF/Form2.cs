using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace ForEKF_WF
{
    public partial class Form2 : Form
    {
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        //private Form1 m_parent;
        public Form2()
        {
            InitializeComponent();

        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        //public Form1 Form1 { get; }
        public Action refreshAction;



        public Form2(int ID, string LastName, string FirstName, string Patronymic, Action form1Refresh)   //, Form1 form1
        {
            InitializeComponent();
            this.ID = ID;
            this.LastName = LastName;
            this.FirstName = FirstName;
            this.Patronymic = Patronymic;
            //Form1 = form1;
            refreshAction = form1Refresh;
            this.Text = "Дети  (" + (LastName + " " + FirstName + " " + Patronymic + ")");
        }

        private void Form2_Load(object sender, EventArgs e) // при откритии формы вызывается метод с подключением к БД таблице Childrens  
        {
            dataGridView1.DataSource = Repository.ConnectToChildrensTab(ID);
            ClearFieldsForm2();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // вызывается метод добавления ребенка в таблицу Childrens
            Repository.InsertToChildrens(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text, ID);

            // обновляется грид
            dataGridView1.DataSource = Repository.ConnectToChildrensTab(ID);

            // очистка полей формы2
            ClearFieldsForm2();

                                       //Form1.ConnectToWorkersTab();
            refreshAction();
        }       


        public void ClearFieldsForm2() // очистка полей на форме2
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
