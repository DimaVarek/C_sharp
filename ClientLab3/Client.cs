using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientLab3
{
    class Client
    {
        TcpClient client;

        TextBox currentfile;
        TextBox infile;
        public Client(TextBox infile, TextBox currentfile)
        {
            this.infile = infile;
            this.currentfile = currentfile;
        }
        public void Connect(string IP, int port)
        {
            try
            {
                client = new TcpClient(IP, port);
            }
            catch (SocketException e)
            {
                MessageBox.Show("Не удалось подключиться к серверу");
            }
        }

        public void Disconnect()
        {
            client.Close();
        }
        public enum Command
        {
            PutFile,
            FillFile,
        }
        public void PutFile(string filename)
        {
            try
            {
                byte[] buffer = BitConverter.GetBytes((int)Command.PutFile);
                var network = client.GetStream();
                network.Write(buffer);

                SendString(filename);

                buffer = new byte[sizeof(bool)];
                network.Read(buffer);

                if (BitConverter.ToBoolean(buffer))
                {
                    MessageBox.Show("Файл создан");
                    currentfile.Text = filename;
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при создании файла");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не подключен к серверу");
            }
        }


        public void FillFile (string currentfile, string infile)
        {
            try
            {
                byte[] buffer = BitConverter.GetBytes((int)Command.FillFile);
                var network = client.GetStream();
                network.Write(buffer);

                SendString(currentfile);
                SendString(infile);

                buffer = new byte[sizeof(bool)];
                network.Read(buffer);

                if (BitConverter.ToBoolean(buffer))
                {
                    MessageBox.Show("Файл заполнен");
                    this.infile.Text = "";
                }
                else
                {
                    MessageBox.Show("Произошла ошибка при заполнении файла");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не подключен к серверу");
            }
        }
        
        private void SendString(string str)
        {
            var network = client.GetStream();
            network.Write(BitConverter.GetBytes(str.Length));
            network.Write(Encoding.UTF8.GetBytes(str));
        }
    }
}
