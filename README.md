# City-Pathfinding
Fast 2D pathfinding algorithm based on Points and Neighbours.

# Compiling
Example program require MonoGame, but the algorithm(in folder CPF) is writting in pure C#. This scripts can work on others game engine, such as Unity.

# Using
1. Add .cs files from **CPF** folder to your project.
2. Instantiate ***Map***
```
Map map = new Map();
```
3. Add points and neighbour relations
```
map.AddPoint(x,y); //Create Point called "P_x_y"

map.AddNeighbour(name1, name2); //Create neighbour relation between name1 and name2 point
//Find script can only move from point to his neighbour
```
4. Find path
```
map.Find(startPoint, endPoint); //Return List<string> contains list of path points
```

# Error
functions throw an exception when point hasn't joined to rest of the map
