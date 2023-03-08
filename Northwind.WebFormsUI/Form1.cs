using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        public Form1()  
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }
        IProductService _productService;
        ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgvProduct.DataSource = _productService.GetAll();
        }
        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            LoadCategoryToUpdateComboBox();

            LoadCategoriesByCategoryId();
        }

        private void LoadCategoryToUpdateComboBox()
        {
            cbxUpdateCategoryId.DataSource = _categoryService.GetAll();
            cbxUpdateCategoryId.DisplayMember = "CategoryName";
            cbxUpdateCategoryId.ValueMember = "CategoryId";
        }

        private void LoadCategoriesByCategoryId()
        {
            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {

            }

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtProductName.Text))
            {
                dgvProduct.DataSource = _productService.GetProductsByProductName(txtProductName.Text);
            }
            else
            {
                LoadProducts();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add(new Product
            {
                CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName = txtProductName2.Text,
                QuantityPerUnit = txtQuantityPerUnit.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                UnitsInStock = Convert.ToInt16(txtUnitsInStock.Text)
            });
            MessageBox.Show("Ürün Eklendi.");
            LoadProducts();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product
            {
                ProductId = Convert.ToInt32(dgvProduct.CurrentRow.Cells[0].Value),
                CategoryId = Convert.ToInt32(cbxUpdateCategoryId.SelectedValue),
                ProductName = tbxUpdateProductName.Text,
                QuantityPerUnit = txtUpdateQuantityPerUnit.Text,
                UnitPrice = Convert.ToDecimal(txtUpdateUnitPrice.Text),
                UnitsInStock = Convert.ToInt16(txtUpdateUnitsInStock.Text)
            });
            MessageBox.Show("Ürün Güncellendi..");
            LoadProducts();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvProduct.CurrentRow;
            tbxUpdateProductName.Text = row.Cells[2].Value.ToString();
            cbxUpdateCategoryId.SelectedValue = row.Cells[1].Value.ToString();
            txtUpdateUnitPrice.Text = row.Cells[3].Value.ToString();
            txtUpdateQuantityPerUnit.Text = row.Cells[4].Value.ToString();
            txtUpdateUnitsInStock.Text = row.Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProduct.CurrentRow != null)
            {
                try
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgvProduct.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürün Silindi..");
                    LoadProducts();
                }
                catch
                {

                }

            }

        }
    }
}
