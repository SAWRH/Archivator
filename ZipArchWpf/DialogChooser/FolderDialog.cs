using System.Windows.Forms;
using System.Collections.Generic;


namespace ZipArchWpf.DialogChooser
{
    public class FolderDialog : IDialogChooser
    {
        private List<string> folders;
        public ItemType Type => ItemType.Folder;
        public List<string> SelectedItems => folders;
        public FolderDialog()
        {
            folders = new List<string>();
        }
        public DialogResult ShowDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            folders.Clear();
            folders.Add(dialog.SelectedPath);
            return result;
        }
    }
}