using System;
using System.Collections.Generic;
using System.Data; // PART 3 MULTI-TASKING: Handles local memory row caching
using System.IO;
using System.Media;
using MySql.Data.MySqlClient; // CONNECTIVITY: Bridges application layer directly to local MySQL engine

namespace PROG6221_Assignment_Part2_ST10449059
{
    /// <summary>
    /// Chatbot logic class responsible for processing cybersecurity queries and handling task database records.
    /// </summary>
    public class Chatbot
    {
        // --- DATABASE PIPELINE SETUP ---
        private readonly string connectionString = "Server=localhost;Database=CyberShieldDB;Uid=root;Pwd=@Labs2026!;";

        // TASK 5: Memory state management properties
        public string UserName { get; set; } = "User";
        public string FavoriteTopic { get; set; } = "";

        // TASK 4: Context Tracking variables
        public string LastTopic { get; set; } = "";

        // TASK 3: Defensive security advisory repository
        private string[] _phishingTips = {
            "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
            "Always check the sender's email address for slight misspellings or odd domains.",
            "If a link looks suspicious, hover over it to see the real destination URL before clicking."
        };

        private Random _rng = new Random();

        // TASK 1: System Identifiers
        public string GetLogo()
        {
            return @"
    ::================================::
    || .............................. ||
    || .. C Y B E R   S H I E L D ..  ||
    || .............................. ||
    || ........... v2.0 ............. ||
    ::================================::";
        }

        // TASK 1 & 7: IO operations hardware check
        public void PlayVoiceGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(path))
                {
                    using (var player = new SoundPlayer(path)) { player.Play(); }
                }
            }
            catch { /* Silent fail safety boundary */ }
        }

        // ==========================================
        //         DATABASE CORE LOGIC ENGINE
        // ==========================================

        /// <summary>
        /// Reads all task rows active on the local MySQL schema instance.
        /// </summary>
        public DataTable GetAllTasks()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, reminder_days, is_completed FROM security_tasks ORDER BY id DESC;";
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Database Read Error: " + ex.Message, "System Integrity Notice");
            }
            return dt;
        }

        /// <summary>
        /// Persists a new row entry directly inside the security_tasks database table.
        /// </summary>
        public bool AddNewTask(string taskTitle)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO security_tasks (title, description, reminder_days, is_completed) VALUES (@title, 'Added via chat assistant conversation processing.', 7, 0);";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", taskTitle);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Database Write Error: " + ex.Message, "System Integrity Notice");
                return false;
            }
        }

        /// <summary>
        /// Targets a specific database record row via integer key matching and flips its boolean resolution flag to True.
        /// </summary>
        public bool CompleteTaskById(int taskId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE security_tasks SET is_completed = 1 WHERE id = @id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Database Update Error: " + ex.Message, "System Integrity Notice");
                return false;
            }
        }

        /// <summary>
        /// Discards a target task entity from the persistent database table storage cluster layout matching on explicit ID field.
        /// </summary>
        public bool DeleteTaskById(int taskId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM security_tasks WHERE id = @id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", taskId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Database Dropping Error: " + ex.Message, "System Integrity Notice");
                return false;
            }
        }

        // ==========================================
        //        INTELLIGENT PROCESSING ENGINE
        // ==========================================

        /// <summary>
        /// Intercepts natural sentence strings and maps database mutations directly from the conversation feed pipeline.
        /// </summary>
        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "CyberShield: Input streams cannot be null. Please provide an interaction query.";
            }

            string cleanInput = input.Trim().ToLower();
            string BotName = "CyberShield";

            // --- SMART CHAT INTERCEPTION: ADD TASK COMMAND ---
            // Triggers on patterns like: "add task configure firewalls"
            if (cleanInput.StartsWith("add task "))
            {
                string rawTaskTitle = input.Substring(9).Trim(); // Pulls out everything trailing after "add task "
                if (string.IsNullOrWhiteSpace(rawTaskTitle)) return $"{BotName}: The task title string cannot be blank.";

                bool success = AddNewTask(rawTaskTitle);
                if (success)
                {
                    return $"{BotName}: Success! I have recorded your administrative requirement into the active schema:\n👉 \"{rawTaskTitle}\"";
                }
                return $"{BotName}: Alert. I encountered an operational structural anomaly committing that file record to the database engine.";
            }

            // --- SMART CHAT INTERCEPTION: COMPLETE TASK COMMAND ---
            // Triggers on patterns like: "complete task 5" or "finish task 1"
            if (cleanInput.StartsWith("complete task ") || cleanInput.StartsWith("finish task "))
            {
                string numericSegment = cleanInput.Replace("complete task ", "").Replace("finish task ", "").Trim();
                if (int.TryParse(numericSegment, out int parseTargetId))
                {
                    bool success = CompleteTaskById(parseTargetId);
                    if (success) return $"{BotName}: Confirmed! Operational database entity Record #{parseTargetId} has been successfully flagged as COMPLETED.";
                    return $"{BotName}: Task target sequence record ID #{parseTargetId} could not be altered. Verify that item index exists inside the grid table view container grid layers.";
                }
                return $"{BotName}: I failed to resolve an execution index identifier parameter. Syntax protocol usage: 'complete task [ID number]'";
            }

            // --- SMART CHAT INTERCEPTION: DELETE TASK COMMAND ---
            // Triggers on patterns like: "delete task 3" or "remove task 12"
            if (cleanInput.StartsWith("delete task ") || cleanInput.StartsWith("remove task "))
            {
                string numericSegment = cleanInput.Replace("delete task ", "").Replace("remove task ", "").Trim();
                if (int.TryParse(numericSegment, out int parseTargetId))
                {
                    bool success = DeleteTaskById(parseTargetId);
                    if (success) return $"{BotName}: Purge execution successful. Database Record #{parseTargetId} dropped from server memory.";
                    return $"{BotName}: Failure to drop target entity segment index #{parseTargetId}. Ensure targeted rows correspond with live schema keys.";
                }
                return $"{BotName}: Parsing target structure fault. Syntax pattern validation requirement: 'delete task [ID number]'";
            }

            // --- TASK 4: SEAMLESS CONVERSATION FLOW ---
            if (cleanInput.Contains("another") || cleanInput.Contains("more") || cleanInput.Contains("explain"))
            {
                if (LastTopic == "phishing") return $"{BotName}: Here is another phishing tip: {_phishingTips[_rng.Next(_phishingTips.Length)]}";
                if (LastTopic == "password") return $"{BotName}: Another password tip: Use a reputable password manager to store complex, unique passwords.";
                if (FavoriteTopic == "privacy") return $"{BotName}: Since you're interested in privacy, you should also review which apps have location permissions.";

                return $"{BotName}: I'd be happy to explain more! Try asking about 'passwords', 'browsing', or 'scams'.";
            }

            // --- TASK 6: SENTIMENT DETECTION ---
            if (cleanInput.Contains("worried") || cleanInput.Contains("scared") || cleanInput.Contains("frustrated"))
            {
                return $"{BotName}: It's completely understandable to feel that way. Scammers can be very convincing. \nTip: {_phishingTips[_rng.Next(_phishingTips.Length)]}";
            }

            // --- TASK 5: MEMORY (STORE) ---
            if (cleanInput.Contains("interested in privacy") || cleanInput.Contains("privacy"))
            {
                FavoriteTopic = "privacy";
                LastTopic = "privacy";
                return $"{BotName}: Great! I'll remember that you're interested in privacy, {UserName}.";
            }

            // --- TASK 2: KEYWORD RECOGNITION (CYBERSECURITY GUIDANCE) ---
            if (cleanInput.Contains("password"))
            {
                LastTopic = "password";
                return $"{BotName}: (NIST Standard) Use at least 12 characters. A phrase like 'BlueElephantJump!' is hard to crack.";
            }

            if (cleanInput.Contains("phishing") || cleanInput.Contains("scam"))
            {
                LastTopic = "phishing";
                string definition = "Phishing is a social engineering attack to steal info via fake links.";
                return $"{BotName}: {definition} \nRandom Tip: {_phishingTips[_rng.Next(_phishingTips.Length)]}";
            }

            if (cleanInput.Contains("browsing"))
            {
                return $"{BotName}: Safe browsing means using TLS/HTTPS protocols and checking for the padlock icon.";
            }

            if (cleanInput.Contains("how are you"))
            {
                return $"{BotName}: I'm doing great, {UserName}! My firewall is strong and I'm ready to assist.";
            }

            if (cleanInput.Contains("purpose"))
            {
                return $"{BotName}: My purpose is to serve as your personal cybersecurity assistant.";
            }

            // --- TASK 7: DEFAULT FALLBACK ---
            return $"{BotName}: I'm not sure how to respond to that. Try asking about 'passwords', 'browsing', or 'scams'.\n💡 (Or manage tasks with 'add task [name]', 'complete task [ID]', or 'delete task [ID]')";
        }
    }
}