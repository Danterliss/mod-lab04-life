using cli_life;

namespace Life.Test
{
    public class UnitTest1
    {
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        [Fact]
        public void CellBirth()
        {
            //####
            //#**#
            //##*#
            //####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //####
            //#**#
            //#**#
            //####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }

        [Fact]
        public void CellDiedFromLoneliness()
        {
            //####
            //####
            //##*#
            //####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //####
            //####
            //####
            //####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }

        [Fact]
        public void CellDiedFromOverpopulating()
        {
            //#####
            //##*##
            //#***#
            //##*##
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //#####
            //#***#
            //#*#*#
            //#***#
            //#####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }

        [Fact]
        public void CellsLiveStable()
        {
            //#####
            //#**##
            //#**##
            //#####
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //#####
            //#**##
            //#**##
            //#####
            //#####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            for (int i = 0; i < 10; i++)
                board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }

        [Fact]
        public void CellsLivePeriodic()
        {
            //#####
            //#*###
            //#*###
            //#*###
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //#####
            //#*###
            //#*###
            //#*###
            //#####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            for (int i = 0; i < 10; i++)
                board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }

        [Fact]
        public void CellsGlider()
        {
            //#####
            //##*##
            //###*#
            //#***#
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            //#####
            //##*##
            //###*#
            //#***#
            //#####
            Cell[][] correct = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            for (int i = 0; i < 20; i++)
                board.Advance();
            for (int x = 0; x < board.Columns; x++)
                for (int y = 0; y < board.Rows; y++)
                    Assert.Equal(correct[x][y].IsAlive, board.Cells[x, y].IsAlive);
        }


        [Fact]
        public void CountAll()
        {
            //#####
            //##*##
            //###*#
            //#***#
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };
            Board board = new Board(cells, 1);
            Assert.Equal(5, board.countAlive());
        }

        [Fact]
        public void CountAllInEntities()
        {
            //#####
            //#**##
            //#**##
            //#####
            //#####
            Cell[][] cells = new Cell[][]
            {
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = true}, new Cell(){IsAlive = true}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false} },
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            new Cell[] {new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}, new Cell(){IsAlive = false}},
            };

            Board board = new Board(cells, 1);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(4, entities["cells at all"]);
        }

        [Fact]
        public void CountBlock()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(3, entities["block"]);
        }

        [Fact]
        public void CountPond()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(1, entities["pond"]);
        }

        [Fact]
        public void CountTub()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(1, entities["tub"]);
        }

        [Fact]
        public void CountBoat()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(4, entities["boat"]);
        }

        [Fact]
        public void CountSpinner()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(2, entities["spinner"]);
        }

        [Fact]
        public void CountLoaf()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(4, entities["loaf"]);
        }

        [Fact]
        public void CountShip()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(2, entities["ship"]);
        }

        [Fact]
        public void CountHive()
        {
            string[] raw = File.ReadAllLines(Path.Combine(projectDirectory, "test.txt"));
            Board board = new Board(60, 30, 1, raw);
            Dictionary<string, int> entities = board.countEntities();
            Assert.Equal(2, entities["hive"]);
        }

        [Fact]
        public void LongLife()
        {
            Board board = new Board(60, 30, 1, 0.2);
            while (!board.isStable())
            {
                board.Advance();
            }
            Assert.True(board.isStable());
        }

        [Fact]
        public void EmptyLife()
        {
            Board board = new Board(60, 30, 1, 0);
            while (!board.isStable())
            {
                board.Advance();
            }
            Assert.True(board.isStable());
        }
    }
}