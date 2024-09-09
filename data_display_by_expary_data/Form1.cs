using System.Data;
using System.Text;
using System.Windows.Forms;

namespace data_display_by_expary_data
{
    public partial class Form1 : Form
    {
        private readonly string csvFilePath = @"B:/Babbu_BHai/data_display_by_expary_data/data_display_by_expary_data/Ram.csv";

        public Form1()
        {
            InitializeComponent();
        }


        private void LoadDataGrid()
        {
            if (File.Exists(csvFilePath))
            {
                DataTable dataTable = new DataTable();
                using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
                {
                    bool isFirstLine = true;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (isFirstLine)
                        {
                            foreach (var header in values)
                            {
                                dataTable.Columns.Add(header);
                            }
                            isFirstLine = false;
                        }
                        else
                        {
                            // Assuming "Expiry Date" is the name of the column containing the dates
                            int expiryDateColumnIndex = dataTable.Columns["expiry"].Ordinal;

                            // Parse the expiry date
                            DateTime expiryDate;
                            if (DateTime.TryParse(values[expiryDateColumnIndex], out expiryDate))
                            {
                                // If you want to filter by the current month
                                if (expiryDate.Month == DateTime.Now.Month)
                                {
                                    dataTable.Rows.Add(values);
                                }
                            }
                        }
                    }
                }

                dataGridView1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("CSV file not found.");
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
    }
}