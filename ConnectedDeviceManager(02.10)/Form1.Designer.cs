namespace ConnectedDeviceManager_02._10_
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

        private void InitializeComponent()
        {
            listView1 = new ListView();
            button1 = new Button();
            button2 = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(20, 20);
            listView1.Name = "listView1";
            listView1.Size = new Size(600, 300);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;

            listView1.View = View.Details;
            listView1.FullRowSelect = true; 
            listView1.GridLines = true;

            listView1.Columns.Add("Тип устройства", 150);  
            listView1.Columns.Add("Имя", 200);             
            listView1.Columns.Add("Статус", 100);          
            listView1.Columns.Add("Модель", 150);          
            listView1.Columns.Add("Идентификатор", 150);
            // 
            // button1
            // 
            button1.Location = new Point(20, 350);
            button1.Name = "button1";
            button1.Size = new Size(100, 30);
            button1.TabIndex = 1;
            button1.Text = "Обновить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(btnRefresh_Click);
            // 
            // button2
            // 
            button2.Location = new Point(143, 350);
            button2.Name = "button2";
            button2.Size = new Size(100, 30);
            button2.TabIndex = 2;
            button2.Text = "Отключить";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(626, 20);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(320, 300);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(962, 450);
            Controls.Add(richTextBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView1);
            Name = "Form1";
            Text = "Управление подключенными устройствами";
            ResumeLayout(false);
        }

        private ListView listView1;
        private Button button1;
        private Button button2;
        private RichTextBox richTextBox1;
    }
}
