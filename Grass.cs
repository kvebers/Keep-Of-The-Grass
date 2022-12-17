using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
struct Tile
{
    public int scrapAmount;
    public int owner;
    public int units;
    public int recycler;
    public int canBuild;
    public int canSpawn;
    public int inRangeOfRecycler;
}

struct Algo
{
    public int totalTileOut;
    public int turn_to_maze;
    public int solve_map;
    public int calculate_priority;
    public int scrapAmount;
    public int owner;
    public int units;
    public int recycler;
    public int canBuild;
    public int canSpawn;
    public int inRangeOfRecycler;
    public int x;
    public int y;
    
}

struct Robot
{
    public int x;
    public int y;
}

struct Data
{
    public int mid_w;
    public int mid_h;   
}

class Player
{
    static void Main(string[] args)
    {
        Player player = new Player();
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]);
        int height = int.Parse(inputs[1]);
        Tile[,] map = new Tile[width, height];
        Algo[,] Algo = new Algo[width, height];
        Data data = new Data();
        //int[,] solve_map2 = new int[width, height]; Potential for the future AI bot
        int turn_counter = 0;
        int check_squeres = 5;
        data.mid_h = height / 2;
        data.mid_w = width / 2;
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int myMatter = int.Parse(inputs[0]);
            int oppMatter = int.Parse(inputs[1]);
            int priorityx;
            int priorityy;
            for (int i = 0; i < height; i++)
            {
               for (int j = 0; j < width; j++)
                {
                inputs = Console.ReadLine().Split(' ');
                int scrapAmount = int.Parse(inputs[0]);
                int owner = int.Parse(inputs[1]); // 1 = me, 0 = foe, -1 = neutral
                int units = int.Parse(inputs[2]);
                int recycler = int.Parse(inputs[3]);
                int canBuild = int.Parse(inputs[4]);
                int canSpawn = int.Parse(inputs[5]);
                int inRangeOfRecycler = int.Parse(inputs[6]);

        // Create a new Tile object with the values for this tile
                Tile tile = new Tile
                {
                    scrapAmount = scrapAmount,
                    owner = owner,
                    units = units,
                    recycler = recycler,
                    canBuild = canBuild,
                    canSpawn = canSpawn,
                    inRangeOfRecycler = inRangeOfRecycler,
                };
            // Save the Tile object in the map array
                map[j, i] = tile;
                }
            }
            List<Algo> Squeres = new List<Algo>();

            List<Robot> playerRobots = new List<Robot>();

    // Create a List to store the enemy's Robot objects with parts
            List<Robot> enemyRobots = new List<Robot>();

            // Iterate over the robots on the map
            int x = 0;
            int y = 0;
            
            while (y < height)
            {
                x = 0;
                while(x < width)
                {
            // Read the inputs for this robot
                    int value = map[x,y].scrapAmount;
                    // Calculates the Tile Value 
                    if(x - 1 >= 0)
                        value = value + map[x - 1, y].scrapAmount;
                    if(x + 1 < width)
                        value = value + map[x + 1, y].scrapAmount;
                    if(y - 1 >= 0)
                        value = value + map[x, y - 1].scrapAmount;
                    if(y + 1 < height)
                        value = value + map[x, y + 1].scrapAmount;

                    // Gets the nutral grounds the enemy grounds my grounds
                    if(map[x,y].scrapAmount == 0 || map[x,y].recycler == 1)
                        Algo[x,y].solve_map = 0;
                    else if (map[x, y].scrapAmount == 1 && map[x,y].recycler == 1)
                        Algo[x,y].solve_map = 0;
                    else if (map[x,y].owner == 1)
                        Algo[x,y].solve_map = 1;
                    else if(turn_counter > 70 && map[x,y].owner == -1)
                        Algo[x,y].solve_map = 10;
                    else if (map[x,y].owner == 0)
                        Algo[x,y].solve_map = 6;
                    else if (map[x,y].owner == -1)
                        Algo[x,y].solve_map = 5;
                    // turn map in 0 and 1 for can go and can not go
                    if(Algo[x,y].solve_map == 0)
                        Algo[x,y].turn_to_maze = 0;
                    else 
                        Algo[x,y].turn_to_maze = 1;
                    // Gives priority to an X Cordinate in a squere
                    if (turn_counter < 15)
                        if (x > data.mid_w - 2 && x < data.mid_w + 2)
                            priorityx = 6;
                        else if (x > data.mid_w - 4 && x < data.mid_w + 4)
                            priorityx = 5;
                        else if (x > data.mid_w - 5 && x < data.mid_w + 5)
                            priorityx = 4;
                        else 
                            priorityx = 3;
                    else 
                        priorityx = 3;


                    // Gives a Priority to Y Cordinates
                    if (y > data.mid_h - 3 && x < data.mid_w + 3)
                        priorityy = 4;
                    else if (x > data.mid_w - 5 && x < data.mid_h + 5)
                        priorityy = 4;
                    else if (x > data.mid_w - 9 && x < data.mid_h + 9)
                        priorityy = 3;
                    else 
                        priorityy = 4;
    
                    Algo[x,y].calculate_priority = Algo[x,y].solve_map * (priorityx * priorityy);
                        //Need to add value to the Squere
                    Algo algo = new Algo
                    {
                        totalTileOut = value,
                        turn_to_maze = Algo[x,y].turn_to_maze,
                        solve_map = Algo[x,y].solve_map,
                        calculate_priority = Algo[x,y].calculate_priority,
                        scrapAmount = map[x,y].scrapAmount,
                        owner = map[x,y].owner,
                        units = map[x,y].units,
                        recycler = map[x,y].recycler,
                        canBuild = map[x,y].canBuild,
                        canSpawn = map[x,y].canSpawn,
                        inRangeOfRecycler = map[x,y].inRangeOfRecycler,
                        x = x,
                        y = y
                    };
                    // Create a new Robot object with the values for this robot
                    Squeres.Add(algo);
                    if (map[x,y].units > 0)
                    {
                    int cnt = 0;
                    while (cnt < map[x,y].units)
                    {
                        Robot robot = new Robot
                        {
                        x = x,
                        y = y,
                        // ...
                        };
                            // Check if the tile has units and is owned by the player
                            if (map[x,y].units > 0 && map[x,y].owner == 1)
                            {
                            // Add the Robot object to the list of player's robots
                            playerRobots.Add(robot);
                            }

                    // Check if the tile has units and is owned by the enemy
                            if (map[x,y].units > 0 && map[x,y].owner == 0)
                            {
                    // Add the Robot object to the list of enemy's robots
                            enemyRobots.Add(robot);
                            }
                            cnt++;
                        }
                    }
                    x++;
                }
                y++;
            }
        int count = 0;
        Console.Error.WriteLine(turn_counter);
        if  (width * height < 100 && turn_counter < 3)
        {

        }
        else if (width * height < 200 && turn_counter == 1)
        {
            int tempOut = 0;
            int best = 0;
            int cnt = 0;
            foreach (Algo prioTiles in Squeres)
            {
                if (prioTiles.canBuild == 1)
                {
                    if (prioTiles.scrapAmount > tempOut && prioTiles.x > 1 && prioTiles.y > 1 && prioTiles.x < width - 1 && prioTiles.y < height - 1)
                    {
                        tempOut = prioTiles.scrapAmount;
                        best = cnt;
                    }
                }
                cnt++;
            }
            Algo winner = Squeres[best];
            Console.Write("BUILD {0} {1};",winner.x, winner.y);
        }
        else if (width * height > 180 && (turn_counter == 0 || turn_counter == 2))
        {
            int tempOut = 0;
            int best = 0;
            int cnt = 0;
            foreach (Algo prioTiles in Squeres)
            {
                if (prioTiles.canBuild == 1)
                {
                    if (prioTiles.scrapAmount > tempOut && prioTiles.x > 1 && prioTiles.y > 1 && prioTiles.x < width - 1 && prioTiles.y < height - 1)
                    {
                        tempOut = prioTiles.scrapAmount;
                        best = cnt;
                    }
                }
                cnt++;
            }
            Algo winner = Squeres[best];
            Console.Write("BUILD {0} {1};",winner.x, winner.y);
        }

        foreach (Algo prioTiles in Squeres)
        {
            if (prioTiles.x - 1 > 0 && prioTiles.x + 1 < width && prioTiles.y - 1 > 0 && prioTiles.y + 1 < height)
            {
                if(prioTiles.canBuild == 1)
                {
                    if(map[prioTiles.x + 1, prioTiles.y].units > 0 && map[prioTiles.x + 1, prioTiles.y].owner == 0)
                    {
                        Console.Write("BUILD {0} {1};",prioTiles.x, prioTiles.y);
                    }
                    else if (map[prioTiles.x - 1, prioTiles.y].units > 0 && map[prioTiles.x - 1, prioTiles.y].owner == 0)
                    {
                        Console.Write("BUILD {0} {1};",prioTiles.x, prioTiles.y);
                    }
 //                   else if (map[prioTiles.x, prioTiles.y + 1].units > 0 && map[prioTiles.x, prioTiles.y + 1].owner == 0)
                    // {
                    //     Console.Write("BUILD {0} {1};",prioTiles.x, prioTiles.y);
                    // }
                    // else if (map[prioTiles.x, prioTiles.y - 1].units > 0 && map[prioTiles.x, prioTiles.y - 1].owner == 0)
                    // {
                    //     Console.Write("BUILD {0} {1};",prioTiles.x, prioTiles.y);
                    // }
                }
            }
        }

        if (turn_counter > 50)
            check_squeres = 3;

        while (myMatter >= 10)
        {
            int tempPrio = 0;
            int best = 0;
            int cnt = 0;
            foreach (Algo prioTiles in Squeres)
            {
                if (prioTiles.canSpawn == 1)
                {
                    if (prioTiles.calculate_priority > tempPrio)
                    {
                        if (map[prioTiles.x, prioTiles.y].units < 3)
                        {
                            tempPrio = prioTiles.calculate_priority;
                            best = cnt;
                        }
                    }
                }
                cnt++;
            }
            Algo winner = Squeres[best];
            winner.calculate_priority = winner.calculate_priority - 10;
            Console.Write("SPAWN {0} {1} {2};", 1, winner.x, winner.y);
            myMatter = myMatter - 10;
        }

        foreach (Robot playerBot in playerRobots)
        {
            int x_1 = 0;
            int x_2 = 0;
            int y_1 = 0;
            int y_2 = 0;
            y = 0;
            while(y < height)
            {
                x = 0;
                while (x < width)
                {
                    if(Math.Abs(playerBot.x - x) + Math.Abs(playerBot.y - y) <= check_squeres)
                    {
                       if(map[x,y].units - 1 <= map[playerBot.x,playerBot.y].units)
                       {
                        if (Algo[x,y].calculate_priority > Algo[x_1,y_1].calculate_priority)
                        {
                            x_2 = x_1;
                            y_2 = y_1;
                            x_1 = x;
                            y_1 = y;
                        }
                       }
                    }
                    x++;
                }
                y++;
            }
            if(count == playerRobots.Count - 1)
            {
//                Console.Error.WriteLine(Algo[x_1,y_1].calculate_priority);
                Console.WriteLine("MOVE {0} {1} {2} {3} {4}", 1, playerBot.x, playerBot.y, x_1, y_1);
            }
            else if (count % 2 == 0)
            {
                Console.Write("MOVE {0} {1} {2} {3} {4};", 1, playerBot.x, playerBot.y, x_1, y_1);
                Algo[x_1,y_1].calculate_priority = Algo[x_1,y_1].calculate_priority - 30;
            }
            else
            {
                Console.Write("MOVE {0} {1} {2} {3} {4};", 1, playerBot.x, playerBot.y, x_2, y_2);
                Algo[x_1,y_1].calculate_priority = Algo[x_1,y_1].calculate_priority - 30;
            }

        //}
        count++;
        }
        Squeres.Clear();
        playerRobots.Clear();
        enemyRobots.Clear();
        turn_counter++;
        }
        //
    }
}
// Write an action using Console.WriteLine()
// To debug: Console.Error.WriteLine("Debug messages...");
