using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using MySql.Data.MySqlClient;
namespace AppIHMSimples
{
    public partial class Form1 : Form
    {
        delegate void funcaoRecepcao();
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private MySqlDataAdapter mAdapter;
        private DataSet mDataSet;
        public Form1()
        {
            InitializeComponent();
            InitializeComponent();
            server = "localhost";
            database = "ircontrol";
            uid = "root";
            password = "32412308";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
             database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=3000";

 

            connection = new MySqlConnection(connectionString);

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
        }  
        private bool OpenConnection()
        {
            try
            {
                connection.Open();  
                


                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            funcaoRecepcao recepcaodelegate = new funcaoRecepcao(RecepcaoSerial);
            Invoke(recepcaodelegate);
        }

        public void RecepcaoSerial()
        {
           txtRec.Text += serialPort1.ReadExisting();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            #region Config_Port
            String[] valoresPort = SerialPort.GetPortNames();
            for (int i = 0; i < valoresPort.Length; i++)
            {
                cbBoxPort.Items.Add(valoresPort[i]);
            }
            cbBoxPort.Text = "COM1";
            #endregion

            #region Config_Baud
            int[] valoresBaud = { 2400, 4800, 9600, 19200, 57600, 115200 };
            for (int i = 0; i < valoresBaud.Length; i++)
            {
                cbBoxBaud.Items.Add(valoresBaud[i].ToString());
            }
            cbBoxBaud.Text = "9600";
            #endregion

            #region Config_Data
            cbBoxData.Items.Add("7");
            cbBoxData.Items.Add("8");
            cbBoxData.Text = "8";
            #endregion

            #region Config_Stop
            cbBoxStop.Items.Add("None");
            cbBoxStop.Items.Add("One");
            cbBoxStop.Items.Add("two");
            cbBoxStop.Text = "One";
            #endregion

            #region Config_Parity
            cbBoxParity.Items.Add("NONE");
            cbBoxParity.Items.Add("EVEN");
            cbBoxParity.Items.Add("ODD");
            cbBoxParity.Items.Add("MARK");
            cbBoxParity.Items.Add("SPACE");
            cbBoxParity.Text = "NONE";
            #endregion
            

        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true) serialPort1.Close();
            else
            {
                serialPort1.PortName = cbBoxPort.Text;
                serialPort1.BaudRate = int.Parse(cbBoxBaud.Text);
                serialPort1.DataBits = int.Parse(cbBoxData.Text);
                serialPort1.StopBits = (StopBits)(cbBoxStop.SelectedIndex);
                serialPort1.Parity = (Parity)(cbBoxParity.SelectedIndex);
            }

            try
            {
                serialPort1.Open();
                btnCon.Enabled = false;
                btnDes.Enabled = true;
                btnExit.Enabled = false;
                cbBoxPort.Enabled = false;
                cbBoxBaud.Enabled = false;
                cbBoxData.Enabled = false;
                cbBoxStop.Enabled = false;
                cbBoxParity.Enabled = false;
                pnlMsg.BackColor = Color.Green;
                lblMsg.Text = "Close Port";
            }
            catch
            {
                MessageBox.Show("Erro de comunicação com a porta!!");
                btnCon.Enabled = true;
                btnDes.Enabled = false;
                btnExit.Enabled = true;
                cbBoxPort.Enabled = true;
                cbBoxBaud.Enabled = true;
                cbBoxData.Enabled = true;
                cbBoxStop.Enabled = true;
                cbBoxParity.Enabled = true;
                pnlMsg.BackColor = Color.Red;
                lblMsg.Text = "Open Port";
            }

        }

        private void btnDes_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                btnCon.Enabled = true;
                btnDes.Enabled = false;
                btnExit.Enabled = true;
                cbBoxPort.Enabled = true;
                cbBoxBaud.Enabled = true;
                cbBoxData.Enabled = true;
                cbBoxStop.Enabled = true;
                cbBoxParity.Enabled = true;
                pnlMsg.BackColor = Color.Red;
                lblMsg.Text = "Open Port";
                
            }
            catch
            {
                MessageBox.Show("Erro de comunicação com a porta!!");
                btnCon.Enabled = false;
                btnDes.Enabled = true;
                btnExit.Enabled = false;
                cbBoxPort.Enabled = false;
                cbBoxBaud.Enabled = false;
                cbBoxData.Enabled = false;
                cbBoxStop.Enabled = false;
                cbBoxParity.Enabled = false;
                pnlMsg.BackColor = Color.Green;
                lblMsg.Text = "Close Port";
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                if (chBoxEnviar.Checked)
                {
                    serialPort1.Write(txtEnviar.Text + "\r");
                }
                else
                {
                    serialPort1.Write(txtEnviar.Text);
                }

                txtEnviar.Text = null;
            }
            else
            {
                MessageBox.Show("Erro de comunicação com a porta!!");
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            txtRec.Text = null;
        }



        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Insert()
        {
            string query = "INSERT INTO codigosrecebidos (idcodigos, idData) VALUES('123', '33/90/78')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void Update()
        {
            string query = "UPDATE codigosrecebidos SET idcodigos='123', idData='33/90/78' WHERE idcodigos='143'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

                    
            public void Delete()
        {
            string query = "DELETE FROM codigosrecebidos WHERE idcodigos='123'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

            private void button1_Click(object sender, EventArgs e)
            {
                mDataSet = new DataSet();
            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigosrecebidos ORDER BY idcodigos ", connection);
            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet, "codigosrecebidos");
            dataGridView1.DataSource = mDataSet;
            dataGridView1.DataMember = ("codigosrecebidos");

            textBox1.Text = dataGridView1[1, 6].Value.ToString();

            }

            
    }
}
