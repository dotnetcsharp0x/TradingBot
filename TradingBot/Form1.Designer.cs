namespace TradingBot
{
    partial class Form1
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
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            textBox4 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            listBox3 = new ListBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            groupBox1 = new GroupBox();
            label14 = new Label();
            listBox4 = new ListBox();
            button3 = new Button();
            button2 = new Button();
            label13 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            textBox5 = new TextBox();
            label18 = new Label();
            label19 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 29);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "Введите тикер:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(160, 29);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(618, 3);
            button1.Name = "button1";
            button1.Size = new Size(185, 327);
            button1.TabIndex = 2;
            button1.Text = "Старт";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(160, 132);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 4;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 135);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 3;
            label2.Text = "Введите шаг цены (%):";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(160, 175);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 175);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 5;
            label3.Text = "Цена от:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(160, 221);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 224);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 7;
            label4.Text = "Цена до:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(627, 344);
            label5.Name = "label5";
            label5.Size = new Size(106, 15);
            label5.TabIndex = 9;
            label5.Text = "Загружено акций:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(25, 66);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 11;
            label6.Text = "Название:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(161, 66);
            label7.Name = "label7";
            label7.Size = new Size(0, 15);
            label7.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 101);
            label8.Name = "label8";
            label8.Size = new Size(86, 15);
            label8.TabIndex = 13;
            label8.Text = "Текущая цена:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(160, 101);
            label9.Name = "label9";
            label9.Size = new Size(0, 15);
            label9.TabIndex = 14;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(24, 35);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 244);
            listBox1.TabIndex = 15;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(187, 35);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(120, 244);
            listBox2.TabIndex = 16;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(328, 35);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(120, 244);
            listBox3.TabIndex = 17;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(24, 17);
            label10.Name = "label10";
            label10.Size = new Size(51, 15);
            label10.TabIndex = 18;
            label10.Text = "Тикеры:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(187, 17);
            label11.Name = "label11";
            label11.Size = new Size(57, 15);
            label11.TabIndex = 19;
            label11.Text = "Покупка:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(328, 17);
            label12.Name = "label12";
            label12.Size = new Size(60, 15);
            label12.TabIndex = 20;
            label12.Text = "Продажа:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(listBox4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(listBox2);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(listBox3);
            groupBox1.Controls.Add(label10);
            groupBox1.Location = new Point(1, 367);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(802, 288);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Заявки:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(464, 17);
            label14.Name = "label14";
            label14.Size = new Size(137, 15);
            label14.TabIndex = 24;
            label14.Text = "Запрет на размещение:";
            // 
            // listBox4
            // 
            listBox4.FormattingEnabled = true;
            listBox4.ItemHeight = 15;
            listBox4.Location = new Point(464, 35);
            listBox4.Name = "listBox4";
            listBox4.Size = new Size(120, 244);
            listBox4.TabIndex = 23;
            // 
            // button3
            // 
            button3.Location = new Point(674, 78);
            button3.Name = "button3";
            button3.Size = new Size(113, 37);
            button3.TabIndex = 22;
            button3.Text = "Удалить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(674, 35);
            button2.Name = "button2";
            button2.Size = new Size(113, 37);
            button2.TabIndex = 21;
            button2.Text = "Обновить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(744, 344);
            label13.Name = "label13";
            label13.Size = new Size(0, 15);
            label13.TabIndex = 22;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(24, 267);
            label15.Name = "label15";
            label15.Size = new Size(110, 15);
            label15.TabIndex = 23;
            label15.Text = "Количество лотов:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(25, 306);
            label16.Name = "label16";
            label16.Size = new Size(108, 15);
            label16.TabIndex = 25;
            label16.Text = "Количество сеток:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(160, 306);
            label17.Name = "label17";
            label17.Size = new Size(0, 15);
            label17.TabIndex = 26;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(160, 259);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 27;
            textBox5.Text = "1";
            textBox5.TextChanged += textBox5_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(24, 334);
            label18.Name = "label18";
            label18.Size = new Size(82, 15);
            label18.TabIndex = 28;
            label18.Text = "Ср. значение:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(161, 334);
            label19.Name = "label19";
            label19.Size = new Size(0, 15);
            label19.TabIndex = 29;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 657);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(textBox5);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label13);
            Controls.Add(groupBox1);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "TradingBot";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox listBox1;
        private ListBox listBox2;
        private ListBox listBox3;
        private Label label10;
        private Label label11;
        private Label label12;
        private GroupBox groupBox1;
        private Button button2;
        private Label label13;
        private Button button3;
        private Label label14;
        private ListBox listBox4;
        private Label label15;
        private Label label16;
        private Label label17;
        private TextBox textBox5;
        private Label label18;
        private Label label19;
    }
}
