using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;



namespace Fond
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

        }

        private void GridFond_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            FondsData fond = new FondsData();
            int sv_id = 0;
            if (GridFond.CurrentColumn != null)
            {
                sv_id = (int)fond.dt.Rows[GridFond.SelectedIndex]["ID_отчёта"];
            }

            Window1 Win1 = new Window1();
            Win1.id_rep = sv_id;
            Win1.Show();
            
        }

       
    }

    public class FondsData
    {
        public ObservableCollection<FondParam> FondParamCollection { get; set; }

       
      

        public DataTable dt;
        public FondsData()
        {
            FondParamCollection = new ObservableCollection<FondParam>();
            string connectionString = " User ID=user; Password=pasword; Persist Security Info=False; Initial Catalog=GIS; Data Source=IPGG-SQL";
            string sql = "SELECT f.ID_отчёта, f.Год_договора, f.Номер_договора, f.Название, f.Организации  FROM Фонды_отчёты f ORDER BY f.Год_договора";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                dt = new DataTable();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, connection);
                sqlAdapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FondParam FondParamFill = new FondParam
                    {
                        Id = (int)dt.Rows[i]["ID_отчёта"],
                        God = (int)dt.Rows[i]["Год_договора"],
                        Nomer = (string)dt.Rows[i]["Номер_договора"],
                        Name = (string)dt.Rows[i]["Название"],
                        Organization = (string)dt.Rows[i]["Организации"],
                    };

                   FondParamCollection.Add(FondParamFill);
                }
                connection.Close();
            }

        }


        public class FondParam
        {
            public int Id { get; set; }
            public int God { get; set; }
            public string Nomer { get; set; }
            public string Name { get; set; }
            public string Organization { get; set; }
        }
    }


}
