using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
namespace AppIHMSimples
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        private DataSet lucas1;
        private DataSet lucas2;
        private DataSet lucas3;
        private DataSet lucas4;
        private DataSet mDataSet2;
        private string recebido;
        private int numero = 0;
        private int i;
        private MySqlDataAdapter mAdapter2;
        private MySqlDataAdapter mAdapter;
        private DataSet lucas5,lucas6;
        private MySqlDataAdapter mAdapter3;
        private MySqlDataAdapter mAdapter4;
        private MySqlDataAdapter mAdapter5;
        delegate void funcaoRecepcao();
        private MySqlDataAdapter mAdapter6;
        private long recebe;
        private int  senha;
        private int admin = 3241 ;
        private int t = 0;

        private string s1;
        private string s2;
        private string s3;
        private string s4;
        private string s5;

        private string port;
        
        public Form1()
       
        {

            using (var reader = new StreamReader("c:\\irconfig\\config.txt"))
            {
                string line1 = reader.ReadLine();
                string line2 = reader.ReadLine();
                string line3 = reader.ReadLine();
                string line4 = reader.ReadLine();
                string line5 = reader.ReadLine();
                // etc..



                // Split the string on line breaks.
                // ... The return value from Split is a string array.
                string[] lines = Regex.Split(line1, "servidor:");
                string[] l2 = Regex.Split(line2, "porta:");
                string[] l3 = Regex.Split(line3, "usuario:");
                string[] l4 = Regex.Split(line4, "senhaservidor:");
                string[] l5 = Regex.Split(line5, "nomeDb:");
                foreach (string line in lines)
                {
                    s1 = line;
                }

                foreach (string line in l2)
                {
                    s2 = line;
                }
                foreach (string line in l3)
                {
                    s3 = line;
                }
                foreach (string line in l4)
                {
                    s4 = line;
                }
                foreach (string line in l5)
                {
                    s5 = line;
                }



            }


            

         server = s1;
      database = s5;
         uid = s3;
         password = s4;
         port = s2;
         

            InitializeComponent();
       
            
            
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
             database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT="+port+";";


            
            connection = new MySqlConnection(connectionString);


            // Get the current date.
            DateTime thisDay = DateTime.Today;
            // Display the date in the default (general) format.

            textBox1.Text = DateTime.Now.ToString();






            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);


        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            funcaoRecepcao recepcaodelegate = new funcaoRecepcao(RecepcaoSerial);
            Invoke(recepcaodelegate);
        }

        public void RecepcaoSerial()
        {
            
            recebido = serialPort1.ReadLine();
            
            txtRec.Text = recebido;
            Insert();
            t = 1;


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
            timer1.Enabled = true;
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(timer1_Tick);

            mDataSet2 = new DataSet();
            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido ORDER BY idData ", connection);
            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet2, "codigosrecebidos");
            dataGridView1.DataSource = mDataSet2;
            dataGridView1.DataMember = ("codigosrecebidos");

            lucas1 = new DataSet();
            MySqlDataAdapter mAdapter2 = new MySqlDataAdapter("SELECT * FROM historico ORDER BY idhistorico ", connection);
            //preenche o dataset através do adapter
            mAdapter2.Fill(lucas1, "historico");
            dataGridView2.DataSource = lucas1;
            dataGridView2.DataMember = ("historico");

            lucas2 = new DataSet();
            MySqlDataAdapter mAdapter3 = new MySqlDataAdapter("SELECT * FROM bloqueio ORDER BY idbloqueio ", connection);
            //preenche o dataset através do adapter
            mAdapter3.Fill(lucas2, "historico");
            dataGridView3.DataSource = lucas2;
            dataGridView3.DataMember = ("historico");

            lucas3 = new DataSet();
            MySqlDataAdapter mAdapter4 = new MySqlDataAdapter("SELECT * FROM funcoescontroladas ORDER BY idfuncoescontroladas ", connection);
            //preenche o dataset através do adapter
            mAdapter4.Fill(lucas3, "historico");
            dataGridView4.DataSource = lucas3;
            dataGridView4.DataMember = ("historico");

            lucas4 = new DataSet();
            MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro ORDER BY idcadastro ", connection);
            //preenche o dataset através do adapter
            mAdapter5.Fill(lucas4, "cadastro");
            dataGridView5.DataSource = lucas4;
            dataGridView5.DataMember = ("cadastro");

            lucas5 = new DataSet();
            MySqlDataAdapter mAdapter6 = new MySqlDataAdapter("SELECT * FROM hora ORDER BY idhora ", connection);
            //preenche o dataset através do adapter
            mAdapter6.Fill(lucas5, "cadastro");
            dataGridView6.DataSource = lucas5;
            dataGridView6.DataMember = ("cadastro");




            

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

        public void atuTab()
        {

            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido ORDER BY idData ", connection);
            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet2, "codigosrecebidos");
            dataGridView1.DataSource = mDataSet2;
            dataGridView1.DataMember = ("codigosrecebidos");
        }

        public void Insert()
        {

            DateTime thisDay = DateTime.Today;

            string datahora = DateTime.Now.ToString();

            numero++;


            string query = "INSERT INTO codigorecebido (idcodigos, idData) VALUES('" + recebido + "', '" + datahora + "')";

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


        private void timer1_Tick(object sender, EventArgs e)
        {

            if (dataGridView1.RowCount > 10)
            {
                string query = "DELETE FROM `ircontrol`.`codigorecebido` WHERE numero;";

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
                DateTime thisDay = DateTime.Today;

                string datahora = DateTime.Now.ToString();

                string add = "INSERT INTO codigorecebido (idcodigos, idData) VALUES('" + recebido + "', '" + datahora + "')";

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(add, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();


                }
            }



 
            long recebe = 0;

            mDataSet2 = new DataSet();
            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido ORDER BY idData ", connection);
            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet2, "codigosrecebidos");
            dataGridView1.DataSource = mDataSet2;
            dataGridView1.DataMember = ("codigosrecebidos");


            lucas5 = new DataSet();
            MySqlDataAdapter mAdapter6 = new MySqlDataAdapter("SELECT * FROM hora ORDER BY idhora ", connection);
            //preenche o dataset através do adapter
            mAdapter6.Fill(lucas5, "cadastro");
            dataGridView6.DataSource = lucas5;
            dataGridView6.DataMember = ("cadastro");

            lucas2 = new DataSet();
            MySqlDataAdapter mAdapter3 = new MySqlDataAdapter("SELECT * FROM bloqueio ORDER BY idbloqueio ", connection);
            //preenche o dataset através do adapter
            mAdapter3.Fill(lucas2, "historico");
            dataGridView3.DataSource = lucas2;
            dataGridView3.DataMember = ("historico");

            lucas3 = new DataSet();
            MySqlDataAdapter mAdapter4 = new MySqlDataAdapter("SELECT * FROM funcoescontroladas ORDER BY idfuncoescontroladas ", connection);
            //preenche o dataset através do adapter
            mAdapter4.Fill(lucas3, "historico");
            dataGridView4.DataSource = lucas3;
            dataGridView4.DataMember = ("historico");

            lucas4 = new DataSet();
            MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro ORDER BY idcadastro ", connection);
            //preenche o dataset através do adapter
            mAdapter5.Fill(lucas4, "cadastro");
            dataGridView5.DataSource = lucas4;
            dataGridView5.DataMember = ("cadastro");
            int qu = dataGridView6.RowCount - 2;          
            
            


           

            for (i = 0; i <= qu; i++)
            {

                string exec = dataGridView6[1, i].Value.ToString();

                textBox2.Text = textBox2.Text + exec + ",";

                serialPort1.Write(exec);

                string query = "DELETE FROM `ircontrol`.`hora` WHERE agora and idhora;";

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
            int qu1 = dataGridView3.RowCount - 2;
            int qu2 = dataGridView1.RowCount - 2;
            int qu3 = dataGridView4.RowCount - 2;
            int qu4 = dataGridView5.RowCount - 2;

            for (i = 0; i <= qu1; i++)
            {
                if (t == 1)
                {

                    long armazenultimo = Int64.Parse(dataGridView1[1, qu2].Value.ToString());
                    long bloq = Int64.Parse(dataGridView3[1, i].Value.ToString());
                    string funcaof = dataGridView3[5, i].Value.ToString();
                    string tinicial = (dataGridView3[3, i].Value.ToString());
                    string tfinal = (dataGridView3[4, i].Value.ToString());
                    string horaatual = (DateTime.Now.ToString("hh:mm"));

                    DateTime ti1 = Convert.ToDateTime(tinicial);
                    DateTime tf1 = Convert.ToDateTime(tfinal);
                    DateTime ha1 = Convert.ToDateTime(horaatual);



                    if (bloq == armazenultimo && (ha1 >= ti1 && ha1 < tf1))
                    {

                        t = 0;



                        serialPort1.Write(funcaof);



                    }
                    if (ha1 >= tf1)
                    {



                        int nad = Int32.Parse(dataGridView3[0, i].Value.ToString());
                        string delline = "DELETE FROM bloqueio WHERE idbloqueio = '" + nad + "';";

                        //open connection
                        if (this.OpenConnection() == true)
                        {
                            //create command and assign the query and connection from the constructor
                            MySqlCommand cmd = new MySqlCommand(delline, connection);

                            //Execute command
                            cmd.ExecuteNonQuery();

                            //close connection
                            this.CloseConnection();


                        }


                    }
                }
            }

            for (i = 0; i <= qu3; i++)
            {


                

                string hora = (dataGridView4[2, i].Value.ToString());

                string funccontrol = dataGridView4[3, i].Value.ToString();


                string horaatual = (DateTime.Now.ToString("hh:mm"));

                DateTime horac = Convert.ToDateTime(hora);

                DateTime ha1 = Convert.ToDateTime(horaatual);


                if(ha1 >= horac){

                    serialPort1.Write(funccontrol);

                    int nad2 = Int32.Parse(dataGridView4[0, i].Value.ToString());
                    string query10 = "DELETE FROM `ircontrol`.`funcoesControladas` WHERE idfuncoesControladas = '"+nad2+"'; ";

                    //open connection
                    if (this.OpenConnection() == true)
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query10, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();

                        //close connection
                        this.CloseConnection();


                    }

                }
            }

           
            for (i = 0; i <= qu4;i++ )
            {

                recebe = Int64.Parse(dataGridView1[1, qu2].Value.ToString());
                long codigocadastrado = Int64.Parse(dataGridView5[1, i].Value.ToString());
                string function = (dataGridView5[3, i].Value.ToString());
                string aparelho = (dataGridView5[2, i].Value.ToString());
                string hatual = (DateTime.Now.ToString("hh:mm"));

                if(t == 1){

                if (recebe == codigocadastrado)
                {
                    t = 0;
                    string query12 = "INSERT INTO historico (codigo, funcao,aparelho,hora) VALUES('"+codigocadastrado+"','"+function+"','"+aparelho+"','"+hatual+"')";

                    //open connection
                    if (this.OpenConnection() == true)
                    {
                        //create command and assign the query and connection from the constructor
                        MySqlCommand cmd = new MySqlCommand(query12, connection);

                        //Execute command
                        cmd.ExecuteNonQuery();

                        //close connection
                        this.CloseConnection();


                    }
                }

                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            lucas1 = new DataSet();
            MySqlDataAdapter mAdapter2 = new MySqlDataAdapter("SELECT * FROM historico ORDER BY idhistorico ", connection);
            //preenche o dataset através do adapter
            mAdapter2.Fill(lucas1, "historico");
            dataGridView2.DataSource = lucas1;
            dataGridView2.DataMember = ("historico");
            atuTab();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            lucas4 = new DataSet();
            MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro ORDER BY idcadastro ", connection);
            //preenche o dataset através do adapter
            mAdapter5.Fill(lucas4, "cadastro");
            dataGridView5.DataSource = lucas4;
            dataGridView5.DataMember = ("cadastro");
        }

        private void button3_Click(object sender, EventArgs e)
        {




        }

        private void button4_Click(object sender, EventArgs e)


        {

            string query = "DELETE FROM `ircontrol`.`bloqueio` WHERE idbloqueio ;";

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

        private void button5_Click(object sender, EventArgs e)
        {
            string querry11 = "DELETE FROM `ircontrol`.`funcoesControladas` WHERE idfuncoesControladas ; ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(querry11, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string delhist = "DELETE FROM `ircontrol`.`historico` WHERE idhistorico ; ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(delhist, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            string delcad = "DELETE FROM `ircontrol`.`cadastro` WHERE idcadastro ; ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(delcad, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string delarm = "DELETE FROM `ircontrol`.`codigorecebido` WHERE numero ; ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(delarm, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();


            }



        }

        private void button9_Click(object sender, EventArgs e)
        {


            senha =  Int32.Parse(maskedTextBox1.Text);


            if(senha == admin){

            MessageBox.Show("BEM VINDO ADMIN");

            button6.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button10.Enabled = true;

            }else{

                MessageBox.Show("senha errada");

}
        }

        private void button10_Click(object sender, EventArgs e)
        {

            string pass = "UPDATE `ircontrol`.`senha`SET`idsenha` = 1,`senha`= "+textBox3.Text+";  ";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(pass, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();

                MessageBox.Show("senha trocada para " + textBox3.Text + "");
            }

        }



    }
}

