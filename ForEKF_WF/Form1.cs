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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) // при откритии формы вызывается метод с подключением к БД
        {
            dataGridView1.DataSource = Repository.ConnectToWorkersTab();
            ClearFieldsForm1();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e) 
        {   
            // вызывается метод добавления сотрудника в таблицу Workers
            Repository.InsertToWorkers(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text, comboBox1.Text, 0);
            
            // обновляется грид
            dataGridView1.DataSource = Repository.ConnectToWorkersTab();

            // очистка полей формы1
            ClearFieldsForm1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // по нажатию на грид вызывается форма2
        {
            int ID = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentRow.Index].Value);
            string LN = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            string FN = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            string PAT = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();

            Form2 frm2 = new Form2(ID, LN, FN, PAT, ()=> { dataGridView1.DataSource = Repository.ConnectToWorkersTab(); } );   //, this
            frm2.Show();
        }

        public void ClearFieldsForm1() // очистка полей на форме1
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.ResetText();
        }

          


    }
}
