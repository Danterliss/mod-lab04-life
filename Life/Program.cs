using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Drawing;
using System.Formats.Asn1;
using System.Threading;
using ScottPlot;

namespace cli_life
{
    public class Cell
    {
        public bool IsAlive;
        public readonly List<Cell> neighbors = new List<Cell>();
        private bool IsAliveNext;
        public void DetermineNextLiveState()
        {
            int liveNeighbors = neighbors.Where(x => x.IsAlive).Count();
            if (IsAlive)
                IsAliveNext = liveNeighbors == 2 || liveNeighbors == 3;
            else
                IsAliveNext = liveNeighbors == 3;
        }
        public void Advance()
        {
            IsAlive = IsAliveNext;
        }
    }
    public class Board
    {
        public readonly Cell[,] Cells;
        public readonly int CellSize;

        private Dictionary<int, int> count_sleeping;
        public int Columns { get { return Cells.GetLength(0); } }
        public int Rows { get { return Cells.GetLength(1); } }
        public int Width { get { return Columns * CellSize; } }
        public int Height { get { return Rows * CellSize; } }
        public int SleepAccuracy { get { return Convert.ToInt32(Math.Round(Math.Sqrt(Height * Width))); } }

        public Board(int width, int height, int cellSize, double liveDensity = .1)
        {
            count_sleeping = new Dictionary<int, int>();
            CellSize = cellSize;

            Cells = new Cell[width / cellSize, height / cellSize];
            for (int x = 0; x < Columns; x++)
                for (int y = 0; y < Rows; y++)
                    Cells[x, y] = new Cell();

            ConnectNeighbors();
            Randomize(liveDensity);
        }

        readonly Random rand = new Random();
        public void Randomize(double liveDensity)
        {
            foreach (var cell in Cells)
                cell.IsAlive = rand.NextDouble() < liveDensity;
        }

        public void Advance()
        {
            foreach (var cell in Cells)
                cell.DetermineNextLiveState();
            foreach (var cell in Cells)
                cell.Advance();
        }

        public int countAlive()
        {
            int count = 0;
            foreach (Cell cell in Cells)
            {
                if (cell.IsAlive) count++;
            }
            return count;
        }

        public Dictionary<string, int> countEntities()
        {
            Dictionary<string, int> entities = new Dictionary<string, int>();
            entities["cells at all"] = countAlive();
            Dictionary<string, string> life = new Dictionary<string, string>()
            {
                // block 4x4
                {"00000110011000004x4", "block"},
                // hive 6x5, 5x6
                {"0000000011000100100011000000006x5", "hive"},
                {"0000000100010100101000100000005x6", "hive"},
                // loaf 6x6
                {"0000000001000010100100100011000000006x6", "loaf"},
                {"0000000010000101000100100011000000006x6", "loaf"},
                {"0000000011000100100101000010000000006x6", "loaf"},
                {"0000000011000100100010100001000000006x6", "loaf"},
                // tub 5x5
                {"00000001000101000100000005x5", "tub"},
                // boat 5x5
                {"00000011000101000100000005x5", "boat"},
                {"00000001100101000100000005x5", "boat"},
                {"00000001000101001100000005x5", "boat"},
                {"00000001000101000110000005x5", "boat"},
                // ship 5x5
                {"00000011000101000110000005x5", "ship"},
                {"00000001100101001100000005x5", "ship"},
                // pond 6x6
                {"0000000011000100100100100011000000006x6", "pond"},
                // spinner 5x3, 3x5
                {"0000001110000005x3", "spinner"},
                {"0000100100100003x5", "spinner"}

            };
            int[] heights = new int[] { 3, 4, 5, 6 };
            int[] widths = new int[] { 3, 4, 5, 6 };
            foreach (int w in widths)
            {
                foreach (int h in heights)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            string window = "";
                            for (int j = y; j < y + h; j++)
                            {
                                for (int i = x; i < x + w; i++)
                                {
                                    window += Cells[i % Width, j % Height].IsAlive ? "1" : "0";
                                }
                            }
                            window += w.ToString() + "x" + h.ToString();
                            if (life.ContainsKey(window))
                            {
                                if (!entities.ContainsKey(life[window]))
                                {
                                    entities[life[window]] = 0;
                                }
                                entities[life[window]] += 1;
                            }
                        }
                    }
                }
            }
            return entities;
        }

        public bool isStable()
        {
            int count = countAlive();
            if (!count_sleeping.ContainsKey(count)) count_sleeping[count] = 0;
            count_sleeping[count] += 1;
            return count_sleeping[count] >= SleepAccuracy;
        }

        private void ConnectNeighbors()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    int xL = (x > 0) ? x - 1 : Columns - 1;
                    int xR = (x < Columns - 1) ? x + 1 : 0;

                    int yT = (y > 0) ? y - 1 : Rows - 1;
                    int yB = (y < Rows - 1) ? y + 1 : 0;

                    Cells[x, y].neighbors.Add(Cells[xL, yT]);
                    Cells[x, y].neighbors.Add(Cells[x, yT]);
                    Cells[x, y].neighbors.Add(Cells[xR, yT]);
                    Cells[x, y].neighbors.Add(Cells[xL, y]);
                    Cells[x, y].neighbors.Add(Cells[xR, y]);
                    Cells[x, y].neighbors.Add(Cells[xL, yB]);
                    Cells[x, y].neighbors.Add(Cells[x, yB]);
                    Cells[x, y].neighbors.Add(Cells[xR, yB]);
                }
            }
        }

    }

    class SettingsParser
    {
        public int width { get; set; }
        public int height { get; set; }
        public int cellSize { get; set; }
        public double liveDensity { get; set; }
    }

    public class SimulationData
    {
        public double Density { get; set; }
        public int[] AliveCellsPerGeneration { get; set; }
    }

    public class PlotGenerator
    {
        private const int MaxGenerations = 700;
        private const int Width = 50;
        private const int Height = 20;
        private const int CellSize = 1;

        private static readonly double[] Densities = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
        private static readonly ScottPlot.Color[] Colors =
        {
        ScottPlot.Colors.Red, ScottPlot.Colors.Black, ScottPlot.Colors.Yellow, ScottPlot.Colors.Green,
        ScottPlot.  Colors.Pink, ScottPlot.Colors.Purple, ScottPlot.Colors.Blue, ScottPlot.Colors.Orange, ScottPlot.Colors.LightBlue
        };

        public static void GeneratePlot(string outputDirectory)
        {
            var dataFile = Path.Combine(outputDirectory, "data.txt");

            if (!File.Exists(dataFile))
            {
                GenerateSimulationData(dataFile);
            }

            CreateVisualization(dataFile, outputDirectory);
        }

        private static void GenerateSimulationData(string outputPath)
        {
            var simulations = Densities.Select(density => RunSimulation(density)).ToList();

            using var writer = new StreamWriter(outputPath);
            foreach (var sim in simulations)
            {
                writer.WriteLine(sim.Density);
                foreach (var count in sim.AliveCellsPerGeneration)
                {
                    writer.WriteLine(count);
                }
            }
        }

        private static SimulationData RunSimulation(double density)
        {
            var board = new Board(Width, Height, CellSize, density);
            var results = new int[MaxGenerations];

            for (int gen = 0; gen < MaxGenerations; gen++)
            {
                results[gen] = board.countAlive();
                board.Advance();
            }

            return new SimulationData
            {
                Density = density,
                AliveCellsPerGeneration = results
            };
        }

        private static void CreateVisualization(string dataPath, string outputDirectory)
        {
            var plot = new Plot();
            plot.XLabel("Generation");
            plot.YLabel("Alive Cells Count");
            plot.Legend.Alignment = Alignment.UpperRight;
            plot.ShowLegend();

            using var reader = new StreamReader(dataPath);

            for (int i = 0; i < Densities.Length; i++)
            {
                var density = double.Parse(reader.ReadLine());
                var counts = new int[MaxGenerations];
                var generations = Enumerable.Range(1, MaxGenerations).ToArray();

                for (int j = 0; j < MaxGenerations; j++)
                {
                    counts[j] = int.Parse(reader.ReadLine());
                }

                var scatter = plot.Add.ScatterLine(generations, counts, Colors[i]);
                scatter.LineWidth = 2;
                scatter.LegendText = density.ToString("P0");
            }

            var outputPath = Path.Combine(outputDirectory, "plot.png");
            plot.SavePng(outputPath, 1920, 1080);
        }
    }

    class Program
    {
        static Board board;
        static int genCount;
        static private void Reset(string projectDirectory, double dens = 0)
        {
            if (!Directory.Exists("Input")) Directory.CreateDirectory(Path.Combine(projectDirectory, "Input/"));
            string settingsDirectory = Path.Combine(projectDirectory, "Input/settings.json");
            if (!File.Exists(settingsDirectory))
                File.Create(settingsDirectory);
            string raw = File.ReadAllText(settingsDirectory);
            if (raw != "")
            {
                var settings = JsonSerializer.Deserialize<SettingsParser>(raw);
                if (dens != 0)
                {
                    board = new Board(
                        width: settings.width,
                        height: settings.height,
                        cellSize: settings.cellSize,
                        liveDensity: dens);
                }
                else
                {
                    board = new Board(
                        width: settings.width,
                        height: settings.height,
                        cellSize: settings.cellSize,
                        liveDensity: settings.liveDensity);
                }
            }
            else
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var settings = new SettingsParser
                {
                    width = 50,
                    height = 20,
                    cellSize = 1,
                    liveDensity = 0.5,
                };
                string jsonString = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(settingsDirectory, jsonString);
                board = new Board(
                    width: settings.width,
                    height: settings.height,
                    cellSize: settings.cellSize,
                    liveDensity: settings.liveDensity);
            }
        }

        static void Render()
        {
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var cell = board.Cells[col, row];
                    if (cell.IsAlive)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.Write('\n');
            }
        }

        static int Load(string projectDirectory)
        {
            string loadDirectory = Path.Combine(projectDirectory, "Data/gen-0.txt");
            if (!Directory.Exists(Path.Combine(projectDirectory, "Data")))
            {
                Directory.CreateDirectory(Path.Combine(projectDirectory, "Data/"));
                File.WriteAllText(loadDirectory, "");
            }
            else if (!File.Exists(loadDirectory))
            {
                File.WriteAllText(loadDirectory, "");
            }

            string[] raw = File.ReadAllLines(loadDirectory);

            int wid = 0;
            int hei = 0;
            int gen = 0;
            if (raw.Length > 0)
            {
                wid = raw[0].Length;
                hei = raw.Length - 1;
                gen = int.Parse(raw[hei]);
            }

            board = new Board(
                width: wid,
                height: hei,
                cellSize: 1,
                liveDensity: 0);

            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var cell = board.Cells[col, row];
                    if (raw[row][col] == '1')
                    {
                        cell.IsAlive = true;
                    }
                    else
                    {
                        cell.IsAlive = false;
                    }
                }
            }
            return gen;
        }

        static void Save(string projectDirectory, Dictionary<string, int> c_data = null)
        {
            string fname = "gen-0";
            if (!Directory.Exists(Path.Combine(projectDirectory, "Data")))
                Directory.CreateDirectory(Path.Combine(projectDirectory, "Data/"));
            StreamWriter writer = new StreamWriter(Path.Combine(projectDirectory, "Data/" + fname + ".txt"));
            double[,] data = new double[board.Rows, board.Columns];
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var cell = board.Cells[col, row];
                    if (cell.IsAlive)
                    {
                        writer.Write('1');
                        data[row, col] = 1;
                    }
                    else
                    {
                        writer.Write('0');
                        data[row, col] = 0;
                    }
                }
                writer.Write("\n");
            }
            writer.Write(genCount);
            if (c_data != null)
            {
                foreach (var entity in c_data)
                {
                    writer.WriteLine(entity.Key + " - " + entity.Value);
                }
            }
            writer.Close();
        }

        private static double StablePhaseTransitionAverageTime(double density = .5)
        {
            double sum = 0;
            int experimentsNumber = 10;
            for (int i = 0; i < experimentsNumber; i++)
            {
                Board board = new Board(
                width: 50,
                height: 20,
                cellSize: 1,
                liveDensity: density);
                int genCount = 0;
                while (true)
                {
                    genCount++;
                    if (board.isStable())
                    {
                        genCount -= board.SleepAccuracy;
                        sum += genCount;
                        break;
                    }
                    if (genCount >= 10000) break;
                    board.Advance();
                }
            }
            return sum / experimentsNumber;
        }

        static void Main()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            Reset(projectDirectory);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo name = Console.ReadKey(true);
                    if (name.KeyChar == 'q')
                        break;
                    else if (name.KeyChar == 's')
                    {
                        Save(projectDirectory);
                    }
                    else if (name.KeyChar == 'l')
                    {
                        genCount = Load(projectDirectory);
                    }
                }

                Console.Clear();
                Render();
                Console.WriteLine("generation " + genCount);
                board.Advance();

                if (board.isStable())
                {
                    genCount -= board.SleepAccuracy;
                    Dictionary<string, int> count_data = board.countEntities();
                    Console.Clear();
                    Render();
                    Console.WriteLine("generation " + genCount);
                    foreach (var entity in count_data)
                    {
                        Console.WriteLine(entity.Key + " - " + entity.Value);
                    }
                    break;
                }

                Thread.Sleep(300);

                ++genCount;
                if (genCount >= board.Width * board.Height) break;
            }
            PlotGenerator.GeneratePlot(projectDirectory);
            Console.WriteLine("\nThe average time of transition to a stable phase: " + Math.Round(StablePhaseTransitionAverageTime()));
        }
    }
}