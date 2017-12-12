using System;
using System.Collections.Generic;
using ICT13580570B2.Models;
using Xamarin.Forms;

namespace ICT13580570B2
{
    public partial class ProductNewPage : ContentPage
    {
        private Product product;

        public ProductNewPage(Product product = null)
        {
            InitializeComponent();

            this.product = product;
            titleLable.Text = product == null ? "เพิ่มสินค้า" : "แก้ไขข้อมูลสินค้า";

            saveButton.Clicked += SaveButton_Clicked;
            cancelButton.Clicked += CancelButton_Clicked;

            categeryPicker.Items.Add("เสื้อ");
            categeryPicker.Items.Add("กางเกง");
            categeryPicker.Items.Add("ถุงเท้า");

            if (product != null)
            {
                nameEntry.Text = product.Name;
                descriptionEditor.Text = product.Description;
                categeryPicker.SelectedItem = product.Category;
                productPriceEntry.Text = product.ProductPrice.ToString();
                sellPriceEntry.Text = product.SellPrice.ToString();
                stockEntry.Text = product.Stock.ToString();
            }


        }



        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var isOk = await DisplayAlert("ยืนยัน", "คุณต้องการบันทึกใช่หรือไม่", "ใช่", "ไม่ใช่");
            if (isOk)
            {
                if (product == null)
                {
                    product = new Product();

                    product.Name = nameEntry.Text;
                    product.Description = descriptionEditor.Text;
                    product.Category = categeryPicker.SelectedItem.ToString();
                    product.ProductPrice = decimal.Parse(productPriceEntry.Text);
                    product.SellPrice = decimal.Parse(sellPriceEntry.Text);
                    product.Stock = int.Parse(stockEntry.Text);
                    var id = App.DbHelper.AddProduct(product);
                    await DisplayAlert("บันทึกสินค้า", "รหัสสินค้าของท่านคือ #" + id, "ตกลง");

                }
                else
                {
                    product.Name = nameEntry.Text;
                    product.Description = descriptionEditor.Text;
                    product.Category = categeryPicker.SelectedItem.ToString();
                    product.ProductPrice = decimal.Parse(productPriceEntry.Text);
                    product.SellPrice = decimal.Parse(sellPriceEntry.Text);
                    product.Stock = int.Parse(stockEntry.Text);
                    var id = App.DbHelper.UpdateProduct(product);
                    await DisplayAlert("บันทึกสำเร็จ", "แก้ไขข้อมูลสินค้าเรียบร้อย", "ตกลง");

                }
                await Navigation.PopModalAsync();
            }
        }

        void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
