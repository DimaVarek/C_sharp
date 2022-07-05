namespace ClientLab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            client = new Client(infile, currentfile);
        }
        Client client;
        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Connect("127.0.0.1", 5000);
        }

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (command.Text.Split(new char[] { ' ' })[0])
            {
                case "Put":
                    client.PutFile(command.Text.Split(new char[] { ' ' })[1]);
                    break;             
                default:
                    MessageBox.Show("������������ �������");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentfile.Text == "")
            {
                MessageBox.Show("����� ���� ���������???");
            }
            else
            {
                client.FillFile(currentfile.Text, infile.Text);
            }
        }
    }
}