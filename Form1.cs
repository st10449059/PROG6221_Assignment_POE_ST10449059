using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG6221_Assignment_Part2_ST10449059
{
    public partial class Form1 : Form
    {
        // Task 8: OOP - Instance of the logic class
        Chatbot myBot = new Chatbot();

        public Form1()
        {
            InitializeComponent();
            // Allows the user to press 'Enter' on their keyboard to send messages
            this.AcceptButton = button1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Task 1: UI Branding and Theme
            this.BackColor = Color.FromArgb(25, 25, 25);
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.Cyan;
            richTextBox1.Font = new Font("Consolas", 10);
            richTextBox1.SelectionIndent = 10; // Adds a neat margin to the text

            // Display Logo and Initial Greeting
            richTextBox1.AppendText(myBot.GetLogo() + Environment.NewLine);
            richTextBox1.SelectionColor = Color.Cyan;
            richTextBox1.AppendText("\nCyberShield: System Online. What is your name, User?\n");

            // Task 1: Multimedia - Voice Greeting
            myBot.PlayVoiceGreeting();

            textBox2.Focus();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string input = textBox2.Text.Trim();

            // Task 7: Error handling - ignore empty input to prevent crashes
            if (!string.IsNullOrWhiteSpace(input))
            {
                // 1. Display User Message with Timestamp
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.AppendText($"\n[{DateTime.Now:HH:mm}] {myBot.UserName}: {input}\n");
                textBox2.Clear();

                // 2. Simulate "Thinking" Delay for Natural Flow
                await Task.Delay(500);

                // 3. Logic Branching: Setup vs. Conversation
                if (myBot.UserName == "User")
                {
                    // Task 5: Memory - Store the User's name
                    myBot.UserName = input;
                    richTextBox1.SelectionColor = Color.Cyan;
                    richTextBox1.AppendText($"CyberShield: Setup complete. Welcome, {myBot.UserName}. How can I help you stay safe today?\n");
                }
                else
                {
                    // Task 2, 3, & 6: Process Cybersecurity Logic
                    string response = myBot.ProcessInput(input);
                    richTextBox1.SelectionColor = Color.Cyan;
                    richTextBox1.AppendText(response + Environment.NewLine);
                }

                // 4. UI Polish: Divider and Auto-Scroll
                richTextBox1.SelectionColor = Color.FromArgb(40, 40, 40);
                richTextBox1.AppendText("________________________________________________\n");
                richTextBox1.ScrollToCaret();
                textBox2.Focus();
            }
        }

        // Keep this method to prevent the Designer Error from image_d27735.png
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}