namespace KalkulatorSederhana
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtLayar = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBackspace = new System.Windows.Forms.Button();
            this.btnPercent = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btnDecimal = new System.Windows.Forms.Button();
            this.btnEquals = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtLayar
            this.txtLayar.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.txtLayar.Location = new System.Drawing.Point(20, 20);
            this.txtLayar.Name = "txtLayar";
            this.txtLayar.ReadOnly = true;
            this.txtLayar.Size = new System.Drawing.Size(270, 45);
            this.txtLayar.TabIndex = 0;
            this.txtLayar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLayar.Text = "0";

            // Baris 1: C, backspace, %, ÷
            this.btnClear.Location = new System.Drawing.Point(20, 90);
            this.btnClear.Size = new System.Drawing.Size(60, 60);
            this.btnClear.Text = "C";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.btnBackspace.Location = new System.Drawing.Point(90, 90);
            this.btnBackspace.Size = new System.Drawing.Size(60, 60);
            this.btnBackspace.Text = "⌫";
            this.btnBackspace.Click += new System.EventHandler(this.btnBackspace_Click);

            this.btnPercent.Location = new System.Drawing.Point(160, 90);
            this.btnPercent.Size = new System.Drawing.Size(60, 60);
            this.btnPercent.Text = "%";
            this.btnPercent.Click += new System.EventHandler(this.btnPercent_Click);

            this.btnDivide.Location = new System.Drawing.Point(230, 90);
            this.btnDivide.Size = new System.Drawing.Size(60, 60);
            this.btnDivide.Text = "÷";
            this.btnDivide.Click += new System.EventHandler(this.OperatorButton_Click);

            // Baris 2: 7 8 9 ×
            this.btn7.Location = new System.Drawing.Point(20, 160);
            this.btn7.Size = new System.Drawing.Size(60, 60);
            this.btn7.Text = "7";
            this.btn7.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn8.Location = new System.Drawing.Point(90, 160);
            this.btn8.Size = new System.Drawing.Size(60, 60);
            this.btn8.Text = "8";
            this.btn8.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn9.Location = new System.Drawing.Point(160, 160);
            this.btn9.Size = new System.Drawing.Size(60, 60);
            this.btn9.Text = "9";
            this.btn9.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btnMultiply.Location = new System.Drawing.Point(230, 160);
            this.btnMultiply.Size = new System.Drawing.Size(60, 60);
            this.btnMultiply.Text = "×";
            this.btnMultiply.Click += new System.EventHandler(this.OperatorButton_Click);

            // Baris 3: 4 5 6 −
            this.btn4.Location = new System.Drawing.Point(20, 230);
            this.btn4.Size = new System.Drawing.Size(60, 60);
            this.btn4.Text = "4";
            this.btn4.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn5.Location = new System.Drawing.Point(90, 230);
            this.btn5.Size = new System.Drawing.Size(60, 60);
            this.btn5.Text = "5";
            this.btn5.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn6.Location = new System.Drawing.Point(160, 230);
            this.btn6.Size = new System.Drawing.Size(60, 60);
            this.btn6.Text = "6";
            this.btn6.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btnMinus.Location = new System.Drawing.Point(230, 230);
            this.btnMinus.Size = new System.Drawing.Size(60, 60);
            this.btnMinus.Text = "−";
            this.btnMinus.Click += new System.EventHandler(this.OperatorButton_Click);

            // Baris 4: 1 2 3 +
            this.btn1.Location = new System.Drawing.Point(20, 300);
            this.btn1.Size = new System.Drawing.Size(60, 60);
            this.btn1.Text = "1";
            this.btn1.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn2.Location = new System.Drawing.Point(90, 300);
            this.btn2.Size = new System.Drawing.Size(60, 60);
            this.btn2.Text = "2";
            this.btn2.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btn3.Location = new System.Drawing.Point(160, 300);
            this.btn3.Size = new System.Drawing.Size(60, 60);
            this.btn3.Text = "3";
            this.btn3.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btnPlus.Location = new System.Drawing.Point(230, 300);
            this.btnPlus.Size = new System.Drawing.Size(60, 60);
            this.btnPlus.Text = "+";
            this.btnPlus.Click += new System.EventHandler(this.OperatorButton_Click);

            // Baris 5: 0 (lebar dobel), ., =
            this.btn0.Location = new System.Drawing.Point(20, 370);
            this.btn0.Size = new System.Drawing.Size(130, 60);
            this.btn0.Text = "0";
            this.btn0.Click += new System.EventHandler(this.AngkaButton_Click);

            this.btnDecimal.Location = new System.Drawing.Point(160, 370);
            this.btnDecimal.Size = new System.Drawing.Size(60, 60);
            this.btnDecimal.Text = ".";
            this.btnDecimal.Click += new System.EventHandler(this.btnDecimal_Click);

            this.btnEquals.Location = new System.Drawing.Point(230, 370);
            this.btnEquals.Size = new System.Drawing.Size(60, 60);
            this.btnEquals.Text = "=";
            this.btnEquals.Click += new System.EventHandler(this.btnEquals_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(315, 455);
            this.Controls.Add(this.txtLayar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnBackspace);
            this.Controls.Add(this.btnPercent);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btnMultiply);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btnDecimal);
            this.Controls.Add(this.btnEquals);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Kalkulator Sederhana";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtLayar;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBackspace;
        private System.Windows.Forms.Button btnPercent;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnDecimal;
        private System.Windows.Forms.Button btnEquals;
    }
}
