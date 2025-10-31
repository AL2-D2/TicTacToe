Game.Start();
class Player
{
    public string XO { get; }
    public Player()
    {
        XO = GetTic();
    }
    private string GetTic()
    {
        string input;
        while (true) { 
        Console.WriteLine("select your selection:");
        input = Console.ReadLine().Trim().ToUpper();
            if (input == "X" || input == "O")
                break;
            else Console.WriteLine("this is not a choice");
        }
        Console.Clear();
        return input;
    }

    public void SetTile(Board board)
    {
       
        while (true) {
        
            Console.WriteLine("Please select for placement.");
            string answer = Console.ReadLine();
             if(int.TryParse(answer, out int choice) && choice >= 1 && choice <= 9)
            {
                int index = choice - 1;
                int row = index / 3;
                int col = index % 3;
                if (!board.OccupiedTile[row, col])
                {
                    board.tiles[row, col] = XO;
                    board.OccupiedTile[row, col] = true;
                    break;
                }
                else
                    Console.WriteLine("spot is already taken.");
            }
            else
                Console.WriteLine("i dont understand");
            
        }
    }
}
class Board
{
    public bool[,] OccupiedTile = new bool[3,3] {{false,false,false  }, { false, false, false }, { false, false, false } };
    public string[,] tiles = new string[3, 3] { {" "," "," "},{" "," "," "},{" "," "," "} };
    public void CurrentState()
    {
        for(int row = 0; row < tiles.GetLength(0); row++) {
            Console.WriteLine("+---+---+---+");
            for(int col = 0;  col < tiles.GetLength(1); col++) 
            {                 
                Console.Write($"| {tiles[row,col]} ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("+---+---+---+");
    }
}

class Game
{
    public bool[] renderer { get; set; }
    public static void DisplayRound(int round)
    {
        Console.WriteLine($"Round: {round}");
    }

    public static void Start()
    {
        Board board = new Board();
        Player player1 = new Player();
        Player player2 = new Player();
        int round = 1;
        do
        {
            board.CurrentState();
            if (round == 1 || round % 2 == 1)
            {
                
                Console.WriteLine($"{player1.XO} its your turn.");
                DisplayRound(round);
                player1.SetTile(board);
                round++;
                Console.Clear();

            }
            else if (round % 2 == 0)
            {
                
                Console.WriteLine($"{player2.XO} its your turn.");
                DisplayRound(round);
                player2.SetTile(board);
                round++;
                Console.Clear();

            }
            if      (Renderer.IsGameEnd(board) == false && Renderer.IsAllOccupied(board) == true) { board.CurrentState(); Console.WriteLine("its draw."); }
            else if (Renderer.IsGameEnd(board) == true) { board.CurrentState(); Console.WriteLine("game is over."); }
        }   while   (Renderer.IsGameEnd(board) == false && Renderer.IsAllOccupied(board) == false);
    }
}
class Renderer
{
  
    public static bool IsGameEnd(Board board)
    {
        if      (board.tiles[0, 0] == "X" && board.tiles[1, 1] == "X" && board.tiles[2, 2] == "X") return true;
        else if (board.tiles[0, 0] == "X" && board.tiles[0, 1] == "X" && board.tiles[0, 2] == "X") return true;
        else if (board.tiles[0, 0] == "X" && board.tiles[1, 0] == "X" && board.tiles[2, 0] == "X") return true;
        else if (board.tiles[0, 2] == "X" && board.tiles[1, 1] == "X" && board.tiles[2, 0] == "X") return true;
        else if (board.tiles[0, 1] == "X" && board.tiles[1, 1] == "X" && board.tiles[2, 1] == "X") return true;
        else if (board.tiles[0, 2] == "X" && board.tiles[1, 2] == "X" && board.tiles[2, 2] == "X") return true;

        else if (board.tiles[0, 0] == "O" && board.tiles[1, 1] == "O" && board.tiles[2, 2] == "O") return true;
        else if (board.tiles[0, 0] == "O" && board.tiles[0, 1] == "O" && board.tiles[0, 2] == "O") return true;
        else if (board.tiles[0, 0] == "O" && board.tiles[1, 0] == "O" && board.tiles[2, 0] == "O") return true;
        else if (board.tiles[0, 2] == "O" && board.tiles[1, 1] == "O" && board.tiles[2, 0] == "O") return true;
        else if (board.tiles[0, 1] == "O" && board.tiles[1, 1] == "O" && board.tiles[2, 1] == "O") return true;
        else if (board.tiles[0, 2] == "O" && board.tiles[1, 2] == "O" && board.tiles[2, 2] == "O") return true;

        else return false;
    }
    public static bool IsAllOccupied(Board board)
    {
        foreach(bool occupied in board.OccupiedTile)
        {
            if (!occupied) return false;
        }
        return true;
    }
}