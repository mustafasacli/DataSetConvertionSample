using Net.FreeORM.DataSetConversion.Conversion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DataSetConvertingSample1_WFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person
            {
                Id = 12,
                Name = "Halid",
                SurName = "Güneri",
                DepartmentId = null,//21
                BirthDate = new DateTime(1985, 12, 1),
                Age = 31
            });

            DataSet ds = personList.GenerateDataSetFromDsObject();
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            lblTableName.Text = string.Format("Table Name: {0}", ds.Tables[0].TableName);
            personList[0].GetSearchParameters();
        }
    }
}