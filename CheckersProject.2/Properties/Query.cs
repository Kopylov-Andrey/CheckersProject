using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;


namespace CheckersProject._2
{
    class Query // для работы с бд
    {
        OleDbConnection        connection;
        OleDbCommand           command;
        OleDbDataAdapter       dataAdapter;
        DataTable              bufferTable;

        public Query(string Conn)
        {
            connection = new OleDbConnection(Conn);
            bufferTable = new DataTable();
        }

        public DataTable UpdatePerson()
        {
            connection.Open();
            dataAdapter = new OleDbDataAdapter("SELECT * FROM UserData", connection);
            bufferTable.Clear();
            dataAdapter.Fill(bufferTable);
            connection.Close();
            return bufferTable;
        }

        public void Add(string login, string password/*, int record*/)
        {
            connection.Open();
            command = new OleDbCommand($"INSERT INTO UserData ([login], [password]) VALUES (@login, @password)", connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", password);
            //command.Parameters.AddWithValue("record", record);
            command.ExecuteNonQuery();
            connection.Close();
        }


        public void Delete(int ID)
        {
            connection.Open();
            command = new OleDbCommand($"DELETE FROM UserData WHERE ID ={ID}", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string[] Read_name()
        {
            connection.Open();
            OleDbCommand dbCommand = new OleDbCommand("SELECT * FROM UserData", connection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();
            int i = dbReader.FieldCount;



            string[] name = new string[100];

            int j = -1;
            while (dbReader.Read()){
                j++;
                name[j] = Convert.ToString(dbReader["login"]);

            }
            connection.Close();
            return name;
        }

        public string[] Read_password()
        {
            connection.Open();
            OleDbCommand dbCommand = new OleDbCommand("SELECT * FROM UserData", connection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();
            int i = dbReader.FieldCount;



            string[] name = new string[100];

            int j = -1;
            while (dbReader.Read())
            {
                j++;
                name[j] = Convert.ToString(dbReader["password"]);

            }
            connection.Close();
            return name;
        }

        public int FieldCount()
        {
            connection.Open();
            OleDbCommand dbCommand = new OleDbCommand("SELECT * FROM UserData", connection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();
            int i = dbReader.FieldCount;
            connection.Close();
            return 100;
        }

    }


   
}
