namespace PROG6221_Assignment_Part2_ST10449059
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
            richTextBox1 = new RichTextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            dgvTasks = new DataGridView();
            pnlQuiz = new Panel();
            btnAdd = new Button();
            btnComplete = new Button();
            btnDelete = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Top;
            richTextBox1.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(1503, 517);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged_1;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(12, 0);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(191, 27);
            textBox2.TabIndex = 2;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.Location = new Point(258, -1);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 3;
            button1.Text = "SEND";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnComplete);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox2);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 523);
            panel1.Name = "panel1";
            panel1.Size = new Size(1503, 60);
            panel1.TabIndex = 4;
            // 
            // dgvTasks
            // 
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTasks.Location = new Point(726, 0);
            dgvTasks.Name = "dgvTasks";
            dgvTasks.RowHeadersWidth = 51;
            dgvTasks.Size = new Size(777, 250);
            dgvTasks.TabIndex = 5;
            // 
            // pnlQuiz
            // 
            pnlQuiz.BackColor = SystemColors.ControlLight;
            pnlQuiz.Location = new Point(726, 245);
            pnlQuiz.Name = "pnlQuiz";
            pnlQuiz.Size = new Size(777, 272);
            pnlQuiz.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(745, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add Task ";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnComplete
            // 
            btnComplete.Location = new Point(862, 0);
            btnComplete.Name = "btnComplete";
            btnComplete.Size = new Size(159, 29);
            btnComplete.TabIndex = 5;
            btnComplete.Text = "Mark Completed ";
            btnComplete.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1048, -2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete Task";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1503, 583);
            Controls.Add(pnlQuiz);
            Controls.Add(dgvTasks);
            Controls.Add(panel1);
            Controls.Add(richTextBox1);
            ForeColor = Color.LimeGreen;
            Name = "Form1";
            Text = "CyberShield Security Bot v2.0";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Panel panel1;
        private DataGridView dgvTasks;
        private Panel pnlQuiz;
        private Button btnDelete;
        private Button btnComplete;
        private Button btnAdd;
    }
}
