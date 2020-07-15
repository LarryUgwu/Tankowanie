using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Tankowanie.Utils;
using MySql.Data.MySqlClient;

namespace Tankowanie
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            //dodać do References MySql.Data
            //w pliku konfiguracyjnym app.config dodać węzeł <appSettings>
            //w sekcji using dodać:
                //using System.Configuration;
                //using Tankowanie.Utils;
                //using MySql.Data.MySqlClient;

            String cs = ConfigurationManager.AppSettings["cs"];

            try
            {
                if (String.IsNullOrWhiteSpace(tbLogin.Text) ||
                    String.IsNullOrWhiteSpace(tbPassword.Text))
                {
                    DialogHelper.Error("Podaj dane logowania");
                    return;
                }

                //zmiana kursora na czas podłączania do bazy
                Cursor.Current = Cursors.WaitCursor;
                //dotyczy bieżacego okna, więc kursor wróci do bazowej wersji przy zamknieciu okienka

                cs = String.Format(cs, tbLogin.Text.Trim(), tbPassword.Text.Trim());

                //dodać folder Utils, a w nim klasę statyczną GlobalData
                GlobalData.connection = new MySqlConnection(cs);
                GlobalData.connection.Open();

                //przed zamknięciem okna zwrócmy kod wyjścia i przekazać go do formy głównej w pasku na dole formy
                this.DialogResult = DialogResult.OK;

                

                Close();


                //połączenie mamy tylko z formy login, więc jej zamknięcie
                //spowoduje rozłączenie z bazą. trzeba zrobić "kontener" na
                //połączenie globalne (klasę statyczną) - folder utils a tam 
                //klase pomocniczą - GlobalData

            }
            catch (Exception exc)
            {
                DialogHelper.Error(exc.Message);
            }
            finally //wykona się zawsze
            {
                //przywrócenie domyslnego kursora 
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
