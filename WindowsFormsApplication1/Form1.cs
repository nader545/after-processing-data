using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_file_Click(object sender, EventArgs e)
        {
            int column1;
            string column2, column3;  
            string date_file = txt_path.Text;
            string inputFilePath = "E:\\" + date_file + ".pu";                      
            DataTable dt = new DataTable();
            dt = CreateDataTableFromFile(inputFilePath, 15, 1);
            string count = dt.Rows[0][1].ToString();
            string date = dt.Rows[0][0].ToString();

            if (date != date_file)
            {
                MessageBox.Show("invalid file !: file name don’t match with date in first line");
                return;
            }
            if (Convert.ToInt32(count) != Convert.ToInt32(dt.Rows.Count)-1)
            {
                MessageBox.Show("invalid file !:count on first line don’t match with lines count -1");
                return;
            }
            string OutputFilePath = (@"E:\" + date_file + ".ur");
            StreamWriter file = new StreamWriter(OutputFilePath);
            for (int i = 1; i < Convert.ToInt32(dt.Rows.Count); i++)
            {
                try
                {
                    column1 = Convert.ToInt32(dt.Rows[i][0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("invalid file");                    
                    return;
                }
                column2 = dt.Rows[i][1].ToString();
                column3 = dt.Rows[i][2].ToString();
                file.WriteLine(OutputFilePath, (column1 + "," + column2 + "," + column3));
            }               
        }



//-------------------------------------------------------------------------------------------------------    
        private DataTable CreateDataTableFromFile(string inputFilePath, int count, int file_extension)
        {

            int lines;
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            if (file_extension == 1)
            {
                lines = count;
                for (int i = 1; i <= lines; i++)
                {
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = string.Format("f{0}", i);
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                }

                StreamReader file = new StreamReader(@inputFilePath, Encoding.Default);
                string input;
                while ((input = file.ReadLine()) != null)
                {
                    //string[] s = input.Split(new string[] { "||" }, StringSplitOptions.None);
                    string[] arr = input.Split(new char[] { ',' });
                    dr = dt.NewRow();

                    for (int i = 1; i <= lines; i++)
                    {
                        string col = string.Format("f{0}", i);
                        try
                        {
                            dr[col] = arr[i - 1];

                        }
                        catch (Exception e) { }

                    }
                    dt.Rows.Add(dr);

                }
                file.Close();
            }          
            return dt;
        }

    }
}
