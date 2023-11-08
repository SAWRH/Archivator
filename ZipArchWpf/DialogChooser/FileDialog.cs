using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace ZipArchWpf.DialogChooser
{
    public class FileDialog : IDialogChooser
    {
        private List<string> files;
        public ItemType Type => ItemType.File;
        public List<string> SelectedItems => files;
        public FileDialog()
        {
            files = new List<string>();
        }
        public DialogResult ShowDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            var result = ofd.ShowDialog();
            files = ofd.FileNames.ToList();
            return result;
        }
    }
}