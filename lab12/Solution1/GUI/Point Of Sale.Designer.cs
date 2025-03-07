namespace GUI
{
    partial class PointOfSale
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCustomer = new Button();
            SuspendLayout();
            // 
            // btnCustomer
            // 
            btnCustomer.AccessibleName = "btnCustomer";
            btnCustomer.Location = new Point(141, 184);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(94, 29);
            btnCustomer.TabIndex = 0;
            btnCustomer.Text = "Customer";
            btnCustomer.UseVisualStyleBackColor = true;
            btnCustomer.Click += btnCustomer_Click;
            // 
            // PointOfSale
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCustomer);
            Name = "PointOfSale";
            Text = "PointOfSale";
            Load += PointOfSale_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnCustomer;
    }
}
