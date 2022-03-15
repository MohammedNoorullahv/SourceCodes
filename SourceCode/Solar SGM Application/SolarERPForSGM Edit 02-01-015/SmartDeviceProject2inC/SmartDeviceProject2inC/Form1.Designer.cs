namespace SmartDeviceProject2inC
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.button1 = new System.Windows.Forms.Button();
            this.abbrevTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.aHGroupDataSet = new SmartDeviceProject2inC.AHGroupDataSet();
            this.abbrevTableTableAdapter = new SmartDeviceProject2inC.AHGroupDataSetTableAdapters.AbbrevTableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.abbrevTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aHGroupDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // abbrevTableBindingSource
            // 
            this.abbrevTableBindingSource.DataMember = "AbbrevTable";
            this.abbrevTableBindingSource.DataSource = this.aHGroupDataSet;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataSource = this.abbrevTableBindingSource;
            this.dataGrid1.Location = new System.Drawing.Point(3, 3);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(234, 200);
            this.dataGrid1.TabIndex = 1;
            // 
            // aHGroupDataSet
            // 
            this.aHGroupDataSet.DataSetName = "AHGroupDataSet";
            this.aHGroupDataSet.Prefix = "";
            this.aHGroupDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // abbrevTableTableAdapter
            // 
            this.abbrevTableTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.button1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.abbrevTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aHGroupDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private AHGroupDataSet aHGroupDataSet;
        private System.Windows.Forms.BindingSource abbrevTableBindingSource;
        private SmartDeviceProject2inC.AHGroupDataSetTableAdapters.AbbrevTableTableAdapter abbrevTableTableAdapter;
    }
}

