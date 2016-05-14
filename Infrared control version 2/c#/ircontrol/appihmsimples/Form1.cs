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
        private DataSet lucas4,lucas7;
        private DataSet mDataSet2;
        private string recebido;
        private int numero = 0;
        private int i;
        private MySqlDataAdapter mAdapter2;
        private MySqlDataAdapter mAdapter;
        private DataSet lucas5, lucas6;
        private MySqlDataAdapter mAdapter3;
        private MySqlDataAdapter mAdapter4;
        private MySqlDataAdapter mAdapter5;
        delegate void funcaoRecepcao();
        private MySqlDataAdapter mAdapter6;
        private long recebe;
        private int senha;
        private int admin = 3241;
        private int t = 0;
        private string user = "";
        private int velAt = 1000;
        private string s5,s6,s7;
        private long senhachecar;

        private string port;

        public Form1()
        {

            string folder = @"C:\irconfig"; //nome do diretorio a ser criado

            //Se o diretório não existir...

            if (!Directory.Exists(folder))
            {

                //Criamos um com o nome folder
                Directory.CreateDirectory(folder);

            }

            String nomeArquivo = "C:/irconfig/config.txt";

            if (!System.IO.File.Exists(nomeArquivo))
            {
                System.IO.File.Create(nomeArquivo).Close();

                System.IO.TextWriter arqTXT = System.IO.File.AppendText(nomeArquivo);

                arqTXT.WriteLine("user:email do usuario");
                arqTXT.WriteLine("senha:senha do usuario");
                arqTXT.Close();
            }


            using (var reader = new StreamReader("c:\\irconfig\\config.txt"))
            {

                string line6 = reader.ReadLine();
                string line7 = reader.ReadLine();


                string[] l6 = Regex.Split(line6, "user:");
                string[] l7 = Regex.Split(line7, "senha:");


                foreach (string line in l6)
                {
                    s6 = line;
                }

                foreach (string line in l7)
                {
                    s7 = line;
                }
            }




            server = "127.0.0.1";
            database = "sys";
            uid = "root";
            password = "32412308";
            port = "3306";


            InitializeComponent();



            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
             database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "PORT=" + port + ";";



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

                label8.Text = "user:" + s6 +" " +"senha:" + s7 +" "  ;

                if(s6 == "email do usuario" || s7 =="senha do usuario"){

                    MessageBox.Show("Você precisa editar o arquivo irconfig e colocar seu nome e senha");
                    System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

                    pProcess.StartInfo.FileName = @"C:\irconfig\config.txt";

                    pProcess.Start();

                    
                    Close();
                }

                lucas7 = new DataSet();
                MySqlDataAdapter mAdapter7 = new MySqlDataAdapter("SELECT * FROM usuarios  WHERE email='" + s6 + "' AND senha='" + s7 + "' ", connection);
                //preenche o dataset através do adapter
                mAdapter7.Fill(lucas7, "usuarios");
                dataGridView7.DataSource = lucas7;
                dataGridView7.DataMember = ("usuarios");



                if (dataGridView7.RowCount - 1 > 0)
                {
                    user = s6;


                }
                else
                {
                    MessageBox.Show("A senha ou o nome de usuario estão errados!!");
                    Close();

                }


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


                timer1.Interval = velAt;
                timer1.Start();
                timer1.Enabled = true;

                timer2.Interval = 100;
                timer2.Start();
                timer2.Enabled = true;

                timer1.Tick += new EventHandler(timer1_Tick);
                timer2.Tick += new EventHandler(timer2_Tick);



                mDataSet2 = new DataSet();
                MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido WHERE user='" + user + "' ORDER BY idData  ", connection);
                //preenche o dataset através do adapter
                mAdapter.Fill(mDataSet2, "codigosrecebidos");
                dataGridView1.DataSource = mDataSet2;
                dataGridView1.DataMember = ("codigosrecebidos");

                lucas1 = new DataSet();
                MySqlDataAdapter mAdapter2 = new MySqlDataAdapter("SELECT * FROM historico WHERE user='" + user + "' ORDER BY idhistorico  ", connection);
                //preenche o dataset através do adapter
                mAdapter2.Fill(lucas1, "historico");
                dataGridView2.DataSource = lucas1;
                dataGridView2.DataMember = ("historico");

                lucas2 = new DataSet();
                MySqlDataAdapter mAdapter3 = new MySqlDataAdapter("SELECT * FROM bloqueio WHERE user='" + user + "' ORDER BY idbloqueio ", connection);
                //preenche o dataset através do adapter
                mAdapter3.Fill(lucas2, "historico");
                dataGridView3.DataSource = lucas2;
                dataGridView3.DataMember = ("historico");

                lucas3 = new DataSet();
                MySqlDataAdapter mAdapter4 = new MySqlDataAdapter("SELECT * FROM funcoescontroladas WHERE user='" + user + "' ORDER BY idfuncoescontroladas ", connection);
                //preenche o dataset através do adapter
                mAdapter4.Fill(lucas3, "historico");
                dataGridView4.DataSource = lucas3;
                dataGridView4.DataMember = ("historico");

                lucas4 = new DataSet();
                MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro WHERE user='" + user + "' ORDER BY idcadastro ", connection);
                //preenche o dataset através do adapter
                mAdapter5.Fill(lucas4, "cadastro");
                dataGridView5.DataSource = lucas4;
                dataGridView5.DataMember = ("cadastro");

                lucas5 = new DataSet();
                MySqlDataAdapter mAdapter6 = new MySqlDataAdapter("SELECT * FROM hora WHERE user='" + user + "' ORDER BY idhora ", connection);
                //preenche o dataset através do adapter
                mAdapter6.Fill(lucas5, "cadastro");
                dataGridView6.DataSource = lucas5;
                dataGridView6.DataMember = ("cadastro");
            }
            catch
            {
                MessageBox.Show("Erro de comunicação com a porta!!, connect o infrared Control antes de abrir isso");
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
                Close();
                
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

            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido WHERE user='" + user + "' ORDER BY idData ", connection);
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


            string query = "INSERT INTO codigorecebido (idcodigos, idData,user) VALUES('" + recebido + "', '" + datahora + "','" + user + "')";

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


        private void timer2_Tick(object sender, EventArgs e)
        {



            if (dataGridView1.RowCount > 100)
            {
                string query = "DELETE FROM codigorecebido WHERE  user='" + user + "';";

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

                string add = "INSERT INTO codigorecebido (idcodigos, idData,user) VALUES('" + recebido + "', '" + datahora + "','" + user + "')";

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




            int qu = dataGridView6.RowCount - 2;
            
            
            for (i = 0; i <= qu; i++)
            {

                string exec = dataGridView6[1, i].Value.ToString();

                textBox2.Text = textBox2.Text + exec + ",";

                serialPort1.Write(exec);

                string query = "DELETE FROM hora WHERE agora and idhora and user='" + user + "';";

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();

                    lucas5 = new DataSet();
                    MySqlDataAdapter mAdapter6 = new MySqlDataAdapter("SELECT * FROM hora WHERE user='" + user + "' ORDER BY idhora ", connection);
                    //preenche o dataset através do adapter
                    mAdapter6.Fill(lucas5, "cadastro");
                    dataGridView6.DataSource = lucas5;
                    dataGridView6.DataMember = ("cadastro");
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
                        string delline = "DELETE FROM bloqueio WHERE idbloqueio = '" + nad + "' AND user = '" + user + "' ;";

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


                if (ha1 >= horac)
                {

                    serialPort1.Write(funccontrol);

                    int nad2 = Int32.Parse(dataGridView4[0, i].Value.ToString());
                    string query10 = "DELETE FROM funcoesControladas WHERE idfuncoesControladas = '" + nad2 + "' AND user='" + user + "'; ";

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


            for (i = 0; i <= qu4; i++)
            {

                recebe = Int64.Parse(dataGridView1[1, qu2].Value.ToString());
                long codigocadastrado = Int64.Parse(dataGridView5[1, i].Value.ToString());
                string function = (dataGridView5[3, i].Value.ToString());
                string aparelho = (dataGridView5[2, i].Value.ToString());
                string hatual = (DateTime.Now.ToString("hh:mm"));

                if (t == 1)
                {

                    if (recebe == codigocadastrado)
                    {
                        t = 0;
                        string query12 = "INSERT INTO historico (codigo, funcao,aparelho,hora,user) VALUES('" + codigocadastrado + "','" + function + "','" + aparelho + "','" + hatual + "','" + user + "')";

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
            MySqlDataAdapter mAdapter2 = new MySqlDataAdapter("SELECT * FROM historico WHERE user='" + user + "' ORDER BY idhistorico  ", connection);
            //preenche o dataset através do adapter
            mAdapter2.Fill(lucas1, "historico");
            dataGridView2.DataSource = lucas1;
            dataGridView2.DataMember = ("historico");
            atuTab();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            lucas4 = new DataSet();
            MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro WHERE user='" + user + "' ORDER BY idcadastro ", connection);
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

            string query = "DELETE FROM bloqueio WHERE idbloqueio and user='" + user + "';";

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
            string querry11 = "DELETE FROM funcoesControladas WHERE  user='" + user + "' ; ";

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
            string delhist = "DELETE FROM historico WHERE idhistorico AND user='" + user + "'; ";

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

            string delcad = "DELETE FROM cadastro WHERE idcadastro AND user ='" + user + "'; ";

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
            string delarm = "DELETE FROM codigorecebido WHERE numero AND  user='" + user + "'  ; ";

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


            senha = Int32.Parse(maskedTextBox1.Text);


            if (senha == admin)
            {

                MessageBox.Show("BEM VINDO ADMIN");

                button6.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                

            }
            else
            {

                MessageBox.Show("senha errada");

            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            mDataSet2 = new DataSet();
            MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM codigorecebido  WHERE user='" + user + "' ORDER BY idData ", connection);
            //preenche o dataset através do adapter
            mAdapter.Fill(mDataSet2, "codigosrecebidos");
            dataGridView1.DataSource = mDataSet2;
            dataGridView1.DataMember = ("codigosrecebidos");


            lucas5 = new DataSet();
            MySqlDataAdapter mAdapter6 = new MySqlDataAdapter("SELECT * FROM hora  WHERE user='" + user + "' ORDER BY idhora", connection);
            //preenche o dataset através do adapter
            mAdapter6.Fill(lucas5, "cadastro");
            dataGridView6.DataSource = lucas5;
            dataGridView6.DataMember = ("cadastro");

            lucas2 = new DataSet();
            MySqlDataAdapter mAdapter3 = new MySqlDataAdapter("SELECT * FROM bloqueio WHERE user='" + user + "' ORDER BY idbloqueio  ", connection);
            //preenche o dataset através do adapter
            mAdapter3.Fill(lucas2, "historico");
            dataGridView3.DataSource = lucas2;
            dataGridView3.DataMember = ("historico");

            lucas3 = new DataSet();
            MySqlDataAdapter mAdapter4 = new MySqlDataAdapter("SELECT * FROM funcoescontroladas WHERE user='" + user + "' ORDER BY idfuncoescontroladas ", connection);
            //preenche o dataset através do adapter
            mAdapter4.Fill(lucas3, "historico");
            dataGridView4.DataSource = lucas3;
            dataGridView4.DataMember = ("historico");

            lucas4 = new DataSet();
            MySqlDataAdapter mAdapter5 = new MySqlDataAdapter("SELECT * FROM cadastro WHERE user='" + user + "' ORDER BY idcadastro ", connection);
            //preenche o dataset através do adapter
            mAdapter5.Fill(lucas4, "cadastro");
            dataGridView5.DataSource = lucas4;
            dataGridView5.DataMember = ("cadastro");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

            velAt = Int32.Parse( textBox4.Text);
           
        }

    }
}

