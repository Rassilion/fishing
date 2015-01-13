namespace Fishing
{
    partial class Form1
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
            this.b_Calibrator = new System.Windows.Forms.Button();
            this.b_Start = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // b_Calibrator
            // 
            this.b_Calibrator.Location = new System.Drawing.Point(465, 12);
            this.b_Calibrator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_Calibrator.Name = "b_Calibrator";
            this.b_Calibrator.Size = new System.Drawing.Size(83, 40);
            this.b_Calibrator.TabIndex = 0;
            this.b_Calibrator.Text = "Calibrator";
            this.b_Calibrator.UseVisualStyleBackColor = true;
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(465, 56);
            this.b_Start.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(83, 39);
            this.b_Start.TabIndex = 1;
            this.b_Start.Text = "Start Fishing";
            this.b_Start.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Fishing.Properties.Resources.katsura;
            this.pictureBox1.Location = new System.Drawing.Point(9, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(451, 298);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 337);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.b_Calibrator);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Fishing Bot v.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Calibrator;
        private System.Windows.Forms.Button b_Start;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

