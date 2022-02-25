using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSONtoPostgreSQL
{
    public class TCPServer
    {
        public TcpListener _server;
        public Boolean _isRunning;
        public List<string> connected_users;


        public TCPServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            connected_users = new List<string>();
            _server.Start();

            _isRunning = true;



            Thread th = new Thread(listenClients);
            th.Start();
            //listenClients();
        }

        public void listenClients()
        {
            while (_isRunning)
            {
                try
                {
                    // wait for client connection
                    TcpClient newClient = _server.AcceptTcpClient();

                    // client found.
                    // create a thread to handle communication
                    Thread t = new Thread(new ParameterizedThreadStart(HandleClient));

                    t.Start(newClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);


            // reads from client stream
            string sData = sReader.ReadLine();

            if (!string.IsNullOrEmpty(sData))
            {
                string[] arr = sData.Split(',');
                //add name to list
                connected_users.Add(arr[0]);
                 
                sWriter.Flush();
            }

        }

        private void insertToDB(string sData, int n)
        {
            if (n > 20)
            {
                MessageBox.Show("Error inserting data");
                return; ;
            }
            if (SQLconnect.State != ConnectionState.Open)
            {
                SQLconnect.Open();
            }
            //create students table if not exist
            try
            {
                SQLiteCommand SQLcommand = new SQLiteCommand();
                SQLcommand = SQLconnect.CreateCommand();
                SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS Students" + "( Name TEXT, Phone TEXT, Address Text, Passport Text);";
                SQLcommand.ExecuteNonQuery();
                SQLcommand.Dispose();
                // MessageBox.Show("Table Created");

                //insert student
                string[] data = sData.Split(',');
                SQLiteCommand cmd = new SQLiteCommand();

                cmd = SQLconnect.CreateCommand();
                cmd.CommandText = "insert into Students values (@_name,@_phone,@_address,@_passport)";
                cmd.Parameters.AddWithValue("@_name", data[1]);
                cmd.Parameters.AddWithValue("@_phone", data[2]);
                cmd.Parameters.AddWithValue("@_address", data[3]);
                cmd.Parameters.AddWithValue("@_passport", data[4]);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                n++;
                Thread.Sleep(200);
                insertToDB(sData, n);
            }
            finally
            {
                if (SQLconnect.State != ConnectionState.Closed)
                {
                    SQLconnect.Close();
                }
            }
            //MessageBox.Show("Data Inserted");
        }
    }
}
