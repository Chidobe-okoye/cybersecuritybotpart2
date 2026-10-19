using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CybersecurityAwarenessBotPart2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Complete Part 2 Implementation supporting Random Responses, Sentiment Detection,
    /// Memory/Recall, and Follow-up Topic Flow.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Session Memory
        private string userName = string.Empty;
        private bool isAwaitingName = true;
        private string lastTopic = string.Empty;
        private string userInterest = string.Empty;

        // Random Number Generator for dynamic responses
        private readonly Random random = new Random();

        // Part 2 Data Structures: Random response repository mapped by topic keywords
        private readonly Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>
        {
            {
                "phishing", new List<string>
                {
                    "Phishing is a fraudulent attempt to steal sensitive data via fake emails or links. Always verify the sender address!",
                    "Be cautious of urgent emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                    "Look closely at the email header and URLs before clicking. Phishing links often spoof legitimate domain names."
                }
            },
            {
                "password", new List<string>
                {
                    "Use strong passwords with at least 12 characters, including numbers, symbols, and uppercase letters. Never reuse passwords!",
                    "Avoid using personal details like birthdates in your passwords. Consider using a secure password manager.",
                    "Enabling Multi-Factor Authentication (MFA) alongside a strong password adds an extra layer of defense against breaches."
                }
            },
            {
                "malware", new List<string>
                {
                    "Malware is malicious software like viruses or spyware. Keep your antivirus updated and avoid downloading unknown attachments.",
                    "Keep your operating system and software updated regularly to patch security vulnerabilities exploited by malware.",
                    "Avoid plugging unknown USB drives into your system as they can automatically execute malware."
                }
            },
            {
                "ransomware", new List<string>
                {
                    "Ransomware locks your files until a ransom is paid. Protect yourself by maintaining regular offline backups.",
                    "Never pay the ransom in a ransomware attack, as it does not guarantee your files will be restored.",
                    "Segregate your critical backups away from the main network to keep them safe from ransomware encryption."
                }
            },
            {
                "social engineering", new List<string>
                {
                    "Social engineering manipulates people into breaking security protocols. Be skeptical of urgent requests for credentials.",
                    "Attackers often impersonate IT staff or authority figures. Always verify identity through an official secondary channel.",
                    "Never share sensitive security information over an unsolicited phone call or instant message."
                }
            },
            {
                "privacy", new List<string>
                {
                    "Privacy is about controlling what personal data you share online. Audit your app permissions regularly!",
                    "Review social media privacy settings to limit who can see your personal photos, location, and activity.",
                    "Use privacy-focused web browsers and search engines to minimise online tracking and data collection."
                }
            }
        };

        public MainWindow()
        {
            InitializeComponent();

            // Play startup audio greeting
            AudioPlayer.PlayAudio("Assets/VoiceGreeting.wav");

            // Display ASCII banner at launch
            string banner = UIHelper.GetAsciiBanner();
            if (!string.IsNullOrEmpty(banner))
            {
                AppendBotMessage(banner);
            }

            // Initial prompt
            AppendBotMessage("Hello! Welcome to the Cybersecurity Awareness Bot. What is your name?");
        }

        public void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ProcessInput();
        }

        public void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtChatHistory.Text = string.Empty;
            string banner = UIHelper.GetAsciiBanner();
            if (!string.IsNullOrEmpty(banner))
            {
                AppendBotMessage(banner);
            }
            AppendBotMessage("Hello! Welcome to the Cybersecurity Awareness Bot. What is your name?");

            // Reset Memory
            isAwaitingName = true;
            userName = string.Empty;
            lastTopic = string.Empty;
            userInterest = string.Empty;
        }

        public void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessInput();
            }
        }

        public void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                TxtUserInput.Text = btn.Tag.ToString();
                ProcessInput();
            }
        }

        private void ProcessInput()
        {
            string input = TxtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendUserMessage(input);
            TxtUserInput.Clear();

            // Name capture and initial memory setup
            if (isAwaitingName)
            {
                if (UIHelper.IsValidName(input, out string errorMessage))
                {
                    userName = UIHelper.FormatName(input);
                    isAwaitingName = false;
                    AppendBotMessage($"Nice to meet you, {userName}! How can I assist you with cybersecurity today? (You can ask about Phishing, Passwords, Malware, Ransomware, or Privacy)");
                }
                else
                {
                    AppendBotMessage(errorMessage);
                }
                return;
            }

            // Process conversation with Sentiment Detection, Memory, and Topic Flow
            string response = GenerateSmartResponse(input);
            AppendBotMessage(response);
        }

        /// <summary>
        /// Generates intelligent responses combining Sentiment Detection, Memory, Random Selection, and Follow-ups.
        /// </summary>
        private string GenerateSmartResponse(string input)
        {
            string lowerInput = input.ToLower();
            string responsePrefix = string.Empty;

            // 1. Part 2 Sentiment Detection (Worried, Curious, Frustrated)
            if (lowerInput.Contains("worried") || lowerInput.Contains("scared") || lowerInput.Contains("afraid") || lowerInput.Contains("nervous"))
            {
                responsePrefix = $"It's completely understandable to feel that way, {userName}. Cyber threats can be intimidating, but staying informed helps protect you! ";
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("interested") || lowerInput.Contains("want to learn"))
            {
                responsePrefix = $"I love your curiosity, {userName}! Continuous learning is the best defense in cybersecurity. ";
            }
            else if (lowerInput.Contains("frustrated") || lowerInput.Contains("confused") || lowerInput.Contains("annoyed"))
            {
                responsePrefix = $"Don't worry, {userName}, cybersecurity concepts can be tricky at first. Let's break it down simply. ";
            }

            // 2. Part 2 Memory & Recall Capture ("interested in...")
            if (lowerInput.Contains("interested in") || lowerInput.Contains("like to know about"))
            {
                foreach (var topic in topicResponses.Keys)
                {
                    if (lowerInput.Contains(topic))
                    {
                        userInterest = topic;
                        lastTopic = topic;
                        string tip = GetRandomTopicTip(topic);
                        return $"{responsePrefix}Great! I've noted that you're interested in {userInterest}. It's a crucial part of staying safe online!\n\nHere is a key tip: {tip}";
                    }
                }
            }

            // 3. Part 2 Follow-up Flow ("tell me more", "explain more", "give me another tip")
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("explain more") || lowerInput.Contains("another tip") || lowerInput.Contains("more details"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && topicResponses.ContainsKey(lastTopic))
                {
                    string extraTip = GetRandomTopicTip(lastTopic);
                    return $"{responsePrefix}Here is another important tip regarding {lastTopic}:\n{extraTip}";
                }
                else if (!string.IsNullOrEmpty(userInterest) && topicResponses.ContainsKey(userInterest))
                {
                    string extraTip = GetRandomTopicTip(userInterest);
                    return $"{responsePrefix}As someone interested in {userInterest}, here is another great tip to keep in mind:\n{extraTip}";
                }
            }

            // 4. Part 2 Keyword Match with Random Response Selection
            foreach (var key in topicResponses.Keys)
            {
                if (lowerInput.Contains(key))
                {
                    lastTopic = key;
                    string randomTip = GetRandomTopicTip(key);

                    // Recall saved preference if available
                    string recallNote = (!string.IsNullOrEmpty(userInterest) && userInterest == key)
                        ? $" (Since you mentioned your interest in {userInterest} earlier!)"
                        : string.Empty;

                    return $"{responsePrefix}{randomTip}{recallNote}";
                }
            }

            // 5. Recall Memory context on generic responses
            if (!string.IsNullOrEmpty(userInterest))
            {
                return $"{responsePrefix}As someone interested in {userInterest}, you can ask me for more tips on that, or explore topics like Passwords, Phishing, Malware, or Ransomware!";
            }

            // Default fallback response for unknown keywords
            return $"{responsePrefix}I'm not sure I understand that command. You can ask me about Phishing, Passwords, Malware, Ransomware, Privacy, or tell me how you feel!";
        }

        /// <summary>
        /// Selects a random tip from the dictionary lists to fulfill Part 2 Random Responses criteria.
        /// </summary>
        private string GetRandomTopicTip(string topic)
        {
            if (topicResponses.TryGetValue(topic, out List<string> tips) && tips.Count > 0)
            {
                int index = random.Next(tips.Count);
                return tips[index];
            }
            return "Always stay vigilant online and keep your software updated!";
        }

        private void AppendUserMessage(string message)
        {
            TxtChatHistory.Text += $"\n\nYou: {message}";
            ChatScrollViewer.ScrollToEnd();
        }

        private void AppendBotMessage(string message)
        {
            TxtChatHistory.Text += $"\n\nBot:\n{message}";
            ChatScrollViewer.ScrollToEnd();
        }
    }
}