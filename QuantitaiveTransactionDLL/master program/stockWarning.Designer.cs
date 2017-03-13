namespace master_program
{
    partial class stockWarning
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddWarning = new System.Windows.Forms.Button();
            this.btnDeleteWarning = new System.Windows.Forms.Button();
            this.btnCloseWarning = new System.Windows.Forms.Button();
            this.tboxstockSelected = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddWarning
            // 
            this.btnAddWarning.Location = new System.Drawing.Point(12, 395);
            this.btnAddWarning.Name = "btnAddWarning";
            this.btnAddWarning.Size = new System.Drawing.Size(75, 23);
            this.btnAddWarning.TabIndex = 0;
            this.btnAddWarning.Text = "+ 添加预警";
            this.btnAddWarning.UseVisualStyleBackColor = true;
            // 
            // btnDeleteWarning
            // 
            this.btnDeleteWarning.Location = new System.Drawing.Point(201, 395);
            this.btnDeleteWarning.Name = "btnDeleteWarning";
            this.btnDeleteWarning.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteWarning.TabIndex = 1;
            this.btnDeleteWarning.Text = "- 删除所选";
            this.btnDeleteWarning.UseVisualStyleBackColor = true;
            // 
            // btnCloseWarning
            // 
            this.btnCloseWarning.Location = new System.Drawing.Point(393, 395);
            this.btnCloseWarning.Name = "btnCloseWarning";
            this.btnCloseWarning.Size = new System.Drawing.Size(75, 23);
            this.btnCloseWarning.TabIndex = 2;
            this.btnCloseWarning.Text = "X 关闭所选";
            this.btnCloseWarning.UseVisualStyleBackColor = true;
            // 
            // tboxstockSelected
            // 
            this.tboxstockSelected.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tboxstockSelected.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tboxstockSelected.Location = new System.Drawing.Point(12, 12);
            this.tboxstockSelected.Name = "tboxstockSelected";
            this.tboxstockSelected.Size = new System.Drawing.Size(264, 21);
            this.tboxstockSelected.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(720, 350);
            this.dataGridView1.TabIndex = 4;
            // 
            // stockWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 430);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tboxstockSelected);
            this.Controls.Add(this.btnCloseWarning);
            this.Controls.Add(this.btnDeleteWarning);
            this.Controls.Add(this.btnAddWarning);
            this.Name = "stockWarning";
            this.Text = "股票预警";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddWarning;
        private System.Windows.Forms.Button btnDeleteWarning;
        private System.Windows.Forms.Button btnCloseWarning;
        private System.Windows.Forms.TextBox tboxstockSelected;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

