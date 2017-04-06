using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuantitaiveTransactionDLL;

namespace master_program
{
    public partial class stockWarning : Form
    {
        public stockWarning()
        {
            InitializeComponent();

            tboxstockSelected.AutoCompleteCustomSource.AddRange( DBUtility.GetStockList());
        }
    }
}
