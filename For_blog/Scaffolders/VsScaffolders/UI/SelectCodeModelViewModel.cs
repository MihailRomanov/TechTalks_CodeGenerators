using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsScaffolders.Model;

namespace VsScaffolders.UI
{
    public class SelectCodeModelViewModel
    {
        public CodeTypeModel[] TypeModels { get; set; }
        public CodeTypeModel SelectedType { get; set; }
        public CodePropertyModel[] SelectedProperties { get; set; } = new CodePropertyModel[0];
    }
}
