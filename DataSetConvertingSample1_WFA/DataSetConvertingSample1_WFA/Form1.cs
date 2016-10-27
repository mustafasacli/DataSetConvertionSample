using Net.FreeORM.DataSetConversion.Conversion;
using Net.FreeORM.DataSetConversion.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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

        private void btnIsDifferent_Click(object sender, EventArgs e)
        {
            Person p1 = new Person { Age = 12, Id = 2, Name = "John", SurName = "Adams" };
            Person p2 = p1.Copy();
            p2.Age = 18;
            p2.Name = "James";
            if (p1.Age == p2.Age)
            {
                MessageBox.Show("Age is same.");
            }
            else
            {
                MessageBox.Show("Age is different.");
            }
            if (p1.Name == p2.Name)
            {
                MessageBox.Show("Name is same.");
            }
            else
            {
                MessageBox.Show("Name is different.");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Person p1 = new Person { Age = 12, Id = 2, Name = "John", SurName = "Adams" };
            Person p2 = p1.ObjClone();
            p2.Age = 18;
            p2.Name = "James";
            if (p1.Age == p2.Age)
            {
                MessageBox.Show("Age is same.");
            }
            else
            {
                MessageBox.Show("Age is different.");
            }
            if (p1.Name == p2.Name)
            {
                MessageBox.Show("Name is same.");
            }
            else
            {
                MessageBox.Show("Name is different.");
            }
        }

        private void btnShowChanges_Click(object sender, EventArgs e)
        {
            Person p1 = new Person { Age = 12, Id = 2, Name = "John", SurName = "Adams" };
            Person p2 = p1.Copy();
            p2.Age = 18;
            p2.Name = "James";

            Dictionary<string, object> diff = p1.GetDifferences(p2);
            StringBuilder strBldr = new StringBuilder();

            foreach (string item in diff.Keys)
            {
                strBldr.AppendLine(string.Format("{0} : {1}", item, diff[item]));
            }
            string snc = strBldr.ToString();
            MessageBox.Show(snc);
        }
    }
}