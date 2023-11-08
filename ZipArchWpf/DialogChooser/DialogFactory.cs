using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZipArchWpf.DialogChooser
{
    public class DialogFactory
    {
        public static IDialogChooser GetDialog(bool IsFile)
        {
            if(IsFile)
            {
                return new FileDialog();
            }
            else
            {
                return new FolderDialog();
                
            }
        }
    }
}