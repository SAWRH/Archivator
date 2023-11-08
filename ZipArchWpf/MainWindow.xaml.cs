using ZipArchWpf.DialogChooser;
using System.Windows;
using Ionic.Zip;
using System.IO;
using System.Threading.Tasks;

namespace ZipArchWpf
{
    public partial class MainWindow : Window
    {
        IDialogChooser CurrentDialog;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            bool checkbox_result = false;
            if(checkFile.IsChecked == true)
            {
                checkbox_result = true;
            }
            CurrentDialog = DialogFactory.GetDialog(checkbox_result);
            System.Windows.Forms.DialogResult result = CurrentDialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                if(CurrentDialog.Type == ItemType.File)
                {
                    if(CurrentDialog.SelectedItems.Count > 1)
                    {
                        path_txt.Text = $"{CurrentDialog.SelectedItems.Count} Files selected";
                    }
                    else
                    {
                        path_txt.Text = CurrentDialog.SelectedItems[0].ToString();
                    }
                }
                else
                {
                    if(CurrentDialog.SelectedItems.Count > 1)
                    {
                        path_txt.Text = $"{CurrentDialog.SelectedItems.Count} Folders selected";
                    }
                    else
                    {
                        path_txt.Text = CurrentDialog.SelectedItems[0].ToString();
                    }
                }
            }
            else
            {
                return; 
            }
            

        }
        public async void btnZip_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "ZipFile | *.zip";
            var result = sfd.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                string password_saved = password_txt.Text;
                await Task.Run(()=>Archivate(sfd, password_saved)); 
                
            }
        }

        public async void btnUnZip_Click(object sender, RoutedEventArgs e)
        {
            IDialogChooser newDialog = DialogFactory.GetDialog(false);
            var result = newDialog.ShowDialog();
            if(result==System.Windows.Forms.DialogResult.OK)
            {
                string password_saved = password_txt.Text;
                await Task.Run(()=>Extract(newDialog, password_saved));
                
            }
        }

        private void SetPassword(ZipFile zipFile, string password_saved)
        {
            if(!string.IsNullOrEmpty(password_saved))
            {
                zipFile.Password = password_saved;
                zipFile.Encryption = EncryptionAlgorithm.WinZipAes256;
            }
            
        }

        private void Archivate(System.Windows.Forms.SaveFileDialog sfd, string password_saved)
        {
            if(CurrentDialog.Type == ItemType.File)
            {
                using(ZipFile zipFile = new ZipFile(sfd.FileName))
                {
                    
                    
                    SetPassword(zipFile, password_saved);
                    
                    
                    foreach(string fileName in CurrentDialog.SelectedItems)
                    {
                        zipFile.AddFile(fileName, Path.GetFileName(fileName));
                    }
                    zipFile.Save();
                    MessageBox.Show("File zipped and saved");
                }
            }
            else
            {
                using(ZipFile zipFile = new ZipFile(sfd.FileName))
                {
                    SetPassword(zipFile, password_saved);
                    zipFile.AddDirectory(CurrentDialog.SelectedItems[0].ToString());
                    zipFile.Save();
                    MessageBox.Show("File zipped and saved");
                }
            }
        }

        private void Extract(IDialogChooser newDialog, string password_saved)
        {
            using(ZipFile zipFile = new ZipFile(CurrentDialog.SelectedItems[0].ToString()))
            {
                SetPassword(zipFile, password_saved);
                zipFile.ExtractAll(newDialog.SelectedItems[0]);
            }
            MessageBox.Show("Extraction complited");
        }
    }
}
