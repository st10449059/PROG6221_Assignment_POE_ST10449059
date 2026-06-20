using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG6221_Assignment_Part2_ST10449059
{
    public partial class Form1 : Form
    {
        Chatbot myBot = new Chatbot();

        public Form1()
        {
            InitializeComponent();

            // Wire up the KeyDown event directly to the RichTextBox
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Main Form and Console Styling
            this.BackColor = Color.FromArgb(25, 25, 25);
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.Cyan;
            richTextBox1.Font = new Font("Consolas", 10);
            richTextBox1.SelectionIndent = 10;
            richTextBox1.BorderStyle = BorderStyle.None;

            // Display application branding and initial greeting
            richTextBox1.AppendText(myBot.GetLogo() + Environment.NewLine);
            richTextBox1.SelectionColor = Color.Cyan;
            richTextBox1.AppendText("\nCyberShield: System Online. What is your name, User?\n\n");

            // Drop the initial input prompt
            richTextBox1.SelectionColor = Color.LimeGreen;
            richTextBox1.AppendText("User> ");

            myBot.PlayVoiceGreeting();

            // 2. DataGridView Dark Mode Overhaul
            dgvTasks.BackgroundColor = Color.FromArgb(25, 25, 25);
            dgvTasks.BorderStyle = BorderStyle.None;
            dgvTasks.GridColor = Color.FromArgb(60, 60, 60);
            dgvTasks.RowHeadersVisible = false;
            dgvTasks.AllowUserToAddRows = false;

            dgvTasks.DefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgvTasks.DefaultCellStyle.ForeColor = Color.LightGray;
            dgvTasks.DefaultCellStyle.SelectionBackColor = Color.DarkCyan;
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTasks.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            dgvTasks.EnableHeadersVisualStyles = false;
            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20);
            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.Cyan;
            dgvTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvTasks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            RefreshTaskList();
            richTextBox1.Focus(); // Set focus to the terminal immediately
        }

        private void RefreshTaskList()
        {
            try
            {
                DataTable tasksData = myBot.GetAllTasks();
                dgvTasks.DataSource = tasksData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load tasks into the grid view: " + ex.Message, "UI Sync Notice");
            }
        }

        // This is the new Terminal Logic that replaces the SEND button
        private async void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the user pressed the Enter key
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents the Enter key from just making a blank new line

                // Grab all the text currently in the console
                string[] allLines = richTextBox1.Lines;

                // Look at the very last line where the user was just typing
                string lastLine = allLines[allLines.Length - 1];

                // Remove the "Name> " prompt to isolate just the command they typed
                string promptPrefix = $"{myBot.UserName}> ";
                string input = lastLine.Replace(promptPrefix, "").Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    await Task.Delay(300); // Simulate network thinking

                    if (myBot.UserName == "User")
                    {
                        // Registration Phase
                        myBot.UserName = input;
                        richTextBox1.SelectionColor = Color.Cyan;
                        richTextBox1.AppendText($"\nCyberShield: Setup complete. Welcome, {myBot.UserName}. How can I help you?\n\n");
                    }
                    else
                    {
                        // Pass input to NLP engine
                        string response = myBot.ProcessInput(input);

                        richTextBox1.SelectionColor = Color.Cyan;
                        richTextBox1.AppendText($"\n{response}\n\n");

                        // Dynamic UI Synchronization
                        string cleanInp = input.ToLower();
                        if (cleanInp.Contains("add") ||
                            cleanInp.Contains("complete") || cleanInp.Contains("finish") ||
                            cleanInp.Contains("delete") || cleanInp.Contains("remove") ||
                            cleanInp.Contains("remind"))
                        {
                            RefreshTaskList();
                        }
                    }

                    // Drop a fresh prompt for the next command
                    richTextBox1.SelectionColor = Color.LimeGreen;
                    richTextBox1.AppendText($"{myBot.UserName}> ");
                    richTextBox1.ScrollToCaret();
                }
                else
                {
                    // If they just hit Enter with no text, drop a new prompt on the next line
                    richTextBox1.AppendText($"\n{myBot.UserName}> ");
                    richTextBox1.ScrollToCaret();
                }
            }
            // Basic protection to stop the user from backspacing over the prompt prefix
            else if (e.KeyCode == Keys.Back)
            {
                string[] lines = richTextBox1.Lines;
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    if (lastLine == $"{myBot.UserName}> ")
                    {
                        e.SuppressKeyPress = true; // Stop backspace
                    }
                }
            }
        }

        // Empty event handlers to satisfy the Windows Forms Designer
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged_1(object sender, EventArgs e) { }
    }
}