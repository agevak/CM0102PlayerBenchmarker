using CM.Model;
using CM.SaveFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;

namespace CM
{
    public partial class MainForm : Form
    {
        private const string CM_FOLDER = @"C:\Program Files (x86)\Championship Manager 01-02";
        private const string BENCH_FOLDER = "Bench";
        private static readonly string DUPS_FOLDER = Path.Combine("Bench", "Dups");
        private const string BENCHMARK_RESULT_FILENAME = "benchresult.csv";
        private static readonly string REPOSITORY_FILENAME = Path.Combine(BENCH_FOLDER, "repository.csv");
        private static readonly string CM_TACK_TOOL_FILENAME = Path.Combine(BENCH_FOLDER, "tactool.txt");

        private const bool SHOULD_IMPROVE_PLAYERS = false;

        private string saveFilename;
        private SaveFileParser saveFileParser;
        private IList<IList<CMStaff>> playersByGroup;
        private IList<IList<TextBox>> attrTextBoxes;

        public MainForm()
        {
            InitializeComponent();

            //LoadSaveFile("a_benchmark.sav", txtClubName.Text);
        }

        private void LoadSaveFile(string filename, string clubName)
        {
            SaveFileParser parser = new SaveFileParser();
            parser.Load(filename);

            IList<CMStaff> players = parser.Staffs.Where(x => x.Player != null && x.ClubJob != null && string.Equals(x.ClubJob.ShortName, clubName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static bool IsGK(CMPlayer player) => player.Goalkeeper == 20;
        public static bool IsDC(CMPlayer player) => !IsDLR(player) && player.Defender == 20 && player.CentreSide == 20;
        public static bool IsDLR(CMPlayer player) => player.Defender == 20 && (player.LeftSide == 20 || player.RightSide == 20);
        public static bool IsDMC(CMPlayer player) => player.DefensiveMidfielder == 20;// || (player.Midfielder == 20 && player.DefensiveMidfielder >= 15);
        public static bool IsAMC(CMPlayer player) => player.AttackingMidfielder == 20 || (player.Midfielder == 20 && !IsDMC(player));
        //public static bool IsAMC(CMPlayer player) => player.Midfielder == 20 && !IsDMC(player);
        //public static bool IsAMFC(CMPlayer player) => player.AttackingMidfielder == 20 && !IsDMC(player) && !IsAMC(player);// && player.Attacker >= 15;
        public static bool IsFC(CMPlayer player) => player.Attacker == 20 && !IsDMC(player) && !IsAMC(player);

        private void RunBenchmark()
        {
            int n = int.Parse(txtRunsCount.Text);
            int maxProcesses = int.Parse(txtConcurrentRunsCount.Text);
            pgbBenchmark.Value = 0;
            pgbBenchmark.Maximum = n;

            File.Delete(Path.Combine(CM_FOLDER, BENCHMARK_RESULT_FILENAME));
            Environment.SetEnvironmentVariable("CM3_PREFS", CM_FOLDER);
            IList<Process> processes = new List<Process>();
            for (int i = 1; i <= n; i++)
            {
                while (processes.Count >= maxProcesses)
                {
                    processes = processes.Where(x => !x.HasExited).ToList();
                    pgbBenchmark.Value = i - 1 - processes.Count;
                    Application.DoEvents();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }

                string dupFolder = Path.GetFullPath(Path.Combine(DUPS_FOLDER, $"{i}"));
                Directory.CreateDirectory(dupFolder);
                Environment.SetEnvironmentVariable("CM3_TEMP", dupFolder);
                processes.Add(Process.Start(new ProcessStartInfo()
                {
                    FileName = "cm0102_bm1.1.exe",
                    Arguments = "-load a_benchmark.sav",
                    WorkingDirectory = CM_FOLDER
                    //UseShellExecute = true
                }));
                pgbBenchmark.Value = i - processes.Count;
                Application.DoEvents();
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            // Wait for all CMs to finish.
            foreach (Process process in processes) process.WaitForExit();
            pgbBenchmark.Value = n;

            // Move benchmark result file.
            string inputFilename = Path.Combine(BENCH_FOLDER, txtTestName.Text);
            try { File.Move(Path.Combine(CM_FOLDER, BENCHMARK_RESULT_FILENAME), inputFilename); } catch { }

            // Add result to repository.
            ConvertBenchmarkResultToCMTacToolFormat(inputFilename, REPOSITORY_FILENAME, CM_TACK_TOOL_FILENAME, txtTestName.Text, txtClubName.Text);
        }

        private void ConvertBenchmarkResultToCMTacToolFormat(string inputFilename, string outputFilename, string cmTactToolFilename, string testName, string clubName)
        {
            // Parse input file.
            int playedSum = 0, pointsSum = 0, winSum = 0, drawSum = 0, lossSum = 0, goalForSum = 0, goalAgSum = 0, count = 0;
            string line;
            using (StreamReader reader = new StreamReader(inputFilename))
            {
                StreamWriter cmTackToolWriter = null;
                if (cmTactToolFilename != null) cmTackToolWriter = new StreamWriter(cmTactToolFilename);
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.ToLower().Contains(clubName.ToLower())) continue;

                        // Parse single season result.
                        string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int i = tokens.Length - 1;
                        int points = int.Parse(tokens[i--]);
                        int goalAg = int.Parse(tokens[i--]);
                        int goalFor = int.Parse(tokens[i--]);
                        int loss = int.Parse(tokens[i--]);
                        int draw = int.Parse(tokens[i--]);
                        int win = int.Parse(tokens[i--]);
                        goalAg += int.Parse(tokens[i--]);
                        goalFor += int.Parse(tokens[i--]);
                        loss += int.Parse(tokens[i--]);
                        draw += int.Parse(tokens[i--]);
                        win += int.Parse(tokens[i--]);
                        int played = int.Parse(tokens[i--]);

                        // Add to sums.
                        pointsSum += points;
                        goalAgSum += goalAg;
                        goalForSum += goalFor;
                        lossSum += loss;
                        drawSum += draw;
                        winSum += win;
                        playedSum += played;
                        count++;

                        // Write single season result to CMTactTool file.
                        if (cmTackToolWriter != null) cmTackToolWriter.WriteLine($"{testName} {goalFor}-{goalAg} {points} {played} {win}-{draw}-{loss}");
                    }
                }
                finally
                {
                    try { cmTackToolWriter.Close(); } catch { }
                }
            }

            if (outputFilename != null && count > 0)
            {
                // Calculate output.
                double playedAvg = playedSum / (double)count;
                double pointsAvg = pointsSum / (double)count;
                double winAvg = winSum / (double)count;
                double drawAvg = drawSum / (double)count;
                double lossAvg = lossSum / (double)count;
                double goalForAvg = goalForSum / (double)count;
                double goalAgAvg = goalAgSum / (double)count;

                // Write output.
                //IList<string> lines = new string[] { "Name For Ag Pts Pld Won Drn Lst" }.ToList();
                IList<string> lines = new string[] { "Name For Ag Pts" }.ToList();
                if (File.Exists(outputFilename)) lines = File.ReadAllLines(outputFilename).ToList();
                int i;
                for (i = 0; i < lines.Count; i++) if (lines[i].ToLower().StartsWith(clubName.ToLower())) break;
                if (i == lines.Count) lines.Add("");
                lines[i] = $"{testName} {goalForAvg:0.00} {goalAgAvg:0.00} {pointsAvg:0.00}";// {playedAvg:0.00}";// {playedAvg};{winAvg};{drawAvg};{lossAvg}";
                File.WriteAllLines(outputFilename, lines);
            }
        }

        private void btnConvertExternalFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { DefaultExt = "csv" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                ConvertBenchmarkResultToCMTacToolFormat(dialog.FileName, REPOSITORY_FILENAME, CM_TACK_TOOL_FILENAME, txtTestName.Text, txtClubName.Text);
            }
        }

        private void btnRunBenchmark_Click(object sender, EventArgs e)
        {
            RunBenchmark();
        }

        private void btnLoadSaveFile_Click(object sender, EventArgs e)
        {
            // Load file.
            IList<string> filenames = Directory.GetFiles(CM_FOLDER, "*.sav");
            if (filenames.Count == 0) MessageBox.Show(this, "No save files found at CM folder.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string filename = filenames.First();
            if (filenames.Count > 1)
            {
                using (OpenFileDialog dialog = new OpenFileDialog() { DefaultExt = "sav", InitialDirectory = CM_FOLDER })
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK) return;
                    filename = dialog.FileName;
                }
            }
            saveFileParser = new SaveFileParser();
            try
            {
                saveFileParser.Load(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to parse CM save file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveFileParser = null;
                return;
            }
            if (saveFileParser.Staffs == null)
            {
                MessageBox.Show(this, "Save file must be uncompressed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveFileParser = null;
                return;
            }

            // Impove all players.
            if (SHOULD_IMPROVE_PLAYERS)
            {
                Random rnd = new Random();
                foreach (CMStaff staff in saveFileParser.Staffs.Where(p => p.Player != null))
                {
                    foreach (CMAttribute attr in CMAttribute.LIST)
                    {
                        int newVal = attr.GetValue(staff);
                        if (attr.FieldName == "Dirtiness" || attr.FieldName == "InjuryProneness")
                            ;
                        if (attr.FieldName == "Reflexes")
                            newVal = Math.Max(Math.Min(newVal, -80), newVal - 15);
                        if (attr.FieldName == "Pace" || attr.FieldName == "Acceleration" || attr.FieldName == "Jumping" || attr.FieldName == "Stamina")
                            newVal = Math.Max(newVal, rnd.Next(15, 21));
                        else if (attr.Max == 200) newVal = Math.Min(attr.Max, (int)(Math.Round(newVal * 1.25) + 1e-9));
                        else if (attr.Max == 20) newVal = Math.Min(attr.Max, (int)(Math.Round(newVal * 1.3) + 1e-9));
                        else newVal = Math.Min(Math.Max(newVal, 80), newVal + 15);
                        attr.SetValue(staff, newVal);
                    }
                    staff.Player.NaturalFitness = 20;
                    staff.Player.InjuryProneness = 1;
                }
            }

            // Extract club players.
            IList<CMStaff> players = saveFileParser.Staffs.Where(p => p.Player != null && p.ClubJob != null && string.Equals(p.ClubJob.ShortName, txtClubName.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!players.Any())
            {
                MessageBox.Show(this, "No players from specified club found in the CM save file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saveFileParser = null;
                return;
            }
            saveFilename = filename;

            // Split players on groups by position.
            playersByGroup = new List<IList<CMStaff>>();
            // GK.
            IList<CMStaff> group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsGK(players[i].Player))
            {
                    group.Add(players[i]);
                    players.RemoveAt(i);
            }
            // DC.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsDC(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.Defender = 20;
                    p.DefensiveMidfielder = p.Midfielder = p.AttackingMidfielder = p.Attacker = p.WingBack = p.FreeRole = 1;
                    p.CentreSide = 20;
                    p.LeftSide = p.RightSide = 1;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }
            // DLR.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsDLR(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.Defender = p.DefensiveMidfielder = p.Midfielder = p.AttackingMidfielder = p.WingBack = 20;
                    p.Sweeper = p.Attacker = p.FreeRole = 1;
                    p.CentreSide = 1;
                    p.LeftSide = p.RightSide = 20;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }
            // DMC.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsDMC(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.DefensiveMidfielder = p.Midfielder = 20;
                    p.Sweeper = p.Defender = p.AttackingMidfielder = p.Attacker = p.WingBack = p.FreeRole = 1;
                    p.CentreSide = 20;
                    p.LeftSide = p.RightSide = 1;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }
            // AMC.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsAMC(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.AttackingMidfielder = p.Midfielder = 20;
                    p.Sweeper = p.Defender = p.DefensiveMidfielder = p.Attacker = p.WingBack = p.FreeRole = 1;
                    p.CentreSide = 20;
                    p.LeftSide = p.RightSide = 1;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }
            /*// AMFC.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsAMFC(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.AttackingMidfielder = p.Attacker = 20;
                    p.Sweeper = p.Defender = p.DefensiveMidfielder = p.Midfielder = p.WingBack = p.FreeRole = 1;
                    p.CentreSide = 20;
                    p.LeftSide = p.RightSide = 1;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }*/
            // FC.
            group = new List<CMStaff>();
            playersByGroup.Add(group);
            for (int i = players.Count - 1; i >= 0; i--) if (IsFC(players[i].Player))
                {
                    CMPlayer p = players[i].Player;
                    p.Attacker = 20;
                    p.Sweeper = p.Defender = p.DefensiveMidfielder = p.Midfielder = p.AttackingMidfielder = p.WingBack = p.FreeRole = 1;
                    p.CentreSide = 20;
                    p.LeftSide = p.RightSide = 1;
                    group.Add(players[i]);
                    players.RemoveAt(i);
                }

            //
            SuspendLayout();
            int yStep = 20;
            int xStep = 50;
            // Attribute names.
            int x = 5;
            int y = 5 + yStep;
            foreach (CMAttribute attr in CMAttribute.LIST)
            {
                Label label = new Label()
                {
                    Parent = this,
                    Text = attr.DisplayName,
                    AutoSize = true,
                    Location = new Point(x, y)
                };
                if (attr.Min < 0) label.BackColor = SystemColors.ControlDark;
                Controls.Add(label);
                y += yStep;
            }
            // Group headers.
            x = 100;
            y = 5;
            for (int i = 0; i < playersByGroup.Count; i++)
            {
                string name = "GK";
                if (i == 1) name = "DC";
                else if (i == 2) name = "DLR";
                else if (i == 3) name = "DMC";
                else if (i == 4) name = "AMC";
                //else if (i == 5) name = "AMFC";
                else if (i == 5) name = "FC";
                Label label = new Label()
                {
                    Parent = this,
                    Text = $"{name} {playersByGroup[i].Count}",
                    AutoSize = true,
                    Location = new Point(x, y)
                };
                Controls.Add(label);
                x += xStep;
            }
            // Attributes.
            attrTextBoxes = new List<IList<TextBox>>();
            x = 100;
            for (int i = 0; i < playersByGroup.Count; i++)
            {
                attrTextBoxes.Add(new List<TextBox>());
                y = 5 + yStep;
                foreach (CMAttribute attr in CMAttribute.LIST)
                {
                    IList<int> values = playersByGroup[i].Select(p => attr.GetValue(p)).ToList();
                    TextBox textBox = new TextBox()
                    {
                        Parent = this,
                        Text = values.Min() == values.Max() ? values.First().ToString() : "",
                        TextAlign = HorizontalAlignment.Right,
                        Location = new Point(x, y),
                        Size = new Size(40, 15)
                    };
                    textBox.TextChanged += txtAttr_TextChanged;
                    attrTextBoxes.Last().Add(textBox);
                    textBox.SuspendLayout();
                    Controls.Add(textBox);
                    y += yStep;
                }
                x += xStep;
            }
            ResumeLayout();
            PerformLayout();
        }

        private void btnSaveSaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileParser == null) return;

            // Validate values.
            bool allValid = true;
            for (int i = 0; i < playersByGroup.Count; i++)
            {
                for (int j = 0; j < CMAttribute.LIST.Length; j++)
                {
                    CMAttribute attr = CMAttribute.LIST[j];
                    TextBox textBox = attrTextBoxes[i][j];
                    if (string.IsNullOrWhiteSpace(textBox.Text)) continue;
                    if (!int.TryParse(textBox.Text, out int val) || !(attr.Min <= val && val <= attr.Max))
                    {
                        allValid = false;
                        textBox.ForeColor = Color.Red;
                    }
                    else textBox.ForeColor = Color.Black;
                }
            }
            if (!allValid)
            {
                MessageBox.Show(this, "Invalid attribute value.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Apply values.
            for (int i = 0; i < playersByGroup.Count; i++)
            {
                for (int j = 0; j < CMAttribute.LIST.Length; j++)
                {
                    CMAttribute attr = CMAttribute.LIST[j];
                    TextBox textBox = attrTextBoxes[i][j];
                    if (string.IsNullOrWhiteSpace(textBox.Text)) continue;
                    int val = int.Parse(textBox.Text);
                    foreach (CMStaff player in playersByGroup[i]) attr.SetValue(player, val);
                }
            }

            // Save to file.
            IList<CMStaff> players = new List<CMStaff>();
            foreach (IList<CMStaff> group in playersByGroup) players = players.Concat(group).ToList();
            if (SHOULD_IMPROVE_PLAYERS) players = saveFileParser.Staffs;
            saveFileParser.Save(saveFilename, players);

            for (int i = 0; i < playersByGroup.Count; i++) for (int j = 0; j < CMAttribute.LIST.Length; j++) attrTextBoxes[i][j].Font = new Font(attrTextBoxes[i][j].Font, FontStyle.Regular);
        }

        private void txtAttr_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Font = new Font(textBox.Font, FontStyle.Bold);
        }
    }
}
