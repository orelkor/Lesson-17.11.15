using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Students2.View
{
    /// <summary>
    /// Interaction logic for Group.xaml
    /// </summary>
    public partial class Group : MetroWindow
    {
        public static Group Instance;

        public Group()
        {
            InitializeComponent();
            Instance = this;
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}
