namespace AWP_CollectionOrder
{
    partial class CameraDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.zoomcomboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pbViewFinder = new System.Windows.Forms.PictureBox();
            this.ReleaseprogressBar = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewFinder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zoomcomboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 530);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 80);
            this.panel1.TabIndex = 0;
            // 
            // zoomcomboBox1
            // 
            this.zoomcomboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoomcomboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zoomcomboBox1.FormattingEnabled = true;
            this.zoomcomboBox1.Items.AddRange(new object[] {
            "Нет"});
            this.zoomcomboBox1.Location = new System.Drawing.Point(287, 12);
            this.zoomcomboBox1.Name = "zoomcomboBox1";
            this.zoomcomboBox1.Size = new System.Drawing.Size(91, 32);
            this.zoomcomboBox1.TabIndex = 2;
            this.zoomcomboBox1.SelectedIndexChanged += new System.EventHandler(this.zoomcomboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Увеличение изображения:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(435, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 74);
            this.button1.TabIndex = 0;
            this.button1.Text = "Фотографировать";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbViewFinder
            // 
            this.pbViewFinder.Location = new System.Drawing.Point(6, 7);
            this.pbViewFinder.Name = "pbViewFinder";
            this.pbViewFinder.Size = new System.Drawing.Size(648, 486);
            this.pbViewFinder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbViewFinder.TabIndex = 1;
            this.pbViewFinder.TabStop = false;
            this.pbViewFinder.Paint += new System.Windows.Forms.PaintEventHandler(this.pbViewFinder_Paint);
            // 
            // ReleaseprogressBar
            // 
            this.ReleaseprogressBar.Location = new System.Drawing.Point(8, 499);
            this.ReleaseprogressBar.Name = "ReleaseprogressBar";
            this.ReleaseprogressBar.Size = new System.Drawing.Size(648, 26);
            this.ReleaseprogressBar.TabIndex = 2;
            // 
            // CameraDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 610);
            this.ControlBox = false;
            this.Controls.Add(this.ReleaseprogressBar);
            this.Controls.Add(this.pbViewFinder);
            this.Controls.Add(this.panel1);
            this.Name = "CameraDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Получение фотографии";
            this.Shown += new System.EventHandler(this.CameraDialog_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraDialog_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewFinder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pbViewFinder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox zoomcomboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar ReleaseprogressBar;
    }
}