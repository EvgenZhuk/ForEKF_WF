using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Data;

namespace ForEKF_WF
{
    static class Repository
    {
        private static DataSet ds = new DataSet();
        private static DataTable dt = new DataTable();

        private static DataSet ds2 = new DataSet();
        private static DataTable dt2 = new DataTable();

        static public DataTable ConnectToWorkersTab() // подключение к таблице Workers и отображение в гриде
        {
            using (NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1;Database=ekf_db"))
            {
                con.Open();
                string sql = ("select id as \"№\", lastname as \"Фамилия\", firstname as \"Имя\", patronymic as \"Отчество\", birthday as \"ДР\", positions as \"Должность\", total_ch as \"Дети\" from workers;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                return dt;
            }
        }

        // добавление новой записи в таблицу Workers
        static public void InsertToWorkers(string lastname, string firstname, string patronymic, string birthday, string positions, int total_ch)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1;Database=ekf_db"))
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO workers (lastname, firstname, patronymic, birthday, positions, total_ch) VALUES ('" + lastname + "', '" + firstname + "', '" + patronymic + "', '" + birthday + "', '" + positions + "','"+ total_ch +"');";
                cmd.CommandType = CommandType.Text;   //
                cmd.ExecuteNonQuery();               
            }
        }


        static public DataTable ConnectToChildrensTab(int ID)  // подключение к таблице Childrens и отображение в гриде
        {
            using (NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1;Database=ekf_db"))
            {
                con.Open();
                string sql = ("select lastname as \"Фамилия\", firstname as \"Имя\", patronymic as \"Отчество\", birthday as \"ДР\" from childrens where worker_id = " + ID);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds2.Reset();
                da.Fill(ds2);
                dt2 = ds2.Tables[0];
                return dt2;
            }
        }

        // добавление новой записи в таблицу Childrens
        static public void InsertToChildrens(string lastname, string firstname, string patronymic, string birthday, int ID)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=1;Database=ekf_db"))
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO childrens (lastname, firstname, patronymic, birthday, worker_id) VALUES ('" + lastname + "', '" + firstname + "', '" + patronymic + "', '" + birthday + "', " + ID + ");";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                NpgsqlCommand cmd2 = new NpgsqlCommand();
                cmd2.Connection = connection;
                cmd2.CommandText = "UPDATE workers SET total_ch = total_ch+1 where id=" + ID;
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
            }
        }


    }
}
