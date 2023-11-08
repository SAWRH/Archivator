using System.Windows.Forms;
using System.Collections.Generic;


namespace ZipArchWpf.DialogChooser
{
    public enum ItemType{ File, Folder }
    public interface IDialogChooser
    {
        ItemType Type { get; }
        List<string> SelectedItems { get; }
        DialogResult ShowDialog();
    }
}