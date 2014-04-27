using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EFApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayCustomers();
            DisplayCountries();
            DisplayTitles();
        }

        private void DisplayCustomers()
        {
            using (var dc = new NorthwindEntities())
            {
                var customers = from c in dc.Customers
                                select new
                                {
                                    CusomerId = c.CustomerID,
                                    Name = c.ContactName,
                                    Title = c.ContactTitle,
                                    Country = c.Country
                                };

                if (!string.IsNullOrWhiteSpace(txtCustomerName.Text))
                    customers = customers.Where(c => c.Name.Contains(txtCustomerName.Text));
                if (cmbTitle.SelectedIndex > 0)
                    customers = customers.Where(c => c.Title == cmbTitle.SelectedItem );
                if (cmbCountry.SelectedIndex > 0)
                    customers = customers.Where(c => c.Country == cmbCountry.SelectedItem );
                dgvCustomers.DataSource = customers;
				int z = 0;
				z++;
            }
        }

        private void DisplayCountries()
        {
            using (var ct = new NorthwindEntities())
            { 
                var countries = (from c in ct.Customers orderby c.Country select c.Country).Distinct();
                cmbCountry.Items.AddRange(countries.ToArray());
                cmbCountry.SelectedIndex = 0;
            }
        }

        private void DisplayTitles()
        {
            using (var ct = new NorthwindEntities())
            {
                var titles = (from c in ct.Customers orderby c.ContactTitle select c.ContactTitle).Distinct();
                cmbTitle.Items.AddRange(titles.ToArray());
                cmbTitle.SelectedIndex = 0;
            }
        }

        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

    }
}
