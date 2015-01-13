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
            this.SuspendLayout();
            // 
            // b_Calibrator
            // 
            this.b_Calibrator.Location = new System.Drawing.Point(12, 155);
            this.b_Calibrator.Name = "b_Calibrator";
            this.b_Calibrator.Size = new System.Drawing.Size(111, 49);
            this.b_Calibrator.TabIndex = 0;
            this.b_Calibrator.Text = "Calibrator";
            this.b_Calibrator.UseVisualStyleBackColor = true;
            // 
            // b_Start
            // 
            this.b_Start.Location = new System.Drawing.Point(159, 155);
            this.b_Start.Name = "b_Start";
            this.b_Start.Size = new System.Drawing.Size(111, 48);
            this.b_Start.TabIndex = 1;
            this.b_Start.Text = "Start Fishing";
            this.b_Start.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.b_Start);
            this.Controls.Add(this.b_Calibrator);
            this.Name = "Form1";
            this.Text = "Fishing Bot v.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_Calibrator;
        private System.Windows.Forms.Button b_Start;
    }
}

