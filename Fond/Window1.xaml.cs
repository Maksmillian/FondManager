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
using System.IO;
using System.Diagnostics;



namespace Fond
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        public int id_rep;
        public string God;
        public string Nomer;
        public string Annot;
        public string Org;
        public string Slova;
        public string NameReport;
        public string Otv_isp;
        public string Prim;
        public string Link_Main;
        public int UserRights;
        public string Slova_new;
        public string Annot_new;
        public string Name_new;
        public string Prim_new;
        public string connectionString = " User ID=user; Password=password; Persist Security Info=False; Initial Catalog=GIS; Data Source=IPGG-SQL";



        public string[] NameDirectory;
       
        public string SelectPapka;



        public Window1()
        {
            InitializeComponent();
            GetUserRights();
        }

        public void GetUserRights()
        {
            string UserName = Environment.UserName;
            string UserDomain = Environment.UserDomainName;
            string UserLogin =  UserDomain + @"\" + UserName;
           
            string sql = string.Format( "SELECT Роль FROM Фонды_пользователи  WHERE Login={0}", UserLogin) ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                connection.Open();

                SqlDataAdapter dad = new SqlDataAdapter();
                dad.SelectCommand = new SqlCommand("SELECT  Роль FROM Фонды_пользователи  WHERE Login=@UserLogin", connection);
                dad.SelectCommand.Parameters.Add("@UserLogin",SqlDbType.NVarChar).Value=UserLogin;
               
               
                
                DataTable dt = new DataTable();
                dad.Fill(dt);
               
                
                    foreach (DataRow dr in dt.Rows )
                    {
                        UserRights = int.Parse(dr["Роль"].ToString());
                        

                    }
                
                connection.Close();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ButtSave.Visibility = Visibility.Hidden;
            if (UserRights!=1)
            {
                ButtRedact.Visibility = Visibility.Hidden ;
                ButtSave.Visibility = Visibility.Hidden;
            }
           
            string sql = $"SELECT f.Год_договора, f.Номер_договора, f.Название, f.Организации, f.Ответственный_исполнитель, f.Примечание, f.Ключевые_слова, f.Аннотация, f.Ссылка_корневая  FROM Фонды_отчёты f where f.ID_отчёта = {id_rep}";
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, connection);
                sqlAdapter.Fill(dt);

                God = dt.Rows[0]["Год_договора"].ToString();
                Nomer = dt.Rows[0]["Номер_договора"].ToString();
                NameReport = dt.Rows[0]["Название"].ToString();
                Org = dt.Rows[0]["Организации"].ToString();
                Slova = dt.Rows[0]["Ключевые_слова"].ToString();
                Annot = dt.Rows[0]["Аннотация"].ToString();
                Otv_isp = dt.Rows[0]["Ответственный_исполнитель"].ToString();
                Prim = dt.Rows[0]["Примечание"].ToString();
                Link_Main += dt.Rows[0]["Ссылка_корневая"].ToString();
                connection.Close();
            }

            tb_god.Text = God;
            tb_annot.Text = Annot;
            tb_isp.Text = Otv_isp;
            tb_name.Text = NameReport;
            tb_nomer.Text = Nomer;
            tb_org.Text = Org;
            tb_prim.Text = Prim;
            tb_slova.Text = Slova;

            NameDirectory =  Directory.GetFileSystemEntries(Link_Main, ".", SearchOption.TopDirectoryOnly);
           
            foreach (var item in NameDirectory)
            {
                //df.FileInfo1.Add(new DirectoryInfo(item).Name);
                ListBoxFile.Items.Add(new DirectoryInfo(item).Name);
            }
            //ListBoxFile.ItemsSource = df.FileInfo1;
        }

       

        private void ButtBack_Click(object sender, RoutedEventArgs e)
        {
            ListBoxFile.Items.Clear();
          
            NameDirectory = Directory.GetFileSystemEntries(Link_Main, ".", SearchOption.TopDirectoryOnly);
            foreach (var item in NameDirectory)
            {
                ListBoxFile.Items.Add(new DirectoryInfo(item).Name);
                //df.FileInfo1.Add(new DirectoryInfo(item).Name);

            }
          //  ListBoxFile.ItemsSource = df.FileInfo1;

        }

        private void ButtRedact_Click(object sender, RoutedEventArgs e)
        {
            ButtRedact.Visibility = Visibility.Hidden;
            ButtSave.Visibility = Visibility.Visible;
            Canvas.Background = Brushes.LightYellow;
            tb_name.IsReadOnly = false;
            tb_prim.IsReadOnly = false;
            tb_slova.IsReadOnly = false;
            tb_annot.IsReadOnly = false;

        }

        private void ButtSave_Click(object sender, RoutedEventArgs e)
        {

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@tb_name", tb_name.Text);
            param[1] = new SqlParameter("@tb_prim", tb_prim.Text);
            param[2] = new SqlParameter("@tb_slova", tb_slova.Text);
            param[3] = new SqlParameter("@tb_annot", tb_annot.Text);
            param[4] = new SqlParameter("@id_rep", id_rep);

            int i = 0;
            string new_str = "";

            if (tb_slova.Text != Slova)
            {
                new_str = "Ключевые слова";
                i += 1;
            }

            if (tb_name.Text != NameReport)
            {
                new_str += " Название отчёта";
                i += 1;
            }

            if (tb_prim.Text != Prim)
            {
                new_str += " Примечание";
                i += 1;
            }

            if (tb_annot.Text != Annot)
            {
                new_str += " Аннотация";
                i += 1;
            }

            string mtext = "Было изменено поле " + new_str + ". Сохранить изменения ? ";
            if (i > 1)
            {
                mtext = "Были изменены поля: " + new_str + ". Сохранить изменения?";
            }
            else if (i==0)
            {
                if (  MessageBox.Show("Ни одно поле не было изменено!"+ '\n' +"Продолжить изменения?", "Ошибка сохранения", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.OK)
                {
                    return;

                }
                else
                {
                    ButtRedact.Visibility = Visibility.Visible;
                    ButtSave.Visibility = Visibility.Hidden;
                    Canvas.Background = Brushes.AliceBlue;
                    tb_name.IsReadOnly = true;
                    tb_prim.IsReadOnly = true;
                    tb_slova.IsReadOnly = true;
                    tb_annot.IsReadOnly = true;
                }
              
            }


            if (i>0)
            {

                if (MessageBox.Show(mtext, "Вы уверены?", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK) 
                {
                   
                    ButtRedact.Visibility = Visibility.Visible;
                    ButtSave.Visibility = Visibility.Hidden;
                    Canvas.Background = Brushes.AliceBlue;
                    tb_name.IsReadOnly = true;
                    tb_prim.IsReadOnly = true;
                    tb_slova.IsReadOnly = true;
                    tb_annot.IsReadOnly = true;


                    //using (SqlConnection connection = new SqlConnection(connectionString))
                    //{
                        //connection.Open();
                        //SqlCommand sqlcomm = new SqlCommand();
                        //sqlcomm.CommandText = $"UPDATE Фонды_отчёты SET Название = @tb_name, Примечание = @tb_prim, Ключевые_слова = @tb_slova, Аннотация = @tb_annot WHERE ID_отчёта = @id_rep";
                        //sqlcomm.Parameters.AddRange(param);
                        //sqlcomm.Connection = connection;
                        //sqlcomm.ExecuteNonQuery();
                        //connection.Close();
                    //}
                } 
            }
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ButtRedact.Visibility == Visibility.Hidden)
            {
                if (MessageBox.Show("Сделанные изменения не были сохранены. Точно закрыть форму?", "Вы уверены?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ListBoxFile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = 0;
           
          
           
                if (ListBoxFile.SelectedIndex >= 0)
                {
                    index = ListBoxFile.SelectedIndex;
                  
                    if (new DirectoryInfo(NameDirectory[index]).Extension =="")
                    {
                        ListBoxFile.Items.Clear();
                        SelectPapka = NameDirectory[index];
                        NameDirectory = Directory.GetFileSystemEntries(SelectPapka, ".", SearchOption.TopDirectoryOnly);
                        foreach (var item in NameDirectory)
                        {
                       
                          ListBoxFile.Items.Add(new DirectoryInfo(item).Name);

                        }

                    }
                    else
                    {
                        Process.Start(NameDirectory[ListBoxFile.SelectedIndex]);
                    }

                }
        }
       

    }
          
}
